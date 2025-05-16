using System;
using System.Collections.Generic;

namespace Book_Borrow_App
{
    internal class ManageOption
    {
        private readonly Dictionary<string, Action> _menuActions;

        public ManageOption(Dictionary<string, Action> menuActions)
        {
            _menuActions = menuActions;
        }

        public void Manage(string selectedOption)
        {
            if (_menuActions.ContainsKey(selectedOption))
            {
                _menuActions[selectedOption].Invoke();
            }
            else
            {
                Console.WriteLine("Invalid option");
            }
        }
    }
}
