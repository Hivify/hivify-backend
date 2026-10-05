using AppTenant.Domain;
using SharedKernel.ValuesObjects;

namespace Association.Domain
{
    public class Membership
    {
        public AssociationID AssociationID { get; private set; }
        public UserID UserID { get; private set; }
        public AssociationRoles Role { get; private set; }
    }
}
