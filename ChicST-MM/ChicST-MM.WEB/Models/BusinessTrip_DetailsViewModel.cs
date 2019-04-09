﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChicST_MM.WEB.Models
{
    public class BusinessTrip_DetailsViewModel
    {

        public int ID { get; set; }
        public int 出差记录ID { get; set; }
        public System.DateTime 出差时间 { get; set; }
        public string 城市 { get; set; }
        
        public string 商场 { get; set; }
        public string 同行人员 { get; set; }
        public string 巡店目的 { get; set; }
        public string 计划内容 { get; set; }
        public string 计划方案 { get; set; }
        public string 备注 { get; set; }
        public System.DateTime 提交时间 { get; set; }

        public string 经销商 { get; set; }

        public string 门店{ get; set; }

        public virtual ICollection<HR_出差汇报> HR_出差汇报 { get; set; }
        public virtual HR_出差计划 HR_出差计划 { get; set; }
       
    }
}