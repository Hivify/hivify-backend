using SharedKernel;

namespace Houses.Domain.Houses
{
    public sealed record Facility : BaseValue<string>
    {
        public Facility(string value) : base(value)
        {
        }

    }
}
