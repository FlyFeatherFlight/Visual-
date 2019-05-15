﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    public class SalesRecordViewModel
    {
        public int ID { get; set; }
        public DateTime 销售日期 { get; set; }
        public string 销售单号 { get; set; }
        public string 合同单号 { get; set; }
        public decimal 销售金额 { get; set; }
        public string 折扣 { get; set; }
        public string 订库样 { get; set; }
        public int 销售人员ID { get; set; }
        public string 销售人员 { get; set; }
        public DateTime 制单日期 { get; set; }
        public int 制单人员ID { get; set; }
        public DateTime 更新日期 { get; set; }
        public int 更新人ID { get; set; }
        public int 接待记录ID { get; set; }
        public bool? 是否全款 { get; set; }
        public string 备注 { get; set; }
        public Nullable<int> 店铺ID { get; set; }
        public string 店铺 { get; set; }
        public Nullable<bool> 是否自营业绩 { get; set; }
        public Nullable<bool> 是否业务业绩 { get; set; }

    }
}