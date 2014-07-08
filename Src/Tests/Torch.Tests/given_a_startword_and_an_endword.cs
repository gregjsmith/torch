using System;
using FluentAssertions;
using Moq;
using Torch.Core;
using Xunit;

namespace Torch.Tests
{
    public class given_a_startword_and_an_endword
    {
        private readonly Mock<IDictionaryIO> _fileLoader;
        public given_a_startword_and_an_endword()
        {
            _fileLoader = new Mock<IDictionaryIO>();
        }

        [Fact]
        public void it_can_extract_a_working_data_set_from_a_word_dictionary()
        {
            var words = new string[]
            {
                "how", "now", "brown", "cow"
            };

            _fileLoader.Setup(m => m.GetWordListFrom("")).Returns(words);

            var dictionary = new WordDictionary("", _fileLoader.Object);

            var set = dictionary.GetWorkingSet("now", "cow");

            set.Should().Contain("now", "brown", "cow");
        }

        [Fact]
        public void it_can_extract_a_working_set_in_reverse_when_the_endword_appears_higher_up_than_the_startword()
        {
            var words = new string[]
            {
                "how", "now", "brown", "cow"
            };

            _fileLoader.Setup(m => m.GetWordListFrom("")).Returns(words);

            var dictionary = new WordDictionary("", _fileLoader.Object);

            var set = dictionary.GetWorkingSet("cow", "now");

            set.Should().Contain("cow", "brown", "now");
        }

        [Fact]
        public void it_will_throw_an_exception_if_the_startword_doesnt_appear_in_the_dictionary()
        {
            var words = new[]
            {
                "hello", "is", "it", "me", "you're", "looking", "for"
            };

            _fileLoader.Setup(m => m.GetWordListFrom("")).Returns(words);

            var dictionary = new WordDictionary("", _fileLoader.Object);

            Exception ex =
                Assert.Throws<ArgumentException>(() => dictionary.GetWorkingSet("bonnie", "clyde"));

            ex.Message.Should().Contain("Start word bonnie was not found in dictionary");
        }

        [Fact]
        public void it_will_throw_an_exception_if_the_endword_doesnt_appear_in_the_dictionary()
        {
            var words = new[]
            {
                "hello", "is", "it", "me", "you're", "looking", "for"
            };

            _fileLoader.Setup(m => m.GetWordListFrom("")).Returns(words);

            var dictionary = new WordDictionary("", _fileLoader.Object);

            Exception ex = Assert.Throws<ArgumentException>(() => dictionary.GetWorkingSet("hello", "clyde"));

            ex.Message.Should().Contain("End word clyde was not found in dictionary");
        }

      
    }
}
