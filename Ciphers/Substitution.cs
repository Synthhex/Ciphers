using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Ciphers
{
    class Substitution
    {
        public static string Encrypt(string message, string key)
        {
            string alph = Utilities.GetAlphabet();
            message = message.ToLower();
            StringBuilder sb = new StringBuilder("");
            foreach (char ch in message)
            {
                sb.Append(key[alph.IndexOf(ch)]);
            }
            return sb.ToString();
        }
        public static string Decrypt(string message, string key)
        {
            string alph = Utilities.GetAlphabet();
            message = message.ToLower();
            StringBuilder sb = new StringBuilder("");
            foreach (char ch in message)
            {
                sb.Append(alph[key.IndexOf(ch)]);
            }
            return sb.ToString();
        }
        public static async Task Test(string message, string key)
        {
            bool random_m = false;
            bool random_k = false;

            if (message == null)
            {
                random_m = true;
                message = await Utilities.GenerateSentencesAsync(1);
            }

            if (key == null)
            {
                random_k = true;
                key = Utilities.GenerateDictionaryKey();
            }

            if (key.Length != Utilities.GetAlphabet().Length)
            {
                Console.WriteLine($"Testing skipped. Key is of different length than library.");
                return;
            }

            Console.WriteLine($"Encrypting {(random_m ? "RANDOM " : "")}message with {(random_k ? "RANDOM " : "")}key {key}.");

            Console.WriteLine($"Original message: {message}");

            string enc = Encrypt(message, key);
            Console.WriteLine($"Encrypted message: {enc}");

            string dec = Decrypt(enc, key);
            Console.WriteLine($"Decrypted message: {dec}");

            Console.WriteLine($"\nThe encrypted and decrypted messages do {(message.ToLower() == dec ? "" : "NOT ")}match\n(There is probably some lost capitalization.)");
            return;
        }

    }
}
