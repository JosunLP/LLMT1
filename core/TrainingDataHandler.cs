using System.IO;

namespace LLMT1.core
{
	internal class TrainingDataHandler
	{
		private readonly string _directory;

		public TrainingDataHandler(string directory)
		{
			_directory = directory;
		}

		public void Train(LanguageModel languageModel)
		{
			string[] files;

			try
			{
				files = Directory.GetFiles(_directory, "*.txt", SearchOption.AllDirectories);
			}
			catch (Exception)
			{
				Console.WriteLine("Could not find training data directory: " + _directory);
				throw;
			}

			if (files.Length == 0)
			{
				Console.WriteLine("No training data found in directory: " + _directory);
				return;
			}

			foreach (string file in files)
			{
				string text = File.ReadAllText(file);
				languageModel.Train(text);
			}

			languageModel.SaveToFile();


		}
	}
}
