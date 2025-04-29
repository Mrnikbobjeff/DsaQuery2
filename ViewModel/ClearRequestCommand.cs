using DsaQuery;
using System.Windows.Input;

namespace ViewModel
{
    public class ClearRequestCommand : ICommand
    {
        private readonly IQueryController queryController;

        public ClearRequestCommand(IQueryController queryController)
        {
            this.queryController = queryController;
            queryController.PropertyChanged += QueryController_PropertyChanged;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return string.IsNullOrEmpty(queryController.RequestMessage);
        }

        public void Execute(object? parameter)
        {
            queryController.ClearRequest();
        }

        private void QueryController_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == queryController.RequestMessage)
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}