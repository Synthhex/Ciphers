using Ciphers.Ciphers;
using System;
using System.Threading.Tasks;

namespace Ciphers
{
    class Program
    {
        public static async Task TestAll()
        {
            Console.Clear();

            Utilities.Log("Testing Caesar Cipher in 2 seconds...");
            await Caesar.Test(null, 0);
            Utilities.Log("Testing concluded. Starting new test in 5 seconds.");
            await Task.Delay(5000);

            Console.Clear();

            Utilities.Log("Testing Transposition Cipher in 2 seconds...");
            await Transposition.Test(null, 0);
            Utilities.Log("Testing concluded. Starting new test in 5 seconds.");
            await Task.Delay(5000);

            Console.Clear();
        
            Utilities.Log("Testing Affine Cipher in 2 seconds...");
            await Affine.Test(null, 5, 0);
            Utilities.Log("Testing concluded. There are no more ciphers. Starting from beginning in 10 seconds.");
            await Task.Delay(10000);
            
            TestAll().GetAwaiter().GetResult();
        }
        static void Main(string[] args)
        {
            TestAll().GetAwaiter().GetResult();
        }
    }
}
