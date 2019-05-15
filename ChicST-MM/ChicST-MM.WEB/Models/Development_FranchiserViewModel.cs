﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 拓展_经销商档案
    /// </summary>
    public class Development_FranchiserViewModel
    {
        public int ID { get; set; }
        public Nullable<int> 城市ID { get; set; }
        public string 城市 { get; set; }
        public string 城市等级 { get; set; }
        public string 企业名称 { get; set; }
        public string 地址 { get; set; }
        public string 社会资源描述 { get; set; }
        public string 代理品牌 { get; set; }
        public Nullable<bool> 是否现代品牌 { get; set; }
        public string 经营年限 { get; set; }
        public Nullable<decimal> 年销售额 { get; set; }
        public Nullable<int> 团队人数 { get; set; }
        public string 入驻商场 { get; set; }
        public Nullable<int> 门店数量 { get; set; }
        public Nullable<decimal> 投资预算 { get; set; }
        public Nullable<int> 意向商场ID { get; set; }
        public string 意向商场 { get; set; }
        public string 商场等级 { get; set; }
        public string 预算面积 { get; set; }
        public string 意向位置分级 { get; set; }
        public string 理念描述 { get; set; }
        public string 备注 { get; set; }
        public Nullable<int> 录入人ID { get; set; }
        public string 录入人{ get; set; }
        public Nullable<System.DateTime> 更新日期 { get; set; }
        public string 匹配度 { get; set; }
        public string 盈利情况 { get; set; }

    }
}