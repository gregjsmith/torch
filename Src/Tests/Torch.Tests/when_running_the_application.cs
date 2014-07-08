using FluentAssertions;
using Moq;
using Torch.Cli;
using Torch.Core;
using Xunit;

namespace Torch.Tests
{
    public class when_running_the_application
    {
        private readonly Mock<IApplicationFactory> _factory;
        public when_running_the_application()
        {
            _factory = new Mock<IApplicationFactory>();
        }

        [Fact]
        public void it_will_print_a_help_message_if_no_arguments_are_supplied()
        {
            var app = new CommandLineApplication(new CommandLineArguments(new string[0]), _factory.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(_factory.Object));


            var output = app.Run();

            output.Results.Should().BeNullOrEmpty();
            output.Message.ShouldBeEquivalentTo(Messages.Help);
        }


        [Fact]
        public void it_will_initialise_a_word_dictionary_if_a_seach_is_directed_and_start_and_end_words_are_provided()
        {
            var app = new CommandLineApplication(new CommandLineArguments(new[] {"search", "this", "that"}), _factory.Object);

            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(_factory.Object));
            app.Run();
            
            _factory.Verify(m => m.InitialiseWordDictionaryFor(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void it_will_invoke_a_matching_request_if_a_seach_is_directed_and_a_start_and_end_word_are_provided()
        {
            var dictionary = new Mock<IWordDictionary>();

            _factory.Setup(m => m.InitialiseWordDictionaryFor(It.IsAny<string>())).Returns(dictionary.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(_factory.Object));

            var app = new CommandLineApplication(new CommandLineArguments("search four five".Split(' ')), _factory.Object);

            app.Run();

            dictionary.Verify(m => m.GetMatches(It.IsAny<IWordMatchingStrategy>(), "four", "five"), Times.Once);
        }

        [Fact]
        public void it_will_print_an_invalid_arguments_message_if_a_search_is_directed_but_start_word_isnt_provided()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search".Split(' ')), _factory.Object);

            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(_factory.Object));

            var output = app.Run();

            output.Message.ShouldBeEquivalentTo(Messages.InvalidArguments);

        }

        [Fact]
        public void it_will_print_an_invalid_arguments_message_if_a_search_is_directed_but_end_word_isnt_provided()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search this".Split(' ')), _factory.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(_factory.Object));

            var output = app.Run();

            output.Message.ShouldBeEquivalentTo(Messages.InvalidArguments);
        }

        [Fact]
        public void it_will_print_an_invalid_arguments_message_if_a_startword_does_not_contain_exactly_four_letters()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search one that".Split(' ')), _factory.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(_factory.Object));

            var output = app.Run();

            output.Message.Should().Contain("Start word one does not contain exactly four letters");
        }

        [Fact]
        public void it_will_print_an_invalid_arguments_message_if_an_endword_does_not_contain_exactly_four_letters()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search prim two".Split(' ')), _factory.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(_factory.Object));

            var output = app.Run();

            output.Message.Should().Contain("End word two does not contain exactly four letters");
        }
    }
}
