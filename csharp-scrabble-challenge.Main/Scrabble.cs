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
        private int _totalScore = 0;

        public Scrabble(string word)
        {            
            _word = word;
        }

        public int score()
        {
            foreach (char letter in _word.ToUpper())
            {
                if (_letterValues.TryGetValue(letter, out int value)) // check if the letter exists and retrive its value
                {
                    _totalScore += value;          
                }
                else
                {
                    _totalScore += 0; // Things that are not letters and not in the dict will be ignored and do not contribute to the score
                }
            }

            return _totalScore;
        }

    }
}