using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Speech.Synthesis;

class Program
{
    static void Main(string[] args)
    {
        // The voice to be called out
        SpeechSynthesizer _SS = new SpeechSynthesizer();

        _SS.Speak("Welcome to the Cybersecurity Chatbot!");
        Console.WriteLine("Type 'help' for a list of commands.");

        while (true)
        {
            Console.Write("> ");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "exit")
            {
                Console.WriteLine("Goodbye! Stay secure!");
                break;
            }

            string response = GetResponse(userInput);
            Console.WriteLine(response);
        }
    }

    //
    static string GetResponse(string userInput)
    {
        switch (userInput)
        {
            case "help":
                return "Commands: help, tips, phishing, malware, password, checklink, exit";
            case "tips":
                return "Cybersecurity Tips:\n1. Use strong passwords.\n2. Enable two-factor authentication.\n3. Keep your software updated.";
            case "phishing":
                return "Phishing is a type of attack where attackers trick you into revealing sensitive information.";
            case "malware":
                return "Malware is malicious software designed to harm or exploit devices, networks, or data.";
            case "password":
                Console.WriteLine("Type your password to check its strength:");
                string password = Console.ReadLine();
                return CheckPasswordStrength(password);
            case "checklink":
                Console.WriteLine("Enter a URL to check for phishing:");
                string url = Console.ReadLine();
                return CheckPhishingLink(url);
            default:
                return "I don't understand that command. Type 'help' for a list of commands.";
        }
    }

    static string CheckPasswordStrength(string password)
    {
        int strength = 0;

        if (password.Length >= 8) strength++;
        if (password.Any(char.IsUpper)) strength++;
        if (password.Any(char.IsLower)) strength++;
        if (password.Any(char.IsDigit)) strength++;
        if (password.Any(ch => !char.IsLetterOrDigit(ch))) strength++;

        return strength switch
        {
            5 => "Your password is very strong!",
            4 => "Your password is strong.",
            3 => "Your password is moderate.",
            2 => "Your password is weak.",
            _ => "Your password is very weak."
        };
    }

    static string CheckPhishingLink(string url)
    {
        // Basic regex to detect suspicious URLs
        var phishingPatterns = new[] { "login", "account", "verify", "password" };
        bool isSuspicious = phishingPatterns.Any(pattern => Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase));

        return isSuspicious
            ? "Warning: This link looks suspicious. Do not enter any personal information!"
            : "This link looks safe, but always be cautious.";
    }
}