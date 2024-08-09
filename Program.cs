namespace Bank_ATM
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    class Program
    {
        static List<AccountHolder> accountHolders = new List<AccountHolder>();

        static void input()
        {
            Console.WriteLine("Welcome to the Mathebula ATM!");
            Console.Write("Enter your card/account number: ");
            string accNumber = Console.ReadLine();
            Console.Write("Enter your PIN: ");
            string pin = Console.ReadLine();
            AccountHolder user = accountHolders.Find(a => a.AccNumber == accNumber && a.PIN == pin);

            if (user != null)
            {
                Console.WriteLine("Welcome, {0}!", user.Name);
                ShowMenu(user);
            }
            else
            {
                Console.WriteLine("Invalid credentials. Exiting...");
                input();
            }
        }

        static void Account()
        {
            // Initialize some sample account holders
            accountHolders.Add(new AccountHolder("Ntsako M", "123456", "1234", 1000, 0, 370));
            accountHolders.Add(new AccountHolder("Emah M", "987654", "4321", 2000, 0, 70));
        }
        static void getLoan(AccountHolder user)
        {

            Console.Write("Enter loan amount: R");
            double loanAmount = double.Parse(Console.ReadLine());
            Console.Write("Enter annual interest rate (%): ");
            double interestRate = double.Parse(Console.ReadLine()) / 100;
            Console.Write("Enter loan term (months): ");
            int loanTermMonths = int.Parse(Console.ReadLine());

            double monthlyInstallment = loanAmount * (interestRate / 12) / (1 - Math.Pow(1 + interestRate / 12, -loanTermMonths));
            user.Balance += loanAmount;
            Console.WriteLine("Loan approved! Your monthly installment: R{0}", monthlyInstallment);
            ShowMenu(user);
        }
        static void Main()
        {
            Account();
            input();
        }

        static void ShowMenu(AccountHolder user)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. Deposit Money");
            Console.WriteLine("4. Pay Someone");
            Console.WriteLine("5. Apply for a Loan");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Your balance: R{0}", user.Balance);
                    ShowMenu(user);
                    break;
                case 2:
                    Console.Write("Enter withdrawal amount: R");
                    double withdrawalAmount = double.Parse(Console.ReadLine());
                    user.Balance -= withdrawalAmount;
                    Console.WriteLine("Withdrawn R{0}. New balance: R{1}", withdrawalAmount, user.Balance);
                    ShowMenu(user);
                    break;
                case 3:
                    Console.Write("Enter deposit amount: R");
                    double depositAmount = double.Parse(Console.ReadLine());
                    user.Balance += depositAmount;
                    Console.WriteLine("Deposited R{0}. New balance: R{1}", depositAmount, user.Balance);
                    ShowMenu(user);
                    break;


                case 4:
                    Console.WriteLine("Here are available Beneficiaries : ");
                    foreach (AccountHolder holder in accountHolders) { Console.WriteLine("\t \t {0} {1} ", holder.AccNumber, holder.Name); }

                    Console.Write("Enter recipient's account number: ");
                    string recipientAccountNumber = Console.ReadLine();
                    Console.Write("Enter transfer amount: R");
                    double transferAmount = double.Parse(Console.ReadLine());

                    AccountHolder recipient = accountHolders.Find(a => a.AccNumber == recipientAccountNumber);
                    if (recipient != null && user.Balance >= transferAmount)
                    {
                        user.Balance -= transferAmount;
                        recipient.Balance += transferAmount;
                        Console.WriteLine("Transferred R{0} to {1}. New balance: R{2}", transferAmount, recipient.AccNumber, user.Balance);
                    }
                    else if (recipient == null)
                    {
                        Console.WriteLine("Account does not exist! Would you like to add it");

                    }
                    else
                    {
                        Console.WriteLine("Invalid recipient or insufficient balance.");
                    }
                    ShowMenu(user);
                    break;

                case 5:
                    if (user.Score < 100)
                    {
                        Console.WriteLine("Your Credit score is: {0}. Sorry you don't qualify for a loan", user.Score);
                        Console.WriteLine("\t You need credit score of above 100 to qualify");
                        ShowMenu(user);
                    }
                    else
                    {
                        Console.WriteLine("Your Credit score is: {0}. You qualify for a loan", user.Score);
                        Console.WriteLine();
                        getLoan(user);

                    }
                    break;

                case 6:
                    Console.WriteLine("Thank you for using the ATM. Have a great day!");
                    Console.WriteLine();
                    Main();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    ShowMenu(user);
                    break;
            }
        }
    }

    class AccountHolder
    {
        public string AccNumber { get; }
        public string PIN { get; }
        public double Balance { get; set; }
        public double Loan { get; set; }
        public double Score { get; }
        public string Name { get; }



        public AccountHolder(string name, string accNumber, string pin, double balance, double loan, double score)
        {
            AccNumber = accNumber;
            PIN = pin;
            Balance = balance;
            Loan = loan;
            Score = score;
            Name = name;
        }
    }
}