using NEO_SYSTEM_TECHNOLOGY.Data;
using NEO_SYSTEM_TECHNOLOGY.Entity;

namespace NEO_SYSTEM_TECHNOLOGY.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();
        private GenericRepository<Employee> employeeRepository;
        private GenericRepository<Organization> organizationRepository;
        private GenericRepository<Dogovor> dogovorRepository;

        private bool disposed = false;

        public GenericRepository<Dogovor> DogovorRepository
        {
            get
            {
                if (dogovorRepository == null)
                {
                    dogovorRepository = new GenericRepository<Dogovor>(context);                   
                }
                return dogovorRepository;
            }
        }

        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {
                if (employeeRepository == null)
                {
                    employeeRepository = new GenericRepository<Employee>(context);
                }
                return employeeRepository;
            }
        }

        public GenericRepository<Organization> OrganizationRepository
        {
            get
            {
                if (organizationRepository == null)
                {
                    organizationRepository = new GenericRepository<Organization>(context);
                }
                return organizationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
