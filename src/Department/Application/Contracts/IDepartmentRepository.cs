using Department.Domain.Departments;

namespace Department.Application.Contracts
{
    public interface IDepartmentRepo
    {
        Task<DepartmentEntity?> GetByIdAsync(DepartmentID id, CancellationToken cancellationToken);
        Task<IReadOnlyList<DepartmentEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(DepartmentEntity DepartmentEntity, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
