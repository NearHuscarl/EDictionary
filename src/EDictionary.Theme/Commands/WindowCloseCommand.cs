using System;
using System.Windows;
using System.Windows.Input;

namespace EDictionary.Theme.Commands
{
    public class WindowCloseCommand : ICommand
    {     

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var window = parameter as Window;

            if (window != null)
            {
                window.Close();
            }
        }
    }
}
