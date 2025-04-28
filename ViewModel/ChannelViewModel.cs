namespace ViewModel
{
    public class ChannelViewModel(string name)
    {
        public string Name { get; } = name;
        public bool IsSelected { get; set; } = false;
    }
}
