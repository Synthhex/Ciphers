using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
	public static async Task<string> GetSentence()
	{
		HttpClient cl = new HttpClient();
		HttpResponseMessage response = await cl.GetAsync("http://metaphorpsum.com/paragraphs/1/1");
		string res = await response.Content.ReadAsStringAsync();
		return res.Split(".")[0];
	}
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
	public static async Task StartAsync(Random gen)
	{
		Console.Clear();

		string message = await GetSentence();
		int key = gen.Next(2, message.Length > 10 ? 10 : message.Length);

		string enc = Encrypt(key, message);

		Console.WriteLine($"Original message: \n{message}\n\nEncrypted message: \n{enc}\n");
		Console.WriteLine($"Original key is [{key}]");

		for (int i = 2; i < message.Length; i++)
        	{
			var result = Decrypt(i, enc) == message;
			Console.WriteLine($"Trying key [{i}]: Match: {result}");
			if (result)
            		{
				Console.WriteLine($"Match has been found at key [{i}]... Generating new string in 10 seconds...");
				// Could I have set up another variable to check for a solution?
				// Yes. Yes, I could have. 
				// I just really wanted to use a goto statement.
				goto end;
            		}
		}

		Console.WriteLine("No match has been found... Generating new string in 10 seconds...\n(This is a bug, please report it back to me.)");
		
		end:
		await Task.Delay(10000);
		StartAsync(gen).GetAwaiter().GetResult();
		return;
	}

	public static void Main()
	{
		Random generator = new Random();
		StartAsync(generator).GetAwaiter().GetResult();
	}
}
