﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    public class Review_5SViewModel
    {
        public int ID { get; set; }
        public string 评估项目 { get; set; }
        public string 具体内容 { get; set; }
        public string 评估标准 { get; set; }
        public int 分值 { get; set; }
        public string 扣分标准 { get; set; }
    }
}