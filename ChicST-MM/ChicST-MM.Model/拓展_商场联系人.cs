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

public partial class 拓展_商场联系人
{
    public int ID { get; set; }
    public Nullable<int> 商场ID { get; set; }
    public string 联系人 { get; set; }
    public string 职务 { get; set; }
    public string 联系方式 { get; set; }
    public string 性别 { get; set; }

    public virtual 拓展_商场档案表 拓展_商场档案表 { get; set; }
}
