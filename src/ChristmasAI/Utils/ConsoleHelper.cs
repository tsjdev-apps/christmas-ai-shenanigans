using Spectre.Console;

namespace ChristmasAI.Utils;

internal static class ConsoleHelper
{
    /// <summary>
    ///     Clears the console and creates the header for the application.
    /// </summary>
    public static void ShowHeader()
    {
        AnsiConsole.Clear();

        Grid grid = new();
        grid.AddColumn();
        grid.AddRow(new FigletText("ChristmasAI").Centered().Color(Color.Red));
        grid.AddRow(Align.Center(new Panel("[red]Sample by Thomas Sebastian Jensen ([link]https://www.tsjdev-apps.de[/])[/]")));

        AnsiConsole.Write(grid);
        AnsiConsole.WriteLine();
    }

    /// <summary>
    ///     Prompts the user to select from a list of options.
    /// </summary>
    /// <param name="options">The list of options to display.</param>
    /// <param name="prompt">The prompt message for the selection.</param>
    /// <returns>The selected option as a string.</returns>
    public static string SelectFromOptions(
        List<string> options,
        string prompt)
    {
        ShowHeader();

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(prompt)
                .AddChoices(options));
    }

    /// <summary>
    ///     Displays a prompt with the provided message and returns the user input.
    /// </summary>
    /// <param name="prompt">The prompt message.</param>
    /// <param name="shouldClear">Indicates whether the console should be cleared before showing the prompt.</param>
    /// <returns>The user input as a string.</returns>
    public static string GetString(string prompt, bool shouldClear = true)
    {
        if (shouldClear)
        {
            ShowHeader();
        }

        return AnsiConsole.Prompt(
            new TextPrompt<string>(prompt)
            .PromptStyle("white")
            .ValidationErrorMessage("[red]Invalid prompt[/]")
            .Validate(result =>
            {
                return result.Length switch
                {
                    < 3 => ValidationResult.Error("[red]Value too short[/]"),
                    > 200 => ValidationResult.Error("[red]Value too long[/]"),
                    _ => ValidationResult.Success()
                };
            }));
    }

    /// <summary>
    ///     Prompts the user to confirm a choice with a yes/no question.
    /// </summary>
    /// <param name="prompt">The confirmation prompt.</param>
    /// <param name="defaultValue">The default value if no input is given.</param>
    /// <returns>True if the user confirms; otherwise, false.</returns>
    public static bool GetConfirmation(
        string prompt,
        bool defaultValue)
    {
        DisplayMessage(string.Empty);

        return AnsiConsole.Confirm(
            prompt,
            defaultValue);
    }

    /// <summary>
    ///     Displays an error message to the console.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    public static void DisplayError(string message)
        => WriteToConsole($"[red]{message}[/]");

    /// <summary>
    ///     Displays an error message to the console.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    public static void DisplayMessage(string message)
        => WriteToConsole($"[white]{message.Replace('[', '\'').Replace(']', '\'')}[/]");

    /// <summary>
    ///     Writes the specified text to the console.
    /// </summary>
    /// <param name="text">The text to write.</param>
    private static void WriteToConsole(string text)
    {
        AnsiConsole.Markup(text);
        AnsiConsole.WriteLine();
    }
}
