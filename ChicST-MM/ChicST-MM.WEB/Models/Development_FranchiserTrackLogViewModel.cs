﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 经销商追踪日志
    /// </summary>
    public class Development_FranchiserTrackLogViewModel
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> 日期 { get; set; }
        public string 地点 { get; set; }
        public Nullable<System.DateTime> 时间 { get; set; }
        public string 类型 { get; set; }
        public string 拜访人 { get; set; }
        public string 拜访内容 { get; set; }
        public string 是否了解希可 { get; set; }
        public Nullable<bool> 半年内看过希可门店 { get; set; }
        public Nullable<bool> 关注希可超过一年 { get; set; }
        public string 了解程度说明 { get; set; }
        public string 意向等级 { get; set; }
        public string 意向描述 { get; set; }
        public string 结果 { get; set; }
        public Nullable<int> 录入人ID { get; set; }
        public string 录入人 { get; set; }
        public Nullable<System.DateTime> 更新日期 { get; set; }
        public int 经销商ID { get; set; }
        public string 经销商 { get; set; }

        public string 推进卡点 { get; set; }
    }
}