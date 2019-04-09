using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 商务接待记录
    /// </summary>
    public class BusinessReceiving_RecordsViewModel
    {
        public int ID { get; set; }
        public string 部门 { get; set; }
        public DateTime 申请日期 { get; set; }
        public int 申请人ID { get; set; }
        public string 申请人 { get; set; }
        public string 航班号 { get; set; }
        public DateTime? 抵达日期 { get; set; }
        public DateTime? 离开日期 { get; set; }
        public string 入住酒店 { get; set; }
        public decimal? 费用总预算 { get; set; }
        public string 主要沟通内容 { get; set; }
        public string 配合部门 { get; set; }
        public int 提交人ID { get; set; }
        public string 提交人 { get; set; }
        public DateTime 提交日期 { get; set; }
        public int 更新人ID { get; set; }
        public string 更新人 { get; set; }
        public DateTime 更新日期 { get; set; }
        public bool? 审核状态 { get; set; }
        public int? 审核人ID { get; set; }
        public string 审核人 { get; set; }
        public virtual List<营销_接待计划费用预估> 营销_接待计划费用预估 { get; set; }
        public virtual List<营销_接待计划明细> 营销_接待计划明细 { get; set; }
        public virtual List<营销_接待来宾信息> 营销_接待来宾信息 { get; set; }
        public virtual List<营销_接待来访主要事项> 营销_接待来访主要事项 { get; set; }

    }
}