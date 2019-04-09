using System;
using System.Collections.Generic;

namespace StoreAnalyze.Models
{
    public class StoreViewModel
    {
        public string 密码 { get; set; }
        public DateTime? 制单日期 { get; set; }
        public int? 制单人 { get; set; }
        public bool? 停用标志 { get; set; }
        public string 等级 { get; set; }
        public int? 使用面积 { get; set; }
        public virtual ICollection<设计_设计案完结单> 设计_设计案完结单 { get; set; }
        public string 收货人电话 { get; set; }
        public string 收货人 { get; set; }
        public string 联系人电话 { get; set; }
        public string 联系人 { get; set; }
        public string 负责人电话 { get; set; }
        public string 负责人 { get; set; }
        public int? 地区ID { get; set; }
        public string 商场 { get; set; }
        public string 地址 { get; set; }
        public string 名称 { get; set; }
        public string 编号 { get; set; }
        public int? 品牌ID { get; set; }
        public int? 经销商ID { get; set; }
        public int ID { get; set; }
        public string 收货地址 { get; set; }
    }
}