﻿using System;

namespace StoreAnalyze.Models
{
    public class ReceivingRecordsViewModel
    {
        public string 预算金额 { get; set; }
        public string 预报价折扣 { get; set; }
        public string 返点 { get; set; }
        public string 安装地址 { get; set; }
        public string 客户建议 { get; set; }
        public bool? 是否有意向 { get; set; }
        public DateTime? 制单日期 { get; set; }
        public int? 更新人 { get; set; }
        public string 更新人姓名 { get; set; }
        public DateTime? 更新日期 { get; set; }
        public string 喜欢产品 { get; set; }
        public string 喜欢元素 { get; set; }
        public string 不喜欢产品 { get; set; }
        public string 不喜欢元素 { get; set; }
        public string 客户目的 { get; set; }
        public string 比较品牌产品备注 { get; set; }
        public string 户型大小 { get; set; }
        public string 客厅预算家具 { get; set; }
        public string 客厅预算金额 { get; set; }
        public string 客厅预算备注 { get; set; }
        public string 餐厅预算家具 { get; set; }
        public string 餐厅预算金额 { get; set; }
        public string 餐厅预算备注 { get; set; }
        public string 卧室预算家具 { get; set; }
        public string 卧室预算金额 { get; set; }
        public string 卧室预算备注 { get; set; }
        public string 其它空间家具 { get; set; }
        public string 其它空间预算 { get; set; }
        public string 其它空间备注 { get; set; }
        public bool? 是否关闭 { get; set; }
        public string 关闭备注 { get; set; }
        public string 销售讲解 { get; set; }
        public string 比较品牌产品 { get; set; }
        public string 比较品牌 { get; set; }
        public string 如何得知品牌 { get; set; }
        public int ID { get; set; }
        public int 店铺ID { get; set; }
        public string 店铺 { get; set; }
        public string 接待序号 { get; set; }
        public DateTime  接待日期 { get; set; }
        public string 客户来源 { get; set; }
        public string 客户姓名 { get; set; }
        public string 性别 { get; set; }
        public string 年龄段 { get; set; }
        public string 客户类别 { get; set; }
        public string 客户电话 { get; set; }
        public string 客户类型 { get; set; }
        public string 社交软件 { get; set; }
        public int 来店次数 { get; set; }
        public int 接待人ID { get; set; }
        public string 接待人 { get; set; }
        public int? 跟进人ID { get; set; }
        public string 跟进人 { get; set; }
        public DateTime 进店时间 { get; set; }
        public DateTime 出店时间 { get; set; }
        public DateTime? 预计使用时间 { get; set; }
        public string 装修进度 { get; set; }
        public string 装修情况 { get; set; }
        public string 装修风格 { get; set; }
        public string 使用空间 { get; set; }
        public string 家庭成员 { get; set; }
        public virtual 销售_店铺员工档案 销售_店铺员工档案 { get; set; }
        public string 设计师 { get; set; }
        public string 主导者喜好风格 { get; set; }
        public string 主导者 { get; set; }
        public string 同行人 { get; set; }
        public string 客户着装 { get; set; }
        public string 客户职业 { get; set; }
        public int 进店时长 { get; set; }
        public string 特征 { get; set; }
    }
}