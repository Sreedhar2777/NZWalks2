using NZWalksApi.Models.Domain;

namespace NZWalksApi.Repository.Interfaces
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
    }
}
