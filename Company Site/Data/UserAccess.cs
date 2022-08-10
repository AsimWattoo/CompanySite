using Company_Site.Enum;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class UserAccess
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The page to which access is given
        /// </summary>
        [Required]
        public UserPages Page { get; set; }
    }
}
