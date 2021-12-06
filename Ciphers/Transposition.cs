using System;
using System.Text;
using System.Threading.Tasks;
using Ciphers;

public class Transposition
{
	public static int[] Dimensions(int key, string message)
	{
		int h = message.Length / key + 1;
		int w = key;
		int[] res = { w, h };
		return res;
	}
	public static string Encrypt(int key, string message)
	{
		int[] dim = Dimensions(key, message);
		int width = dim[0], height = dim[1];

		StringBuilder sb = new StringBuilder("");
		for (int w = 0; w < width; ++w)
		{
			for (int h = 0; h < height; ++h)
			{
				if (h * width + w < message.Length)
					sb.Append(message[h * width + w]);
			}
		}
		return sb.ToString();
	}
	public static string Decrypt(int key, string message)
	{
		int[] dim = Dimensions(key, message);
		int width = dim[0], height = dim[1];

		int rem = message.Length % key;
		for (int i = rem; i < width - 1; ++i)
		{
			message = message.Insert((i + 1) * height - 1, "_");
		}

		StringBuilder sb = new StringBuilder("");
		for (int w = 0; w < height; ++w)
		{
			for (int h = 0; h < width; ++h)
			{
				if (h * height + w < message.Length)
				{
					if (message[h * height + w] == '_')
						continue;
					else sb.Append(message[h * height + w]);
				}

			}
		}
		return sb.ToString();
	}
	public static async Task Test(string message, int key)
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
			key = Utilities.RandomNumber(2, message.Length > 10 ? 10 : message.Length);
		}

		Console.WriteLine($"Encrypting {(random_m ? "RANDOM " : "")}message with {(random_k ? "RANDOM " : "")}key {key}.");

		Console.WriteLine($"Original message: {message}");

		string enc = Encrypt(key, message);
		Console.WriteLine($"Encrypted message: {enc}");

		string dec = Decrypt(key, enc);
		Console.WriteLine($"Decrypted message: {dec}");

		Console.WriteLine($"The encrypted and decrypted messages do {(message == dec ? "" : "NOT ")}match");
		return;
	}

	public static void Bruteforce(string message)
    {
		for (int i = 2; i < message.Length; i++)
		{
			var result = Decrypt(i, message);
			Console.WriteLine($"Trying key [{i}]: Result: {result}");
		}
		return;
	}
}