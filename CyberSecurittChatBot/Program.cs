using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    // Memory for storing user-specific data
    static Dictionary<string, string> userMemory = new Dictionary<string, string>();

    // Track current conversation state
    static string currentState = "main"; // "main", "beginner", "intermediate", "advanced"

    // Question and answer database
    static Dictionary<string, Dictionary<string, string>> qaDatabase = new Dictionary<string, Dictionary<string, string>>()
    {
        { "beginner", new Dictionary<string, string>
            {
                { "1", "Cybersecurity is the practice of protecting systems, networks, and programs from digital attacks. These attacks often aim to access, change, or destroy sensitive information, extort money, or interrupt business processes." },
                { "2", "A strong password is at least 12 characters long, uses a mix of uppercase and lowercase letters, numbers, and special characters. Avoid using personal information or common words. Consider using a password manager to generate and store complex passwords." },
                { "3", "Phishing is a cyber attack where attackers disguise themselves as trustworthy entities to trick victims into revealing sensitive information like passwords or credit card details. They typically use emails, messages, or fake websites that look legitimate to the unsuspecting user." },
                { "4", "Regular software updates are crucial because they often contain security patches that fix vulnerabilities discovered in the software. Without these updates, your system remains vulnerable to known exploits that attackers can easily use." },
                { "5", "To spot fake websites, check for: suspicious URLs (misspellings, unusual domains), missing HTTPS security, poor design/grammar, requests for excessive personal information, and pressure tactics. Always verify a site's legitimacy before entering sensitive information." }
            }
        },
        { "intermediate", new Dictionary<string, string>
            {
                { "1", "A firewall is a network security device that monitors and filters incoming and outgoing network traffic based on predetermined security rules. It establishes a barrier between a trusted network and untrusted networks, such as the Internet, to block malicious traffic." },
                { "2", "Multi-factor authentication (MFA) is a security system that requires more than one method of authentication to verify the user's identity. Typically, this combines something you know (password), something you have (security token), and something you are (biometric verification)." },
                { "3", "Ransomware is malicious software that encrypts a victim's files, making them inaccessible. The attacker demands a ransom payment to restore access. Modern ransomware attacks often also threaten to publish stolen data if payment isn't made, creating a double extortion scenario." },
                { "4", "Social engineering is the psychological manipulation of people into performing actions or divulging confidential information. It relies on human error rather than technical hacking, exploiting trust, fear, or helpfulness to bypass security measures." },
                { "5", "The principle of least privilege states that users should only have the minimum levels of access necessary to complete their job functions. This limits potential damage from accidents, errors, or security breaches and reduces the attack surface of your systems." }
            }
        },
        { "advanced", new Dictionary<string, string>
            {
                { "1", "Penetration testing is an authorized simulated cyberattack on a computer system to evaluate its security. Pentesters use the same tools and techniques as attackers to find and demonstrate exploitable vulnerabilities before malicious hackers do." },
                { "2", "Asymmetric encryption uses a pair of mathematically related keys: a public key for encryption and a private key for decryption. The private key cannot be derived from the public key. This allows secure communication without sharing a secret key in advance, and enables digital signatures." },
                { "3", "A zero-day exploit attacks a previously unknown vulnerability that hasn't yet been patched. Since there's no patch available (zero days of protection), even systems with up-to-date security can be compromised. These are particularly valuable and dangerous threats." },
                { "4", "An Intrusion Detection System (IDS) monitors network traffic for suspicious activity and issues alerts when detected, but doesn't actively block threats. An Intrusion Prevention System (IPS) actively blocks identified threats in addition to detection, providing real-time protection." },
                { "5", "To prevent SQL injection: use parameterized queries or prepared statements, validate and sanitize all user inputs, implement proper error handling to avoid information leakage, use ORM frameworks when possible, and employ the principle of least privilege for database accounts." }
            }
        }
    };

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

