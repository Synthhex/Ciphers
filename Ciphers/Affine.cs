using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers.Ciphers
{
    class Affine
    {
        public static string Encrypt(string message, int akey, int bkey)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < message.Length; ++i)
            {
                if (!Char.IsLetter(message[i])) {
                    sb.Append(message[i]);
                    continue;
                }
                char ch = Char.IsLower(message[i]) ? 'a' : 'A';
                sb.Append(Convert.ToChar(ch + ((message[i] - ch) * akey + bkey) % 26));
            }

            return sb.ToString();
        }
        public static string Decrypt(string message, int akey, int bkey)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < message.Length; ++i)
            {
                if (!Char.IsLetter(message[i]))
                {
                    sb.Append(message[i]);
                    continue;
                }
                for (int j = 0; j < akey; ++j)
                {
                    char ch = Char.IsLower(message[i]) ? 'a' : 'A';
                    int origin = 26 * j + (message[i] - ch) - bkey;
                    if (origin % akey == 0)
                    {
                        int res = origin / akey;
                        if (res < 0)
                            res = 26 + res;
                        sb.Append(Convert.ToChar(ch + res));
                        break;
                    }
                } 
            }
            return sb.ToString();
        }
        public static async Task Test(string message, int akey = 0, int bkey = 0)
        {
            int[] valid = { 3, 5, 7, 9, 11, 13, 15, 17, 19, 21, 23, 25};
            bool random_m = false;
            bool random_ak = false;
            bool random_bk = false;
            if (Utilities.GCD(akey, 26) != 1)
            {
                Console.WriteLine($"The multiplicative key {akey} is not coprime with 26. You may lose information.");
            }
            if (message == null)
            {
                random_m = true;
                message = await Utilities.GenerateSentencesAsync(1);
            }
            if (akey == 0)
            {
                random_ak = true;
                akey = valid[Utilities.RandomNumber(0, 12)];
            }
            if (bkey == 0)
            {
                random_bk = true;
                bkey = Utilities.RandomNumber(1, 26);
            }
            Console.WriteLine($"Encrypting {(random_m ? "RANDOM " : "")}message with {(random_ak ? "RANDOM " : "")}multiplicative key {akey} and {(random_bk ? "RANDOM " : "")}linear key {bkey}.");

            Console.WriteLine($"Original message: {message}");

            string enc = Encrypt(message, akey, bkey);
            Console.WriteLine($"Encrypted message: {enc}");

            string dec = Decrypt(enc, akey, bkey);
            Console.WriteLine($"Decrypted message: {dec}");

            Console.WriteLine($"The encrypted and decrypted messages do {(message == dec ? "" : "NOT ")}match");
            return;
        }
    }
}
