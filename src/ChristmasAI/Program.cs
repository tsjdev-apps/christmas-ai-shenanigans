using System.ClientModel;
using Azure.AI.OpenAI;
using ChristmasAI.Utils;
using Microsoft.Extensions.AI;
using OpenAI;

IChatClient? chatClient = null;
bool shouldRerun = true;

/// <summary>
///     Gets the chat host from the user and 
///     initializes the chat client accordingly.
/// </summary>
string chatHost =
    ConsoleHelper.SelectFromOptions(
    [
        Statics.OllamaKey, Statics.OpenAiKey,
        Statics.AzureOpenAiKey
    ], "Please select the [yellow]host[/].");

switch (chatHost)
{
    case Statics.OllamaKey:
        {
            /// <summary>
            /// Gets the OLLAMA model name from the 
            /// user and creates the chat client.
            /// </summary>
            string modelName =
                ConsoleHelper.GetString("Enter your [yellow]Ollama model[/] name:");

            chatClient = new OllamaChatClient(
                new Uri("http://localhost:11434"),
                modelName);

            break;
        }

    case Statics.OpenAiKey:
        {
            /// <summary>
            /// Gets the OpenAI API key and model name 
            /// from the user and creates the chat client.
            /// </summary>
            string apiKey =
                ConsoleHelper.GetString("Enter your [yellow]OpenAi API[/] key:");

            string modelName =
                ConsoleHelper.SelectFromOptions(
                    [
                        Statics.Gpt4oMiniModelName, Statics.Gpt4oModelName,
                    Statics.Gpt4TurboModelName, Statics.Gpt4ModelName
                    ], "Please select the [yellow]OpenAI model[/].");

            chatClient = new OpenAIClient(
                    apiKey)
                .AsChatClient(modelName);

            break;
        }

    case Statics.AzureOpenAiKey:
        {
            /// <summary>
            /// Gets the Azure OpenAI endpoint, API key, 
            /// and model name from the user and creates 
            /// the chat client.
            /// </summary>
            string endpoint =
                ConsoleHelper.GetString("Enter your [yellow]Azure OpenAI[/] endpoint:");

            string apiKey =
                ConsoleHelper.GetString("Enter your [yellow]Azure OpenAI API[/] key:");

            string modelName =
                ConsoleHelper.GetString("Enter your [yellow]Azure OpenAI chat model[/] name:");

            chatClient = new AzureOpenAIClient(
                    new Uri(endpoint),
                    new ApiKeyCredential(apiKey))
                .AsChatClient(modelName);

            break;
        }
}

if (chatClient is null)
{
    ConsoleHelper.DisplayError("Invalid chat host selected.");
    return;
}

/// <summary>
/// D   isplays the header and runs the main loop 
/// for user interaction.
/// </summary>
ConsoleHelper.ShowHeader();

while (shouldRerun)
{
    var option =
        ConsoleHelper.SelectFromOptions(
            [Statics.GreetingKey, Statics.StoryKey,
             Statics.RecipeKey], 
            "Please select an [yellow]option[/].");

    switch (option)
    {
        case Statics.GreetingKey:
            await ChristmasHelper.GenerateChristmasGreetingAsync(chatClient);
            break;
        case Statics.StoryKey:
            await ChristmasHelper.GenerateChristmasStoryAsync(chatClient);
            break;
        case Statics.RecipeKey:
            await ChristmasHelper.SuggestChristmasRecipeAsync(chatClient);
            break;
    }

    shouldRerun =
        ConsoleHelper.GetConfirmation(
            "Do you want to continue?",
            true);
}