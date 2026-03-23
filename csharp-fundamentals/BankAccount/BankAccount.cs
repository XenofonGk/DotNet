
   public class BankAccount
{
   public string OwnerName { get; private set; }
   public decimal Balance { get; private set; }
   public List<string> TransactionHistory { get; private set; }

        // Constructor
   public BankAccount(string ownerName, decimal initialDeposit)
        {
    OwnerName = ownerName;
    Balance = initialDeposit;
    TransactionHistory = new List<string>();
    TransactionHistory.Add($"Hello" + ownerName + ", Your balance is: " + Balance);
        }

        // Methods
    public void Deposit(decimal amount){
    if (amount > 0){
        Balance += amount;
        TransactionHistory.Add($"Deposit: +{amount} | Balance: {Balance}");
       
        }
    }

    public bool Withdraw(decimal amount)
        {
            if (amount > 0)
            {
                if(amount <= Balance)
                {
                    Balance -= amount;
                    TransactionHistory.Add($"Withdraw: -{amount} | Balance: {Balance}");
                    return true;
                } else
                {
                    Console.WriteLine("Not enough Money to deposit");
                                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    public void PrintHistory()
        {
            for (int i = 0; i < TransactionHistory.Count; i++)
            {
                Console.WriteLine(TransactionHistory[i]);
            }

        }

}