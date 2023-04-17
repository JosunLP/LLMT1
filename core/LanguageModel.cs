using System.Linq;
    

namespace LLMT1.core
{
    internal class LanguageModel
    {
            private Dictionary<string, Dictionary<string, int>> memory;

            private readonly CSVManager csvManager;

            public LanguageModel()
            {
                memory = new Dictionary<string, Dictionary<string, int>>();
                csvManager = new CSVManager("c://");
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

                csvManager.WriteToFile(memory);
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
        }
    
}
