using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QRMENU.Models.EkModeller
{
    public class model_Stok_Resim
    {
        public long stok_id { get; set; }
        public List<HttpPostedFileBase> ek_resimler { get; set; }
    }
}