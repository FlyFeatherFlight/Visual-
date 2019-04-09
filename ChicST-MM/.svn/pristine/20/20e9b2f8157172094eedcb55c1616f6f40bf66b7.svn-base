using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 其他报销
    /// </summary>
    public class OtherReimburseViewModel
    {
        public int ID { get; set; }
    
        public int 报销部门ID { get; set; }
        public string 报销部门 { get; set; }

        public int 报销人ID { get; set; }
        public string 报销人 { get; set; }
        public DateTime 提交日期 { get; set; }
        public bool? 审核状态 { get; set; }
        public int? 会计审核人ID { get; set; }
        public string 会计审核人 { get; set; }
        public decimal? 原借款 { get; set; }
        public decimal? 应退余款 { get; set; }
        [Required]
        public string 汇款银行 { get; set; }
        [Required]
        public string 汇款银行卡账号 { get; set; }

        public  ICollection<OtherReimburse_DetailsViewModel> 财务_其他报销_副表 { get; set; }
    }
}