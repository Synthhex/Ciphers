using System;
using System.Text;		

public class Program
{
	public static void Main()
	{
		// string message = "It is also important that the work goes beyond.";
		string message = Console.ReadLine();
		int key = 8;
		
		if (message.Length < key) {
			Console.WriteLine("Your message is shorter than your key.");
			return;
		}
		
		StringBuilder sb = new StringBuilder("");
		
		for (int c = 0; c < key; ++c) {
			int bound = message.Length / key + 1;
			for (int x = 0; x < bound; ++x) {
				if (x * key + c >= message.Length)
					continue;
				else
					sb.Append(message[x * key + c]);
			}	
		}
		
		Console.WriteLine(sb.ToString());
	}
}
