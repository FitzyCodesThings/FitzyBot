using System;
using System.Threading.Tasks;

namespace FitzyBot.SmartContract
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string buffer = Console.In.ReadToEnd();
            
                CommandHandler f = new CommandHandler();

                string responseValue = await f.Handle(buffer);

                if (responseValue != null)
                {
                    Console.Write(responseValue);
                }
            } catch (Exception ex)
            {
                // Write log information to stderr
                Console.Error.Write(ex.Message);
            }
        }
    }
}
