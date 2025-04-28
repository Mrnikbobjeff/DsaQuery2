using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ViewModel
{
    public class OuterViewModel
    {
        public string ServerID { get; set; } = "6454612084654254654";

        public string RequestMessage { get; set; } = "Fasse mir die Geschehnisse in Fasar zusammen";

        public ObservableCollection<ChannelViewModel> ChannelList { get; } =
            [
            new ChannelViewModel("ChannelNameA"),
            new ChannelViewModel("ChannelNameB"),
            new ChannelViewModel("ChannelNameC"),
            new ChannelViewModel("ChannelNameD"),
            ];

        public ICommand SendRequestCommand { get; } = new SendRequestCommand();
        public ICommand ClearRequestCommand { get; } = new ClearRequestCommand();

        public string Result { get; } =
            """
            Die Chat-Historie enthält *keine* detaillierten Beschreibungen von Ereignissen in der Stadt Fasar. Fasar wird lediglich indirekt im Zusammenhang mit der Suche nach Informationen über die Gor und den ehemaligen Akademieleiter Liscom erwähnt:

            *   **11. Rondra 1008 BF:** Während Kergil die Magierakademie in Khunchom besucht, erfährt er, dass Liscom, der ehemalige Akademieleiter in Fasar, in Ungnade gefallen und aus der Akademie vertrieben wurde. Es gibt Gerüchte, dass er nun mit Sklavenhändlern in der Gor zusammenarbeitet und Borbaradianer sein soll. Außerdem soll er von Spinnern aus Selem und einem Zwerg begleitet werden.

            Das ist die einzige Erwähnung von Fasar und Liscom in den bereitgestellten Daten. Um eine Zusammenfassung der Geschehnisse in Fasar zu geben, bräuchte ich weitere Informationen.
            """
            ;
    }
}