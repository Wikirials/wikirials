using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wikirials.DAL;

namespace Wikirials.Controllers
{
    public class FileMainController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /FileMain/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.FileMains.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}