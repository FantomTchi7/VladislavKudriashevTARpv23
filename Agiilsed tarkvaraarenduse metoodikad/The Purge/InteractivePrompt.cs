using System;
using System.Collections.Generic;
using System.Linq;

namespace The_Purge
{
    public enum AnchorType
    {
        TopLeft, TopCenter, TopRight,
        MiddleLeft, MiddleCenter, MiddleRight,
        BottomLeft, BottomCenter, BottomRight
    }

    public struct AnchorPoint
    {
        public AnchorType Type { get; }
        public AnchorPoint(AnchorType type) { Type = type; }

        public static readonly AnchorPoint TopLeft = new AnchorPoint(AnchorType.TopLeft);
        public static readonly AnchorPoint TopCenter = new AnchorPoint(AnchorType.TopCenter);
        public static readonly AnchorPoint TopRight = new AnchorPoint(AnchorType.TopRight);
        public static readonly AnchorPoint MiddleLeft = new AnchorPoint(AnchorType.MiddleLeft);
        public static readonly AnchorPoint Center = new AnchorPoint(AnchorType.MiddleCenter);
        public static readonly AnchorPoint MiddleRight = new AnchorPoint(AnchorType.MiddleRight);
        public static readonly AnchorPoint BottomLeft = new AnchorPoint(AnchorType.BottomLeft);
        public static readonly AnchorPoint BottomCenter = new AnchorPoint(AnchorType.BottomCenter);
        public static readonly AnchorPoint BottomRight = new AnchorPoint(AnchorType.BottomRight);
    }

    public class InteractivePrompt
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Text { get; set; }
        public List<string> Choices { get; private set; }

        public InteractivePrompt(int x, int y, string text)
        {
            X = x;
            Y = y;
            Text = text ?? string.Empty;
            Choices = new List<string>();
        }

        public InteractivePrompt(int x, int y, string text, params string[] initialChoices)
            : this(x, y, text)
        {
            if (initialChoices != null)
            {
                Choices.AddRange(initialChoices.Where(c => c != null));
            }
        }

        public void AddChoice(string choice)
        {
            if (choice != null) Choices.Add(choice);
        }

        public void AddChoices(IEnumerable<string> choicesToAdd)
        {
            if (choicesToAdd != null) Choices.AddRange(choicesToAdd.Where(c => c != null));
        }

        public bool RemoveChoice(string choice)
        {
            return Choices.Remove(choice);
        }

        public void RemoveChoiceAt(int index)
        {
            if (index >= 0 && index < Choices.Count) Choices.RemoveAt(index);
        }

        public void ClearChoices()
        {
            Choices.Clear();
        }

