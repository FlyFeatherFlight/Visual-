using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 商务接待报销
    /// </summary>
    public class BusinessRecReimburseViewModel
    {
        public int ID { get; set; }
        public string 城市 { get; set; }
        public string 经销商 { get; set; }
        public string 事由 { get; set; }
        public System.DateTime 时间 { get; set; }
        public decimal 金额 { get; set; }
        public int 接待计划ID { get; set; }
        public string 提交人 { get; set; }
        public int 提交人ID { get; set; }
        public System.DateTime 提交时间 { get; set; }
        public string 更新人 { get; set; }
        public int? 更新人ID { get; set; }
        public DateTime? 更新时间 { get; set; }
        public bool? 审核状态 { get; set; }
        public string 财务审核人 { get; set; }
        public int? 财务审核人ID { get; set; }

       public List<财务_接待报销分摊> RecSharingList { get; set; }
       
    }
}