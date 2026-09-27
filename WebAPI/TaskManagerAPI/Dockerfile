# Official .NET 10 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy everything and restore/publish
COPY . .
RUN dotnet publish -c Release -o out

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Run as the unprivileged user the base image provides, not root.
USER $APP_UID

# The runtime image has no curl or wget, so probe /health with bash's /dev/tcp.
HEALTHCHECK --interval=30s --timeout=5s --start-period=90s --retries=3 \
  CMD ["bash", "-c", "exec 3<>/dev/tcp/127.0.0.1/8080 && printf 'GET /health HTTP/1.0\\r\\nHost: localhost\\r\\n\\r\\n' >&3 && grep -q healthy <&3"]

ENTRYPOINT ["dotnet", "TaskManagerAPI.dll"]
