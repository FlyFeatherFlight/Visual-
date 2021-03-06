﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    public class AcountInformation
    {
        public int ID { get; set; }
        public int 用户ID { get; set; }
        public string 编号 { get; set; }
        public string 姓名 { get; set; }

        public int 部门 { get; set; }

        public int? 管理者 { get; set; }
        public bool 停用标志 { get; set; }

        public string 岗位 { get; set; }
    }
}