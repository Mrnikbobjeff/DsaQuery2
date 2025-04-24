using System.Diagnostics;
using System.Reflection;
using GenerativeAI;
using GenerativeAI.Types;
using Newtonsoft.Json;

namespace DsaQuery;

public class Coordinate
{
    [JsonProperty("channel")] public Channel Channel { get; set; }

    [JsonProperty("dateRange")] public DateRange DateRange { get; set; }

    [JsonProperty("exportedAt")] public string ExportedAt { get; set; }

    [JsonProperty("guild")] public Guild Guild { get; set; }

    [JsonProperty("messageCount")] public double MessageCount { get; set; }

    [JsonProperty("messages")] public Message[] Messages { get; set; }
}

public class Channel
{
    [JsonProperty("category")] public string Category { get; set; }

    [JsonProperty("categoryId")] public string CategoryId { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("topic")] public object Topic { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}

public class DateRange
{
    [JsonProperty("after")] public object After { get; set; }

    [JsonProperty("before")] public object Before { get; set; }
}

public class Guild
{
    [JsonProperty("iconUrl")] public string IconUrl { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }
}

public class Message
{
    [JsonProperty("attachments")] public object[] Attachments { get; set; }

    [JsonProperty("author")] public Author Author { get; set; }

    [JsonProperty("callEndedTimestamp")] public object CallEndedTimestamp { get; set; }

    [JsonProperty("content")] public string Content { get; set; }

    [JsonProperty("embeds")] public object[] Embeds { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("inlineEmojis")] public InlineEmoji[] InlineEmojis { get; set; }

    [JsonProperty("isPinned")] public bool IsPinned { get; set; }

    [JsonProperty("mentions")] public Mention[] Mentions { get; set; }

    [JsonProperty("reactions")] public Reaction[] Reactions { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public Reference Reference { get; set; }

    [JsonProperty("stickers")] public object[] Stickers { get; set; }

    [JsonProperty("timestamp")] public string Timestamp { get; set; }

    [JsonProperty("timestampEdited", NullValueHandling = NullValueHandling.Ignore)]
    public string TimestampEdited { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}

public class Author
{
    [JsonProperty("avatarUrl")] public string AvatarUrl { get; set; }

    [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
    public string Color { get; set; }

    [JsonProperty("discriminator")] public string Discriminator { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("isBot")] public bool IsBot { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("nickname")] public string Nickname { get; set; }

    [JsonProperty("roles")] public AuthorRole[] Roles { get; set; }
}

public class AuthorRole
{
    [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
    public string Color { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("position")] public double Position { get; set; }
}

public class InlineEmoji
{
    [JsonProperty("code")] public string Code { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("imageUrl")] public string ImageUrl { get; set; }

    [JsonProperty("isAnimated")] public bool IsAnimated { get; set; }

    [JsonProperty("name")] public string Name { get; set; }
}

public class Mention
{
    [JsonProperty("avatarUrl")] public string AvatarUrl { get; set; }

    [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
    public string Color { get; set; }

    [JsonProperty("discriminator")] public string Discriminator { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("isBot")] public bool IsBot { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("nickname")] public string Nickname { get; set; }

    [JsonProperty("roles")] public MentionRole[] Roles { get; set; }
}

public class MentionRole
{
    [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
    public string Color { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("position")] public double Position { get; set; }
}

public class Reaction
{
    [JsonProperty("count")] public double Count { get; set; }

    [JsonProperty("emoji")] public Emoji Emoji { get; set; }

    [JsonProperty("users")] public User[] Users { get; set; }
}

public class Emoji
{
    [JsonProperty("code")] public string Code { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("imageUrl")] public string ImageUrl { get; set; }

    [JsonProperty("isAnimated")] public bool IsAnimated { get; set; }

    [JsonProperty("name")] public string Name { get; set; }
}

public class User
{
    [JsonProperty("avatarUrl")] public string AvatarUrl { get; set; }

    [JsonProperty("color", NullValueHandling = NullValueHandling.Ignore)]
    public string Color { get; set; }

    [JsonProperty("discriminator")] public string Discriminator { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("isBot")] public bool IsBot { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("nickname")] public string Nickname { get; set; }
}

public class Reference
{
    [JsonProperty("channelId")] public string ChannelId { get; set; }

    [JsonProperty("guildId")] public string GuildId { get; set; }

    [JsonProperty("messageId")] public string MessageId { get; set; }
}

public record class SimpleMessage(string Message, string Author, DateTimeOffset PostedAt);

internal class Program
{
    string discordKey = "<key>"
    private static async Task<int> Main(string[] args)
    {
        string serverId = null;
        if (args.Length >= 1)
        {
            serverId = args[0];
        }
        else
        {
            Console.WriteLine("Gib die Server id ein");
            serverId = Console.ReadLine();
        }

        var p = Process.Start(".\\Ressources/DiscordChatExporter.Cli.exe",
            $"channels -g {serverId} -t {key}");
        await p.WaitForExitAsync();

        Console.WriteLine("Gebe eine der folgenden Channel ids ein:");
        var channelId = Console.ReadLine();
        if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                $"{channelId}.json")))
        {
            var p2 = Process.Start(".\\Ressources/DiscordChatExporter.Cli.exe",
                $" export -f Json -o {channelId}.json -t {key} -c {channelId}");
            await p2.WaitForExitAsync();
        }

        var fileContent =
            File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                $"{channelId}.json"));
        var coordinate = JsonConvert.DeserializeObject<Coordinate>(fileContent);
        var messages = coordinate.Messages
            .Select(m => new SimpleMessage(m.Content, m.Author.Nickname, DateTimeOffset.Parse(m.Timestamp)))
            .ToList();
        var smallOutput = JsonConvert.SerializeObject(messages);
        string? apiKey = null;
        if (args.Length > 0)
        {
            apiKey = args[1];
        }
        else
        {
            Console.WriteLine("Gib deinen gemini api key ein");
            apiKey = Console.ReadLine();
        }

        var googleAI = new GoogleAi(apiKey);
        var generativeModel = googleAI.CreateGenerativeModel("models/gemini-2.0-flash");
        var chatSession = generativeModel.StartChat();
        chatSession.SystemInstruction = "ONLY answer in german";
        var request = new GenerateContentRequest();
        request.AddText(
            "In future queries, answer detailed questions about the provided document which represents a chat history in json format: " +
            smallOutput);
        GenerateContentResponse response = null;
        try
        {
            response = await chatSession.GenerateContentAsync(request);
        }
        catch (Exception)
        {
            Console.WriteLine("Einfach programm neu starten, hab keine retry logik");
            return -1;
        }

        // Continue the conversation
        while (true)
            try
            {
                Console.WriteLine("Stell deine Frage");
                var line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;
                var secondResponse = await chatSession.GenerateContentAsync(line);
                Console.WriteLine(secondResponse.Text());
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error: " + ex.Message);
            }

        return 0;
    }
}