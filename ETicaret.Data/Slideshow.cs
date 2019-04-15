using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data
{
    [Table("Slideshow")]
    public class Slideshow
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ResimId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string ButonAdi { get; set; }
        public string ButonLink { get; set; }
    }
}
