using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company_Site.Base
{
    public class BaseModel<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
