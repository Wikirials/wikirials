using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wikirials.DAL;

namespace Wikirials.Controllers
{
    public class FileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /File/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }

        public ActionResult ShowPdf()
        {
            if (System.IO.File.Exists(Server.MapPath("~/")))
            {
                string pathSource = Server.MapPath("~/");
                FileStream fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read);
                

                return new FileStreamResult(fsSource, "application/pdf");
            }
            else
            {
                return RedirectToAction("Index", "Tutorial");
            }
        }
    }
}