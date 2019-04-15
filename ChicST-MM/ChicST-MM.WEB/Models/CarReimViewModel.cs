﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 车辆报销主表
    /// </summary>
    public class CarReimViewModel
    {
        public int ID { get; set; }
        public int 车辆数 { get; set; }
        public System.DateTime 用车开始时间 { get; set; }
        public System.DateTime 用车截止时间 { get; set; }
        public string 搭乘人员 { get; set; }
        public decimal 总计花费金额 { get; set; }
        public string 备注 { get; set; }
        public int 提交人ID { get; set; }
        public string 提交人 { get; set; }
        public System.DateTime 提交时间 { get; set; }
        public int 更新人ID { get; set; }
        public string 更新人 { get; set; }
        public System.DateTime 更新时间 { get; set; }
        public int 商务接待ID { get; set; }
        public bool? 审核状态 { get; set; }
        public int? 财务审核人ID { get; set; }
        public string 财务审核人 { get; set; }
    }
}