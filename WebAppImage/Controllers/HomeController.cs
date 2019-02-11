using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppImage.Models;

namespace WebAppImage.Controllers
{
    public class HomeController : Controller
    {
        imageListEntities db = new imageListEntities();
        public ActionResult Index()
        {
            var list = db.mYimages.ToList(); 
            return View(list);
        }
        [HttpGet]
        public ActionResult Create(int id=0)
        {
            mYimage tb = new mYimage();
            if (id > 0)
            {
                tb = db.mYimages.Find(id);
                
            }
            TempData["tmp"] = tb.Image;
            return View(tb);
        }
        [HttpPost]
        public ActionResult Create(mYimage m, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
                if(img!=null)
                {
                    img.SaveAs(HttpContext.Server.MapPath("~/Uploads/") + img.FileName);
                    m.Image = img.FileName;
                }
            else if(img==null)
                {
                    if (TempData["tmp"] == null)
                    {
                        m.Image = "default.jpg";
                    }
                    else
                        m.Image = TempData["tmp"].ToString();
                }
            if(m.imageId>0)
            {
                db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                db.mYimages.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}