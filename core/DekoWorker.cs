namespace Basis_.Net_Core_Konsolen_App;

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
        Console.WriteLine("Today is the " + DateTime.Now  + "\n" +
        "\n\n\nWelcome to \n\n"+
        "  ___                _   _                      \n" +
        " / _ \\              | \\ | |                     \n" +
        "/ / \\ \\_ __  _ __   |  \\| | __ _ _ __ ___   ___ \n" +
        "|  _  | '_ \\| '_ \\  | . ` |/ _` | '_ ` _ \\ / _  \\\n" +
        "| | | | |_) | |_) | | |\\  | (_| | | | | | |  __/\n" +
        "\\_| |_/ .__/| .__/  \\_| \\_/\\__,_|_| |_| |_|\\___|\n" +
        "      | |   | |                                 \n" +
        "      |_|   |_|                                 \n");
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
