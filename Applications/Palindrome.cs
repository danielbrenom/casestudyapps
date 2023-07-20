using System.Threading.Tasks;
using Foundation.Abstracts;

namespace Applications
{
    public class Palindrome : ApplicationExecutable<Palindrome>
    {
        private string Word { get; set; }
        private char[] Vowels = {'a','e','i','o','u'}; 
        public override void ReceiveParameters(params object[] parameters)
        {
            Word = (string) parameters[0];
        }

        public override async Task<object> Execute()
        {
            var forwardVerify = WordVerify(Word, 0, Word.Length - 1, 1);
            var backwardVerify = WordVerify(Word, Word.Length - 1, 0, -1);
            return await Task.FromResult(forwardVerify == backwardVerify);
        }

        private int WordVerify(string word, int start, int end, int iteration)
        {
            var wordValue = 0;
            while (start != end)
            {
                var isVowel = false;
                foreach (var vowel in Vowels)
                {
                    if (!Equals(vowel, word[start])) continue;
                    isVowel = true;
                    break;
                }

                wordValue += isVowel ? word[start] : word[start] * start;
                start += iteration;
            }

            return wordValue;
        }
        
        
    }
}