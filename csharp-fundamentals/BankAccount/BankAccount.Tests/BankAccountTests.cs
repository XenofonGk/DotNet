using Xunit;

// Tests for the BankAccount exercise. The interesting cases are the refusals:
// a deposit or withdrawal that should not happen must leave the balance and the
// history untouched, not merely return false.
public class BankAccountTests
{
    [Fact]
    public void OpeningAnAccountRecordsTheInitialBalance()
    {
        var account = new BankAccount("Xenofon", 100m);

        Assert.Equal("Xenofon", account.OwnerName);
        Assert.Equal(100m, account.Balance);
        Assert.Single(account.TransactionHistory);
    }

    [Fact]
    public void DepositIncreasesTheBalance()
    {
        var account = new BankAccount("Xenofon", 100m);

        account.Deposit(50m);

        Assert.Equal(150m, account.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-25)]
    public void DepositOfZeroOrLessIsIgnored(decimal amount)
    {
        var account = new BankAccount("Xenofon", 100m);
        var historyBefore = account.TransactionHistory.Count;

        account.Deposit(amount);

        Assert.Equal(100m, account.Balance);
        Assert.Equal(historyBefore, account.TransactionHistory.Count);
    }

    [Fact]
    public void WithdrawTakesTheMoneyAndReportsSuccess()
    {
        var account = new BankAccount("Xenofon", 100m);

        var ok = account.Withdraw(40m);

        Assert.True(ok);
        Assert.Equal(60m, account.Balance);
    }

    [Fact]
    public void WithdrawingTheWholeBalanceIsAllowed()
    {
        var account = new BankAccount("Xenofon", 100m);

        Assert.True(account.Withdraw(100m));
        Assert.Equal(0m, account.Balance);
    }

    [Fact]
    public void OverdraftIsRefusedAndChangesNothing()
    {
        var account = new BankAccount("Xenofon", 100m);
        var historyBefore = account.TransactionHistory.Count;

        var ok = account.Withdraw(150m);

        Assert.False(ok);
        // The balance must be untouched — a refused withdrawal that still
        // moved money would be the worst kind of bug here.
        Assert.Equal(100m, account.Balance);
        Assert.Equal(historyBefore, account.TransactionHistory.Count);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void WithdrawOfZeroOrLessIsRefused(decimal amount)
    {
        var account = new BankAccount("Xenofon", 100m);

        Assert.False(account.Withdraw(amount));
        Assert.Equal(100m, account.Balance);
    }

    [Fact]
    public void EverySuccessfulMovementIsRecorded()
    {
        var account = new BankAccount("Xenofon", 100m);

        account.Deposit(50m);      // recorded
        account.Withdraw(30m);     // recorded
        account.Withdraw(9999m);   // refused, not recorded
        account.Deposit(-5m);      // ignored, not recorded

        // One line from opening, plus the two movements that actually happened.
        Assert.Equal(3, account.TransactionHistory.Count);
        Assert.Equal(120m, account.Balance);
    }
}
