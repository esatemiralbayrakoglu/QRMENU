using QRMENU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QRMENU.ViewModels
{
    public class MenuViewModel
    {
        public stok stok { get; set; }
        public stok_ekranlar stok_ekranlar { get; set; }
        public stok_fiyatlar stok_fiyatlar { get; set; }
        public grup grup { get; set; }
        public stok_birimler stok_birimler { get; set; }
    }
}