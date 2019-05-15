//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class HR_出差计划
{
    public HR_出差计划()
    {
        this.HR_出差汇报 = new HashSet<HR_出差汇报>();
        this.HR_出差汇报凭证 = new HashSet<HR_出差汇报凭证>();
        this.HR_出差计划详细 = new HashSet<HR_出差计划详细>();
        this.财务_机票明细 = new HashSet<财务_机票明细>();
    }

    public int ID { get; set; }
    public int 部门ID { get; set; }
    public Nullable<int> 出差人 { get; set; }
    public string 出差内容 { get; set; }
    public System.DateTime 提交时间 { get; set; }
    public System.DateTime 开始时间 { get; set; }
    public System.DateTime 结束时间 { get; set; }
    public string 备注 { get; set; }
    public int 提交人ID { get; set; }
    public Nullable<decimal> 车船费预计 { get; set; }
    public Nullable<decimal> 住宿费预计 { get; set; }
    public Nullable<decimal> 餐补费预计 { get; set; }
    public Nullable<int> 关联审核人ID { get; set; }
    public Nullable<bool> 审核状态 { get; set; }
    public Nullable<System.DateTime> 审核时间 { get; set; }
    public Nullable<decimal> 合计 { get; set; }

    public virtual ICollection<HR_出差汇报> HR_出差汇报 { get; set; }
    public virtual ICollection<HR_出差汇报凭证> HR_出差汇报凭证 { get; set; }
    public virtual ICollection<HR_出差计划详细> HR_出差计划详细 { get; set; }
    public virtual ICollection<财务_机票明细> 财务_机票明细 { get; set; }
}
