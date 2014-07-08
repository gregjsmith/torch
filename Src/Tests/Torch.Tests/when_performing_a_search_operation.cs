using Moq;
using Torch.Cli;
using Torch.Core;
using Xunit;

namespace Torch.Tests
{
    public class when_performing_a_search_operation
    {
        private readonly Mock<IApplicationFactory> _factory;
        public when_performing_a_search_operation()
        {
            _factory = new Mock<IApplicationFactory>();
        }

        [Fact]
        public void it_will_invoke_a_matching_request_if_a_seach_is_directed_and_a_start_and_end_word_are_provided()
        {
            var dictionary = new Mock<IWordDictionary>();

            var search = new SearchCommand(_factory.Object, new WordDictionaryIO());

            _factory.Setup(m => m.InitialiseWordDictionaryFor(It.IsAny<string>())).Returns(dictionary.Object);

            search.Context = new CommandLineArguments("search this that".Split(' '));

            search.Execute();

            dictionary.Verify(m => m.GetMatches(It.IsAny<IWordMatchingStrategy>(), "this", "that"), Times.Once);
        }

        [Fact]
        public void it_will_store_the_results_in_a_results_file_when_an_output_switch_is_specified()
        {
            var dictionary = new Mock<IWordDictionary>();
            var io = new Mock<IDictionaryIO>();

            var search = new SearchCommand(_factory.Object, io.Object);

            _factory.Setup(m => m.InitialiseWordDictionaryFor(It.IsAny<string>())).Returns(dictionary.Object);

            search.Context = new CommandLineArguments("search this that -o c:\file.txt".Split(' '));

            var results = search.Execute();

            io.Verify(m => m.Save(It.IsAny<string[]>(), "c:\file.txt"), Times.Once);


        }
    }
}