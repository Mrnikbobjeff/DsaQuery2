using DsaQuery;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ViewModel
{
    public class OuterViewModel : INotifyPropertyChanged
    {
        private readonly IQueryController queryController;

        public OuterViewModel(IQueryController queryController)
        {
            RequestChannelCommand = new RequestChannelCommand(queryController);
            ClearRequestCommand = new ClearRequestCommand(queryController);
            SendRequestCommand = new SendRequestCommand(queryController);
            this.queryController = queryController;
            queryController.PropertyChanged += QueryController_PropertyChanged;
            queryController.SimpleChannels.CollectionChanged += SimpleChannels_CollectionChanged;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ChannelViewModel> ChannelList { get; } =
            [

            ];

        public ICommand ClearRequestCommand { get; }

        public ICommand RequestChannelCommand { get; }

        public string RequestMessage
        {
            get
            {
                return queryController.RequestMessage;
            }
            set
            {
                if (value != queryController.RequestMessage)
                {
                    queryController.RequestMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Result => queryController.Result;

        public ICommand SendRequestCommand { get; }

        public string ServerID
        {
            get
            {
                return queryController.ServerID;
            }
            set
            {
                if (value != queryController.ServerID)
                {
                    queryController.ServerID = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void QueryController_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender != queryController)
                throw new Exception();

            if (e.PropertyName == nameof(queryController.ServerID))
                OnPropertyChanged(nameof(ServerID));
            if (e.PropertyName == nameof(queryController.Result))
                OnPropertyChanged(nameof(Result));
        }

        private void SimpleChannels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add && e.NewItems.Count == 1)
                ChannelList.Add(new ChannelViewModel((SimpleChannel)e.NewItems[0]));
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                ChannelList.Clear();
        }
    }
}