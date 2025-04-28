using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DsaQuery
{
    public interface IQueryController : INotifyPropertyChanged
    {
        bool IsRequestAvailable { get; }
        string RequestMessage { get; set; }
        string Result { get; }
        string ServerID { get; set; }
        ObservableCollection<SimpleChannel> SimpleChannels { get; }

        void ClearRequest();

        void CreateRequest();

        void RequestChannels();
    }
}