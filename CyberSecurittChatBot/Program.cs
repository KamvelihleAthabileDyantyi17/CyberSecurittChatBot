using System;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(@"
        /\
        |==|
       /__ \
   /|---- ==  ===  --- |\
     /|____| \
      |  __  |
     /| |  | |\
    / | |  | | \
   |  | |__| |  |
   |  |______|  |
 __|______|______|__
|____________________|

Welcome to the Cybersecurity Chatbot!.");

        // Play custom audio at the beginning
        AudioPlayer audioPlayer = new AudioPlayer("Audio/Chatbot_voice.mp3");
        audioPlayer.Play();

        Console.WriteLine("Welcome to the Cybersecurity Chatbot!");
        Console.WriteLine("Choose a difficulty: beginner, intermediate, advanced, or type 'password' to check your password strength.");

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

    static string GetResponse(string userInput)
    {
        switch (userInput)
        {
            case "beginner":
                return "Beginner Level:\nChoose a question (1-5):\n1. What is cybersecurity?\n2. What is a strong password?\n3. What is phishing?\n4. Why should you update software?\n5. How do you spot fake websites?";

            case "intermediate":
                return "Intermediate Level:\nChoose a question (1-5):\n1. What is a firewall?\n2. What is multi-factor authentication (MFA)?\n3. What is ransomware?\n4. What is social engineering?\n5. What’s the principle of least privilege?";

            case "advanced":
                return "Advanced Level:\nChoose a question (1-5):\n1. What is penetration testing?\n2. How does asymmetric encryption work?\n3. What is a zero-day exploit?\n4. What’s the difference between IDS and IPS?\n5. How do you prevent SQL injection?";

            // Beginner answers
            case "beginner 1":
                return "Answer: Cybersecurity protects systems, networks, and data from cyber attacks.";
            case "beginner 2":
                return "Answer: A strong password is at least 8 characters, includes uppercase, lowercase, numbers, and symbols.";
            case "beginner 3":
                return "Answer: Phishing tricks you into providing sensitive info by pretending to be a trusted source.";
            case "beginner 4":
                return "Answer: Updates fix security holes that hackers may exploit.";
            case "beginner 5":
                return "Answer: Look for HTTPS, avoid misspelled URLs, and never enter personal data on unknown sites.";

            // Intermediate answers
            case "intermediate 1":
                return "Answer: A firewall blocks unauthorized access to or from your network.";
            case "intermediate 2":
                return "Answer: MFA adds an extra layer of security by requiring more than just a password.";
            case "intermediate 3":
                return "Answer: Ransomware locks or encrypts your files, demanding a ransom to restore access.";
            case "intermediate 4":
                return "Answer: Social engineering manipulates people into giving up confidential information.";
            case "intermediate 5":
                return "Answer: Least privilege ensures users only have the access they absolutely need.";

            // Advanced answers
            case "advanced 1":
                return "Answer: Penetration testing simulates a cyber attack to find and fix vulnerabilities.";
            case "advanced 2":
                return "Answer: Asymmetric encryption uses a public key for encryption and a private key for decryption.";
            case "advanced 3":
                return "Answer: A zero-day exploit is an attack targeting a software vulnerability before it's patched.";
            case "advanced 4":
                return "Answer: IDS (Intrusion Detection System) monitors for threats, IPS (Intrusion Prevention System) blocks them.";
            case "advanced 5":
                return "Answer: Prevent SQL injection by using parameterized queries and avoiding raw SQL inputs.";

            case "password":
                Console.WriteLine("Type your password to check its strength:");
                string password = Console.ReadLine();
                return CheckPasswordStrength(password);

            case "checklink":
                Console.WriteLine("Enter a URL to check for phishing:");
                string url = Console.ReadLine();
                return CheckPhishingLink(url);

            default:
                return "I don't understand that command. Type 'beginner', 'intermediate', 'advanced', 'password', or 'exit'.";
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
        var phishingPatterns = new[] { "login", "account", "verify", "password" };
        bool isSuspicious = phishingPatterns.Any(pattern => Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase));

        return isSuspicious
            ? "Warning: This link looks suspicious. Do not enter any personal information!"
            : "This link looks safe, but always be cautious.";
    }
}
