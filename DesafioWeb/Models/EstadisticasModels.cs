using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesafioWeb.Models
{
    [Table("Estadisticas")]
    public class EstadisticasModels
    {
        [Key]
        public int? ID { get; set; }
        
        public string PaisID { get; set; }
        
        public string PaisNombre { get; set; }

        public int Fecha { get; set; }

        public int Indicador { get; set; }

    }
}