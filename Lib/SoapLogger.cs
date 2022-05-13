using System.ServiceModel.Channels;

namespace Lib;

public class SoapLogger : ISoapLogger
{
    public void LogMessage<T>(string correlationId, T message) where T : Message
    {
        if (message == null) return;
        ConsoleColor.Yellow.WriteLine($"CorrelationId = {correlationId}");
        ConsoleColor.Green.WriteLine($"Message = {message}");
    }
}