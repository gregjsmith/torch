using System;
using System.IO;
using FluentAssertions;
using Torch.Core;
using Xunit;

namespace Torch.Tests
{
    public class when_getting_a_word_dictionary : IDisposable
    {
        private readonly string[] _words = { "hello", "and", "goodbye" };

        public when_getting_a_word_dictionary()
        {
            File.AppendAllLines("words.txt", _words);
        }

        [Fact]
        public void it_can_load_the_dictionary_from_a_textfile()
        {
            var loader = new WordDictionaryIO();

            var contents = loader.GetWordListFrom("words.txt");

            contents.ShouldBeEquivalentTo(_words);
        }

        [Fact]
        public void it_will_throw_an_exception_when_asked_to_load_from_a_file_that_doesnt_exist()
        {
            var loader = new WordDictionaryIO();

            Exception ex = Assert.Throws<FileLoadingException>(() => loader.GetWordListFrom("words123.txt"));

            ex.Message.Should().Contain("does not exist");
        }

        public void Dispose()
        {
            File.Delete("words.txt");
        }
    }
}