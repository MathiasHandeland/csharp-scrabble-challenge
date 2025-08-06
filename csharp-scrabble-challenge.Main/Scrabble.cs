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

        public Scrabble(string word)
        {
            _word = word;
        }

        public int score()
        {
            int _totalScore = 0;
            int index = 0;

            while (index < _word.Length)
            {
                char currentChar = _word[index];

                if (currentChar == '{' || currentChar == '[')
                {
                    int multiplier = (currentChar == '{') ? 2 : 3;
                    index++; // Move past the opening brace or bracket to the letter that has double or triple score

                    // check that the character after the letter inside brackets is also a closing bracket
                    if (index < _word.Length && (_word[index + 1] == '}' || _word[index + 1] == ']'))
                    {
                        // Get the letter and its score
                        if (_letterValues.TryGetValue(currentChar, out int baseValue))
                        {
                            _totalScore += baseValue * multiplier;
                        }

                    }
                    index += 2; // Move past the letter and the closing brace or bracket

                }
                else if (_letterValues.TryGetValue(currentChar, out int value))
                {
                    _totalScore += value;
                }
                index++;
            }
            return _totalScore;
        }