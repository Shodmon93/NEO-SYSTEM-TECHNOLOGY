using NEO_SYSTEM_TECHNOLOGY.Entity;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class PersonOrganizationVM
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public int PersonID { get; set; }
        public string CustFirstName { get; set; }
        public string CustLastName { get; set; }
        public string CustFullName { get; set; }
        public string CustPhoneNumber { get; set; }
        public string CustEmail { get; set; }
        public ICollection<Person> People { get; set; }
        public IFormFile formFile { get; set; }


        public List<PersonOrganizationVM> GetPersonOrganizationList(List<Organization> organization)
        {
            List<PersonOrganizationVM> result = new List<PersonOrganizationVM>();
            foreach (var item in organization)
            {
                result.Add(new PersonOrganizationVM
                {
                    OrganizationID = item.ID,
                    OrganizationName = item.Name,
                    People = item.Person
                });
            }
            return result;
        }

        public PersonOrganizationVM GetPersonOrganizationByID(Organization organization)
        {
            PersonOrganizationVM result = new PersonOrganizationVM
            {
                OrganizationName = organization.Name,
                People = organization.Person
            };
            return result;
        }

        public Organization AddNewOrganization(PersonOrganizationVM newOrganization)
        {
            Organization organization = new Organization()
            {
                Name = newOrganization.OrganizationName,
                Person = new List<Person>
                    {
                    new Person
                        {
                        FirstName = newOrganization.CustFirstName,
                        LastName = newOrganization.CustLastName,
                        PhoneNumber = newOrganization.CustPhoneNumber,
                        Email = newOrganization.CustEmail,
                    }
                }
            };
            return organization;
        }

        public Organization EditOrganization(Organization organization)
        {
            return organization;
        }
    }
}
