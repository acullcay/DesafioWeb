using DesafioWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesafioWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region Model
            var model = new PaisesModels();
            using (ConexionModels cshparpEntity = new ConexionModels())
            {
                var dbData = cshparpEntity.Paises.ToList();
                model.Paises = GetSelectListItems(dbData);
            }

            return View(model);
            #endregion
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<PaisesModels> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.PaisID.ToString(),
                    Text = element.Name
                });
            }
            return selectList;
        }

        public ActionResult ConsultarEstadisticas()
        {
            try
            {
                using (ConexionModels _context = new ConexionModels())
                {
                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                    int FechaInicio = 0;
                    int FechaFin = 0;
                    var PaisID = Request.Form.GetValues("PaisID").FirstOrDefault();

                    if (Request.Form.GetValues("fecha_inicio").FirstOrDefault() != "")
                    {
                        FechaInicio = Convert.ToInt16(Request.Form.GetValues("fecha_inicio").FirstOrDefault().Substring(6, 4));
                    }

                    if (Request.Form.GetValues("fecha_fin").FirstOrDefault() != "")
                    {
                        FechaFin = Convert.ToInt16(Request.Form.GetValues("fecha_fin").FirstOrDefault().Substring(6, 4));
                    }

                    if (Request.Form.GetValues("fecha_fin").FirstOrDefault() != "")
                    {
                        FechaFin = Convert.ToInt16(Request.Form.GetValues("fecha_fin").FirstOrDefault().Substring(6, 4));
                    }

                    PaisID = PaisID.Trim();

                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    var EstadisticasDatos = (from tempestadisticas in _context.Estadisticas
                                             select tempestadisticas);

                    if (FechaInicio != 0 && FechaFin != 0)
                    {
                        EstadisticasDatos = (from tmp in _context.Estadisticas
                                                 where tmp.Fecha >= FechaInicio && tmp.Fecha <= FechaFin
                                                 select tmp);
                    }

                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        EstadisticasDatos = EstadisticasDatos.OrderBy(m => m.PaisNombre);
                    }

                    //Search
                    if (PaisID != "")
                    {
                        EstadisticasDatos = EstadisticasDatos.Where(m => m.PaisID == PaisID);
                    }

                    //total number of rows count     
                    recordsTotal = EstadisticasDatos.Count();
                    //Paging     
                    var data = EstadisticasDatos.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hola mundo desde ABOUT";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Hola Mundo desde CONTACT";

            return View();
        }
    }
}