Welcome to the Cybersecurity Chatbot!");

        Console.WriteLine("Choose a difficulty: beginner, intermediate, advanced, or type 'password' to check your password strength.");
        Console.WriteLine("Type 'help' for available commands or 'exit' to quit.");

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
            string userInput = Console.ReadLine();
            userInput = userInput?.Trim().ToLower() ?? "";  // Handle null input

            // Handle empty input
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Please type something.");
                continue;
            }

            if (userInput == "exit")
            {
                Console.WriteLine("Goodbye! Stay secure!");
                break;
            }

            string response = HandleUserInput(userInput);
            Console.WriteLine(response);
        }
    }

    // Method to handle all user input and determine responses
    static string HandleUserInput(string userInput)
    {
        // Check for help command first
        if (userInput == "help")
        {
            return GetHelpMessage();
        }

        // Check if we're in a difficulty level and user is selecting a question
        if ((currentState == "beginner" || currentState == "intermediate" || currentState == "advanced")
            && Regex.IsMatch(userInput, @"^[1-5]$"))
        {
            if (qaDatabase.ContainsKey(currentState) && qaDatabase[currentState].ContainsKey(userInput))
            {
                return qaDatabase[currentState][userInput] + "\n\nType another number (1-5) or 'back' to return to main menu.";
            }
        }

        // Check if user wants to go back to main menu
        if (userInput == "back" || userInput == "main" || userInput == "menu")
        {
            currentState = "main";
            return "Back to main menu. Choose a difficulty: beginner, intermediate, advanced, or type 'password' to check your password strength.";
        }

        // Sentiment detection
        string sentiment = AnalyzeSentiment(userInput);
        if (sentiment == "negative")
        {
            return "I'm sorry to hear that. Let's work through this together.";
        }

        // Check for predefined commands and memory recall
        if (HandlePredefinedCommands(userInput, out string predefinedResponse))
        {
            return predefinedResponse;
        }

        // Check for keyword-based responses
        if (HandleKeywordResponse(userInput, out string keywordResponse))
        {
            return keywordResponse;
        }

        // Check for predefined chatbot functionality (e.g., difficulty levels)
        string chatbotResponse = GetChatbotResponse(userInput);
        if (chatbotResponse != null)
        {
            return chatbotResponse;
        }

        // Fallback response for unrecognized inputs
        return "Sorry, I don't understand that. Type 'help' for available commands.";
    }

    // Method to provide help information
    static string GetHelpMessage()
    {
        return @"Available commands:
- beginner, intermediate, advanced: Choose difficulty level
- password: Check your password strength
- checklink: Check if a URL might be suspicious
- my name is [name]: Tell the bot your name
- what's my name?: Ask the bot your stored name
- back or menu: Return to main menu
- help: Show this help message
- exit: Exit the application";
    }

    // Sentiment detection (improved but still simple)
    static string AnalyzeSentiment(string input)
    {
        var negativeWords = new[] { "frustrated", "angry", "annoyed", "upset", "terrible", "hate", "worst", "bad", "awful" };
        var positiveWords = new[] { "happy", "great", "excellent", "awesome", "good", "love", "best", "wonderful", "pleased" };

        bool hasNegative = negativeWords.Any(word => input.Contains(word));
        bool hasPositive = positiveWords.Any(word => input.Contains(word));

        if (hasNegative && !hasPositive)
            return "negative";
        else if (hasPositive && !hasNegative)
            return "positive";
        return "neutral";
    }

    // Method to handle predefined commands like "my name is"
    static bool HandlePredefinedCommands(string userInput, out string response)
    {
        response = null;

        if (userInput.StartsWith("my name is"))
        {
            string name = userInput.Substring(11).Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                response = "I didn't catch your name. Please say 'my name is [your name]'.";
                return true;
            }
            userMemory["name"] = name;
            response = $"Nice to meet you, {name}!";
            return true;
        }

        if (userInput == "what's my name?" || userInput == "whats my name" || userInput == "who am i")
        {
            response = userMemory.ContainsKey("name")
                ? $"Your name is {userMemory["name"]}."
                : "I don't know your name yet. You can tell me by saying 'my name is [your name]'.";
            return true;
        }

        return false;
    }

    // Method to handle keyword-based responses
    static bool HandleKeywordResponse(string userInput, out string response)
    {
        var keywordResponses = new Dictionary<string, string>
        {
            { "phishing", "Phishing is a cyber-attack where attackers trick users into revealing sensitive information by posing as trustworthy entities. Stay cautious of suspicious emails that ask for personal information or contain unexpected attachments/links!" },
            { "malware", "Malware is malicious software designed to harm your system or steal data. Types include viruses, worms, trojans, ransomware, and spyware. Ensure your antivirus is up-to-date and avoid downloading files from untrusted sources!" },
            { "virus", "Computer viruses are malicious programs that can replicate themselves and spread to other computers. They can corrupt files, steal information, or take control of your system. Always use updated antivirus software for protection." },
            { "hacker", "A hacker is someone who exploits computer systems or networks to gain unauthorized access. While the term often has negative connotations, ethical hackers (or 'white hat' hackers) help identify security vulnerabilities to improve security." },
            { "firewall", "A firewall is a network security device that monitors and filters incoming and outgoing traffic based on predetermined security rules, establishing a barrier between trusted and untrusted networks." },
        };

        foreach (var keyword in keywordResponses.Keys)
        {
            if (userInput.Contains(keyword))
            {
                response = keywordResponses[keyword];
                return true;
            }
        }

        response = null;
        return false;
    }

    // Method to get responses based on chatbot functionality
    static string GetChatbotResponse(string userInput)
    {
        switch (userInput)
        {
            case "beginner":
                currentState = "beginner";
                return "Beginner Level:\nChoose a question (1-5):\n1. What is cybersecurity?\n2. What is a strong password?\n3. What is phishing?\n4. Why should you update software?\n5. How do you spot fake websites?";

            case "intermediate":
                currentState = "intermediate";
                return "Intermediate Level:\nChoose a question (1-5):\n1. What is a firewall?\n2. What is multi-factor authentication (MFA)?\n3. What is ransomware?\n4. What is social engineering?\n5. What's the principle of least privilege?";

            case "advanced":
                currentState = "advanced";
                return "Advanced Level:\nChoose a question (1-5):\n1. What is penetration testing?\n2. How does asymmetric encryption work?\n3. What is a zero-day exploit?\n4. What's the difference between IDS and IPS?\n5. How do you prevent SQL injection?";

            case "password":
                Console.WriteLine("Type your password to check its strength (input will be hidden):");
                string password = GetMaskedInput();
                return CheckPasswordStrength(password);

            case "checklink":
                Console.WriteLine("Enter a URL to check for phishing:");
                string url = Console.ReadLine();
                url = url?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(url))
                {
                    return "No URL entered. Please try again with a valid URL.";
                }
                return CheckPhishingLink(url);

            default:
                return null;
        }
    }

    // Method to hide password input with asterisks
    static string GetMaskedInput()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            // Ignore any control keys like arrows
            if (!char.IsControl(key.KeyChar))
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            // Handle backspace
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b"); // Erase the last * displayed
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine(); // Add a newline after the password input
        return password;
    }

    // Method to check password strength
    static string CheckPasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return "No password entered. Please try again.";
        }

        int strength = 0;
        StringBuilder feedback = new StringBuilder();
        feedback.AppendLine("Password Analysis:");

        // Length check
        if (password.Length >= 12)
        {
            strength += 2;
            feedback.AppendLine("✓ Good length (12+ characters)");
        }
        else if (password.Length >= 8)
        {
            strength += 1;
            feedback.AppendLine("✓ Minimum length met (8+ characters)");
            feedback.AppendLine("! Consider using 12+ characters for better security");
        }
        else
        {
            feedback.AppendLine("✗ Too short (need at least 8 characters)");
        }

        // Character variety checks
        if (password.Any(char.IsUpper))
        {
            strength++;
            feedback.AppendLine("✓ Contains uppercase letters");
        }
        else feedback.AppendLine("✗ Missing uppercase letters");

        if (password.Any(char.IsLower))
        {
            strength++;
            feedback.AppendLine("✓ Contains lowercase letters");
        }
        else feedback.AppendLine("✗ Missing lowercase letters");

        if (password.Any(char.IsDigit))
        {
            strength++;
            feedback.AppendLine("✓ Contains numbers");
        }
        else feedback.AppendLine("✗ Missing numbers");

        if (password.Any(ch => !char.IsLetterOrDigit(ch)))
        {
            strength++;
            feedback.AppendLine("✓ Contains special characters");
        }
        else feedback.AppendLine("✗ Missing special characters");

        // Common patterns check
        if (ContainsCommonPattern(password))
        {
            strength--;
            feedback.AppendLine("✗ Contains common patterns (123, abc, etc.)");
        }

        // Overall rating
        feedback.AppendLine("\nOverall Rating: " + strength switch
        {
            >= 6 => "Excellent - Very strong password!",
            5 => "Very Good - Strong password.",
            4 => "Good - Moderately strong password.",
            3 => "Fair - Consider improving your password.",
            2 => "Weak - Password needs improvement.",
            _ => "Very Weak - Password is not secure."
        });

        return feedback.ToString();
    }

    // Check for common patterns in passwords
    static bool ContainsCommonPattern(string password)
    {
        var commonPatterns = new[]
        {
            "123", "abc", "qwerty", "password", "admin", "login", "welcome",
            "letmein", "monkey", "sunshine", "iloveyou", "princess", "dragon"
        };

        return commonPatterns.Any(pattern =>
            password.ToLower().Contains(pattern.ToLower()));
    }

    // Method to check if a URL is suspicious
    static string CheckPhishingLink(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return "Please provide a valid URL.";
        }

        int riskScore = 0;
        StringBuilder analysis = new StringBuilder();
        analysis.AppendLine("URL Analysis:");

        // Check for proper URL format with HTTP/HTTPS
        if (!url.StartsWith("http://") && !url.StartsWith("https://"))
        {
            analysis.AppendLine("⚠️ URL doesn't start with http:// or https://");
            riskScore++;
        }

        // Check for HTTPS
        if (url.StartsWith("http://") && !url.StartsWith("https://"))
        {
            analysis.AppendLine("⚠️ Not using HTTPS (secure connection)");
            riskScore++;
        }

        // Check for suspicious keywords in URL
        var phishingKeywords = new[] {
            "login", "account", "verify", "password", "banking", "secure", "update",
            "confirm", "paypal", "signin", "security", "recover"
        };

        foreach (var keyword in phishingKeywords)
        {
            if (url.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                analysis.AppendLine($"⚠️ Contains sensitive keyword: '{keyword}'");
                riskScore++;
                break; // Only count this once
            }
        }

        // Check for suspicious TLDs
        var suspiciousTLDs = new[] { ".tk", ".ml", ".ga", ".cf", ".gq", ".info", ".xyz" };
        if (suspiciousTLDs.Any(tld => url.EndsWith(tld, StringComparison.OrdinalIgnoreCase)))
        {
            analysis.AppendLine("⚠️ Uses suspicious top-level domain");
            riskScore += 2;
        }

        // Check for excessive subdomains
        int subdomainCount = url.Count(c => c == '.');
        if (subdomainCount > 3)
        {
            analysis.AppendLine("⚠️ Contains unusual number of subdomains");
            riskScore++;
        }

        // IP address instead of domain name
        if (Regex.IsMatch(url, @"https?://\d+\.\d+\.\d+\.\d+"))
        {
            analysis.AppendLine("⚠️ Uses IP address instead of domain name");
            riskScore += 2;
        }

        // URL with encoded characters
        if (url.Contains("%"))
        {
            analysis.AppendLine("⚠️ Contains encoded characters (%xx)");
            riskScore++;
        }

        // Overall risk assessment
        analysis.AppendLine("\nRisk Assessment: " + riskScore switch
        {
            0 => "Low Risk - URL appears safe, but always remain cautious.",
            1 or 2 => "Moderate Risk - Some suspicious elements detected. Proceed with caution.",
            3 or 4 => "High Risk - Multiple suspicious elements. Strongly advised to avoid this URL.",
            _ => "Very High Risk - Likely phishing attempt! Do not proceed!"
        });

        analysis.AppendLine("\nImportant: This is an automated analysis and cannot guarantee 100% accuracy.");

        return analysis.ToString();
    }
}