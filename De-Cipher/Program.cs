#region Interactive menu
/*
    Displays active option in cyan
    Function was created in order to make code look more readable
 */
using System.Runtime.CompilerServices;

void displayActiveOption(string option)
{
    ConsoleColor currentColor = Console.ForegroundColor;

    Console.Write("|\t");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(option);
    Console.ForegroundColor = currentColor;
}

/*
    Function was created in order to realize DRY construction for apps, that offer branched menu system

    Returns: Index of option choosen buy user, 
                returns -1 in case user pressed escape
 */
int setInteractiveMenu(string[] options, string caption)
{
    int currentOption = 0;
    do
    {
        Console.WriteLine("_____________________________________________________________");
        Console.WriteLine($"|\t{caption}: ");
        Console.WriteLine("|_____________________________________________________________");
        for (int i = 0; i < options.Length; i++)
        {
            if (i == currentOption)
                displayActiveOption(options[i]);
            else
                Console.WriteLine($"|\t{options[i]}");
        }
        Console.WriteLine("|_____________________________________________________________");
        Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        ConsoleKey pressedKey = Console.ReadKey().Key;

        switch (pressedKey)
        {
            case ConsoleKey.Spacebar:
            case ConsoleKey.Enter: return currentOption;

            case ConsoleKey.W:
            case ConsoleKey.UpArrow: if (--currentOption < 0) currentOption = options.Length - 1; break;

            case ConsoleKey.S:
            case ConsoleKey.DownArrow: if (++currentOption >= options.Length) currentOption = 0; break;
            case ConsoleKey.Escape: return -1;
        }
    } while (true);
}
#endregion

#region Checked inputs
int userInputInt(string message = "")
{
    while (true)
    {
        try
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Hey at least input something here!");
            }
            else if (input.Length > int.MaxValue.ToString().Length - 1)
            {
                throw new Exception("You expect me to move it to the mars or what?\nSorry, but try lesser value");
            }

            return int.Parse(input);
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
        }
    }
}
string userInputString(string message = "")
{
    while (true)
    {
        try
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Hey at least input something here!");
            }

            return input;
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
        }
    }
}
#endregion

#region Cyphers
string CesarusCypher(string message, int move)
{
    const int MaxLowerAscii = 122;
    const int MinLowerAscii = 97;
    message = message.ToLower();

    string newMessage = string.Empty;
    for (int i = 0; i < message.Length; i++)
    {
        char codedLetter;
        if ((int)message[i] + move > MaxLowerAscii)
        {
            codedLetter = (char)((int)message[i] + move - MaxLowerAscii + MinLowerAscii - 1);
        }
        else if ((int)message[i] + move < MinLowerAscii)
        {
            codedLetter = (char)((int)message[i] + move + MaxLowerAscii - MinLowerAscii + 1);
        }
        else
        {
            codedLetter = (char)((int)message[i] + move);
        }
        newMessage += codedLetter;
    }
    return newMessage;
}
string VyzhenerusCypher(string message, string keyWord, bool decode = false)
{
    const int MaxLowerAscii = 122;
    const int MinLowerAscii = 97;
    message = message.ToLower();

    string newMessage = string.Empty;
    for (int i = 0, j = 0; i < message.Length; i++, j++)
    {
        if (j >= 3)
            j = 0;

        char codedLetter;
        int move = ((int)keyWord[j]) - 97;
        if (decode)
            move *= -1;

        if ((int)message[i] + move > MaxLowerAscii)
        {
            codedLetter = (char)((int)message[i] + move - MaxLowerAscii + MinLowerAscii - 1);
        }
        else if ((int)message[i] + move < MinLowerAscii)
        {
            codedLetter = (char)((int)message[i] + move + MaxLowerAscii - MinLowerAscii + 1);
        }
        else
        {
            codedLetter = (char)((int)message[i] + move);
        }
        newMessage += codedLetter;
    }

    return newMessage;
} 
#endregion

bool escPressed = false;
string[] mainMenuOptions = new string[2] { "Ceasar Cipher", "Vigenere Cypher" };
string[] CypherMenuOptions = new string[3] { "Code", "Decode", "About" };

string input = string.Empty;

int move = -1;
string codeWord = string.Empty;

string textToDe_Code = string.Empty;

do
{
    switch (setInteractiveMenu(mainMenuOptions, "Main menu"))
    {
        case 0:
            Console.Clear();

            switch (setInteractiveMenu(CypherMenuOptions, "Cesarus Cypher Menu"))
            {
                case -1:
                    Console.Clear();
                    break;
                case 0:
                    Console.Clear();
                    move = userInputInt("Move: ");

                    textToDe_Code = userInputString("Text to code: ");

                    Console.WriteLine($"\nCoded text: {CesarusCypher(textToDe_Code, move)}");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 1:
                    Console.Clear();
                    move = userInputInt("Move: ") * -1;

                    textToDe_Code = userInputString("Text to decode: ");

                    Console.WriteLine($"\nDecoded text: {CesarusCypher(textToDe_Code, move)}");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
            break;
        case 1:
            Console.Clear();

            switch (setInteractiveMenu(CypherMenuOptions, "Vyzhenerus Cypher Menu"))
            {
                case -1:
                    Console.Clear();
                    break;
                case 0:
                    Console.Clear();
                    codeWord = userInputString("Code word: ");

                    textToDe_Code = userInputString("Text to code: ");

                    Console.WriteLine($"\nCoded text: {VyzhenerusCypher(textToDe_Code, codeWord)}");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 1:
                    Console.Clear();
                    codeWord = userInputString("Code word: ");

                    textToDe_Code = userInputString("Text to decode: ");

                    Console.WriteLine($"\nDecoded text: {VyzhenerusCypher(textToDe_Code, codeWord, true)}");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
            break;
        case -1:
            Console.Clear();

            Console.Write("\n\n\n\n\t\t\t\t\t\tThanks for using the app\n\t\t\t\t\t\t\tHave a nice day ");

            Thread.Sleep(1000);
            Console.Write("<");
            Thread.Sleep(1000);
            Console.Write("3");

            escPressed = true;
            break;
    } 
} while (!escPressed);