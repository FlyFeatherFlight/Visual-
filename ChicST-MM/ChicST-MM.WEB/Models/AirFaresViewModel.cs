﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 机票报销
    /// </summary>
    public class AirFaresViewModel
    {
        public int ID { get; set; }
        public string 部门 { get; set; }
        public string 姓名 { get; set; }
        public string 出发地 { get; set; }
        public string 目的地 { get; set; }
        public DateTime 出发日期 { get; set; }
        public DateTime 到达日期 { get; set; }
        public decimal 机票价格 { get; set; }
        public decimal 退改费用 { get; set; }
        public decimal 其它费用 { get; set; }
        public decimal 费用小计 { get; set; }
        public string 备注 { get; set; }
        public int 出差ID { get; set; }
        public DateTime 订票日期 { get; set; }
        public int 制单人 { get; set; }

        public DateTime 制单日期 { get; set; }
        public bool? 审核标志 { get; set; }
        public DateTime 审核日期 { get; set; }
        public int 乘机人ID { get; set; }
        public string 乘机人员 { get; set; }
        public int 审核人ID { get; set; }
        public string 审核人 { get; set; }
    }
}