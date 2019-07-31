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

        public string Aspirations { get; set; }

        public double Sentiment { get; set; }

        public virtual User User { get; set; }

        public string getSentimentColor
        {
            get
            {
                string color = "#fc0303";
                if (this.Sentiment >= 0.05)
                {
                    // Positive
                    color = "#032cfc";
                }
                else if (this.Sentiment > -0.05 && this.Sentiment < 0.05)
                {
                    // Neutral
                    color = "#d9d9d9";
                }

                return color;
            }
        }
    }
}
