using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ImageSave.Models;
using System.IO;

namespace ImageSave.Controllers
{
    public class HomeController : Controller
    {
        private jayaEntities db = new jayaEntities();
        public ActionResult Index()
        {
            GetImage();
            //ModelState.Clear();
            var data = db.Img_table.ToList();
            ViewBag.imgedata = data;
            return View(data);
        }
        [HttpGet]
        public ActionResult GetImage()

        {
           // Img_table img = new Img_table();
            var Imgdata = db.Img_table.ToList();
            return Json(new { data = Imgdata }, JsonRequestBehavior.AllowGet);
           // return View(img);
        }
        [HttpGet]
        public ActionResult addnewrecord()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult addnewrecord(Img_table imgModel, HttpPostedFileBase imgfile)
        {
            //try
            //{
            //var db = new jayaEntities();
            if (imgfile != null)
            {
                imgModel.picture = new byte[imgfile.ContentLength];
                imgfile.InputStream.Read(imgModel.picture, 0, imgfile.ContentLength);
                db.Img_table.Add(imgModel);
               int i=db.SaveChanges();
                if (i > 0)
                {
                    return Json(data: "Image Inserted Successfully", behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: "Image Not Inserted ", behavior: JsonRequestBehavior.AllowGet);
                }
            }
            ModelState.Clear();

            return View(imgModel);
        }
        public ActionResult Index1()
        {
            return View();
        }

       [HttpGet]
        public ActionResult Update(int id)
        {
            try
            {
                using (jayaEntities db = new jayaEntities())
                {

                    var emp = (from Employee in db.Img_table
                               where Employee.img_id == id
                               select Employee).FirstOrDefault();

                    return Json(emp, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                throw;
            }
        }
        public ActionResult Update(Img_table img ,HttpPostedFileBase imgfile)
        {
            int i = 0;
            if (imgfile != null)
            {
                img.picture = new byte[imgfile.ContentLength];
                imgfile.InputStream.Read(img.picture, 0, imgfile.ContentLength);
            }


                if (ModelState.IsValid)
                {
                    db.Entry(img).State = EntityState.Modified;

                i = db.SaveChanges();
                    return RedirectToAction("addnewrecord");
                }
                if (i > 0)
                {
                    return Json(data: "updated successfully", behavior: JsonRequestBehavior.AllowGet);
                }

                return View(img);
        }
    }
}






