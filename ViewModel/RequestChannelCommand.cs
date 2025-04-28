using System.ComponentModel;
using System.Windows.Input;
using DsaQuery;

namespace ViewModel
{
    public class RequestChannelCommand : ICommand

    {
        private readonly IQueryController queryController;

        public RequestChannelCommand(IQueryController queryController)
        {
            this.queryController = queryController;
            queryController.PropertyChanged += QueryController_PropertyChanged;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(queryController.ServerID);
        }

        public void Execute(object? parameter)
        {
            queryController.RequestChannels();
        }

        private void QueryController_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender == queryController && e.PropertyName == nameof(queryController.ServerID))
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}