using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace iGoo.Areas.Webcms.Models
{
    public class ProductImportModel
    {
        //public string ProductId { get; set; }
        //public string ProductName { get; set; }
        //public string PriceHN { get; set; }
        //public string PriceDealerHN { get; set; }
        //public string PriceHCM { get; set; }
        //public string PriceDealerHCM { get; set; }
        //public string PriceCN3 { get; set; }
        //public string PriceDealerCN3 { get; set; }
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string MucHang { get; set; }
        public string ChungLoai { get; set; }
        public string GiaLe { get; set; }
        public string GiaBuon { get; set; }
//        public string GiaLeHCM { get; set; }
//        public string GiaBuonHCM { get; set; }
//        public string GiaLeCN3 { get; set; }
//        public string GiaBuonCN3 { get; set; }
    }
}