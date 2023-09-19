using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Account : IComparable<Account>
{
    public int AccountID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Balance { get; set; }

    public Account(int accountID, string firstName, string lastName, double balance)
    {
        AccountID = accountID;
        FirstName = firstName;
        LastName = lastName;
        Balance = balance;
    }

    public void DisplayAccountInfo()
    {
        Console.WriteLine($"Account ID: {AccountID}");
        Console.WriteLine($"First Name: {FirstName}");
        Console.WriteLine($"Last Name: {LastName}");
        Console.WriteLine($"Balance: {Balance:C}");
    }

    public static Account CreateAccountFromConsole()
    {
        Console.Write("Enter Account ID: ");
        int accountID = int.Parse(Console.ReadLine());

        Console.Write("Enter First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Enter Balance: ");
        double balance = double.Parse(Console.ReadLine());

        return new Account(accountID, firstName, lastName, balance);
    }

    public int CompareTo(Account other)
    {
        // Sắp xếp theo Account ID, First Name, Balance tăng dần
        int result = AccountID.CompareTo(other.AccountID);
        if (result == 0)
        {
            result = string.Compare(FirstName, other.FirstName, StringComparison.Ordinal);
            if (result == 0)
            {
                result = Balance.CompareTo(other.Balance);
            }
        }
        return result;
    }
}

public class AccountList
{
    private List<Account> accounts = new List<Account>();

    public void NewAccount(Account account)
    {
        accounts.Add(account);
    }

    public void RemoveAccount(int accountID)
    {
        int index = accounts.BinarySearch(new Account(accountID, "", "", 0), new AccountIDComparer());
        if (index >= 0)
        {
            accounts.RemoveAt(index);
            Console.WriteLine($"Account with Account ID {accountID} removed successfully.");
        }
        else
        {
            Console.WriteLine($"Account with Account ID {accountID} not found.");
        }
    }

    public void SaveFile(string fileName)
    {
        try
        {
            string json = JsonConvert.SerializeObject(accounts);
            File.WriteAllText(fileName, json);
            Console.WriteLine("Accounts saved to file successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error saving accounts to file: " + e.Message);
        }
    }

    public void LoadFile(string fileName)
    {
        try
        {
            string json = File.ReadAllText(fileName);
            accounts = JsonConvert.DeserializeObject<List<Account>>(json);
            Console.WriteLine("Accounts loaded from file successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error loading accounts from file: " + e.Message);
        }
    }

    public void Report()
    {
        Console.WriteLine("List of Accounts:");
        foreach (Account account in accounts)
        {
            account.DisplayAccountInfo();
            Console.WriteLine();
        }
    }

    public void SortAccounts(IComparer<Account> comparer)
    {
        accounts.Sort(comparer);
    }
}

public class AccountIDComparer : IComparer<Account>
{
    public int Compare(Account x, Account y)
    {
        return x.AccountID.CompareTo(y.AccountID);
    }
}

public class FirstNameComparer : IComparer<Account>
{
    public int Compare(Account x, Account y)
    {
        return string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal);
    }
}

public class BalanceComparer : IComparer<Account>
{
    public int Compare(Account x, Account y)
    {
        return x.Balance.CompareTo(y.Balance);
    }
}

class Program
{
    static void Main(string[] args)
    {
        AccountList accountList = new AccountList();

        while (true)
        {
            Console.WriteLine("1. Create New Account");
            Console.WriteLine("2. Remove Account");
            Console.WriteLine("3. Save Accounts to File");
            Console.WriteLine("4. Load Accounts from File");
            Console.WriteLine("5. Display Account Report");
            Console.WriteLine("6. Sort Accounts by Account ID");
            Console.WriteLine("7. Sort Accounts by First Name");
            Console.WriteLine("8. Sort Accounts by Balance");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Account newAccount = Account.CreateAccountFromConsole();
                    accountList.NewAccount(newAccount);
                    break;
                case 2:
                    Console.Write("Enter Account ID to remove: ");
                    int accountIDToRemove = int.Parse(Console.ReadLine());
                    accountList.RemoveAccount(accountIDToRemove);
                    break;
                case 3:
                    Console.Write("Enter file name to save accounts: ");
                    string saveFileName = Console.ReadLine();
                    accountList.SaveFile(saveFileName);
                    break;
                case 4:
                    Console.Write("Enter file name to load accounts: ");
                    string loadFileName = Console.ReadLine();
                    accountList.LoadFile(loadFileName);
                    break;
                case 5:
                    accountList.Report();
                    break;
                case 6:
                    accountList.SortAccounts(new AccountIDComparer());
                    break;
                case 7:
                    accountList.SortAccounts(new FirstNameComparer());
                    break;
                case 8:
                    accountList.SortAccounts(new BalanceComparer());
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
