using csharp_scrabble_challenge.Main;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_scrabble_challenge.Test
{
    [TestFixture]
    public class ExtensionTests
    {

        [TestCase("[{h}o1s{e}]", 0)] // error case (zero for errors)
        [TestCase("{h}ous{e}", 13)]
        [TestCase("[{h}ous{e}]", 39)]
        [TestCase("[h}ous{e}]", 0)] //Error case (zero for errors)
        [TestCase("", 0)]
        [TestCase(" ", 0)]
        [TestCase(" \t\n", 0)]
        [TestCase("\n\r\t\b\f", 0)]
        [TestCase("a", 1)]
        [TestCase("f", 4)]
        [TestCase("OXyPHEnBUTaZoNE", 41)]
        [TestCase("quirky", 22)]
        [TestCase("street", 6)]
        public void WordScoreTests(string word, int targetScore)
        {
            Scrabble scrabble = new Scrabble(word);

            Assert.That(scrabble.score(), Is.EqualTo(targetScore));
        }

        private int GetWordScore(string word) => new Scrabble(word).score();
    }
}
