using Complaints.Application.Contracts;
using Complaints.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects;

namespace Complaints.Infrastructure.Persistence;

public class ComplaintRepo : IComplaintRepo
{
    private readonly ComplaintDbContext _context;

    public ComplaintRepo(ComplaintDbContext context)
    {
        _context = context;
    }

    // =====================
    // Commands
    // =====================

    public async Task CreateComplaintAsync(Complaint complaint, CancellationToken cancellationToken = default)
    {
        _context.Complaints.Add(complaint);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateComplaintAsync(Complaint complaint, CancellationToken cancellationToken = default)
    {
        _context.Complaints.Update(complaint);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteComplaintByIdAsync(ComplaintID id, CancellationToken cancellationToken = default)
    {
        var complaint = await GetComplaintByIdAsync(id, cancellationToken);
        if (complaint != null)
        {
            _context.Complaints.Remove(complaint);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    // =====================
    // Queries
    // =====================

    public async Task<IEnumerable<Complaint>> GetAllComplaintsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Complaints
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Complaint>> GetComplaintsByUserAsync(UserID userId, CancellationToken cancellationToken = default)
    {
        return await _context.Complaints
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Complaint?> GetComplaintByIdAsync(ComplaintID id, CancellationToken cancellationToken = default)
    {
        return await _context.Complaints
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}