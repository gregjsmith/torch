using System;
using FluentAssertions;
using Torch.Cli;
using Torch.Core;
using Torch.Core.Cli;
using Xunit;

namespace Torch.Tests
{
    public class when_parsing_command_line_arguments
    {
        [Fact]
        public void it_can_tell_when_there_are_no_arguments()
        {
            var args = new CommandLineArguments(new string[0]);

            args.None.Should().BeTrue();
            args.Any.Should().BeFalse();
        }

        [Fact]
        public void it_can_tell_when_the_command_is_to_search()
        {
            var args = new CommandLineArguments("search this that".Split(' '));

            args.IsSearchCommand.Should().BeTrue();
        }

        [Fact]
        public void it_can_tell_when_a_start_and_end_word_have_not_been_specified_for_searching()
        {
            var args = new CommandLineArguments("search this".Split(' '));

            args.StartAndEndWordsSpecified.Should().BeFalse();
        }

        [Fact]
        public void it_can_tell_if_a_custom_dictionary_file_has_been_specified()
        {
            var args = new CommandLineArguments("search this that -f c:\afile.txt".Split(' '));

            args.CustomDictionaryFileSpecified.Should().BeTrue();
        }

        [Fact]
        public void it_can_return_the_location_of_the_custom_file_argument()
        {
            var args = new CommandLineArguments("search this that -f c:\afile.txt".Split(' '));

            args.CustomFileLocation.ShouldBeEquivalentTo("c:\afile.txt");
        }

        [Fact]
        public void it_will_throw_an_exception_if_the_file_switch_is_passed_but_the_file_is_not_a_valid_text_file()
        {
            var args = new CommandLineArguments("search this that -f hello".Split(' '));

            Assert.Throws<ArgumentException>(() => args.CustomFileLocation);
        }


        [Fact]
        public void it_can_return_the_location_of_the_output_file_argument()
        {
            var args = new CommandLineArguments("search this that -f afile.txt -o anotherfile.txt".Split(' '));

            args.OutputFileSpecified.Should().BeTrue();
            args.OutputFileLocation.ShouldBeEquivalentTo("anotherfile.txt");
        }

        [Fact]
        public void it_will_throw_an_exception_if_the_output_switch_is_passed_but_the_output_path_is_not_a_valid_text_file()
        {
            var args = new CommandLineArguments("search this that -f hello.txt -o hello".Split(' '));

            Assert.Throws<ArgumentException>(() => args.OutputFileLocation);
        }
    }
}