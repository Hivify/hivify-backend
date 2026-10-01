using Department.Application.Contracts;
using Department.Domain.Departments;
using Microsoft.EntityFrameworkCore;

namespace Department.Infrastructure.Persistence;

public sealed class DepartmentRepo : IDepartmentRepo
{
    private readonly DepartmentDbContext _dbContext;

    public DepartmentRepo(DepartmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DepartmentEntity?> GetByIdAsync(
        DepartmentID departmentId,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Departments
            .Include(a => a.StaffMembers)
            .FirstOrDefaultAsync(
                a => a.Id == departmentId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<DepartmentEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _dbContext.Departments
            .Include(a => a.StaffMembers)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        DepartmentEntity DepartmentEntity,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Departments.AddAsync(
            DepartmentEntity,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
