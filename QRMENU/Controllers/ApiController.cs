using QRMENU.Models.EkModeller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QRMENU.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult Index()
        {
            return View();
        }
        public string ResimYukle(model_Stok_Resim gelen_model)
        {
            try
            {
                if (gelen_model.ek_resimler.Count>0)
                {
                    string uzanti = "";
                    int id = 0;
                    string dosya_adi= "";
                    string savefilename = "";

                    foreach (HttpPostedFileBase item in  gelen_model.ek_resimler)
                    {
                        if (item!=null)
                        {
                            dosya_adi = Encoding.UTF8.GetString(Convert.FromBase64String( item.FileName));
                            uzanti = dosya_adi.ToLower();
                            uzanti = uzanti.Substring(uzanti.LastIndexOf('.')+1);
                            savefilename = dosya_adi.Replace("." + uzanti, "");
                            item.SaveAs("YUKLEME YOLU YAZILACAK ");
                        }

                    }
                    return "1";
                }
            }
            catch (Exception e)
            {

                return "0-" + e.Message;
            }
            return "0";
        }
    }
}