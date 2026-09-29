using Association.Domain.Associations;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Commands.AddAssociation;

public sealed record AddAssociationCommand(string Name) : ICommand<AssociationID>;




