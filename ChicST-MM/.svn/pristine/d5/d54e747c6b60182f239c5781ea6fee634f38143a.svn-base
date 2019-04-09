using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 出差
    /// </summary>
    public class BusinessTripViewModel
    {

        public int ID { get; set; }
        [Required]
        public int 部门ID { get; set; }
        public string 部门 { get; set; }
        [Required]
        public int 出差人 { get; set; }
        public string 出差人姓名 { get; set; }
        public string 出差内容 { get; set; }
        [Required]
        public System.DateTime 提交时间 { get; set; }
        [Required]
        public System.DateTime 开始时间 { get; set; }
        [Required]
        public System.DateTime 结束时间 { get; set; }
        public string 备注 { get; set; }
        [Required]
        public int 提交人ID { get; set; }
        public string 提交人 { get; set; }
        public decimal? 车船费预计 { get; set; }
        public decimal? 住宿费预计 { get; set; }
        public decimal? 餐补费预计 { get; set; }
        public int? 关联审核人ID { get; set; }
        public string 关联审核人 { get; set; }
        public string 审核状态 { get; set; }
        public DateTime? 审核时间 { get; set; }
        public decimal? 合计 { get; set; }
        public string 户名 { get; set; }
        public string 是否报销机票 { get; set; }

        public string 是否分摊机票报销 { get; set; }
        public List<BusinessTrip_DetailsViewModel> BusinessTrip_DetailsViewModels { get; set; }

    }
}