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

public partial class 客服_售后发起单
{
    public 客服_售后发起单()
    {
        this.客服_售后跟进日志 = new HashSet<客服_售后跟进日志>();
    }

    public int ID { get; set; }
    public string 售后单号 { get; set; }
    public System.DateTime 客诉日期 { get; set; }
    public string 订货单号 { get; set; }
    public System.DateTime 发货日期 { get; set; }
    public System.DateTime 送货安装日期 { get; set; }
    public string 产品型号 { get; set; }
    public string 尺寸 { get; set; }
    public string 产品描述 { get; set; }
    public int 店铺ID { get; set; }
    public Nullable<int> 销售人员ID { get; set; }
    public string 售后描述 { get; set; }
    public Nullable<int> 照片附件 { get; set; }
    public string 初审意见 { get; set; }
    public int 提交人ID { get; set; }
    public System.DateTime 制单日期 { get; set; }
    public int 更新人ID { get; set; }
    public System.DateTime 更新日期 { get; set; }

    public virtual ICollection<客服_售后跟进日志> 客服_售后跟进日志 { get; set; }
    public virtual 系统用户 系统用户 { get; set; }
    public virtual 系统用户 系统用户1 { get; set; }
    public virtual 销售_店铺档案 销售_店铺档案 { get; set; }
    public virtual 销售_店铺员工档案 销售_店铺员工档案 { get; set; }
}
