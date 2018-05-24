using DesafioWeb.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace DesafioWeb.Controllers
{
    public class DesafioController : Controller
    {

        public ActionResult MostrarResultados()
        {
            return View();
        }

        public ActionResult CargarDatos()
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


                    //Paging Size (10,20,50,100)    
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // 
                    var PaisesDatos = (from temppais in _context.Paises
                                        select temppais);

                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        PaisesDatos = PaisesDatos.OrderBy(sortColumn + " " + sortColumnDir);
                    }
                    //Search    
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        PaisesDatos = PaisesDatos.Where(m => m.Name == searchValue);
                    }

                    //total number of rows count     
                    recordsTotal = PaisesDatos.Count();
                    //Paging     
                    var data = PaisesDatos.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}