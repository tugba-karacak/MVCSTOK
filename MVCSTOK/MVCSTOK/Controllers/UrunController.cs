using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVCSTOK.Models.ENTITY;

namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        mvcdbstokEntities db = new mvcdbstokEntities();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrünler()
        {
           
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                select new SelectListItem
                {
                    Text= i.KATEGORIAD,
            Value= i.KATEGORIID.ToString()
        }).ToList();
            ViewBag.dgr = degerler;
            
            return View();
    }
    [HttpPost]
    public ActionResult YeniUrünler(TBLURUNLER p1)
    {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER = ktg;
        db.TBLURUNLER.Add(p1);
        db.SaveChanges();
            return RedirectToAction("Index");
    }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);

        }
        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urn = db.TBLURUNLER.Find(p1.URUNID);
            urn.URUNAD = p1.URUNAD;
            urn.MARKA = p1.MARKA;
            //  urn.URUNKATEGORI = p1.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI = ktg.KATEGORIID;
            urn.FIYAT = p1.FIYAT;
            urn.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
}
}