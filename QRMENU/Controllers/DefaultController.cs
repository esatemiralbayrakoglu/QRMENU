using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using QRMENU.Models;
using QRMENU.ViewModels;

namespace QRMENU.Controllers
{
    public class DefaultController : Controller
    {
        sirketEntities1 db = new sirketEntities1();
        public ActionResult Index()
        {
            var grubu = Request.QueryString["grubu"];
            var stoklar = db.stok_ekranlar.Join(
                  db.stok,
                  stok_ekran => stok_ekran.stok_id,
                  stok_tbl => stok_tbl.id,
                  (stok_ekran, stok_tbl) =>
                  new

                  { stok = stok_tbl, stok_ekranlar = stok_ekran }

                  ).Join(db.stok_fiyatlar,
                  x => x.stok.id,
                  (y => y.stok_id),
                  (x, y) => new { x.stok, x.stok_ekranlar, y }
                  )
                  .Join(db.stok_birimler,
                  stok_birim => stok_birim.stok.temel_birim_id,
                  stok_birim_tbl => stok_birim_tbl.id,
                  (stok_birim, stok_birim_tbl) =>
                  new
                  MenuViewModel
                  { stok = stok_birim.stok, stok_ekranlar = stok_birim.stok_ekranlar, stok_fiyatlar = stok_birim.y, stok_birimler = stok_birim_tbl }
                  ).Where(x => x.stok.silindi == 0 && x.stok.grubu == grubu && x.stok_fiyatlar.turu == false && x.stok_fiyatlar.tanimi == "SATIŞ FİYATI -1").ToList();

            return View(stoklar);
        }
        public ActionResult Kategori()
        {
            return View();
        }
        public ActionResult Ayrintilar()
        {
            long stok_id = 0;
            long.TryParse(Request.QueryString["stok_id"], out stok_id);
            if (stok_id == 0)
            {
                return RedirectToAction("Index");
            }
            var stok = db.stok.Join(
                db.stok_fiyatlar,
                x => x.id,
                y => y.stok_id,
                (x, y) => new MenuViewModel { stok = x, stok_fiyatlar = y }).First(x => x.stok.id == stok_id && x.stok_fiyatlar.turu == false && x.stok_fiyatlar.tanimi == "SATIŞ FİYATI -1");

            return View(stok);
        }
        public ActionResult KayarMenu()
        {
            ViewBag.grup = db.grup.Where(x => x.webde_gorunsun == 1 && x.modul == "stok").ToList();
            return PartialView();
        }
        public ActionResult favoriyemekler()
        {
            return PartialView();
        }
    }
}