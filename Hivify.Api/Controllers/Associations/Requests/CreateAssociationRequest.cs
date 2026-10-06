namespace Hivify.Api.Controllers.Associations.Requests;

public sealed record CreateAssociationRequest(
    string Name,
    string Identifier);