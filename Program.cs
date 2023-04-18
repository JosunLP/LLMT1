namespace LLMT1;

class Program
{
	static void Main(string[] args)
	{
		#region vars
		string command;
		char[] chars = { '*', ' ', '\'', '"', '#' };
		#endregion

		#region main
		DekoWorker.SetLogo();
		DekoWorker.SetSpacer();
		while (true)
		{
			DekoWorker.SetCommand();
			command = DekoWorker.Input().ToLower();
			command = command.Trim(chars);
			switch (command)
			{
				case "!exit":
					Console.WriteLine();
					return;
				case "!help":

					Menu commands = new()
					{
						Commands = new List<MenuItem>()
						{
							new MenuItem() { Command = "!train", Description = "Starts the training with all files in the './training/' directory." },
							new MenuItem(){
								Command = "!exit",
								Description = "Ends the execution of the program"
							},
							new MenuItem(){
								Command = "!help",
								Description = "Displays this help"
							},
							new MenuItem(){
								Command = "!clear",
								Description = "Deletes the backlog"
							}
						}
					};

					DekoWorker.SetMenu(JsonConvert.SerializeObject(commands));
					DekoWorker.SetSpacer();
					break;
				case "!clear":
					Console.Clear();
					DekoWorker.SetLogo();
					Console.WriteLine();
					DekoWorker.SetSpacer();
					break;
				case "!train":
					TrainingDataHandler trainingDataHandler = new("./training/");
					LanguageModel languageModel = new();
					trainingDataHandler.Train(languageModel);
					break;
				default:
					Console.WriteLine("Command Unknown!\n");
					DekoWorker.SetSpacer();
					break;
			}
		}
		#endregion
	}
}
