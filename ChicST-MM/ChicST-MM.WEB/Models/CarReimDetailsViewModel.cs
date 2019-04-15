using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 车辆报销明细
    /// </summary>
    public class CarReimDetailsViewModel
    {
        public int ID { get; set; }
        public string 报销类型 { get; set; }
        public decimal 金额 { get; set; }
        public string 备注 { get; set; }
        public int 更新人ID { get; set; }
        public string 更新人 { get; set; }
        public System.DateTime 更新时间 { get; set; }
        public int 车辆报销ID { get; set; }

    }
}