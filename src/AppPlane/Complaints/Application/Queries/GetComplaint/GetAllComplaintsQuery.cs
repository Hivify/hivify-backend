using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Complaints.Application.DTOs;


namespace Complaints.Application.Queries.GetComplaint
{
    public sealed record GetAllComplaintsQuery() : IQuery<IReadOnlyList<ComplaintListItem>>;
}
