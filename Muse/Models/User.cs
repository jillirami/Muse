using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muse.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Zip { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        public virtual ICollection<Musing> Musings { get; set; }
    }
}
