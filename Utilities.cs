using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ciphers
{
    class Utilities
    {
		public Dictionary<string, bool> wordBank = new Dictionary<string, bool>();
		public Utilities()
		{
			foreach (var word in File.ReadLines(@"[REDACTED]"))
			{
				wordBank.Add(word, true);
			}
		}
		public bool IsEnglishWord(string word)
        {
			bool found = false;
			return wordBank.TryGetValue(word, out found);
        }
		public static string GetAlphabet()
        {
			return "abcdefghijklmnopqrstuvwxyz,.!? :;'\"`()[]{}-";
		}
		public static string GenerateDictionaryKey()
        {
			var rand = new Random();
			string pool = GetAlphabet();
			StringBuilder sb = new StringBuilder("");
			
			while (pool.Length > 0)
            {
				int ind = rand.Next(pool.Length);
				sb.Append(pool[ind]);
				pool = pool.Replace($"{pool[ind]}", string.Empty);
            }

			return sb.ToString();
        }
		public static async Task<string> GenerateSentencesAsync(int sentences)
		{
			HttpClient cl = new HttpClient();
			HttpResponseMessage response = await cl.GetAsync("http://metaphorpsum.com/paragraphs/1/1");
			string res = await response.Content.ReadAsStringAsync();
			return res.Split(".")[0];
		}
		public static int RandomNumber(int from, int to)
        {
			Random generator = new Random();
			return generator.Next(from, to);
        }
		public static void Log(string message)
        {
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.White;
        }
		public static async Task NextTest()
		{
			Log("Testing concluded. Starting new test in 5 seconds.");
			await Task.Delay(5000);
			return;
        }
		public static async Task Conclude()
        {
			Log("Testing concluded. There are no more ciphers. Starting from beginning in 10 seconds.");
			await Task.Delay(10000);
			return;
		}
		public static int GCD(int a, int b)
        {
			while (b != 0)
            {
				var c = b;
				b = a % b;
				a = c;
            }
			return a;
        }
	}
}
