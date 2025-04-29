using System.Configuration;
using System.Windows;

using DsaQuery;
using ViewModel;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string? discordKey = ConfigurationManager.AppSettings["DiscordKey"];
            string? apiKey = ConfigurationManager.AppSettings["ApiKey"];
            if (discordKey == null || apiKey == null)
                throw new InvalidOperationException();

            QueryController queryController = new QueryController(discordKey, apiKey);
            DataContext = new OuterViewModel(queryController);
            InitializeComponent();
        }
    }
}