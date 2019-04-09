using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    public class BusinessTrip_ReportViewModel
    {
        public int ID { get; set; }
        public int 出差记录ID { get; set; }
        public int 出差关联计划项ID { get; set; }
        public DateTime 巡店开始时间 { get; set; }
        public DateTime 巡店结束时间 { get; set; }
        public string 工作成果 { get; set; }
        public string 实际遇到的问题 { get; set; }
        public string 解决的方案 { get; set; }
        public string 需要的协助 { get; set; }
        public DateTime? 预计完成时间 { get; set; }
        public string 备注 { get; set; }
        public bool 是否包含附件 { get; set; }
        public DateTime 提交时间 { get; set; }
        public int 提交人ID { get; set; }
        public string 提交人 { get; set; }
        public DateTime 更新时间 { get; set; }
        public int 更新人ID { get; set; }
        public string 更新人 { get; set; }
        public bool 是否为新增项 { get; set; }

        public virtual 系统用户 系统用户 { get; set; }
        public virtual 系统用户 系统用户1 { get; set; }
        public virtual List<HR_出差汇报凭证> HR_出差汇报凭证 { get; set; }
        public virtual HR_出差计划详细 HR_出差计划详细 { get; set; }
        public virtual HR_出差计划 HR_出差计划 { get; set; }
    }
}