using Houses.Domain.Houses;

namespace Houses.Application.Contracts
{
    public interface IHouseRepo
    {
        Task<House?> GetByIdAsync(HouseID id, CancellationToken cancellationToken = default);
        Task<IEnumerable<House>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(House house, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
