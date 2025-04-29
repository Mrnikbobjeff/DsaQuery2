using DsaQuery;
using System.Windows.Input;

namespace ViewModel
{
    public class SendRequestCommand : ICommand
    {
        private readonly IQueryController queryController;

        public SendRequestCommand(IQueryController queryController)
        {
            queryController.PropertyChanged += QueryController_PropertyChanged;
            this.queryController = queryController;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return queryController.IsRequestAvailable;
        }

        public void Execute(object? parameter)
        {
            queryController.CreateRequest();
        }

        private void QueryController_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(queryController.IsRequestAvailable))
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}