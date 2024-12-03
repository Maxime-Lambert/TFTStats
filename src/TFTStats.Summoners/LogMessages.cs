namespace TFTStats.Summoners;

internal static partial class LogMessages
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Just published event with name : {Message}")]
    public static partial void LogRabbitMQPublished(this ILogger logger, string Message);

    [LoggerMessage(Level = LogLevel.Information, Message = "Just consumed event with name : {Message}")]
    public static partial void LogRabbitMQConsumed(this ILogger logger, string Message);
}
