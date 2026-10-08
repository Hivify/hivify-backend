using Complaints.Domain;
using SharedKernel.ValuesObjects;

namespace Complaints.Application.Contracts;

public interface IComplaintRepo
{
    // =====================
    // Commands
    // =====================

    Task CreateComplaintAsync(
        Complaint complaint,
        CancellationToken cancellationToken = default);

    Task UpdateComplaintAsync(
        Complaint complaint,
        CancellationToken cancellationToken = default);

    Task DeleteComplaintByIdAsync(
        ComplaintID id,
        CancellationToken cancellationToken = default);

    // =====================
    // Queries
    // =====================

    Task<IEnumerable<Complaint>> GetAllComplaintsAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Complaint>> GetComplaintsByUserAsync(
        UserID userId,
        CancellationToken cancellationToken = default);

    Task<Complaint?> GetComplaintByIdAsync(
        ComplaintID id,
        CancellationToken cancellationToken = default);
}