using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Complaints.Application.DTOs;

namespace Complaints.Application.Queries.GetComplaint;

public sealed record GetUserComplaintsQuery() : IQuery<IReadOnlyList<ComplaintListItem>>;