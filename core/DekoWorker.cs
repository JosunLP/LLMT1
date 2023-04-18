namespace LLMT1.core;

/// <summary>
/// DekoWorker is a class that handles the interaction style with the user.
/// </summary>
public static class DekoWorker
{
	/// <summary>
	/// Writes Logo in console
	/// </summary>
	public static void SetLogo()
	{
		Console.WriteLine("Today is the " + DateTime.Now + "\n" +
		"\n\n\nWelcome to \n\n" +
		"\r\n   __   __   __  ____________\r\n  / /  / /  /  |/  /_  __<  /\r\n / /__/ /__/ /|_/ / / /  / / \r\n/____/____/_/  /_/ /_/  /_/  \r\n                             \r\n");
	}

	/// <summary>
	/// Writes the defined Menu in console
	/// </summary>
	public static void SetMenu(string menuData)
	{
		var commandList = JsonConvert.DeserializeObject<Menu>(menuData);

		SetSpacer();
		Console.WriteLine("\nCommands: ");

		foreach (var item in commandList.Commands)
		{
			Console.WriteLine("              " + item.Command + "          - " + item.Description);
		}
		SetSpacer();
	}

	/// <summary>
	/// Writes the defined Spacer in console
	/// </summary>
	public static void SetSpacer()
	{
		Console.WriteLine("=================================================================================\n");
	}

	/// <summary>
	/// Thells the user that the command is unknown
	/// </summary>
	public static void SetCommand()
	{
		Console.WriteLine("\nPlease insert a command: (use !help for a list of all commands)\n");
	}

	/// <summary>
	/// Reads the input for the user
	/// </summary>
	public static string Input()
	{
		Console.Write("$ ");
		return Console.ReadLine();
	}
}
