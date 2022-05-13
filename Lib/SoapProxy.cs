namespace Lib;

public class SoapProxy : ISoapProxy
{
    private readonly ISoapClient _soapClient;

    public SoapProxy(ISoapClient soapClient)
    {
        _soapClient = soapClient ?? throw new ArgumentNullException(nameof(soapClient));
    }

    public async Task<ICollection<Stadium>> GetStadiumsAsync(CancellationToken cancellationToken = default)
    {
        var infos = await _soapClient.AllStadiumInfoAsync().WaitAsync(cancellationToken);
        var stadiums = infos.Select(x => new Stadium
        {
            Name = x.sName,
            City = x.sCityName,
            Capacity = x.iSeatsCapacity
        }).ToList();
        return stadiums;
    }
}