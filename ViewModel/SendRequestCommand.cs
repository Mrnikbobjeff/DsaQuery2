using System.Windows.Input;

namespace ViewModel
{
    public class SendRequestCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            // Sollte vermutlich nur true sein, wenn ein Suchtext vorhanden ist und Channel selektiert sind => ggfs aus BL Schicht.
            return true;
        }

        public void Execute(object? parameter)
        {
            // Soll Request triggern => BL Send Request
        }
    }
}