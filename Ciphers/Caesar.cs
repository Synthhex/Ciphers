using System;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Ciphers
{
    class Caesar
    {
        public static string Encrypt(string message, int key)
        {
            key %= 26;
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < message.Length; ++i)
            {
                if (!Char.IsLetter(message[i]))
                    sb.Append(message[i]);
                else
                {
                    char ch = Char.IsLower(message[i]) ? 'a' : 'A';
                    sb.Append(Convert.ToChar(ch + ((message[i] + key - ch) % 26)));
                }
            }
            return sb.ToString();
        }
        public static string Decrypt(string message, int key)
        {
            key %= 26;
            key = 26 - key;
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < message.Length; ++i)
            {
                if (!Char.IsLetter(message[i]))
                    sb.Append(message[i]);
                else
                {
                    char ch = Char.IsLower(message[i]) ? 'a' : 'A';
                    sb.Append(Convert.ToChar(ch + ((message[i] + key - ch) % 26)));
                }
            }
            return sb.ToString();
        }
        public static async Task Test(string message, int key = 0)
        {
            bool random_m = false;
            bool random_k = false;
            if (message == null)
            {
                random_m = true;
                message = await Utilities.GenerateSentencesAsync(1);
            }
            if (key == 0)
            {
                random_k = true;
                key = Utilities.RandomNumber(1, 26);
            }
            Console.WriteLine($"Encrypting {(random_m ? "RANDOM " : "")}message with {(random_k ? "RANDOM " : "")}key {key}.");

            Console.WriteLine($"Original message: {message}");

            string enc = Encrypt(message, key);
            Console.WriteLine($"Encrypted message: {enc}");

            string dec = Decrypt(enc, key);
            Console.WriteLine($"Decrypted message: {dec}");

            Console.WriteLine($"The encrypted and decrypted messages do {(message == dec ? "" : "NOT ")}match");
            return;
        }

        public static void Bruteforce(string message)
        {
            Console.WriteLine("Printing possible combinations:");
            for (int i = 1; i <= 25; ++i)
            {
                Console.WriteLine($"Decryption with key {i} yielded: {Decrypt(message, i)}");
            }
        }
    }
}
