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

public partial class 系统用户
{
    public 系统用户()
    {
        this.C5S_评审记录 = new HashSet<C5S_评审记录>();
        this.C5S_评审记录1 = new HashSet<C5S_评审记录>();
        this.C5S_评审记录2 = new HashSet<C5S_评审记录>();
        this.C5S_评审记录3 = new HashSet<C5S_评审记录>();
        this.C5S_评审记录详细 = new HashSet<C5S_评审记录详细>();
        this.HR_出差汇报 = new HashSet<HR_出差汇报>();
        this.HR_出差汇报1 = new HashSet<HR_出差汇报>();
        this.财务_车辆报销分摊 = new HashSet<财务_车辆报销分摊>();
        this.财务_车辆报销明细 = new HashSet<财务_车辆报销明细>();
        this.财务_出差报销单 = new HashSet<财务_出差报销单>();
        this.财务_出差报销单1 = new HashSet<财务_出差报销单>();
        this.财务_出差报销详细 = new HashSet<财务_出差报销详细>();
        this.财务_接待报销 = new HashSet<财务_接待报销>();
        this.财务_接待报销1 = new HashSet<财务_接待报销>();
        this.财务_接待报销2 = new HashSet<财务_接待报销>();
        this.财务_接待车辆报销 = new HashSet<财务_接待车辆报销>();
        this.财务_接待车辆报销1 = new HashSet<财务_接待车辆报销>();
        this.财务_接待车辆报销2 = new HashSet<财务_接待车辆报销>();
        this.财务_接待住宿报销 = new HashSet<财务_接待住宿报销>();
        this.财务_接待住宿报销1 = new HashSet<财务_接待住宿报销>();
        this.财务_接待住宿报销2 = new HashSet<财务_接待住宿报销>();
        this.财务_接待住宿报销分摊 = new HashSet<财务_接待住宿报销分摊>();
        this.财务_接待住宿报销分摊1 = new HashSet<财务_接待住宿报销分摊>();
        this.财务_其它报销 = new HashSet<财务_其它报销>();
        this.财务_其它报销1 = new HashSet<财务_其它报销>();
        this.财务_收款单 = new HashSet<财务_收款单>();
        this.财务_收款单1 = new HashSet<财务_收款单>();
        this.客服_售后发起单 = new HashSet<客服_售后发起单>();
        this.客服_售后发起单1 = new HashSet<客服_售后发起单>();
        this.客服_售后发起单凭证 = new HashSet<客服_售后发起单凭证>();
        this.客服_售后费用分摊 = new HashSet<客服_售后费用分摊>();
        this.客服_售后费用分摊1 = new HashSet<客服_售后费用分摊>();
        this.客服_售后跟进日志 = new HashSet<客服_售后跟进日志>();
        this.客服_售后跟进日志1 = new HashSet<客服_售后跟进日志>();
        this.营销_接待计划 = new HashSet<营销_接待计划>();
        this.营销_接待计划1 = new HashSet<营销_接待计划>();
        this.营销_接待计划2 = new HashSet<营销_接待计划>();
        this.营销_接待计划3 = new HashSet<营销_接待计划>();
        this.营销_接待计划明细 = new HashSet<营销_接待计划明细>();
        this.营销_接待计划明细1 = new HashSet<营销_接待计划明细>();
        this.财务_机票明细 = new HashSet<财务_机票明细>();
        this.财务_机票明细1 = new HashSet<财务_机票明细>();
        this.财务_机票明细2 = new HashSet<财务_机票明细>();
        this.拓展_城市档案 = new HashSet<拓展_城市档案>();
        this.拓展_商场档案表 = new HashSet<拓展_商场档案表>();
        this.拓展_竞品档案表 = new HashSet<拓展_竞品档案表>();
        this.拓展_经销商档案表 = new HashSet<拓展_经销商档案表>();
    }

    public int ID { get; set; }
    public string 编号 { get; set; }
    public string 姓名 { get; set; }
    public string 密码 { get; set; }
    public string 邮箱 { get; set; }
    public bool 停用标志 { get; set; }
    public Nullable<System.DateTime> 注册日期 { get; set; }
    public string 注册IP { get; set; }
    public Nullable<int> 经销商ID { get; set; }
    public Nullable<int> 店铺ID { get; set; }

