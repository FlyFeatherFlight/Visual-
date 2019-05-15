﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
 
    /// <summary>
    ///数据分析
    /// </summary>
    public class StoreAnalyzeModel
    {
        public string 城市 { get; set; }
        public int 店铺ID { get; set; }
        public string 店铺 { get; set; }
        public DateTime 接待日期 { get; set; }
        public string 销售姓名 { get; set; }
        public int 销售ID { get; set; }
        public int 接待人数 { get; set; }


        public int 意向确认人数 { get; set; }

        public int 成交人数 { get; set; }
        public decimal 成交金额 { get; set; }

        public DateTime? 成交日期 { get; set; }
        public string 确认率 { get; set; }
        public double 店铺面积 { get; set; }
        public string 成交率 { get; set; }
        public double 店铺销售目标 { get; set; }
        public string 达成率 { get; set; }
        public double 坪效 { get; set; }
        public decimal 门店自营业绩 { get; set; }
        public decimal 门店业务业绩 { get; set; }
        public decimal 平均单值 { get; set; }
        public decimal 最大单值 { get; set; }
        public int 接待月份 { get; set; }
    }
}