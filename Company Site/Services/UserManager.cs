using Company_Site.Data;

namespace Company_Site.Services
{
    public class UserManager
    {

        #region Private Members

        /// <summary>
        /// The static list of users
        /// TODO: Remove this and replace with database
        /// </summary>
        private List<User> Users = new List<User>()
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
            Access = "All",
            Mobile = "0355968475122"
            },
            new User(){ Id = 2, FirstName = "Muhammad", LastName="Asim", Designation="CFO", Department="Finance",
            Email="asimwattoo@gmail.com",
            Password = "asimwattoo123",
            Role="Business Head",
            DateOfJoining = DateTime.Now.AddDays(-100),
            DOB=new DateTime(2003, 03, 13),
            LastLogin = DateTime.Now,
            OfficeLocation = "Office",
            Status = "On Vacation",
            Access = "All",
            Mobile = "0355968475122"
            }
        };

        #endregion

        /// <summary>
        /// Returns a list of users from the database
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            //TODO: Get users from the database
            return Users;
        }

        /// <summary>
        /// Adds user to the list
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveUser(int id)
        {
            Users.Remove(Users.Where(u => u.Id == id).First());
        }

    }
}
