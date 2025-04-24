using Newtonsoft.Json;

namespace JsonSimplifier;

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
    private static void Main(string[] args)
    {
        var fileContent = File.ReadAllText(args[0]);
        var coordinate = JsonConvert.DeserializeObject<Coordinate>(fileContent);
        var messages = coordinate.Messages
            .Select(m => new SimpleMessage(m.Content, m.Author.Nickname, DateTimeOffset.Parse(m.Timestamp)))
            .ToList();
        File.WriteAllText(args[1], JsonConvert.SerializeObject(messages));
    }
}