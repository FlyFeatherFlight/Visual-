using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 车辆报销分摊
    /// </summary>
    public class CarReimSharingViewModel
    {
        public int ID { get; set; }
        public string 城市 { get; set; }
        public string 对象类型 { get; set; }
        public string 对象名称 { get; set; }
        public decimal 分摊金额 { get; set; }
        public int 车辆报销ID { get; set; }
        public int 更新人ID { get; set; }
        public string 更新人 { get; set; }
        public System.DateTime 更新时间 { get; set; }
        public string 备注 { get; set; }
    }
}