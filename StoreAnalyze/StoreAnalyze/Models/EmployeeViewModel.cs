using System;

namespace StoreAnalyze.Models
{
    public class EmployeeViewModel
    {
        public string 等级 { get; set; }
        public bool? 是否店长 { get; set; }
      
        public bool? 是否设计师 { get; set; }
        public int? 跟进目标计划数 { get; set; }
        public string 密码 { get; set; }
        public DateTime? 制单日期 { get; set; }
        public int? 制单人 { get; set; }
        public bool 停用标志 { get; set; }
        public string 联系方式 { get; set; }
        public int 职务ID { get; set; }
       
        public string 性别 { get; set; }
        public string 姓名 { get; set; }
        public string 编号 { get; set; }
        public int 店铺ID { get; set; }
        public string 店铺 { get; set; }
        public int ID { get; set; }
        public bool? 是否销售 { get; set; }
       
    }
}