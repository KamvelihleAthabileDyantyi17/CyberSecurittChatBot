using System;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static string currentDifficulty = null;

    static void Main(string[] args)
    {
        // Display ASCII art and welcome message
        Console.WriteLine(@"
         /\
        |==| ///////////////////////////////////////////////////////
       /__ \////////////////////////////////////////////////////////////////
   /|---- ==  ===  --- |\//////////////////////////////////////////////////////////// /////
     /|____| \  //////////////////////////////////////////////////////////// //////////////////////////////
      |  __  |   ////////////////// ///////////////////////////////////// /////////////////
     /| |  | |\  ///////////////////////////////////////////////// ////////////////////////////////
    / | |  | | \  /////////////////////////////////////// ///////////////////////////////////////////////////
   |  | |__| |  |///////////////////////////// ////////////////////////////////////////////////////////////////////
   |  |______|  |////////////////////////////////////////////////////////////////////////////////////////////////////
 __|______|______|__//////////////////////////////////////////////////////////////////////////////////////////////////////////////
|____________________|///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

Welcome to the Cybersecurity Chatbot!.");

        // Play custom audio at the beginning
        AudioPlayer audioPlayer = new AudioPlayer(@"C:\Users\kdynt\source\repos\CyberSecurityAI\CyberSecurittChatBot\Chatbot_voice.mp3");
        audioPlayer.Play();

        // Display initial instructions
        Console.WriteLine("Welcome to the Cybersecurity Chatbot!");
        Console.WriteLine("Choose a difficulty: beginner, intermediate, advanced, or type 'password' to check your password strength.");

        // Main loop to handle user input
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

    // Method to get responses based on user input
    static string GetResponse(string userInput)
    {
        switch (userInput)
        {
            // Difficulty levels
            case "beginner":
                currentDifficulty = "beginner";
                return "Beginner Level:\nChoose a question (1-5):\n1. What is cybersecurity?\n2. What is a strong password?\n3. What is phishing?\n4. Why should you update software?\n5. How do you spot fake websites?";

            case "intermediate":
                currentDifficulty = "intermediate";
                return "Intermediate Level:\nChoose a question (1-5):\n1. What is a firewall?\n2. What is multi-factor authentication (MFA)?\n3. What is ransomware?\n4. What is social engineering?\n5. What’s the principle of least privilege?";

            case "advanced":
                currentDifficulty = "advanced";
                return "Advanced Level:\nChoose a question (1-5):\n1. What is penetration testing?\n2. How does asymmetric encryption work?\n3. What is a zero-day exploit?\n4. What’s the difference between IDS and IPS?\n5. How do you prevent SQL injection?";

            // Check password strength
            case "password":
                Console.WriteLine("Type your password to check its strength:");
                string password = Console.ReadLine();
                return CheckPasswordStrength(password);

            // Check for phishing link
            case "checklink":
                Console.WriteLine("Enter a URL to check for phishing:");
                string url = Console.ReadLine();
                return CheckPhishingLink(url);

            default:
                if (currentDifficulty != null)
                {
                    return GetQuestionResponse(userInput);
                }
                return "I don't understand that command. Type 'beginner', 'intermediate', 'advanced', 'password', or 'exit'.";
        }
    }

    // Method to get responses for questions based on the current difficulty level
    static string GetQuestionResponse(string userInput)
    {
        switch (currentDifficulty)
        {
            // Beginner answers
            case "beginner":
                switch (userInput)
                {
                    case "1":
                        return "Answer: Cybersecurity protects systems, networks, and data from cyber attacks.";
                    case "2":
                        return "Answer: A strong password is at least 8 characters, includes uppercase, lowercase, numbers, and symbols.";
                    case "3":
                        return "Answer: Phishing tricks you into providing sensitive info by pretending to be a trusted source.";
                    case "4":
                        return "Answer: Updates fix security holes that hackers may exploit.";
                    case "5":
                        return "Answer: Look for HTTPS, avoid misspelled URLs, and never enter personal data on unknown sites.";
                    default:
                        return "Please choose a question (1-5).";
                }

            // Intermediate answers
            case "intermediate":
                switch (userInput)
                {
                    case "1":
                        return "Answer: A firewall blocks unauthorized access to or from your network.";
                    case "2":
                        return "Answer: MFA adds an extra layer of security by requiring more than just a password.";
                    case "3":
                        return "Answer: Ransomware locks or encrypts your files, demanding a ransom to restore access.";
                    case "4":
                        return "Answer: Social engineering manipulates people into giving up confidential information.";
                    case "5":
                        return "Answer: Least privilege ensures users only have the access they absolutely need.";
                    default:
                        return "Please choose a question (1-5).";
                }

            // Advanced answers
            case "advanced":
                switch (userInput)
                {
                    case "1":
                        return "Answer: Penetration testing simulates a cyber attack to find and fix vulnerabilities.";
                    case "2":
                        return "Answer: Asymmetric encryption uses a public key for encryption and a private key for decryption.";
                    case "3":
                        return "Answer: A zero-day exploit is an attack targeting a software vulnerability before it's patched.";
                    case "4":
                        return "Answer: IDS (Intrusion Detection System) monitors for threats, IPS (Intrusion Prevention System) blocks them.";
                    case "5":
                        return "Answer: Prevent SQL injection by using parameterized queries and avoiding raw SQL inputs.";
                    default:
                        return "Please choose a question (1-5).";
                }

            default:
                return "I don't understand that command. Type 'beginner', 'intermediate', 'advanced', 'password', or 'exit'.";
        }
    }

    // Method to check password strength
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

    // Method to check if a URL is suspicious
    static string CheckPhishingLink(string url)
    {
        var phishingPatterns = new[] { "login", "account", "verify", "password" };
        bool isSuspicious = phishingPatterns.Any(pattern => Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase));

        return isSuspicious
            ? "Warning: This link looks suspicious. Do not enter any personal information!"
            : "This link looks safe, but always be cautious.";
    }
}
