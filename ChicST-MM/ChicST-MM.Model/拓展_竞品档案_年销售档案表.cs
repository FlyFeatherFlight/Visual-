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

public partial class 拓展_竞品档案_年销售档案表
{
    public int ID { get; set; }
    public int 竞品档案ID { get; set; }
    public string 销售年 { get; set; }
    public Nullable<decimal> 销售额 { get; set; }
    public string 销售月 { get; set; }

    public virtual 拓展_竞品档案表 拓展_竞品档案表 { get; set; }
}
