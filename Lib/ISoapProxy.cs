namespace Lib;

public interface ISoapProxy
{
    Task<ICollection<Stadium>> GetStadiumsAsync(CancellationToken cancellationToken = default);
}