    public virtual ICollection<C5S_评审记录> C5S_评审记录 { get; set; }
    public virtual ICollection<C5S_评审记录> C5S_评审记录1 { get; set; }
    public virtual ICollection<C5S_评审记录> C5S_评审记录2 { get; set; }
    public virtual ICollection<C5S_评审记录> C5S_评审记录3 { get; set; }
    public virtual ICollection<C5S_评审记录详细> C5S_评审记录详细 { get; set; }
    public virtual ICollection<HR_出差汇报> HR_出差汇报 { get; set; }
    public virtual ICollection<HR_出差汇报> HR_出差汇报1 { get; set; }
    public virtual ICollection<财务_车辆报销分摊> 财务_车辆报销分摊 { get; set; }
    public virtual ICollection<财务_车辆报销明细> 财务_车辆报销明细 { get; set; }
    public virtual ICollection<财务_出差报销单> 财务_出差报销单 { get; set; }
    public virtual ICollection<财务_出差报销单> 财务_出差报销单1 { get; set; }
    public virtual ICollection<财务_出差报销详细> 财务_出差报销详细 { get; set; }
    public virtual ICollection<财务_接待报销> 财务_接待报销 { get; set; }
    public virtual ICollection<财务_接待报销> 财务_接待报销1 { get; set; }
    public virtual ICollection<财务_接待报销> 财务_接待报销2 { get; set; }
    public virtual ICollection<财务_接待车辆报销> 财务_接待车辆报销 { get; set; }
    public virtual ICollection<财务_接待车辆报销> 财务_接待车辆报销1 { get; set; }
    public virtual ICollection<财务_接待车辆报销> 财务_接待车辆报销2 { get; set; }
    public virtual ICollection<财务_接待住宿报销> 财务_接待住宿报销 { get; set; }
    public virtual ICollection<财务_接待住宿报销> 财务_接待住宿报销1 { get; set; }
    public virtual ICollection<财务_接待住宿报销> 财务_接待住宿报销2 { get; set; }
    public virtual ICollection<财务_接待住宿报销分摊> 财务_接待住宿报销分摊 { get; set; }
    public virtual ICollection<财务_接待住宿报销分摊> 财务_接待住宿报销分摊1 { get; set; }
    public virtual ICollection<财务_其它报销> 财务_其它报销 { get; set; }
    public virtual ICollection<财务_其它报销> 财务_其它报销1 { get; set; }
    public virtual ICollection<财务_收款单> 财务_收款单 { get; set; }
    public virtual ICollection<财务_收款单> 财务_收款单1 { get; set; }
    public virtual ICollection<客服_售后发起单> 客服_售后发起单 { get; set; }
    public virtual ICollection<客服_售后发起单> 客服_售后发起单1 { get; set; }
    public virtual ICollection<客服_售后发起单凭证> 客服_售后发起单凭证 { get; set; }
    public virtual ICollection<客服_售后费用分摊> 客服_售后费用分摊 { get; set; }
    public virtual ICollection<客服_售后费用分摊> 客服_售后费用分摊1 { get; set; }
    public virtual ICollection<客服_售后跟进日志> 客服_售后跟进日志 { get; set; }
    public virtual ICollection<客服_售后跟进日志> 客服_售后跟进日志1 { get; set; }
    public virtual 销售_经销商档案 销售_经销商档案 { get; set; }
    public virtual ICollection<营销_接待计划> 营销_接待计划 { get; set; }
    public virtual ICollection<营销_接待计划> 营销_接待计划1 { get; set; }
    public virtual ICollection<营销_接待计划> 营销_接待计划2 { get; set; }
    public virtual ICollection<营销_接待计划> 营销_接待计划3 { get; set; }
    public virtual ICollection<营销_接待计划明细> 营销_接待计划明细 { get; set; }
    public virtual ICollection<营销_接待计划明细> 营销_接待计划明细1 { get; set; }
    public virtual ICollection<财务_机票明细> 财务_机票明细 { get; set; }
    public virtual ICollection<财务_机票明细> 财务_机票明细1 { get; set; }
    public virtual ICollection<财务_机票明细> 财务_机票明细2 { get; set; }
    public virtual ICollection<拓展_城市档案> 拓展_城市档案 { get; set; }
    public virtual ICollection<拓展_商场档案表> 拓展_商场档案表 { get; set; }
    public virtual ICollection<拓展_竞品档案表> 拓展_竞品档案表 { get; set; }
    public virtual ICollection<拓展_经销商档案表> 拓展_经销商档案表 { get; set; }
}
