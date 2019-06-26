﻿using ChicST_MM.IBLL;
using ChicST_MM.WEB.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicST_MM.WEB.Controllers
{
    /// <summary>
    /// 销售订单操作
    /// </summary>
    public class SalesOrderController : Controller
    {
        private readonly IAccountBLL accountBLL;
        private readonly IStoreOrderBLL storeOrderBLL;
        private readonly IStoreOrder_DetailsBLL storeOrder_DetailsBLL;
        private readonly IStoreEmployeesBLL storeEmployeesBLL;
        private readonly IStoreBLL storeBLL;
        private readonly IProduct_SKUBLL product_SKUBLL;
        private readonly IProduct_SPUBLL product_SPUBLL;
        private readonly ISys_AuthorityBLL sys_AuthorityBLL;

        public SalesOrderController(IAccountBLL accountBLL, IStoreOrderBLL storeOrderBLL, IStoreOrder_DetailsBLL storeOrder_DetailsBLL, IStoreEmployeesBLL storeEmployeesBLL, IStoreBLL storeBLL, IProduct_SKUBLL product_SKUBLL, IProduct_SPUBLL product_SPUBLL, ISys_AuthorityBLL sys_AuthorityBLL)
        {
            this.accountBLL = accountBLL ?? throw new ArgumentNullException(nameof(accountBLL));
            this.storeOrderBLL = storeOrderBLL ?? throw new ArgumentNullException(nameof(storeOrderBLL));
            this.storeOrder_DetailsBLL = storeOrder_DetailsBLL ?? throw new ArgumentNullException(nameof(storeOrder_DetailsBLL));
            this.storeEmployeesBLL = storeEmployeesBLL ?? throw new ArgumentNullException(nameof(storeEmployeesBLL));
            this.storeBLL = storeBLL ?? throw new ArgumentNullException(nameof(storeBLL));
            this.product_SKUBLL = product_SKUBLL ?? throw new ArgumentNullException(nameof(product_SKUBLL));
            this.product_SPUBLL = product_SPUBLL ?? throw new ArgumentNullException(nameof(product_SPUBLL));
            this.sys_AuthorityBLL = sys_AuthorityBLL ?? throw new ArgumentNullException(nameof(sys_AuthorityBLL));
        }




        /// <summary>
        /// 销售订单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var ac = GetAccountInfo();
            if (ac.经销价折扣 == null)
            {
                return Content("<script>alert('您不具有访问此页面的权限,请联系系统管理员。');window.history.go(-1);</script>");
            }
            List<SalesOrderViewModel> models = new List<SalesOrderViewModel>();
            return View(models);
        }

        /// <summary>
        /// 订单详情页
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public ActionResult SalesOrderInfoView(int id)
        {
           
            销售_订单 salesmodel = storeOrderBLL.GetModel(p => p.ID == id);
            if (salesmodel.审核标志 == null && salesmodel.复核标志 == null)
            {
                ViewBag.AllowEdit = true;
            }
            var model = BuildSalesOrderModel(salesmodel);
            return View(model);
        }

        /// <summary>
        /// 改变订单明细经销折扣
        /// </summary>
        /// <param name="id">订单明细修改操作</param>
        ///<param name="discount">经销折扣</param>
        ///<param name="price">折后价格</param>
        /// <returns></returns>
        public ActionResult SalesOrder_DetailsEdit(int id, int discount, decimal price)
        {
            var ac = GetAccountInfo();//得到当前用户信息
            var m = sys_AuthorityBLL.GetModel(p => p.ID == ac.经销价折扣);
            if (m.修改 != true)
            {
                return Content("<script>alert('您不具有修改权限,请联系系统管理员。');window.history.go(-1);</script>");
            }

            var model = storeOrder_DetailsBLL.GetModel(p => p.ID == id);
            if (model == null)
            {
                return Json(new { success = false, msg = "数据异常！" });

            }
            var order = storeOrderBLL.GetModel(p => p.ID == model.单据ID);
            if (order.复核标志 != null || order.审核标志 != null)
            {
                return Json(new { success = false, msg = "该订单已审核！" });
            }
            try
            {

                model.经销价折扣 = discount;
                model.单价 = price;
                model.折扣操作人 = ac.姓名;
                model.折扣日期 = DateTime.Now;
                string[] property = new string[4];
                property[0] = "经销价折扣";
                property[1] = "单价";
                property[2] = "折扣操作人";
                property[3] = "折扣日期";
                storeOrder_DetailsBLL.Modify(model, property);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "修改失败！" + e.Message });
            }
            return Json(new { success = true, msg = "修改成功！" });
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="storeID">门店ID</param>
        /// <param name="strPhone">联系电话</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public ActionResult GetSalesOrdersJson(int? storeID, string strPhone, DateTime? startDate, DateTime? endDate)
        {
            var ac = GetAccountInfo();//得到当前用户信息
            var m = sys_AuthorityBLL.GetModel(p => p.ID == ac.经销价折扣);
            if (m.修改 != true)
            {
                return Content("<script>alert('您不具有查询权限,请联系系统管理员。');window.history.go(-1);</script>");
            }
            List<SalesOrderViewModel> models = new List<SalesOrderViewModel>();
            try
            {
                models = BuildSalesOrderModels(storeID, strPhone, startDate, endDate);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "查询失败！" + e.Message });
            }
            return Json(new { success = true, msg = "查询成功！", data = models });
        }


        /// <summary>
        /// 查询订单明细
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public ActionResult GetSalesOrdersDeatilsJson(int id)
        {
            var ac = GetAccountInfo();//得到当前用户信息
            var m = sys_AuthorityBLL.GetModel(p => p.ID == ac.经销价折扣);
            if (m.修改 != true)
            {
                return Content("<script>alert('您不具有查询权限,请联系系统管理员。');window.history.go(-1);</script>");
            }
            List<SalesOrdersDetailsViewModel> models = new List<SalesOrdersDetailsViewModel>();
            try
            {
                models = BuildSalesOrdersDetailsModels(id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "查询失败！" + e.Message });
            }
            return Json(new { success = true, msg = "查询成功！", data = models });
        }
        /// <summary>
        /// 构建订单实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private SalesOrderViewModel BuildSalesOrderModel(销售_订单 model)
        {
            SalesOrderViewModel salesOrder = new SalesOrderViewModel();
            salesOrder.ID = model.ID;
            salesOrder.IP = model.IP;
            salesOrder.rid = model.rid;
            salesOrder.制单销售人ID = model.制单销售人ID;
            //salesOrder.制单人姓名 = storeEmployeesBLL.GetModel(p => p.ID == model.制单销售人ID).姓名;
            salesOrder.制单日期 = model.制单日期;
            salesOrder.单据备注 = model.单据备注;
            salesOrder.单据日期 = model.单据日期;
            salesOrder.单据类别ID = model.单据类别ID;
            salesOrder.是否业务业绩 = model.是否业务业绩;
            salesOrder.是否自营业绩 = model.是否自营业绩;
            salesOrder.单据编号 = model.单据编号;
            salesOrder.合同编号 = model.合同编号;
            salesOrder.员工ID = model.员工ID;
            salesOrder.折扣 = model.折扣;
            if (model.员工ID != null)
            {
                salesOrder.员工 = storeEmployeesBLL.GetModel(p => p.ID == model.员工ID).姓名;
            }
            salesOrder.复核人 = model.复核人;
            if (salesOrder.复核人 != null)
            {
                salesOrder.复核人姓名 = accountBLL.GetModel(p => p.ID == salesOrder.复核人).姓名;
            }
            salesOrder.复核日期 = model.复核日期;
            salesOrder.复核标志 = model.复核标志;
            salesOrder.审核人 = model.审核人;
            if (salesOrder.审核人 != null)
            {
                salesOrder.审核人姓名 = accountBLL.GetModel(p => p.ID == salesOrder.审核人).姓名;
            }
            salesOrder.审核日期 = model.审核日期;
            salesOrder.审核标志 = model.审核标志;
            salesOrder.客户地址 = model.客户地址;
            salesOrder.客户姓名 = model.客户姓名;
            salesOrder.客户联系方式 = model.客户联系方式;
            salesOrder.店铺ID = model.店铺ID;
            if (salesOrder != null)
            {
                salesOrder.店铺 = storeBLL.GetModel(p => p.ID == salesOrder.店铺ID).名称;
            }
            salesOrder.批准人1 = model.批准人1;
            if (salesOrder.批准人1 != null)
            {
                salesOrder.批准人1姓名 = accountBLL.GetModel(p => p.ID == salesOrder.批准人1).姓名;

            }
            salesOrder.批准日期1 = model.批准日期1;
            salesOrder.批准标志1 = model.批准标志1;
            salesOrder.收货人 = model.收货人;
            salesOrder.收货人联系方式 = model.收货人联系方式;
            salesOrder.收货地址 = model.收货地址;
            salesOrder.确认人 = model.确认人;
            if (salesOrder.确认人 != null)
            {
                salesOrder.确认人姓名 = accountBLL.GetModel(p => p.ID == salesOrder.确认人).姓名;

            }
            salesOrder.确认日期 = model.确认日期;
            salesOrder.确认标志 = model.确认标志;
            salesOrder.订单编号 = model.订单编号;
            salesOrder.零售合计 = model.零售合计;
            salesOrder.预付金额_bak = model.预付金额_bak;

            salesOrder.制单销售人ID = model.制单销售人ID;
            if (salesOrder.制单销售人ID != null)
            {
                salesOrder.制单销售人 = storeEmployeesBLL.GetModel(p => p.ID == model.制单销售人ID).姓名;
            }
            salesOrder.接待ID = model.接待ID;

            return salesOrder;
        }

        /// <summary>
        /// 构建订单实体集
        /// </summary>
        /// <param name="id">门店ID</param>
        /// <param name="emid">员工ID</param>
        /// <param name="strPhone">搜索条件</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        private List<SalesOrderViewModel> BuildSalesOrderModels(int? id, string strPhone, DateTime? startDate, DateTime? endDate)
        {
            List<销售_订单> models = new List<销售_订单>();
            if (id > 0)
            {
                var orders = storeOrderBLL.GetModels(p => p.店铺ID == id);
                if (orders != null)
                {
                    models = orders.ToList();
                }
            }
            else
            {
                var orders = storeOrderBLL.GetModels(p => true);
                if (orders != null)
                {
                    models = orders.ToList();
                }
            }

            if (!string.IsNullOrEmpty(strPhone))
            {

                if (models.Count() <= 0)
                {
                    return null;
                }
                var orders = models.Where(p => p.客户联系方式 == strPhone);
                if (orders != null)
                {

                    models = orders.ToList();

                }


            }
            if (startDate != null)
            {
                if (models.Count() <= 0)
                {
                    return null;
                }

                var orders = models.Where(p => p.单据日期 >= startDate);
                if (orders != null)
                {

                    models = orders.ToList();

                }
            }
            if (endDate != null)
            {
                if (models.Count() <= 0)
                {
                    return null;
                }

                var orders = models.Where(p => p.单据日期 <= endDate);
                if (orders != null)
                {

                    models = orders.ToList();

                }
            }
            if (models == null)
            {
                return null;
            }
            List<SalesOrderViewModel> lis = new List<SalesOrderViewModel>();
            foreach (var item in models)
            {
                var model = BuildSalesOrderModel(item);
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 构建订单明细实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private SalesOrdersDetailsViewModel BuildSalesOrdersDetailsModel(销售_订单明细 model)
        {
            SalesOrdersDetailsViewModel salesOrdersDetails = new SalesOrdersDetailsViewModel();
            salesOrdersDetails.ID = model.ID;
            salesOrdersDetails.SKU_ID = model.SKU_ID;
            salesOrdersDetails.交货周期 = model.交货周期;
            salesOrdersDetails.单价 = model.单价;
            salesOrdersDetails.单据ID = model.单据ID;
            salesOrdersDetails.小计 = model.小计;
            salesOrdersDetails.序号 = model.序号;
            salesOrdersDetails.数量 = model.数量;
            salesOrdersDetails.明细备注 = model.明细备注;
            salesOrdersDetails.标准零售价 = model.标准零售价;
            salesOrdersDetails.零售单价 = model.零售单价;
            salesOrdersDetails.零售小计 = model.零售小计;
            salesOrdersDetails.需求日期 = model.需求日期;
            salesOrdersDetails.预计交期 = model.预计交期;
            salesOrdersDetails.默认交期 = model.默认交期;
            var sku = product_SKUBLL.GetModel(p => p.ID == model.SKU_ID);
            var spu = product_SPUBLL.GetModel(p => p.SPUID == sku.SPU_ID);
            salesOrdersDetails.名称 = spu.名称;
            salesOrdersDetails.描述 = sku.描述;
            salesOrdersDetails.系列 = spu.系列;
            salesOrdersDetails.是否进口 = spu.是否进口;
            salesOrdersDetails.规格 = spu.规格;
            salesOrdersDetails.计量单位 = spu.计量单位;
            salesOrdersDetails.商品型号 = spu.型号;
            salesOrdersDetails.商品编号 = spu.编号;
            salesOrdersDetails.标准经销价 = model.标准经销价;
            salesOrdersDetails.经销价折扣 = model.经销价折扣;
            salesOrdersDetails.折扣操作人 = model.折扣操作人;
            salesOrdersDetails.折扣日期 = model.折扣日期;
            return salesOrdersDetails;
        }

        /// <summary>
        /// 构建销售订单明细实体集
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<SalesOrdersDetailsViewModel> BuildSalesOrdersDetailsModels(int id)
        {
            var models = storeOrder_DetailsBLL.GetModels(p => p.单据ID == id);
            if (models == null)
            {
                return null;
            }
            List<SalesOrdersDetailsViewModel> lis = new List<SalesOrdersDetailsViewModel>();
            foreach (var item in models)
            {
                var model = BuildSalesOrdersDetailsModel(item);
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 设置当前的用户信息
        /// </summary>
        private AcountInformation GetAccountInfo()
        {

            string userName = HttpContext.User.Identity.Name;
            if (userName != null)
            {

                var account = HttpContext.Session["Account"] as AcountInformation;
                return account;
            }
            else
            {
                return null;
            }

        }

    }
}