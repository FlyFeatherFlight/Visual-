﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 出差报销
    /// </summary>
    public class TravelReimbursementViewModel
    {
        public int ID { get; set; }
        public string 汇款银行 { get; set; }
        public string 汇款账号 { get; set; }
        public decimal? 借款金额 { get; set; }
        public decimal? 应退补金额 { get; set; }
        public int? 报销人ID { get; set; }
        public string 报销人 { get; set; }
        public string 报销人职务 { get; set; }
        public DateTime? 提交时间 { get; set; }
        public bool? 审核状态 { get; set; }
        public int? 财务审核人ID { get; set; }
        public string 财务审核人 { get; set; }
        public int? 部门ID { get; set; }
        public string 部门 { get; set; }
        public decimal? 合计 { get; set; }
        public int? 关联出差ID { get; set; }
        public decimal? 住宿费合计 { get; set; }
        public decimal? 车船费合计 { get; set; }
        public decimal? 交通费合计 { get; set; }
        public decimal? 生活补助合计 { get; set; }
        public string 城市 { get; set; }
        public string 出差日期{get;set;}
        public bool 是否作废 { get; set; }
        public string 出差事由 { get; set; }
        public string 户名 { get; set; }
        public  ICollection<财务_出差报销详细> 财务_出差报销详细 { get; set; }
    }
}