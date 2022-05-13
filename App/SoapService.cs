using Lib;

namespace App;

public interface ISoapService
{
    Task PrintStadiumsAsync(CancellationToken cancellationToken = default);
}

public class SoapService : ISoapService
{
    private readonly ISoapProxy _proxy;

    public SoapService(ISoapProxy proxy)
    {
        _proxy = proxy ?? throw new ArgumentNullException(nameof(proxy));
    }

    public async Task PrintStadiumsAsync(CancellationToken cancellationToken)
    {
        var stadiums = await _proxy.GetStadiumsAsync(cancellationToken);
        ConsoleColor.Cyan.WriteLine($"Found {stadiums.Count} stadium(s)");
    }
}