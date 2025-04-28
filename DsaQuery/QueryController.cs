using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using GenerativeAI;
using GenerativeAI.Types;
using System.Reflection;
using Newtonsoft.Json;
using System.Text;

namespace DsaQuery
{
    public class QueryController : IQueryController
    {
        private readonly string discordKey;
        private GenerativeModel generativeModel;
        private string requestMessage = string.Empty;
        private string serverId = string.Empty;

        public QueryController(string discordKey, string apiKey)
        {
            this.discordKey = discordKey;
            GoogleAi googleAI = new GoogleAi(apiKey);
            generativeModel = googleAI.CreateGenerativeModel("models/gemini-2.0-flash");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsRequestAvailable
        {
            get
            {
                bool isRequestAvailable = true;
                if (string.IsNullOrEmpty(RequestMessage))
                    isRequestAvailable = false;

                return isRequestAvailable;
            }
        }

        public string RequestMessage
        {
            get
            {
                return requestMessage;
            }

            set
            {
                if (requestMessage == value)
                    return;

                requestMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsRequestAvailable));
            }
        }

        public string Result { get; private set; } = string.Empty;

        public string ServerID
        {
            get => serverId;
            set
            {
                if (serverId != value)
                {
                    serverId = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<SimpleChannel> SimpleChannels { get; } = [];

        public void ClearRequest()
        {
            RequestMessage = string.Empty;
        }

        public async void CreateRequest()
        {
            var chatSession = generativeModel.StartChat();
            chatSession.SystemInstruction = "ONLY answer in german";
            var request = new GenerateContentRequest();

            string[] channelIds = SimpleChannels.Where(x => x.IsSelected).Select(x => x.ID).ToArray();

            var channelContent = GetChannelInfo(channelIds);
            request.AddText("In future queries, answer detailed questions about the provided document which represents a chat history in json format: " + channelContent);
            GenerateContentResponse response = null;
            try
            {
                await chatSession.GenerateContentAsync(request);
                response = await chatSession.GenerateContentAsync(RequestMessage);

                var r = response.Text();
                Result = r ??= "";
            }
            catch (Exception e)
            {
                Result = "Fehlerhafter Request:" + Environment.NewLine + e.Message;
            }
            finally
            {
                OnPropertyChanged(nameof(Result));
            }
        }

        public async void RequestChannels()
        {
            if (serverId == null)
                throw new InvalidOperationException("ServerID must be set");

            string output = await RunDiscordChatExporter($"channels -g {serverId} -t {discordKey}");

            string[] channels = output.Split(Environment.NewLine);
            List<SimpleChannel> simpleChannels = channels.Where(IsValidChannelString).Select(x => new SimpleChannel(x)).ToList();
            SimpleChannels.Clear();
            simpleChannels.ForEach(SimpleChannels.Add);

            OnPropertyChanged(nameof(SimpleChannels));
        }

        private string GetChannelInfo(params string[] channelIds)
        {
            if (!channelIds.Any())
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var channelId in channelIds)
            {
                string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                       $"{channelId}.json");
                if (!File.Exists(fileName))
                {
                    using Process process = new Process();
                    process.StartInfo.FileName = ".\\Ressources/DiscordChatExporter.Cli.exe ";
                    process.StartInfo.Arguments = $" export -f Json -o {channelId}.json -t {discordKey} -c {channelId}";
                    process.Start();
                    process.WaitForExit();
                }
                var fileContent = File.ReadAllText(fileName);
                var coordinate = JsonConvert.DeserializeObject<Coordinate>(fileContent);
                var messages = coordinate.Messages
                    .Select(m => new SimpleMessage(m.Content, m.Author.Nickname, DateTimeOffset.Parse(m.Timestamp)))
                    .ToList();
                var smallOutput = JsonConvert.SerializeObject(messages);
                sb.AppendLine(smallOutput);
            }

            return sb.ToString();
        }

        private bool IsValidChannelString(string channelName)
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(channelName))
                return false;

            string[] splitedChannel = channelName.Split('|');
            if (splitedChannel.Length != 2)
                return false;

            if (splitedChannel[1].ToLower().Contains("voice"))
                return false;

            if (splitedChannel[1].ToLower().Contains("meister"))
                return false;

            return isValid;
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async Task<string> RunDiscordChatExporter(string args)
        {
            string output = string.Empty;
            using (Process process = new Process())
            {
                process.StartInfo.FileName = ".\\Ressources/DiscordChatExporter.Cli.exe ";
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                output = process.StandardOutput.ReadToEnd();
                await process.WaitForExitAsync();
            }
            return output;
        }
    }

    public class SimpleChannel
    {
        public SimpleChannel(string channelName)
        {
            var t = channelName.Split('|');
            ID = t[0].Trim();
            Name = t[1].Trim();
        }

        public string ID { get; }
        public bool IsSelected { get; set; } = false;
        public string Name { get; }
    }
}