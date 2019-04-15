using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Adi { get; set; }
        [Required]
        public string KisaAciklama { get; set; }
        [Required]
        public string Aciklama { get; set; }
        public int? ResimId { get; set; }
        [Required]
        public DateTime OlusturulmaTarihi { get; set; }
        public string Slug { get; set; }
        public string Tags { get; set; }
    }
}
