using System.ServiceModel.Channels;

namespace Lib;

public interface ISoapLogger
{
    void LogMessage<T>(string correlationId, T message) where T : Message;
}