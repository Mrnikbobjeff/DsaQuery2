using System.Windows.Input;

namespace ViewModel
{
    public class ClearRequestCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            // Sollte nur Enabled sein, wenn auch ein Text vorhanden ist.
            // ggfs aus BL Schicht
            return true;
        }

        public void Execute(object? parameter)
        {
            // Sollte aus BL-Schicht den Suchstring löschen
        }
    }
}