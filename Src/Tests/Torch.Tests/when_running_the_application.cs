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
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(new FakeSearchCommand()));

            var output = app.Run();

            output.Results.Should().BeNullOrEmpty();
            output.Message.ShouldBeEquivalentTo(Messages.Help);
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
        public void it_will_print_an_invalid_arguments_message_if_a_search_is_directed_but_start_word_isnt_provided()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search".Split(' ')), _factory.Object);

            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(new FakeSearchCommand()));

            var output = app.Run();

            output.Message.ShouldBeEquivalentTo(Messages.InvalidArguments);

        }

        [Fact]
        public void it_will_print_an_invalid_arguments_message_if_a_search_is_directed_but_end_word_isnt_provided()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search this".Split(' ')), _factory.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(new FakeSearchCommand()));

            var output = app.Run();

            output.Message.ShouldBeEquivalentTo(Messages.InvalidArguments);
        }

        [Fact]
        public void it_will_print_an_invalid_arguments_message_if_a_startword_does_not_contain_exactly_four_letters()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search one that".Split(' ')), _factory.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(new FakeSearchCommand()));

            var output = app.Run();

            output.Message.Should().Contain("Start word one does not contain exactly four letters");
        }

        [Fact]
        public void it_will_print_an_invalid_arguments_message_if_an_endword_does_not_contain_exactly_four_letters()
        {
            var app = new CommandLineApplication(new CommandLineArguments("search prim two".Split(' ')), _factory.Object);
            _factory.Setup(m => m.InitialiseArgumentHandlers()).Returns(new ArgumentHandler(new FakeSearchCommand()));

            var output = app.Run();

            output.Message.Should().Contain("End word two does not contain exactly four letters");
        }

        private class FakeSearchCommand : ICommand<CommandLineArguments, string[]>
        {
            public CommandLineArguments Context { set; private get; }
            public string[] Execute()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
