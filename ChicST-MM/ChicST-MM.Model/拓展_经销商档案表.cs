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

public partial class 拓展_经销商档案表
{
    public 拓展_经销商档案表()
    {
        this.拓展_经销商_联系人 = new HashSet<拓展_经销商_联系人>();
    }

    public int ID { get; set; }
    public Nullable<int> 城市ID { get; set; }
    public string 城市等级 { get; set; }
    public string 企业名称 { get; set; }
    public string 地址 { get; set; }
    public string 社会资源描述 { get; set; }
    public string 代理品牌 { get; set; }
    public Nullable<bool> 是否现代品牌 { get; set; }
    public string 经营年限 { get; set; }
    public Nullable<decimal> 年销售额 { get; set; }
    public Nullable<int> 团队人数 { get; set; }
    public string 入驻商场 { get; set; }
    public Nullable<int> 门店数量 { get; set; }
    public Nullable<decimal> 投资预算 { get; set; }
    public string 意向商场 { get; set; }
    public string 商场等级 { get; set; }
    public string 预算面积 { get; set; }
    public string 意向位置分级 { get; set; }
    public string 理念描述 { get; set; }
    public string 匹配度 { get; set; }
    public string 备注 { get; set; }
    public Nullable<int> 录入人ID { get; set; }
    public Nullable<System.DateTime> 更新日期 { get; set; }
    public string 盈利情况 { get; set; }
    public string 经销商等级 { get; set; }
    public string 经销商特征 { get; set; }
    public string 经销商来源 { get; set; }

    public virtual 拓展_城市档案 拓展_城市档案 { get; set; }
    public virtual 系统用户 系统用户 { get; set; }
    public virtual ICollection<拓展_经销商_联系人> 拓展_经销商_联系人 { get; set; }
}
