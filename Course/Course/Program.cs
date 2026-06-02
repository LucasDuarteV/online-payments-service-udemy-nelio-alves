using System;
using System.Globalization;
using Course.Entities;
using Course.Entities.Service;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter contract data");

            Console.Write("Number: ");
            int number = int.Parse(Console.ReadLine()!);

            Console.Write("Date (dd/MM/yyyy): ");
            DateTime date = DateTime.ParseExact(
                Console.ReadLine()!,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture
            );

            Console.Write("Contract value: ");
            double totalValue = double.Parse(
                Console.ReadLine()!,
                CultureInfo.InvariantCulture
            );

            Contract contract = new Contract(
                number,
                date,
                totalValue
            );

            Console.Write("Enter number of installments: ");
            int months = int.Parse(Console.ReadLine()!);

            ContractService contractService =
                new ContractService(new PaypalService());

            contractService.ProcessContract(contract, months);

            Console.WriteLine("Installments:");

            foreach (Installement installment in contract.Installement)
            {
                Console.WriteLine(installment);
            }
        }
    }
}