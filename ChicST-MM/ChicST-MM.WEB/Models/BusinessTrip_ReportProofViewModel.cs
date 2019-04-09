using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    public class BusinessTrip_ReportProofViewModel
    {
        public int ID { get; set; }
        public string 路径 { get; set; }
        public int 出差记录ID { get; set; }
        public int 出差汇报项ID { get; set; }

        public virtual HR_出差汇报 HR_出差汇报 { get; set; }
        public virtual HR_出差计划 HR_出差计划 { get; set; }
    }
}