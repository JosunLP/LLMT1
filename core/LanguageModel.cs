using System.IO;
using System.Linq;


namespace LLMT1.core
{
    internal class LanguageModel
    {
        private Dictionary<string, Dictionary<string, int>> memory;

        private string FilePath;

        public LanguageModel(string filePath)
        {
            FilePath = filePath;
            memory = new Dictionary<string, Dictionary<string, int>>();
        }
        public void Train(string inputText)
        {
            string[] words = inputText.Split(' ');

            for (int i = 0; i < words.Length - 1; i++)
            {
                string currentWord = words[i];
                string nextWord = words[i + 1];

                if (!memory.ContainsKey(currentWord))
                {
                    memory[currentWord] = new Dictionary<string, int>();
                }

                if (!memory[currentWord].ContainsKey(nextWord))
                {
                    memory[currentWord][nextWord] = 0;
                }

                memory[currentWord][nextWord]++;
            }

        }

        public string GenerateText(int length)
        {
            string[] words = new string[length];
            string currentWord = GetRandomStartingWord();

            for (int i = 0; i < length; i++)
            {
                words[i] = currentWord;

                if (memory.ContainsKey(currentWord))
                {
                    currentWord = GetNextWord(currentWord);
                }
                else
                {
                    currentWord = GetRandomStartingWord();
                }
            }

            return string.Join(" ", words);
        }

        public string GetResponse(string inputText)
        {
            string[] words = inputText.Split(' ');
            string lastWord = words[words.Length - 1];

            if (memory.ContainsKey(lastWord))
            {
                return GetMostFrequentNextWord(lastWord);
            }
            else
            {
                return "I'm sorry, I don't understand.";
            }
        }

        private string GetRandomStartingWord()
        {
            return memory.Keys.ElementAt(new Random().Next(memory.Count));
        }

        private string GetNextWord(string currentWord)
        {
            var possibleNextWords = memory[currentWord];
            int totalOccurrences = possibleNextWords.Sum(x => x.Value);
            int randomNumber = new Random().Next(totalOccurrences);
            int cumulativeOccurrences = 0;

            foreach (var wordCount in possibleNextWords)
            {
                cumulativeOccurrences += wordCount.Value;

                if (cumulativeOccurrences > randomNumber)
                {
                    return wordCount.Key;
                }
            }

            return possibleNextWords.Keys.ElementAt(new Random().Next(possibleNextWords.Count));
        }

        private string GetMostFrequentNextWord(string currentWord)
        {
            return memory[currentWord].OrderByDescending(x => x.Value).First().Key;
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                using StreamWriter sw = new(filePath);
                foreach (var word in memory)
                {
                    string row = word.Key + "," + string.Join(",", word.Value.Select(x => x.Key + ":" + x.Value));
                    sw.WriteLine(row);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while writing to the file: " + e.Message);
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                using StreamReader sr = new(filePath);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] row = line.Split(',');
                    string word = row[0];
                    string[] nextWords = row[1].Split(',');

                    memory[word] = new Dictionary<string, int>();

                    foreach (var nextWord in nextWords)
                    {
                        string[] nextWordCount = nextWord.Split(':');
                        memory[word][nextWordCount[0]] = int.Parse(nextWordCount[1]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while reading from the file: " + e.Message);
            }
        }
    }

}
