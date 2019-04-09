using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 收款单
    /// </summary>
    public class ReceiptViewModel
    {
        public int ID { get; set; }
        public string 付款方 { get; set; }
        public string 付款银行 { get; set; }
        public int 付款账号 { get; set; }
        public string 收款银行 { get; set; }
        public int 收款账号 { get; set; }
        public DateTime 付款时间 { get; set; }
        public DateTime 到账时间 { get; set; }
        public string 提交人员 { get; set; }
        public int 提交人员ID { get; set; }
        public System.DateTime 提交时间 { get; set; }
        public int 审核人 { get; set; }
        public string 审核人员 { get; set; }
        public DateTime 审核时间 { get; set; }
        public string 审核结果 { get; set; }
        public string 备注 { get; set; }
        public string 款项类型 { get; set; }
        public string 收款方 { get; set; }
    }
}