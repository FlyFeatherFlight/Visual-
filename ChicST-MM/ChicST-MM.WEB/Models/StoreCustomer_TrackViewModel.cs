﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChicST_MM.WEB.Models
{
    /// <summary>
    /// 店铺意向客户跟进日志
    /// </summary>
    public class StoreCustomer_TrackViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "接待序号")]
        [DataType(DataType.Text)]
        [Required]
        public string 接待序号 { get; set; }
        /// <summary>
        /// 接待记录ID
        /// </summary>
        [Display(Name = "接待记录ID")]
        [DataType(DataType.Text)]
        [Required]
        public int 接待ID { get; set; }

        public string 客户姓名 { get; set; }
        public string 客户电话 { get; set; }
        [Display(Name = "跟进人")]
        [DataType(DataType.Text)]
        [Required]
        public string 跟进人 { get; set; }

        public int 跟进人ID { get; set; }
        [Display(Name = "跟进时间")]
        [DataType(DataType.Text)]
        [Required]
        public DateTime 跟进时间 { get; set; }

        [Display(Name = "跟进方式")]
        [DataType(DataType.Text)]
        [Required]
        public string 跟进方式 { get; set; }

        [Display(Name = "跟进内容")]
        [DataType(DataType.Text)]
        [Required]
        public string 跟进内容 { get; set; }

        [Display(Name = "跟进结果")]
        [DataType(DataType.Text)]
        [Required]
        public string 跟进结果 { get; set; }
        [Display(Name = "店长审核")]
        [DataType(DataType.Text)]
        public string 店长审核 { get; set; }

        [Display(Name = "备注")]
        [DataType(DataType.Text)]
        public string 备注 { get; set; }

        [Display(Name = "是否申请设计师")]
        [DataType(DataType.Text)]
        public Nullable<bool> 是否申请设计师 { get; set; }
        public string 店铺 { get; set; }

        public int 店铺ID { get; set; }

       // public IEnumerable<CustomerExceptedBuyModel> CustomerExceptedBuyModels { set; get; }
        public virtual 销售_店铺员工档案 销售_店铺员工档案 { get; set; }
        public virtual 销售_接待记录 销售_接待记录 { get; set; }
    }
  
}
