using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;

namespace NEO_SYSTEM_TECHNOLOGY.DAL
{
    public class OrganizationRepository : IOrganizationRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            this._context = context;            
        }

        public IEnumerable<Organization> GetAllOrganization()
        {
            return _context.Organizations.Include(p => p.Person).ToList();
        }

        public Organization GetOrganizationByID(int organizationId)
        {
            return _context.Organizations.Include(p => p.Person).SingleOrDefault(p => p.ID == organizationId);
        }

        public void InsertOrganization(Organization organization)
        {
            _context.Organizations.Add(organization);
        }

        public void UpdateOrganization(Organization organization)
        {
            _context.Entry(organization).State = EntityState.Modified;
        }

        public void DeleteOrganization(int organizationId)
        {
            Organization organization = _context.Organizations.Include(p => p.Person).SingleOrDefault(p => p.ID == organizationId);
            _context.Organizations.Remove(organization);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool disposed { get; set; }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }                
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
