using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenAI_API;

namespace CyberBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Opening message
            Console.WriteLine("Hello, Welcome to Cybersecurity Awareness Bot, CAB for short. How may I assist you today?");

            // OpenAI API key (replace with your actual API key)
            string apiKey = " ";
            var openai = new OpenAIAPI(apiKey);
            // Simplified while loop
            Console.WriteLine("What would you like to know about Cybersecurity? (Type 'Exit' to quit)");
            string userQuestion;

            while ((userQuestion = Console.ReadLine()) != null &&
                   !userQuestion.Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                string response = await GetResponseFromOpenAI(openai, userQuestion);
                Console.WriteLine(response);
                Console.WriteLine("\nWhat would you like to know about Cybersecurity? (Type 'Exit' to quit)");
            }

            static async Task<string> GetResponseFromOpenAI(OpenAIAPI openai, string userQuestion)
        {
            var completionRequest = new CompletionRequest
            {
                Prompt = userQuestion,
                MaxTokens = 150
            };

            var completionResult = await openai.Completions.CreateCompletionAsync(completionRequest);
            return completionResult.Completions[0].Text.Trim();
        }
    }
}
