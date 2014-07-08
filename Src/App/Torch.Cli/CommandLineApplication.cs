using System;

namespace Torch.Cli
{
    public class CommandLineApplication   
    {
        private readonly CommandLineArguments _args;
        private readonly IApplicationFactory _applicationFactory;

        public CommandLineApplication(CommandLineArguments args, IApplicationFactory applicationFactory)
        {
            _args = args;
            _applicationFactory = applicationFactory;
        }

        public CommandLineOutput Run()  
        {
            try
            {
                var argumentHandler = _applicationFactory.InitialiseArgumentHandlers();

                foreach (var handler in argumentHandler.Handlers)
                {
                    handler.Invoke(_args);

                    if (argumentHandler.Complete)
                    {
                        return argumentHandler.Output;
                    }
                }

                return new InvalidArgumentsOutput();
            }
            catch (ArgumentException ex)
            {
                return new InvalidArgumentsOutput(ex.Message);
            }
            catch (Exception ex)
            {
                return new CommandLineOutput
                {
                    Message = ex.Message,
                    ConsoleColor = ConsoleColor.Red
                };
            }
            
        }
    }
}