using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace The_Purge
{
    internal class Game
    {
        const int PurgeDurationHours = 12;
        const int InitialHealth = 100;
        const int InitialFood = 5;
        const int ScavengeDangerChance = 40;
        const int ScavengeFindFoodChance = 30;
        const int HideDangerChance = 15;

        static Player player = new Player(InitialHealth, InitialFood);
        static int currentTime = 0;
        static Random random = new Random();
        static string lastEventMessage = "The Purge has begun. Sirens echo in the distance.";
        static string turnActionMessage = "";

        static int windowWidth = 120;
        static int windowHeight = 35;
        const int StatBarHeight = 3;
        const int MessageAreaStartY = StatBarHeight + 1;

        static void Main(string[] args)
        {
            SetupConsole();
            ShowIntroScreen();

            while (currentTime < PurgeDurationHours && player.IsAlive)
            {
                Console.Clear();
                DisplayStatus();

                int choice = GetPlayerChoice();
                int timePassed = HandlePlayerAction(choice);

                if (timePassed > 0)
                {
                    currentTime += timePassed;
                }
                else if (choice == -1)
                {
                    turnActionMessage = "You hesitated, losing precious time...";
                    lastEventMessage = "";
                    currentTime += 1;
                }

                if (player.IsAlive && currentTime < PurgeDurationHours)
                {
                    Console.SetCursorPosition(0, MessageAreaStartY + 4);
                    Console.WriteLine("\n(Press any key to continue the night...)");
                    Console.ReadKey(true);
                }
                else
                {
                    Thread.Sleep(2500);
                }
            }

            ShowGameOverScreen();
            CleanupConsole();
        }

        static void SetupConsole()
        {
            Console.Title = "The Purge: Survival";
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                if (OperatingSystem.IsWindows())
                {
                    Console.SetWindowSize(windowWidth, windowHeight);
                    Console.SetBufferSize(windowWidth, windowHeight);
                }
                else
                {
                    Console.WriteLine($"Note: Console resizing may not work reliably on this OS.");
                    windowWidth = Console.WindowWidth;
                    windowHeight = Console.WindowHeight;
                }
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine($"Warning: Could not set console size ({ex.GetType().Name}). Using default size.");
                windowWidth = Console.WindowWidth;
                windowHeight = Console.WindowHeight;
                Console.WriteLine($"Using: {windowWidth}x{windowHeight}. Press any key to continue.");
                Console.ReadKey(true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Warning: Console size {windowWidth}x{windowHeight} might be too large ({ex.GetType().Name}). Using default size.");
                windowWidth = Console.WindowWidth;
                windowHeight = Console.WindowHeight;
                Console.WriteLine($"Using: {windowWidth}x{windowHeight}. Ensure console window is large enough. Press any key to continue.");
                Console.ReadKey(true);
            }
            try { Console.CursorVisible = false; } catch (System.IO.IOException) { }
            Console.Clear();
        }

        static void ShowIntroScreen()
        {
            string titleArt =
                 "▄▄▄█████▓ ██░ ██ ▓█████     ██▓███   █    ██  ██▀███    ▄████ ▓█████ \n" +
                 "▓  ██▒ ▓▒▓██░ ██▒▓█   ▀    ▓██░  ██▒ ██  ▓██▒▓██ ▒ ██▒ ██▒ ▀█▒▓█   ▀ \n" +
                 "▒ ▓██░ ▒░▒██▀▀██░▒███      ▓██░ ██▓▒▓██  ▒██░▓██ ░▄█ ▒▒██░▄▄▄░▒███   \n" +
                 "░ ▓██▓ ░ ░▓█ ░██ ▒▓█  ▄    ▒██▄█▓▒ ▒▓▓█  ░██░▒██▀▀█▄  ░▓█  ██▓▒▓█  ▄ \n" +
                 "  ▒██▒ ░ ░▓█▒░██▓░▒████▒   ▒██▒ ░  ░▒▒█████▓ ░██▓ ▒██▒░▒▓███▀▒░▒████▒\n" +
                 "  ▒ ░░    ▒ ░░▒░▒░░ ▒░ ░   ▒▓▒░ ░  ░░▒▓▒ ▒ ▒ ░ ▒▓ ░▒▓░ ░▒   ▒ ░░ ▒░ ░\n" +
                 "    ░     ▒ ░▒░ ░ ░ ░  ░   ░▒ ░     ░░▒░ ░ ░   ░▒ ░ ▒░  ░   ░  ░ ░  ░\n" +
                 "  ░       ░  ░░ ░   ░      ░░        ░░░ ░ ░   ░░   ░ ░ ░   ░    ░   \n" +
                 "          ░  ░  ░   ░  ░               ░        ░           ░    ░  ░\n";

            string introText = titleArt +
                 "\n" +
                 $"The annual Purge has begun. Emergency services are suspended for {PurgeDurationHours} hours.\n" +
                 "Survive the night by managing your health and food.\n" +
                 "Make your choices carefully. Good luck.";

            InteractivePrompt introPrompt = new InteractivePrompt(
                Console.BufferWidth / 2,
                1,
                introText
            );

            introPrompt.ShowWindow(AnchorPoint.TopCenter, boxPadding: 1, treatTextAsRaw: true);
        }

        static void DisplayStatus()
        {
            Console.SetCursorPosition(0, 0);
            string timeRemaining = $"Time Left: {Math.Max(0, PurgeDurationHours - currentTime)} hrs";
            string statusText = $"{player.GetStatus()} | {timeRemaining}";

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("╔" + new string('═', windowWidth - 2) + "╗");
            Console.Write("║ ");
            Console.Write(statusText.PadRight(windowWidth - 3));
            Console.WriteLine("║");
            Console.WriteLine("╚" + new string('═', windowWidth - 2) + "╝");

            Console.ResetColor();

            Console.SetCursorPosition(0, MessageAreaStartY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Action: " + turnActionMessage.PadRight(windowWidth - 8));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Event:  " + lastEventMessage.PadRight(windowWidth - 8));

            Console.ResetColor();
            Console.WriteLine(new string('-', windowWidth));
        }

        static int GetPlayerChoice()
        {
            int promptY = MessageAreaStartY + 3;

            InteractivePrompt choicePrompt = new InteractivePrompt(
                Console.BufferWidth / 2,
                promptY,
                "The night wears on... What is your next move?",
                $"Rest     (Cost: 1 Food | Gain: 10-20 Health | Time: 1 Hr)",
                $"Scavenge (Risk: {ScavengeDangerChance}% | Reward: {ScavengeFindFoodChance}% Food | Time: 2 Hrs)",
                $"Hide     (Risk: {HideDangerChance}% | Time: 1 Hr)"
            );

            int selectedIndex = choicePrompt.ShowWindow(AnchorPoint.TopCenter, boxPadding: 2);
            return selectedIndex;
        }

        static int HandlePlayerAction(int choiceIndex)
        {
            int timePassed = 0;
            turnActionMessage = "";
            lastEventMessage = "";

            switch (choiceIndex)
            {
                case 0: // Rest
                    timePassed = 1;
                    if (player.Food > 0)
                    {
                        if (player.ConsumeFood(1))
                        {
                            int healthRestored = random.Next(10, 21);
                            player.Heal(healthRestored);
                            turnActionMessage = $"Consumed 1 food ration to rest.";
                        }
                        else
                        {
                            turnActionMessage = "Error: Could not consume food despite having some.";
                        }
                    }
                    else
                    {
                        turnActionMessage = "Tried to rest, but had no food! Hunger weakens you.";
                        player.TakeDamage(5);
                    }
                    break;

                case 1: // Scavenge
                    timePassed = 2;
                    turnActionMessage = "Cautiously ventured out to scavenge...";
                    TriggerRandomEvent(ScavengeDangerChance, ScavengeFindFoodChance);
                    break;

                case 2: // Hide
                    timePassed = 1;
                    turnActionMessage = "Found a dark corner to hide and wait...";
                    TriggerRandomEvent(HideDangerChance, 0);
                    break;

                case -1: // ESC
                    break;

                default:
                    turnActionMessage = "Error: Invalid choice index received.";
                    break;
            }
            return timePassed;
        }

        static void TriggerRandomEvent(int dangerChancePercent, int findFoodChancePercent)
        {
            int roll = random.Next(1, 101);

            if (roll <= dangerChancePercent)
            {
                lastEventMessage = "Danger found you! ";
                int eventType = random.Next(1, 4);
                int damage = 0;

                switch (eventType)
                {
                    case 1:
                        damage = random.Next(15, 31);
                        lastEventMessage += $"Ambushed by Purgers!";
                        player.TakeDamage(damage);
                        break;
                    case 2:
                        damage = random.Next(5, 16);
                        lastEventMessage += $"Triggered a crude trap!";
                        player.TakeDamage(damage);
                        break;
                    case 3:
                        if (player.Food > 0)
                        {
                            int foodLost = random.Next(1, Math.Max(2, (player.Food / 2) + 1));
                            foodLost = Math.Min(foodLost, player.Food);
                            player.ConsumeFood(foodLost);
                            lastEventMessage += $"Desperate scavengers stole {foodLost} of your precious food!";
                        }
                        else
                        {
                            lastEventMessage += "Hostile figures approached, but left seeing you had nothing worth taking.";
                        }
                        break;
                }
            }
            else if (findFoodChancePercent > 0 && roll <= dangerChancePercent + findFoodChancePercent)
            {
                lastEventMessage = "A stroke of luck! ";
                int foodFound = random.Next(1, 3);
                player.AddFood(foodFound);
            }
            else
            {
                lastEventMessage = "The immediate area seems quiet. Nothing of note found.";
            }
        }

        static void ShowGameOverScreen()
        {
            Console.Clear();

            string resultMessage;
            ConsoleColor titleColor = ConsoleColor.DarkRed;

            if (player.IsAlive && currentTime >= PurgeDurationHours)
            {
                resultMessage = "The sirens wail. Dawn breaks.\nYou have SURVIVED the Purge!";
                titleColor = ConsoleColor.DarkGreen;
            }
            else if (!player.IsAlive)
            {
                resultMessage = "You succumbed to the darkness.\nThe Purge has claimed another victim.";
            }
            else
            {
                resultMessage = "The Purge ended unexpectedly.";
                titleColor = ConsoleColor.DarkYellow;
            }

            List<string> lines = new List<string>();
            lines.Add("G A M E   O V E R");
            lines.Add("");
            lines.AddRange(resultMessage.Split('\n'));
            lines.Add("");
            lines.Add("Final Status:");
            lines.Add(player.GetStatus());

            int maxLineWidth = lines.Max(s => s?.Length ?? 0);
            int boxWidth = maxLineWidth + 4;
            int boxHeight = lines.Count + 2;

            int startX = Math.Max(0, (windowWidth - boxWidth) / 2);
            int startY = Math.Max(0, (windowHeight - boxHeight) / 2);

            Console.ForegroundColor = titleColor;

            Console.SetCursorPosition(startX, startY);
            Console.Write("╔" + new string('═', boxWidth - 2) + "╗");

            for (int i = 0; i < lines.Count; i++)
            {
                Console.SetCursorPosition(startX, startY + 1 + i);
                Console.Write("║ ");

                Console.ForegroundColor = (i == 0) ? titleColor : ConsoleColor.Gray;
                Console.Write((lines[i] ?? "").PadRight(maxLineWidth));

                Console.ForegroundColor = titleColor;
                Console.WriteLine(" ║");
            }

            Console.SetCursorPosition(startX, startY + boxHeight - 1);
            Console.Write("╚" + new string('═', boxWidth - 2) + "╝");

            Console.ResetColor();
            Console.SetCursorPosition(0, startY + boxHeight + 1);
            Console.WriteLine("\nPress any key to exit the simulation...");
            Console.ReadKey(true);
        }

        static void CleanupConsole()
        {
            try { Console.CursorVisible = true; } catch (System.IO.IOException) { }
            Console.ResetColor();
            Console.Clear();
        }
    }
}