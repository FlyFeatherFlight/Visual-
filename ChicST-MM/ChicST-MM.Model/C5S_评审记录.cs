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

public partial class C5S_评审记录
{
    public C5S_评审记录()
    {
        this.C5S_评审记录_凭证 = new HashSet<C5S_评审记录_凭证>();
        this.C5S_评审记录详细 = new HashSet<C5S_评审记录详细>();
    }

    public int ID { get; set; }
    public int 展厅 { get; set; }
    public Nullable<int> 分数 { get; set; }
    public Nullable<System.DateTime> 巡店日期 { get; set; }
    public int 巡店人员 { get; set; }
    public string 陈列建议 { get; set; }
    public string 奖惩制度提出意见 { get; set; }
    public System.DateTime 提交日期 { get; set; }
    public int 提交人 { get; set; }
    public int 经销商 { get; set; }
    public Nullable<int> 陈列负责人ID { get; set; }
    public Nullable<int> 运营负责人ID { get; set; }

    public virtual ICollection<C5S_评审记录_凭证> C5S_评审记录_凭证 { get; set; }
    public virtual 系统用户 系统用户 { get; set; }
    public virtual 系统用户 系统用户1 { get; set; }
    public virtual 系统用户 系统用户2 { get; set; }
    public virtual 系统用户 系统用户3 { get; set; }
    public virtual 销售_店铺档案 销售_店铺档案 { get; set; }
    public virtual 销售_经销商档案 销售_经销商档案 { get; set; }
    public virtual ICollection<C5S_评审记录详细> C5S_评审记录详细 { get; set; }
}
