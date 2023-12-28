using NEO_SYSTEM_TECHNOLOGY.Entity;

namespace NEO_SYSTEM_TECHNOLOGY.DAL
{
    public interface IOrganizationRepository : IDisposable
    {
        IEnumerable<Organization> GetAllOrganization();
        Organization GetOrganizationByID(int organizationId);
        void InsertOrganization(Organization organization);
        void UpdateOrganization(Organization organization);
        void DeleteOrganization(int organizationId);
        void Save();

    }
}
