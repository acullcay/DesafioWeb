using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesafioWeb.Models
{
    [Table("Paises")]
    public class PaisesModels
    {
        [Key]
        public int? ID { get; set; }

        [Required(ErrorMessage = "Requiere Nombre Corto")]
        public string PaisID { get; set; }

        [Required(ErrorMessage = "Requiere nombre")]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> Paises { get; set; }
    }
}