        public int ShowWindow(AnchorPoint anchor, int boxPadding = 2, int maxTextWidth = 60, bool treatTextAsRaw = false)
        {
            List<string> textLinesToDisplay;
            if (treatTextAsRaw)
            {
                textLinesToDisplay = this.Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                if (textLinesToDisplay.Count > 0 && string.IsNullOrEmpty(textLinesToDisplay.LastOrDefault()))
                {
                    textLinesToDisplay.RemoveAt(textLinesToDisplay.Count - 1);
                }
            }
            else
            {
                textLinesToDisplay = WrapText(this.Text, maxTextWidth);
            }

            List<string> formattedChoices = new List<string>();
            if (this.Choices.Count > 0)
            {
                for (int i = 0; i < this.Choices.Count; i++)
                {
                    formattedChoices.Add(this.Choices[i]);
                }
            }

            int maxTextLineLength = textLinesToDisplay.Any() ? textLinesToDisplay.Max(line => line?.Length ?? 0) : 0;
            int maxChoiceLineLength = formattedChoices.Any() ? formattedChoices.Max(line => line?.Length ?? 0) : 0;
            int contentWidth = Math.Max(maxTextLineLength, maxChoiceLineLength);
            if (contentWidth == 0) contentWidth = 1;

            int boxWidth = contentWidth + (boxPadding * 2) + 2;
            int separatorHeight = (textLinesToDisplay.Any() && formattedChoices.Any()) ? 1 : 0;
            int boxHeight = textLinesToDisplay.Count + separatorHeight + formattedChoices.Count + 2;

            int actualX = this.X;
            int actualY = this.Y;

            switch (anchor.Type)
            {
                case AnchorType.TopCenter:
                case AnchorType.MiddleCenter:
                case AnchorType.BottomCenter:
                    actualX -= boxWidth / 2;
                    break;
                case AnchorType.TopRight:
                case AnchorType.MiddleRight:
                case AnchorType.BottomRight:
                    actualX -= (boxWidth - 1);
                    break;
            }
            switch (anchor.Type)
            {
                case AnchorType.MiddleLeft:
                case AnchorType.MiddleCenter:
                case AnchorType.MiddleRight:
                    actualY -= boxHeight / 2;
                    break;
                case AnchorType.BottomLeft:
                case AnchorType.BottomCenter:
                case AnchorType.BottomRight:
                    actualY -= (boxHeight - 1);
                    break;
            }

            actualX = Math.Max(0, Math.Min(Console.BufferWidth - boxWidth, actualX));
            actualY = Math.Max(0, Math.Min(Console.BufferHeight - boxHeight, actualY));

            ConsoleColor originalForeground = Console.ForegroundColor;
            ConsoleColor originalBackground = Console.BackgroundColor;
            int originalCursorLeft = Console.CursorLeft;
            int originalCursorTop = Console.CursorTop;
            bool originalCursorVisible = true;
            try { originalCursorVisible = Console.CursorVisible; } catch (System.IO.IOException) { }

            try
            {
                try { Console.CursorVisible = false; } catch (System.IO.IOException) { }

                ConsoleColor borderColor = ConsoleColor.Gray;
                ConsoleColor textColor = ConsoleColor.White;
                ConsoleColor choiceColor = ConsoleColor.Gray;
                ConsoleColor selectedChoiceForeground = ConsoleColor.Black;
                ConsoleColor selectedChoiceBackground = ConsoleColor.Cyan;
                ConsoleColor separatorColor = ConsoleColor.DarkGray;

                Console.ForegroundColor = borderColor;
                Console.BackgroundColor = originalBackground;

                Console.SetCursorPosition(actualX, actualY);
                Console.Write("╔" + new string('═', boxWidth - 2) + "╗");

                int currentLineY = actualY + 1;
                string horizontalPadding = new string(' ', boxPadding);

                Console.ForegroundColor = textColor;
                foreach (string line in textLinesToDisplay)
                {
                    Console.ForegroundColor = borderColor;
                    Console.SetCursorPosition(actualX, currentLineY); Console.Write("║");
                    Console.SetCursorPosition(actualX + boxWidth - 1, currentLineY); Console.Write("║");

                    Console.ForegroundColor = textColor;
                    Console.SetCursorPosition(actualX + 1, currentLineY);
                    Console.Write(horizontalPadding + (line ?? string.Empty).PadRight(contentWidth) + horizontalPadding);
                    currentLineY++;
                }

                if (separatorHeight > 0)
                {
                    Console.ForegroundColor = separatorColor;
                    Console.SetCursorPosition(actualX, currentLineY);
                    Console.Write("╟" + new string('─', boxWidth - 2) + "╢");
                    currentLineY++;
                }

                int firstChoiceLineY = currentLineY;
                for (int i = 0; i < formattedChoices.Count; i++)
                {
                    Console.ForegroundColor = borderColor;
                    Console.SetCursorPosition(actualX, currentLineY); Console.Write("║");
                    Console.SetCursorPosition(actualX + boxWidth - 1, currentLineY); Console.Write("║");

                    Console.ForegroundColor = choiceColor;
                    Console.BackgroundColor = originalBackground;
                    Console.SetCursorPosition(actualX + 1 + boxPadding, currentLineY);
                    Console.Write(formattedChoices[i].PadRight(contentWidth));

                    currentLineY++;
                }

                Console.ForegroundColor = borderColor;
                Console.SetCursorPosition(actualX, currentLineY);
                Console.Write("╚" + new string('═', boxWidth - 2) + "╝");

                if (formattedChoices.Count == 0)
                {
                    Console.ReadKey(true);
                    return -1;
                }

                int selectedIndex = 0;
                ConsoleKeyInfo keyInfo;

                while (true)
                {
                    for (int i = 0; i < formattedChoices.Count; i++)
                    {
                        int choiceLineY = firstChoiceLineY + i;
                        Console.SetCursorPosition(actualX + 1 + boxPadding, choiceLineY);

                        if (i == selectedIndex)
                        {
                            Console.ForegroundColor = selectedChoiceForeground;
                            Console.BackgroundColor = selectedChoiceBackground;
                        }
                        else
                        {
                            Console.ForegroundColor = choiceColor;
                            Console.BackgroundColor = originalBackground;
                        }
                        Console.Write(formattedChoices[i].PadRight(contentWidth));
                    }

                    keyInfo = Console.ReadKey(true);

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        selectedIndex = (selectedIndex - 1 + formattedChoices.Count) % formattedChoices.Count;
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        selectedIndex = (selectedIndex + 1) % formattedChoices.Count;
                    }
                    else if (keyInfo.Key >= ConsoleKey.D1 && keyInfo.Key <= ConsoleKey.D9)
                    {
                        int numChoice = keyInfo.Key - ConsoleKey.D1;
                        if (numChoice < formattedChoices.Count)
                        {
                            selectedIndex = numChoice;
                        }
                    }
                    else if (keyInfo.Key >= ConsoleKey.NumPad1 && keyInfo.Key <= ConsoleKey.NumPad9)
                    {
                        int numChoice = keyInfo.Key - ConsoleKey.NumPad1;
                        if (numChoice < formattedChoices.Count)
                        {
                            selectedIndex = numChoice;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        selectedIndex = -1;
                        break;
                    }
                }
                return selectedIndex;
            }
            finally
            {
                Console.ForegroundColor = originalForeground;
                Console.BackgroundColor = originalBackground;
                try
                {
                    Console.CursorVisible = originalCursorVisible;
                    if (originalCursorTop < Console.BufferHeight && originalCursorLeft < Console.BufferWidth)
                    {
                        Console.SetCursorPosition(originalCursorLeft, originalCursorTop);
                    }
                }
                catch (System.IO.IOException) { }
                catch (ArgumentOutOfRangeException) { }
            }
        }

