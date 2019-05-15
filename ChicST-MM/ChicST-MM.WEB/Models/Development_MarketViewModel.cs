﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 拓展_商场档案
    /// </summary>
    public class Development_MarketViewModel
    {

        public int ID { get; set; }
        public Nullable<int> 所属城市ID { get; set; }
        public string 所属城市 { get; set; }
        public string 城市等级 { get; set; }
        public string 商场名称 { get; set; }
        public string 商场等级 { get; set; }
        public string 地址 { get; set; }
      
        public string 负责人 { get; set; }
        public string 联系方式 { get; set; }
        public string 职务 { get; set; }
        public Nullable<System.DateTime> 续约开始日期1 { get; set; }
        public Nullable<System.DateTime> 续约开始日期2 { get; set; }

        public string 经营总面积 { get; set; }
 
        public string 物业名称 { get; set; }
        public Nullable<System.DateTime> 品牌调整时间 { get; set; }
        public Nullable<int> 录入人ID { get; set; }
        public string 录入人 { get; set; }
        public Nullable<System.DateTime> 更新日期 { get; set; }
    }
}