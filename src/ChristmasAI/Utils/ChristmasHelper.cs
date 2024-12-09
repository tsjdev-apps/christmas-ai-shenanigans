using Microsoft.Extensions.AI;

namespace ChristmasAI.Utils;

/// <summary>
///     Provides helper methods for generating 
///     Christmas-related content using AI.
/// </summary>
public static class ChristmasHelper
{
    /// <summary>
    ///     Generates a Christmas greeting message asynchronously.
    /// </summary>
    /// <param name="chatClient">The chat client used to 
    /// generate the greeting.</param>
    public static async Task GenerateChristmasGreetingAsync(
        IChatClient chatClient)
    {
        string name =
            ConsoleHelper.GetString(
                "Enter a [yellow]name[/] to generate a greeting:");

        string language =
            ConsoleHelper.SelectFromOptions(
                [Statics.GermanLanguage, Statics.EnglishLanguage,
                 Statics.FrenchLanguage, Statics.SpanishLanguage],
                "Please select a [yellow]language[/].");

        string prompt =
            $"Write a warm Christmas greeting for {name} in {language}.";

        ChatCompletion result =
            await chatClient.CompleteAsync(prompt);

        ConsoleHelper.DisplayMessage(
            result.Message.Text ?? "No response available.");
    }

    /// <summary>
    ///     Generates a short Christmas story asynchronously.
    /// </summary>
    /// <param name="chatClient">The chat client used to 
    /// generate the story.</param>
    public static async Task GenerateChristmasStoryAsync(
        IChatClient chatClient)
    {
        string name =
            ConsoleHelper.GetString(
                "Enter a [yellow]name[/] to generate a story:");

        string context =
            ConsoleHelper.GetString(
                "Enter a little bit [yellow]context[/] of your story:");

        string language =
            ConsoleHelper.SelectFromOptions(
                [Statics.GermanLanguage, Statics.EnglishLanguage,
                 Statics.FrenchLanguage, Statics.SpanishLanguage],
                "Please select a [yellow]language[/].");

        string prompt =
            $"Write a short christmas story using the name '{name}' in {language}. " +
            $"Use also the following details: {context}.";

        ChatCompletion result =
            await chatClient.CompleteAsync(
                prompt, 
                new ChatOptions { MaxOutputTokens = 1000 });

        ConsoleHelper.DisplayMessage(
            result.Message.Text ?? "No response available.");
    }

    /// <summary>
    /// Suggests a Christmas recipe based on provided ingredients asynchronously.
    /// </summary>
    /// <param name="chatClient">The chat client used to generate the recipe.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task SuggestChristmasRecipeAsync(
        IChatClient chatClient)
    {
        string ingredients =
            ConsoleHelper.GetString(
                "Enter a comma-separated list of [yellow]ingredients[/]:");

        string language =
            ConsoleHelper.SelectFromOptions(
                [Statics.GermanLanguage, Statics.EnglishLanguage,
                Statics.FrenchLanguage, Statics.SpanishLanguage], 
                "Please select a [yellow]language[/].");

        string prompt =
            $"Create a recipe for a Christmas dish using the following ingredients: " +
            $"{ingredients}. Please write the recipe in {language}.";

        ChatCompletion result =
            await chatClient.CompleteAsync(
                prompt, 
                new ChatOptions { MaxOutputTokens = 1000 });

        ConsoleHelper.DisplayMessage(
            result.Message.Text ?? "No response available.");
    }
}
