using System;
using Repository;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TEST();
        }

        private static async void TEST()
        {
            IRepository rep = new MSSQL();

            var res =  (rep.BlockUser("199803148536", "FGD")).Result;

            Console.WriteLine(res);
        }
    }
}
