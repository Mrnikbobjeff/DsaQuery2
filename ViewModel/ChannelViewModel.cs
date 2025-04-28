using DsaQuery;

namespace ViewModel
{
    public class ChannelViewModel(SimpleChannel simpleChannel)
    {
        public bool IsSelected
        {
            get
            {
                return simpleChannel.IsSelected;
            }
            set
            {
                simpleChannel.IsSelected = value;
            }
        }

        public string Name => simpleChannel.Name;
    }
}