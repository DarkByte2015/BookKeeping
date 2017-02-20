using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

using BookKeeping.Models;

namespace BookKeeping.Controllers
{
    public class HomeController : Controller
    {
        private BKRepository repository = new BKRepository();

        [Route("")]
        public ActionResult Index()
        {
            ViewBag.Status = TempData["Status"];
            ViewBag.Message = TempData["Message"];
            return View();
        }

        [Route("list")]
        public ActionResult List()
        {
            var valuesFromDb = repository.GetAllModels<Платежное_Поручение>();
            var result = valuesFromDb.GroupBy(v => v.Номер).SelectMany(g => g.Select(v => new { HasDuplicates = g.Count() > 1, Value = v }.ToExpando()));
            ViewBag.Items = result;
            return View("List");
        }

        [Route("import")]
        public ActionResult Import()
        {
            return View("Import");
        }

        [Route("upload"), HttpPost]
        public ActionResult Upload(HttpPostedFileWrapper file)
        {
            try
            {
                var valuesFromFile = Parser1C.Parse(file.InputStream, new[] { typeof(Платежное_Поручение) }).OfType<Платежное_Поручение>().ToArray();

                if (valuesFromFile.Length == 0)
                    throw new ArgumentEmptyException("В файле не было элементов.");

                var valuesFromDb = repository.GetAllModels<Платежное_Поручение>().ToArray();

                var result = from value in valuesFromFile
                             let cf = valuesFromFile.Count(v => v.Номер == value.Номер)
                             let cdb = valuesFromDb.Count(v => v.Номер == value.Номер)
                             select new { HasDuplicates = cf + cdb > 1, Value = value }.ToExpando();

                ViewBag.Items = result.ToArray();                
                return View("Preview");
            }
            catch (Exception e)
            {                
                TempData["Status"] = "danger";
                TempData["Message"] = e.Message;
                return RedirectPermanent("/");
            }           
        }

        [Route("save"), HttpPost]
        public ActionResult Save()
        {
            var culture = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            culture.NumberFormat.NumberDecimalSeparator = ".";
            var objects = Request.Form.AsEnumerable(typeof(Платежное_Поручение), culture).Cast<Платежное_Поручение>();
            repository.AddMany(objects, new[] { "Id" });
            TempData["Status"] = "success";
            TempData["Message"] = "Данные успешно добавлены";
            return RedirectPermanent("/");
        }
    }
}
