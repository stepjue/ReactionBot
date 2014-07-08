using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionBot
{
    class CommonWordsList : List<String>
    {
        private Random rand;

        public CommonWordsList()
        {
            rand = new Random();
            initializeList();
        }

        public void initializeList()
        {
            string wordString = System.IO.File.ReadAllText("1000commonwords.txt");
            string[] words = wordString.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                this.Add(word);
            }
        }

        public string randomWord()
        {
            return this[Utility.randomInt(rand, 0, this.Count)];
        }
    }
}
