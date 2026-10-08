using Houses.Domain.Tenants;
using SharedKernel;
using SharedKernel.ValuesObjects;

public class Tenant : BaseEntity<TenantID>
{
    public UserID UserId { get; private set; }
    public Name FullName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    private Tenant() { }

    private Tenant(TenantID id, UserID userId, Name fullName, Email email, PhoneNumber phoneNumber) : base(id)
    {
        UserId = userId;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
    }

    internal static Tenant Create(TenantID id, UserID userId, Name fullName, Email email, PhoneNumber phoneNumber)
    {
        return new Tenant(id, userId, fullName, email, phoneNumber);
    }

    public void Delete()
    {
        if (DeletedAt != null)
            return;

        DeletedAt = DateTime.UtcNow;
    }
}