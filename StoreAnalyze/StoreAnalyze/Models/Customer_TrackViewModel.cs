using System;

namespace StoreAnalyze.Models
{
    public class Customer_TrackViewModel
    {
        public int id { get; set; }
        public int 店铺ID { get; set; }
        public int 接待记录ID { get; set; }
        public int 跟进人 { get; set; }
        public DateTime 跟进时间 { get; set; }
        public string 跟进方式 { get; set; }
        public string 跟进内容 { get; set; }
        public string 跟进结果 { get; set; }
        public string 店长审核 { get; set; }
        public string 备注 { get; set; }
        public bool? 是否申请设计师 { get; set; }
    }
}