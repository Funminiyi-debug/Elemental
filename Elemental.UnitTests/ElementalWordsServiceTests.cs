namespace Elemental.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Elemental.BusinessLogic;
    using Elemental.BusinessLogic.Models;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Xunit;

    public class ElementalWordsServiceTests
    {
        private readonly ElementalWordsService _sut;
        public ElementalWordsServiceTests()
        {
            var elements = JsonConvert.DeserializeObject<PeriodicElementsOptions>(File.ReadAllText("elements.json"));
            _sut = new ElementalWordsService(Options.Create(elements!));
        }
        [Fact]
        public void Test_Word_Formable_By_Element_Symbols()
        {
            string word = "beach";
            var expected = new List<List<string>>
        {
            new List<string> { "Beryllium (Be)", "Actinium (Ac)", "Hydrogen (H)" }
        };

            var actual = _sut.ElementalForms(word);

            Assert.True(ContainsAllCombinations(expected, actual));
        }

        [Fact]
        public void Test_Word_Not_Formable_By_Element_Symbols()
        {
            string word = "xyz";
            var actual = _sut.ElementalForms(word);

            Assert.Empty(actual);
        }

        [Fact]
        public void Test_Empty_String()
        {
            // Test with an empty string
            string word = "";
            var actual = _sut.ElementalForms(word);

            Assert.Empty(actual);
        }

        [Fact]
        public void Test_Single_Letter_Word()
        {
            string word = "S";
            var expected = new List<List<string>>
        {
            new List<string> { "Sulfur (S)" }
        };

            var actual = _sut.ElementalForms(word);

            Assert.True(ContainsAllCombinations(expected, actual));
        }

        [Fact]
        public void Test_Case_Insensitive_Input()
        {
            string wordLower = "fun";
            string wordUpper = "FUN";

            var resultLower = _sut.ElementalForms(wordLower);
            var resultUpper = _sut.ElementalForms(wordUpper);

            Assert.Equal(resultLower.Count, resultUpper.Count);
            Assert.True(ContainsAllCombinations(resultLower, resultUpper));
        }

        [Fact]
        public void Test_Long_Word()
        {
            string word = "University";
            var actual = _sut.ElementalForms(word);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_Word_With_No_Element_Symbols()
        {
            string word = "zzz";
            var actual = _sut.ElementalForms(word);

            Assert.Empty(actual);
        }

        [Fact]
        public void Test_Multiple_Combinations()
        {
            string word = "Snack";
            var actual = _sut.ElementalForms(word);

            Assert.True(actual.Count > 1);
        }

        // Helper method to compare combinations
        private bool ContainsAllCombinations(List<List<string>> expected, List<List<string>> actual)
        {
            var expectedSets = expected.Select(e => string.Join(",", e)).ToHashSet();
            var actualSets = actual.Select(a => string.Join(",", a)).ToHashSet();

            return expectedSets.SetEquals(actualSets);
        }
    }

}