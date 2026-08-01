# Deploying to a free ARM VM

Runs the API, PostgreSQL and a TLS-terminating reverse proxy on one always-free
Oracle Cloud instance. Postgres sits on the same box rather than on a managed
serverless tier, so there is no scale-to-zero and no cold start on the first
request after an idle period.

## 1. Create the instance

Oracle Cloud → Compute → Instances → Create.

- **Shape:** `VM.Standard.A1.Flex` (Ampere ARM) — the always-free allowance is
  4 OCPUs and 24 GB across all A1 instances. 1 OCPU / 6 GB is plenty here and
  leaves room to spare.
- **Image:** Ubuntu 22.04 or newer.
- **SSH key:** upload your public key; there is no password login.

Note the public IP.

> ARM matters: the images used here (`postgres:16-alpine`, `caddy:2-alpine`,
> and the .NET SDK/runtime) all publish `linux/arm64`, so nothing needs
> emulation. A base image without an ARM build would fail to start here.

## 2. Open the ports

Oracle blocks inbound traffic in two independent places, and **both** must be
changed or the site is unreachable with no error to look at:

1. **Security list** — VCN → Subnet → Security List → add ingress rules for
   TCP 80 and 443 from `0.0.0.0/0`.
2. **The instance firewall** — Ubuntu images ship with iptables rules that drop
   everything except SSH:

```bash
sudo iptables -I INPUT 6 -m state --state NEW -p tcp --dport 80 -j ACCEPT
sudo iptables -I INPUT 6 -m state --state NEW -p tcp --dport 443 -j ACCEPT
sudo netfilter-persistent save
```

## 3. Install Docker

```bash
sudo apt update && sudo apt install -y docker.io docker-compose-v2 git
sudo usermod -aG docker $USER && newgrp docker
```

## 4. Deploy

```bash
git clone https://github.com/XenofonGk/DotNet.git
cd DotNet/WebAPI/TaskManagerAPI

cat > .env <<'EOF'
POSTGRES_PASSWORD=<a long random string>
POSTGRES_DB=todo_db
# sslip.io resolves a dashed IP to that IP, so a real certificate can be issued
# without owning a domain. Replace with the VM's public address.
API_DOMAIN=203-0-113-10.sslip.io
PORTFOLIO_ORIGIN=https://xenofongk.github.io
EOF
chmod 600 .env

docker compose --env-file .env -f deploy/docker-compose.prod.yml up -d --build
```

`--env-file` is required rather than optional: Compose resolves a bare `.env`
relative to the compose file's directory, so leaving it out makes every variable
empty and Postgres fails to initialise with an error that does not mention the
cause.

Generate the password with `openssl rand -base64 32`. It exists only in `.env`
on the VM, which is gitignored and never committed.

## 5. Verify

```bash
curl -s https://$API_DOMAIN/health
curl -si -X POST https://$API_DOMAIN/api/todo \
  -H 'Content-Type: application/json' \
  -d '{"title":"first","isCompleted":false}' | head -1
```

Expect `{"status":"healthy"}` and `HTTP/2 201`. Certificate issuance takes a few
seconds on first start; `docker compose --env-file .env -f deploy/docker-compose.prod.yml logs proxy`
shows it.

## Notes

- **The database is not published.** It has no `ports` mapping, so it is
  reachable only from the compose network. Only Caddy is exposed.
- **CORS is restricted** to `PORTFOLIO_ORIGIN` rather than `*`.
- **TLS renews itself.** Caddy handles issuance and renewal, so there is no
  certbot timer to forget.
- **Migrations retry then fail.** If Postgres is not ready the API retries with
  a backoff and then exits rather than starting against a missing schema.
