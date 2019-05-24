using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 出差拜访对象
    /// </summary>
    public class BusinessTrip_VisitSubjectViewModel
    {
        public int ID { get; set; }
        public string 拜访对象类型 { get; set; }
        public int 拜访对象ID { get; set; }
        public string 拜访对象名称 { get; set; }
        public int 出差计划详细ID { get; set; }
    }
}