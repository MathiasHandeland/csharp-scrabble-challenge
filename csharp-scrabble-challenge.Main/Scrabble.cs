using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace csharp_scrabble_challenge.Main
{
    public class Scrabble
    {
        private string _word;
        private readonly Dictionary<char, int> _letterValues = new Dictionary<char, int>
        {
            {'A', 1}, {'B', 3}, {'C', 3}, {'D', 2}, {'E', 1},
            {'F', 4}, {'G', 2}, {'H', 4}, {'I', 1}, {'J', 8},
            {'K', 5}, {'L', 1}, {'M', 3}, {'N', 1}, {'O', 1},
            {'P', 3}, {'Q', 10}, {'R', 1}, {'S', 1}, {'T', 1},
            {'U', 1}, {'V', 4}, {'W', 4}, {'X', 8}, {'Y', 4},
            {'Z', 10}
        };
        private List<char> _validBrackets = new List<char> { '{', '}', '[', ']' };
        private Stack<char> _bracketStack = new Stack<char>();

        public Scrabble(string word)
        {
            _word = word;
        }

        public int score()
        {
            int totalScore = 0;
            int index = 0;

            // check if word is valid
            if (!IsValidWord(_word))
            {
                return 0; // Invalid word, return 0 as total score
            }

            while (index < _word.Length)
            {
                char currentChar = _word[index];

                if (currentChar == '{')
                {

                    int multiplier = 2; // this is potentially a double score word
                    index++; // Move past the curly bracket to the letter inside the brackets

                    // check that the character after the letter inside brackets is the correct closing bracket
                    if ((index + 1 < _word.Length) && (_word[index + 1] == '}'))
                    {
                        // retrieve the letter inside and its base value score
                        char letter = Char.ToUpper(_word[index]);
                        if (_letterValues.TryGetValue(letter, out int baseValue))
                        {
                            totalScore += (baseValue * multiplier);
                        }
                        index += 2; // move past the letter and the closing brace or bracket
                    }

                    continue; // move to next iteration since no closing bracket was found
                }

                else if (currentChar == '[')
                {
                    int multiplier = 3; // this is potentially a triple score word
                    index++; // Move to the letter inside the brackets

                    // check that the character after the letter inside brackets is also the right closing bracket
                    if ((index + 1 < _word.Length) && (_word[index + 1] == ']'))
                    {
                        // retrieve the letter inside and its base value score
                        char letter = Char.ToUpper(_word[index]);
                        if (_letterValues.TryGetValue(letter, out int baseValue))
                        {
                            totalScore += baseValue * multiplier;
                        }

                        index += 2; // move past the letter and the closing brace or bracket
                    }

                    continue; // move to next iteration since no closing bracket was found
                }

                else
                {
                    char letter = Char.ToUpper(_word[index]);
                    if (_letterValues.TryGetValue(letter, out int value))
                    {
                        totalScore += value;
                    }

                    index++; // Move to the next character in the word
                }
            }
            return totalScore;
        }

        private bool IsValidWord(string word)
        {
            Stack<char> bracketStack = new Stack<char>();

            // base check: if a character is not present in dictionary or a valid bracket, we return 0
            foreach (char c in word)
            {
                if (!char.IsLetter(c) && !_validBrackets.Contains(c))
                {
                    return false; // Invalid character found
                }
            }

            // check for balanced brackets
            foreach (char c in word)
            {
                if (c == '{' || c == '[')
                {
                    bracketStack.Push(c);
                }
                else if (c == '}' || c == ']')
                {
                    if (bracketStack.Count == 0)
                    {
                        return false;
                    }
                    char openingBracket = bracketStack.Pop();
                    if ((c == '}' && openingBracket != '{') ||
                        (c == ']' && openingBracket != '['))
                    {
                        return false;
                    }
                }
            }

            // If stack is empty, all brackets are balanced
            return bracketStack.Count == 0;
        }
    }
}