using Company_Site.Data;
using Company_Site.DB;

namespace Company_Site.Services
{
    public class ApplicationUserManager
    {
        #region Private Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion

        #region Constuctor

        /// <summary>
        /// Default Constructor to initialize User Manager
        /// </summary>
        /// <param name="dbContext"></param>
        public ApplicationUserManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a list of users from the database
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        /// <summary>
        /// Adds user to the list
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void RemoveUser(int id)
        {
            _dbContext.Users.Remove(_dbContext.Users.Where(u => u.Id == id).First());
            _dbContext.SaveChanges();
        }

        #endregion

    }
}
