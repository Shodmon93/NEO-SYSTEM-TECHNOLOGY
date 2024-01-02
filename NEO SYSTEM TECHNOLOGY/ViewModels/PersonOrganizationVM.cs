using NEO_SYSTEM_TECHNOLOGY.Entity;
using System.ComponentModel.DataAnnotations;

namespace NEO_SYSTEM_TECHNOLOGY.ViewModels
{
    public class PersonOrganizationVM
    {
        [Display(Name = "Название Oрганизации")]
        [Required]
        public string OrganizationName { get; set; }

        [Display(Name = "Имя Заказчика")]
        [Required]
        public string CustFirstName { get; set; }

        [Display(Name = "Фамилия Заказчика")]
        [Required]
        public string CustLastName { get; set; }

        [Display(Name = "Телефон Заказчика")]
        [Required]
        public string CustPhoneNumber { get; set; }

        [Display(Name = "Почтовый адрес заказчика")]
        [Required]
        public string CustEmail { get; set; }


        public int OrganizationID { get; set; }
        public int PersonID { get; set; }
        public string CustFullName { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public IFormFile FormFile { get; set; }

        public string Title
        {
            get
            {
                return PersonID != 0 ? "Изменить заказчика" : "Добавить заказчика"; 
            }
        }




        public List<PersonOrganizationVM> GetPersonOrganizationList(IEnumerable<Organization> organization)
        {
            List<PersonOrganizationVM> result = new List<PersonOrganizationVM>();
            foreach (var entity in organization)
            {
                result.Add(new PersonOrganizationVM
                {
                    OrganizationID = entity.ID,
                    OrganizationName = entity.Name,
                    Employees = entity.Person
                });
            }
            return result;
        }

        public PersonOrganizationVM GetPersonOrganizationByID(Organization organization)
        {
            PersonOrganizationVM result = new PersonOrganizationVM
            {
                OrganizationName = organization.Name,
                OrganizationID = organization.ID,
                Employees = organization.Person
            };
            return result;
        }

        public Organization AddNewOrganization(PersonOrganizationVM newOrganization)
        {
            Organization organization = new Organization()
            {
                Name = newOrganization.OrganizationName,
                Person = new List<Employee>
                    {
                    new Employee
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

        public Organization EditOrganization(Organization organizationInDb, PersonOrganizationVM updatedOrganization)
        {
            organizationInDb.Name = updatedOrganization.OrganizationName;

            return organizationInDb;
        }

        public Employee AddNewEmployee(PersonOrganizationVM employeeToInsert, Organization organization)
        {
            Employee employee = new Employee
            {
                FirstName = employeeToInsert.CustFirstName,
                LastName = employeeToInsert.CustLastName,
                PhoneNumber = employeeToInsert.CustPhoneNumber,
                Email = employeeToInsert.CustEmail,
                Organization = organization
            };

            return employee;
        } 
        public void EditEmployee(Employee employee, Organization organization)
        {
            PersonID = employee.ID;
            CustFirstName = employee.FirstName;
            CustLastName = employee.LastName;
            CustPhoneNumber = employee.PhoneNumber;
            CustEmail = employee.Email;
            OrganizationID = organization.ID;
            OrganizationName = organization.Name;
        }

        public Employee EditEmployee(PersonOrganizationVM updatedEmployee, Employee employeeToUpdate)
        {
            employeeToUpdate.FirstName = updatedEmployee.CustFirstName;
            employeeToUpdate.LastName = updatedEmployee.CustLastName;
            employeeToUpdate.PhoneNumber = updatedEmployee.CustPhoneNumber;
            employeeToUpdate.Email = updatedEmployee.CustEmail;

            return employeeToUpdate;
        }
    }
}
