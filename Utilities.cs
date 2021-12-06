using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ciphers
{
    class Utilities
    {
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
