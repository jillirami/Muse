using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Muse.Models
{
    public class Musing
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.Now;

        public int SUDS { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Entry { get; set; }

        //public string Aspirations { get; set; }

        public virtual User User { get; set; }
    }
}
