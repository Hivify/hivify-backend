using SharedKernel.ValuesObjects;


namespace Association.Domain
{
    public class Membership
    {
        public AssociationID AssociationID { get; private set; }
        public UserID UserID { get; private set; }
        public AssociationRoles Role { get; private set; }

        private Membership()
        {
        }

        private Membership(
            AssociationID associationId,
            UserID userId,
            AssociationRoles role)
        {
            AssociationID = associationId;
            UserID = userId;
            Role = role;
        }

        public static Membership Create(
            AssociationID associationId,
            UserID userId,
            AssociationRoles role)
        {
            return new Membership(
                associationId,
                userId,
                role);
        }
    }
}

