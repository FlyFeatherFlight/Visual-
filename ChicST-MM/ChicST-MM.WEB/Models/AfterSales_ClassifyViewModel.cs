using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 售后分类
    /// </summary>
    public class AfterSales_ClassifyViewModel
    {
        public int ID { get; set; }
        public string 分类类型名称 { get; set; }
        public string 分类数据 { get; set; }
        public bool? 是否停用 { get; set; }
        public Nullable<System.DateTime> 更新日期 { get; set; }
        public Nullable<int> 更新人 { get; set; }
    }
}