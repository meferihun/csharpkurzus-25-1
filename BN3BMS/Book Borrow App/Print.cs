using System;
using System.Collections.Generic;

namespace Book_Borrow_App
{
    internal class Print
    {
        private readonly Dictionary<string, Action> _menuActions;
        public int CurrentIndex { get; private set; }

        public Print(Dictionary<string, Action> menuActions)
        {
            _menuActions = menuActions;
            CurrentIndex = 1;
        }

        public void PrintMenu()
        {
            while (true)
            {
                Console.Clear();
                DisplayMenu();
                if (!HandleInput())
                {
                    break;
                }
            }
        }

        private bool HandleInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    CurrentIndex = CurrentIndex - 1 > 0 ? CurrentIndex - 1 : CurrentIndex;
                    break;

                case ConsoleKey.DownArrow:
                    CurrentIndex = CurrentIndex + 1 < _menuActions.Count ? CurrentIndex + 1 : CurrentIndex;
                    break;

                case ConsoleKey.Enter:
                    var selectedOption = GetSelectedOption();
                    _menuActions[selectedOption]?.Invoke();
                    return false;
            }

            return true;
        }
               
        private void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var title = _menuActions.Keys.ElementAt(0);
            Console.WriteLine(title);
            Console.ResetColor(); 

                int index = 1; 
            foreach (var option in _menuActions)
            {
                if (option.Key != title)
                {
                    if (index == CurrentIndex && option.Value != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"---> {option.Key} <---");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"     {option.Key}     ");
                    }
                    index++;
                }
            }
        }

        private string GetSelectedOption()
        {
            int index = 0;
            foreach (var option in _menuActions.Keys)
            {
                if (index == CurrentIndex)
                {
                    return option;
                }
                index++;
            }
            return null;
        }
    }
}
