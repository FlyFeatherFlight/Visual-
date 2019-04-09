using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 5S评审详细
    /// </summary>
    public class ReviewRecord_DetailsViewModel
    {

        public int ID { get; set; }
        public int 评审表ID { get; set; }
        public int 门店 { get; set; }
        public string 门店名称 { get; set; }
        public DateTime 日期 { get; set; }
        public int 评审人 { get; set; }
        public string 评审人姓名 { get; set; }
        public int? 扣分 { get; set; }
        public int 得分 { get; set; }
        public string 备注 { get; set; }
        public Nullable<int> 凭证 { get; set; }
        public string 评估项目 { get; set; }
        public string 评估标准 { get; set; }
        public int 分值 { get; set; }
        public string 扣分标准 { get; set; }
        public string 具体内容 { get; set; }
        public int 评分标准序号 { get; set; }
       public  List<int> proofs { get; set; }
    }
}