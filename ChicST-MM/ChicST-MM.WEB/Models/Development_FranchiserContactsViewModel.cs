using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 经销商_联系人
    /// </summary>
    public class Development_FranchiserContactsViewModel
    {
        public int ID { get; set; }
        public string 姓名 { get; set; }
        public string 性别 { get; set; }
        public Nullable<int> 年龄 { get; set; }
        public string 职务 { get; set; }
        public string 联系方式 { get; set; }
        public int 经销商档案ID { get; set; }

    }
}