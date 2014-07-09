using FluentAssertions;
using Torch.Core;
using Xunit;

namespace Torch.Tests
{
    public class given_a_working_set
    {
        [Fact]
        public void it_can_produce_matches_for_a_simple_list()
        {
            var set = new[]
            {
                "where",
                "there",
                "barry",
                "ghere",
                "ghare",
                "glare",
                "spare",
                "flare"
            };

            var matcher = new SingleLetterUpdateMatching();

            var matches = matcher.GetMatches(set, "where", "flare");

            matches.ShouldBeEquivalentTo(new[]
            {
                "where",
                "there",
                "ghere",
                "ghare",
                "glare",
                "flare"
            });
        }

        [Fact]
        public void it_can_produce_matches_for_a_list_where_word_lengths_differ()
        {
            var set = new[]
            {
                "what",
                "who",
                "where",
                "therethere",
                "barryb",
                "ghere",
                "ghare",
                "glaring",
                "sparetyre",
                "flaregun",
                "hello"
            };

            var matcher = new SingleLetterUpdateMatching();

            var matches = matcher.GetMatches(set, "where", "hello");

            matches.ShouldBeEquivalentTo(new[]
            {
                "where",
                "ghere",
                "ghare",
                "hello"
            });
        }

        [Fact]
        public void it_can_produce_matches_for_a_list_containing_slash_characters()
        {
            var set = new[]
            {
                @"\10th",
                @"\st",
                @"\2nd",
                @"\3rd",
                @"\4th",
                @"\5th",
                @"\6th",
                @"\7th",
                @"\8th",
                @"\9th"
            };

            var matcher = new SingleLetterUpdateMatching();

            var matches = matcher.GetMatches(set, @"\4th", @"\7th");

            matches.ShouldBeEquivalentTo(new[]
            {
                @"\4th",
                @"\5th",
                @"\6th",
                @"\7th"
            });
        }


        [Fact]
        public void it_can_produce_matches_for_a_list_containing_apostrophe_characters()
        {
            var set = new[]
            {
                "Huntsville",
                "Hurd",
                "Huron",
                "Hurst",
                "Hurwitz",
                "Huston",
                "Hutchins",
                "Hutchinson",
                "Hutchison",
                "Huxley",
                "Huxtable",
                "Hyades",
                "Hyannis",
                "Hyde",
                "Hyman",
                "I'd",
                "I'll",
                "I'm",
                "I've",
                "IA",
                "IBM",
                "ICC",
                "ID",
                "IEEE",
                "IL",
                "IQ",
                "IR",
                "IRS",
                "IT&T",
                "ITT"
            };

            var matcher = new SingleLetterUpdateMatching();

            var matches = matcher.GetMatches(set, @"I'd", @"IR", true);

            matches.ShouldBeEquivalentTo(new[]
            {
                @"I'd",
                "I'm",
                @"IR"
            });
        }

        [Fact]
        public void matching_is_not_case_sensitive_by_default()
        {
            var list = new[]
            {
                "SPIN",
                "spoN",
                "sPun",
                "Sput"
            };

            var matcher = new SingleLetterUpdateMatching();

            var matches = matcher.GetMatches(list, "SPIN", "Sput");

            matches.ShouldBeEquivalentTo(new[]
            {
                "SPIN",
                "spoN",
                "sPun",
                "Sput"
            });
        }

    }
}