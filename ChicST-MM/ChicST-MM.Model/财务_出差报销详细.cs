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

public partial class 财务_出差报销详细
{
    public int ID { get; set; }
    public Nullable<int> 报销ID { get; set; }
    public Nullable<System.DateTime> 日期 { get; set; }
    public string 起讫地点 { get; set; }
    public string 城市 { get; set; }
    public string 对象类别 { get; set; }
    public Nullable<decimal> 住宿费 { get; set; }
    public Nullable<decimal> 车船费 { get; set; }
    public Nullable<decimal> 交通费 { get; set; }
    public Nullable<decimal> 生活补助 { get; set; }
    public string 同行人 { get; set; }
    public string 备注 { get; set; }
    public Nullable<int> 提交人ID { get; set; }
    public Nullable<System.DateTime> 提交时间 { get; set; }

    public virtual 财务_出差报销单 财务_出差报销单 { get; set; }
    public virtual 系统用户 系统用户 { get; set; }
}