        private static List<string> WrapText(string text, int maxWidth)
        {
            if (string.IsNullOrEmpty(text)) return new List<string>();
            if (maxWidth <= 0) return new List<string> { text };

            List<string> lines = new List<string>();
            string[] paragraphs = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (var paragraph in paragraphs)
            {
                if (string.IsNullOrWhiteSpace(paragraph))
                {
                    lines.Add(string.Empty);
                    continue;
                }

                string currentLine = "";
                string[] words = paragraph.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (word.Length > maxWidth)
                    {
                        if (!string.IsNullOrEmpty(currentLine))
                        {
                            lines.Add(currentLine);
                            currentLine = "";
                        }
                        int startIndex = 0;
                        while (startIndex < word.Length)
                        {
                            int len = Math.Min(maxWidth, word.Length - startIndex);
                            lines.Add(word.Substring(startIndex, len));
                            startIndex += len;
                        }
                        continue;
                    }

                    if (string.IsNullOrEmpty(currentLine))
                    {
                        currentLine = word;
                    }
                    else if (currentLine.Length + word.Length + 1 <= maxWidth)
                    {
                        currentLine += " " + word;
                    }
                    else
                    {
                        lines.Add(currentLine);
                        currentLine = word;
                    }
                }

                if (!string.IsNullOrEmpty(currentLine))
                {
                    lines.Add(currentLine);
                }
            }
            return lines;
        }
    }
}