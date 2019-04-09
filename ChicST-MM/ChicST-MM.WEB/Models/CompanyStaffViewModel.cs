using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 公司员工
    /// </summary>
    public class CompanyStaffViewModel
    {
        public int ID { get; set; }
        public string 姓名 { get; set; }
        public string 部门 { get; set; }
        public string 上级部门 { get; set; }
        public string 岗位 { get; set; }
        public string 职级 { get; set; }
        public int? 用户ID { get; set; }
    }
}