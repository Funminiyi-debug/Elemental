using Elemental.BusinessLogic.Models;
using Microsoft.Extensions.Options;

namespace Elemental.BusinessLogic
{
    public class ElementalWordsService : IElementalWordsService
    {
        private readonly Dictionary<string, string> ELEMENTS;
        public ElementalWordsService(IOptions<PeriodicElementsOptions> elements)
        {
            ELEMENTS = new Dictionary<string, string>(elements.Value.Elements, StringComparer.OrdinalIgnoreCase); ;
        }
        const int symbolMaxLength = 3;
        public List<List<string>> ElementalForms(string word)
        {
            if (string.IsNullOrEmpty(word)) return new List<List<string>> { };
            var memo = new Dictionary<string, List<List<string>>>(StringComparer.OrdinalIgnoreCase);
            var result = ElementalForm(word, memo);
            return result.Count > 0 ? result : new List<List<string>>();
        }

        private List<List<string>> ElementalForm(string s, Dictionary<string, List<List<string>>> memo)
        {

            if (memo.ContainsKey(s))
            {
                return memo[s];
            }

            var results = new List<List<string>>();

            if (string.IsNullOrEmpty(s))
            {
                results.Add(new List<string>());
                return results;
            }

            for (int i = 1; i <= 3 && i <= s.Length; i++)
            {
                string prefix = s.Substring(0, i);
                string symbol = Capitalize(prefix);

                if (ELEMENTS.TryGetValue(symbol, out var element))
                {
                    var suffixes = ElementalForm(s.Substring(i), memo);
                    foreach (var suffix in suffixes)
                    {
                        var combination = new List<string> { $"{element} ({symbol})" };
                        combination.AddRange(suffix);
                        results.Add(combination);
                    }
                }
            }

            memo[s] = results;
            return results;
        }

        private static string Capitalize(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (s.Length == 1)
                return s.ToUpper();

            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }
    }
}
