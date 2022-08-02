using Company_Site.Data;

namespace Company_Site.Services
{
    public class UserManager
    {

        /// <summary>
        /// Returns a list of users from the database
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            //TODO: Get users from the database
            return new List<User>()
            {
                new User(){ Id = 1, FirstName = "Muhammad", LastName="Asim", Designation="CEO", Department="Finance",
                Email="asimwattoo6@gmail.com",
                Password = "asimwattoo123",
                Role="Business Head",
                DateOfJoining = DateTime.Now.AddDays(-100),
                DOB=new DateTime(2003, 03, 13),
                LastLogin = DateTime.Now,
                OfficeLocation = "Office",
                Status = "Active",
                Access = "All"
                },
                new User(){ Id = 2, FirstName = "Muhammad", LastName="Asim", Designation="CEO", Department="Finance",
                Email="asimwattoo6@gmail.com",
                Password = "asimwattoo123",
                Role="Business Head",
                DateOfJoining = DateTime.Now.AddDays(-100),
                DOB=new DateTime(2003, 03, 13),
                LastLogin = DateTime.Now,
                OfficeLocation = "Office",
                Status = "Active",
                Access = "All"
                }
            };
        }

    }
}
