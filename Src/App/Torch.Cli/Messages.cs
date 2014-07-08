namespace Torch.Cli
{
    public class Messages
    {
        public static string Help
        {
            get { return @"basic usage:

torch search startWord endWord [options...]

startWord: word representing the start point of the search (must contain exactly 4 letters, and no special characters)
endWord: word representing the end point of the search (must contain exactly 4 letters, and no special characters)
options: 
    -f the fully-qualified path to a custom dictionary file
    -o the fully qualified path to an output text file
    
Examples:

torch search this that 
torch search this that -f c:\temp\somefile.txt
torch search this that -f c:\temp\somefile.txt -o c:\temp\someotherfile.txt    

"; }
        }

        public static string InvalidArguments
        {
            get
            {
                return "The arguments supplied were not valid. Invoke the application with no arguments to get help";
            }
        }
    }
}