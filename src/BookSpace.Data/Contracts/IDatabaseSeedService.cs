using System.Threading.Tasks;

namespace BookSpace.Data.Contracts
{
    public interface IDatabaseSeedService
    {

        Task SeedDataAsync();
    }
}
