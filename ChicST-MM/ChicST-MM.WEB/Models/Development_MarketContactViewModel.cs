using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 商场联系人档案
    /// </summary>
    public class Development_MarketContactViewModel
    {
        public int ID { get; set; }
        public Nullable<int> 商场ID { get; set; }
        public string 商场 { get; set; }
        public string 联系人 { get; set; }
        public string 职务 { get; set; }
        public string 联系方式 { get; set; }
        public string 性别 { get; set; }
    }
}