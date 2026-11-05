using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Complaints.Application.Contracts;
using Complaints.Application.DTOs;


namespace Complaints.Application.Queries.GetComplaint
{
    public sealed class GetAllComplaintsQueryHandler : IQueryHandler<GetAllComplaintsQuery, IReadOnlyList<ComplaintListDto>>
    {
        private readonly IComplaintRepo _complaintRepository;

        public GetAllComplaintsQueryHandler(IComplaintRepo complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public async Task<IReadOnlyList<ComplaintListDto>> Handle(
            GetAllComplaintsQuery query,
            CancellationToken cancellationToken)
        {
            var complaints = await _complaintRepository.GetAllComplaintsAsync(cancellationToken);

            return complaints
                .OrderByDescending(c => c.CreatedDate)
                .Select(c => new ComplaintListDto(
                    c.Id.Value,
                    c.DepartmentId.Value,
                    c.Title.Value,
                    c.Description.Value,
                    c.Status,
                    c.CreatedDate,
                    c.ImageUrl))
                .ToList();
        }
    }
}
