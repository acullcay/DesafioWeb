using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DesafioWeb.Models
{
    public class ConexionModels : DbContext
    {
        public ConexionModels() : base("DBConnection")  
        {

        }
        public DbSet<PaisesModels> Paises { get; set; }

        public DbSet<EstadisticasModels> Estadisticas { get; set; }
    }
}