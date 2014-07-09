using System;
using Torch.Core.Cli;

namespace Torch.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var response = new CommandLineApplication(new CommandLineArguments(args), new ApplicationFactory()).Run();

            Console.ForegroundColor = response.ConsoleColor;

            if (!string.IsNullOrWhiteSpace(response.Message))
            {
                Console.WriteLine(response.Message);
            }

            if (response.Results != null && response.Results.Length > 0)
            {
                foreach (var result in response.Results)
                {
                    Console.WriteLine(result);
                }    
            }

            Console.ResetColor(); 
        }
    }
}
