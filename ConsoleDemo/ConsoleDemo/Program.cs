using ConsoleDemo.Classes;
using ConsoleDemo.Interfaces;
using System;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount(1000);
            bankAccount.addTooBalance(100);
            Console.WriteLine(bankAccount.Balance);

            ChildBankAccount childBankAccount = new ChildBankAccount();
            childBankAccount.addTooBalance(10);

            Information(bankAccount);
            Console.ReadLine();
        }

        private static void Information(IInformation information)
        {
            Console.WriteLine(information.GetInformation());
        }
    }
}
