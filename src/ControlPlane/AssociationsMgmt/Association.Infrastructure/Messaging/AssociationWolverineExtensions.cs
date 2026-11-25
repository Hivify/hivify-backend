
using Association.Infrastructure.Persistence;
using Wolverine;
using Wolverine.Persistence.Durability;
using Wolverine.SqlServer;

namespace Association.Infrastructure.Messaging;

public static class AssociationWolverineExtensions
{
    public static void AddAssociationPersistence(
        this WolverineOptions opts,
        string connectionString)
    {
        opts.PersistMessagesWithSqlServer(
            connectionString,
            "association_wolverine",
            MessageStoreRole.Ancillary)
            .Enroll<AssociationDbContext>();
    }
}
