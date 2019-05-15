using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
     ///拓展_城市
    /// </summary>
    public class Development_CityViewModel
    {
        public int ID { get; set; }
        public string 省 { get; set; }
        public string 市 { get; set; }
        public string 城市分级 { get; set; }
        public Nullable<bool> 是否为目标城市 { get; set; }
        public Nullable<int> 录入人ID { get; set; }
        public string 录入人 { get; set; }
        public Nullable<System.DateTime> 更新时间 { get; set; }
    }
}