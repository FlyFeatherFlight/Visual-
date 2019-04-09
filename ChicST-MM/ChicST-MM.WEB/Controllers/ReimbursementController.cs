﻿using ChicST_MM.IBLL;
using ChicST_MM.Model;
using ChicST_MM.WEB.Models;
using JPager.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ChicST_MM.WEB.Controllers
{
    /// <summary>
    /// 报销控制
    /// </summary>
    public class ReimbursementController : Controller
    {
        private readonly IBusinessTripBLL businessTripBLL;
        private readonly IAccountBLL accountBLL;
        private readonly IEmployeeInformationBLL employeeInformationBLL;
        private readonly IDepartmentBLL departmentBLL;
        private readonly IOtherReimburseBLL otherReimburseBLL;
        private readonly IOtherReimburse_DetailsBLL otherReimburse_DetailsBLL;
        private readonly ITravelReimbursementBLL travelReimbursementBLL;
        private readonly ITravelReimbursement_DetailsBLL travelReimbursement_DetailsBLL;
        private readonly IBusinessTrip_TypeBLL businessTrip_TypeBLL;
        private readonly IRegionBLL regionBLL;
        private readonly IDistributorBLL distributorBLL;
        private readonly IStoreBLL storeBLL;
        private readonly IReimbursement_BusinessReceptionBLL reimbursement_BusinessReceptionBLL;
        private readonly IReimbursement_ReceptionSharingBLL reimbursement_ReceptionSharingBLL;

        public ReimbursementController(IBusinessTripBLL businessTripBLL, IAccountBLL accountBLL, IEmployeeInformationBLL employeeInformationBLL, IDepartmentBLL departmentBLL, IOtherReimburseBLL otherReimburseBLL, IOtherReimburse_DetailsBLL otherReimburse_DetailsBLL, ITravelReimbursementBLL travelReimbursementBLL, ITravelReimbursement_DetailsBLL travelReimbursement_DetailsBLL, IBusinessTrip_TypeBLL businessTrip_TypeBLL, IRegionBLL regionBLL, IDistributorBLL distributorBLL, IStoreBLL storeBLL, IReimbursement_BusinessReceptionBLL reimbursement_BusinessReceptionBLL, IReimbursement_ReceptionSharingBLL reimbursement_ReceptionSharingBLL)
        {
            this.businessTripBLL = businessTripBLL ?? throw new ArgumentNullException(nameof(businessTripBLL));
            this.accountBLL = accountBLL ?? throw new ArgumentNullException(nameof(accountBLL));
            this.employeeInformationBLL = employeeInformationBLL ?? throw new ArgumentNullException(nameof(employeeInformationBLL));
            this.departmentBLL = departmentBLL ?? throw new ArgumentNullException(nameof(departmentBLL));
            this.otherReimburseBLL = otherReimburseBLL ?? throw new ArgumentNullException(nameof(otherReimburseBLL));
            this.otherReimburse_DetailsBLL = otherReimburse_DetailsBLL ?? throw new ArgumentNullException(nameof(otherReimburse_DetailsBLL));
            this.travelReimbursementBLL = travelReimbursementBLL ?? throw new ArgumentNullException(nameof(travelReimbursementBLL));
            this.travelReimbursement_DetailsBLL = travelReimbursement_DetailsBLL ?? throw new ArgumentNullException(nameof(travelReimbursement_DetailsBLL));
            this.businessTrip_TypeBLL = businessTrip_TypeBLL ?? throw new ArgumentNullException(nameof(businessTrip_TypeBLL));
            this.regionBLL = regionBLL ?? throw new ArgumentNullException(nameof(regionBLL));
            this.distributorBLL = distributorBLL ?? throw new ArgumentNullException(nameof(distributorBLL));
            this.storeBLL = storeBLL ?? throw new ArgumentNullException(nameof(storeBLL));
            this.reimbursement_BusinessReceptionBLL = reimbursement_BusinessReceptionBLL ?? throw new ArgumentNullException(nameof(reimbursement_BusinessReceptionBLL));
            this.reimbursement_ReceptionSharingBLL = reimbursement_ReceptionSharingBLL ?? throw new ArgumentNullException(nameof(reimbursement_ReceptionSharingBLL));
        }



        // GET: Reimbursement
        public ActionResult Index()
        {
            return View();
        }

        #region 出差报销
        /// <summary>
        /// 出差报销历史记录页
        /// </summary>
        /// <returns></returns>
        public ActionResult TravelReimbursementHistoryView(string searchString, PagerInBase pagerInBase)
        {
            if (BuildTravelReimbursementModels(0) == null)
            {
                return View();
            }
            //获得数据
            var models = BuildTravelReimbursementModels(0).OrderByDescending(p => p.ID).ToList();
            //查看出差信息

            //搜索数据
            var query = searchString != null ?
                models.Where(t => t.部门.Contains(searchString)).ToList() :
                models.ToList();
            //默认倒叙排列
            query = query.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            var count = models.Count;
            var res = new PagerResult<TravelReimbursementViewModel>
            {
                Code = 0,
                DataList = data,
                Total = count,
                PageSize = pagerInBase.PageSize,
                PageIndex = pagerInBase.PageIndex,
                RequestUrl = pagerInBase.RequetUrl
            };
            return View(res);

        }

        /// <summary>
        /// 出差-报销申请
        /// </summary>
        /// <param name="id">出差</param>
        /// <returns></returns>
        public ActionResult ApplyTravelReimbursementView(int id)
        {
            Session["View"] = true;
            var models = travelReimbursementBLL.GetModels(p => p.关联出差ID == id);
            if (models.Count() > 0)
            {
                foreach (var item in models)
                {
                    if (item.是否作废 != true)
                    {
                        return Content("<script>alert('当前出差数据已提交报销！');window.history.go(-1);</script>");
                    }

                }

            }
            var ac = GetAccountInfo();

            TravelReimbursementViewModel travelReimbursementViewModel = new TravelReimbursementViewModel();
            travelReimbursementViewModel.关联出差ID = id;
            travelReimbursementViewModel.报销人 = ac.姓名;
            travelReimbursementViewModel.报销人职务 = ac.岗位;
            travelReimbursementViewModel.部门 = departmentBLL.GetModel(p => p.ID == ac.部门).名称;
            var model = BuildBusinessTrip(id);
            travelReimbursementViewModel.出差日期 = model.开始时间.ToShortDateString() + "至" + model.结束时间.ToShortDateString();
            travelReimbursementViewModel.出差事由 = model.出差内容;


            return View(travelReimbursementViewModel);
        }

        /// <summary>
        /// 出差报销提交操作
        /// </summary>
        /// <returns></returns>
        public ActionResult TravelReimbursementApply(TravelReimbursementViewModel travelReimbursementViewModel)
        {
            if (Session["View"].ToString() == "false")
            {
                return Content("<script>alert('重复操作！');window.history.go(-1);</script>");
            }
            var ac = GetAccountInfo();
            财务_出差报销单 model = new 财务_出差报销单();
            model.借款金额 = travelReimbursementViewModel.借款金额;
            model.审核状态 = travelReimbursementViewModel.审核状态;
            model.应退补金额 = travelReimbursementViewModel.应退补金额;
            model.报销人ID = ac.用户ID;
            model.关联出差ID = travelReimbursementViewModel.关联出差ID;
            model.提交时间 = DateTime.Now;
            model.汇款账号 = travelReimbursementViewModel.汇款账号;
            model.汇款银行 = travelReimbursementViewModel.汇款银行;
            model.户名 = travelReimbursementViewModel.户名;
            model.部门ID = ac.部门;
            model.交通费合计 = travelReimbursementViewModel.交通费合计;
            model.住宿费合计 = travelReimbursementViewModel.住宿费合计;
            model.合计 = model.合计;
            model.生活补助合计 = travelReimbursementViewModel.生活补助合计;
            model.车船费合计 = travelReimbursementViewModel.车船费合计;

            if (ModelState.IsValid)
            {
                model = travelReimbursementBLL.AddReturnModel(model);
                Session["View"] = false;
            }

            else
            {
                List<string> sb = new List<string>();
                //获取所有错误的Key
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        sb.Add(error.ErrorMessage);
                    }
                }
                string msg = null;
                foreach (var item in sb)
                {
                    msg += item.ToString() + "<br/>";
                }
                return Content("<script>alert('" + msg + "');window.history.go(-1);</script>");
            }
            return Json(model.ID);
        }

        /// <summary>
        /// 出差报销副表提交操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult TravelReimbursement_DetailsApply(string models)
        {
            var lis = JsonConvert.DeserializeObject<List<财务_出差报销详细>>(models);
            var ac = GetAccountInfo();
            if (Session["View"].ToString() == "false")
            {
                return Content("<script>alert('重复操作！');window.history.go(-1);");
            }
            foreach (var item in lis)
            {


                var m = travelReimbursementBLL.GetModel(p => p.ID == item.报销ID);
                if (m == null)
                {
                    return Json(false);
                }
                item.提交人ID = ac.用户ID;
                item.提交时间 = DateTime.Now;

                try
                {
                    var model = travelReimbursement_DetailsBLL.AddReturnModel(item);


                    Session["View"] = false;
                }
                catch (Exception e)
                {

                    return Json(new { success = false, msg = e.Message });
                }
            }
            return Json(new { success = true, msg = "提交成功！" });
        }

        /// <summary>
        /// 出差报销及出差报销详细，撤回
        /// </summary>
        /// <param name="id">出差报销ID</param>
        /// <returns></returns>
        public ActionResult DelTravelReimbursement(int id)
        {
            var model = travelReimbursementBLL.GetModel(p => p.ID == id);
            model.是否作废 = true;
            try
            {
                travelReimbursementBLL.Modify(model);

            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "提交成功!" });
        }
        /// <summary>
        /// 出差报销详细信息
        /// </summary>
        /// <param name="id">出差报销ID</param>
        /// <param name="role">当前角色()</param>
        /// <returns></returns>
        public ActionResult TravelReimbursementDetailsInfoView(int id, bool? role)
        {
            Session["View"] = true;
            if (role == true)
            {
                var ac = GetAccountInfo();
                //if (ac.部门 != 3)
                //{
                //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                //}
            }
            var m = travelReimbursementBLL.GetModel(p => p.ID == id);
            var model = BuildTravelReimbursementModel(m);
            ViewBag.ReCheck = role;
            return View(model);
        }

        /// <summary>
        /// 出差报销对象
        /// </summary>
        /// <returns></returns>
        public ActionResult TravelReimbursementType()
        {
            var lis = BusinessTrip_TypeViewModels();
            return Json(lis);
        }
        #endregion

        #region 商务接待报销


        /// <summary>
        /// 商务接待报销首页
        /// </summary>
        /// <param name="searchString">根据城市查询</param>
        /// <param name="pagerInBase"></param>
        /// <returns></returns>
        public ActionResult BusinessRec_IndexView(string searchString, PagerInBase pagerInBase, string role)
        {
            ViewBag.ReCheck = role;
            if (role == "true")
            {
                var ac = GetAccountInfo();
                //if (ac.部门 != 3)
                //{
                //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                //}
            }
            var models = BuildBusinessRecReimburseModels();
            //获得数据

            //搜索数据
            var query = searchString != null ?
                models.Where(t => t.城市.Contains(searchString)).ToList() :
                models.ToList();
            //默认倒叙排列
            query = query.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            var count = models.Count;
            var res = new PagerResult<BusinessRecReimburseViewModel>
            {
                Code = 0,
                DataList = data,
                Total = count,
                PageSize = pagerInBase.PageSize,
                PageIndex = pagerInBase.PageIndex,
                RequestUrl = pagerInBase.RequetUrl
            };
            return View(res);

        }

        /// <summary>
        /// 商务接待报销增加
        /// </summary>
        /// <param name="id">商务接待ID</param>
        /// <returns></returns>
        public ActionResult BusinessRec_AddView(int id)
        {
            var ac = GetAccountInfo();
            BusinessRecReimburseViewModel model = new BusinessRecReimburseViewModel();
            model.提交人ID = ac.用户ID;
            model.提交人 = ac.姓名;
            model.接待计划ID = id;
            return View(model);
        }

        /// <summary>
        ///商务接待报销提交
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BusinessRec_Add(BusinessRecReimburseViewModel model)
        {
            var ac = GetAccountInfo();
            财务_接待报销 m = new 财务_接待报销();
            m.事由 = model.事由;
            m.城市 = model.城市;
            m.接待计划ID = model.接待计划ID;
            m.提交人ID = ac.用户ID;
            m.提交时间 = DateTime.Now;
            m.时间 = model.时间;
            m.更新人ID = ac.用户ID;
            m.更新时间 = DateTime.Now;
            m.经销商 = model.经销商;
            m.金额 = model.金额;
            try
            {
                m = reimbursement_BusinessReceptionBLL.AddReturnModel(m);
            }
            catch (Exception e)
            {

                return Content("<script>alert('" + e.Message + "');window.history.go(-1);</script>");
            }
            return Json(new { id = m.ID });
        }

        /// <summary>
        /// 商务接待报销修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BusinessRec_Edit(BusinessRecReimburseViewModel model)
        {

            var ac = GetAccountInfo();
            财务_接待报销 m = new 财务_接待报销();
            m = reimbursement_BusinessReceptionBLL.GetModel(p => p.ID == model.ID);
            m.事由 = model.事由;
            m.城市 = model.城市;
            m.时间 = model.时间;
            m.更新人ID = ac.用户ID;
            m.更新时间 = DateTime.Now;
            m.经销商 = model.经销商;
            m.金额 = model.金额;
            try
            {
                reimbursement_BusinessReceptionBLL.Modify(m);
            }
            catch (Exception e)
            {

                return Content("<script>alert('" + e.Message + "');window.history.go(-1);</script>");
            }
            return Json(new { id = m.ID });
        }

        /// <summary>
        /// 商务接待详情页
        /// </summary>
        /// <param name="id">商务接待报销ID</param>
        ///<param name="isEdit">是否修改</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BusinessRec_InfoView(int id, bool? isEdit, bool? role)
        {
            if (role == true)
            {
                var ac = GetAccountInfo();
                //if (ac.部门 !=3)
                //{
                //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                //}
            }

            ViewBag.IsEdit = isEdit;
            ViewBag.ReCheck = role;
            BusinessRecReimburseViewModel model = new BusinessRecReimburseViewModel();
            model.ID = id;


            return View(model);
        }

        /// <summary>
        /// 获取商务接待报销JSON数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetBusinessRec_Info(int id)
        {
            try
            {
                var model = reimbursement_BusinessReceptionBLL.GetModel(p => p.ID == id);
                if (model != null)
                {
                    var m = BuildBusinessRecReimburseModel(model);
                    m.RecSharingList = reimbursement_ReceptionSharingBLL.GetModels(p => p.接待报销ID == m.ID).ToList();

                    return Json(new { success = true, msg = "查询成功", data = m }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, msg = "获取数据失败!该数据不存在！", data = 0 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "获取数据失败!" + e.Message, data = 0 }, JsonRequestBehavior.AllowGet);
            }


        }

        /// <summary>
        /// 接待报销分摊添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BusinessRecShare_Add(财务_接待报销分摊 model)
        {
            try
            {
                model = reimbursement_ReceptionSharingBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "提交成功！", data = model.ID });
        }

        /// <summary>
        /// 接待报销分摊编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BusinessRecShare_Edit(财务_接待报销分摊 model)
        {
            try
            {
                reimbursement_ReceptionSharingBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！", data = model });
        }

        /// <summary>
        /// 接待报销分摊删除
        /// </summary>
        /// <param name="id">接待报销分摊ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BusinessRecShare_Del(int id)
        {
            try
            {
                reimbursement_ReceptionSharingBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "删除成功！" }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 其他报销
        /// <summary>
        /// 其它报销申请历史记录页
        /// </summary>
        /// <returns></returns>
        public ActionResult OtherReimbursementHistoryView(string searchString, PagerInBase pagerInBase)
        {
            Session["View"] = true;
            var ac = GetAccountInfo();

            //获得数据
            var models = BuildOtherReimburses(0).OrderByDescending(p => p.ID).ToList();
            //搜索数据
            var query = searchString != null ?
                models.Where(t => t.汇款银行.Contains(searchString)).ToList() :
                models.ToList();
            //默认倒叙排列

            query = query.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            var count = models.Count;
            var res = new PagerResult<OtherReimburseViewModel>
            {
                Code = 0,
                DataList = data,
                Total = count,
                PageSize = pagerInBase.PageSize,
                PageIndex = pagerInBase.PageIndex,
                RequestUrl = pagerInBase.RequetUrl
            };
            return View(res);

        }

        /// <summary>
        /// 其它报销申请页
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyOtherReimbursementView()
        {
            Session["View"] = true;
            OtherReimburseViewModel model = new OtherReimburseViewModel();
            var ac = GetAccountInfo();
            var d = departmentBLL.GetModel(p => p.ID == ac.部门).名称;
            model.报销部门 = d;
            model.报销人 = ac.姓名;
            return View(model);
        }

        /// <summary>
        /// 其它报销申请提交操作
        /// </summary>
        /// <returns></returns>
        public ActionResult OtherReimbursementAdd(OtherReimburseViewModel model)
        {
            if (Session["View"].ToString() == "false")
            {
                return Content("<script>alert('重复操作！');window.history.go(-1);</script>");
            }
            var ac = GetAccountInfo();
            财务_其它报销 OtherReimbursementModel = new 财务_其它报销();
            OtherReimbursementModel.原借款 = model.原借款;
            OtherReimbursementModel.应退余款 = model.应退余款;
            OtherReimbursementModel.报销人ID = ac.用户ID;
            OtherReimbursementModel.报销部门ID = ac.部门;
            OtherReimbursementModel.提交日期 = DateTime.Now;
            OtherReimbursementModel.汇款银行 = model.汇款银行;
            OtherReimbursementModel.汇款银行卡账号 = model.汇款银行卡账号;

            if (ModelState.IsValid)
            {
                OtherReimbursementModel = otherReimburseBLL.AddReturnModel(OtherReimbursementModel);
                Session["View"] = false;
            }

            else
            {
                List<string> sb = new List<string>();
                //获取所有错误的Key
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        sb.Add(error.ErrorMessage);
                    }
                }
                string msg = null;
                foreach (var item in sb)
                {
                    msg += item.ToString() + "<br/>";
                }
                return Content("<script>alert('" + msg + "');window.history.go(-1);</script>");
            }
            return Json(OtherReimbursementModel.ID);
        }

        /// <summary>
        ///  其它报销申请—详细提交操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult OtherReimbursement_DetailsAdd(财务_其他报销_副表 OtherReimbursementModel)
        {
            if (Session["View"].ToString() == "false")
            {
                return Content("<script>alert('重复操作！');window.history.go(-1);</script>");
            }
            var model = otherReimburseBLL.GetModel(p => p.ID == OtherReimbursementModel.其他报销ID);

            if (model == null)
            {
                return Json(false);
            }
            OtherReimbursementModel.其他报销ID = OtherReimbursementModel.其他报销ID;
            OtherReimbursementModel.提交时间 = DateTime.Now;

            try
            {

                OtherReimbursementModel = otherReimburse_DetailsBLL.AddReturnModel(OtherReimbursementModel);

                Session["View"] = false;
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }

            return Json(OtherReimbursementModel);

        }

        /// <summary>
        /// 删除 其它报销详细清单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelReimbursement_Details(int id)
        {
            if (id > 0)
            {
                try
                {
                    otherReimburse_DetailsBLL.DelBy(p => p.ID == id);
                    // travelReimbursement_DetailsBLL.DelBy(p => p.ID == id);
                }
                catch (Exception e)
                {

                    return Json(new { success = false, msg = e.Message });
                }

            }
            else
            {
                return Json(new { success = false, msg = "删除失败！" });
            }
            return Json(new { success = true, msg = "删除成功！" });
        }
        /// <summary>
        /// 其他报销详情页
        /// </summary>
        /// <param name="id">其他报销主表ID</param>
        /// <returns></returns>
        public ActionResult OtherReimbursement_DetailsView(int id, string role)
        {
            Session["View"] = true;
            if (role == "true")
            {
                var ac = GetAccountInfo();
                //if (ac.部门 !=3)
                //{
                //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                //}
            }
            if (id <= 0)
            {
                return Content("<script>alert('未查到数据！');window.history.go(-1);</script>"); ;
            }

            var model = otherReimburseBLL.GetModel(p => p.ID == id);
            if (model == null)
            {
                return Content("<script>alert('未查到具体数据！');window.history.go(-1);</script>");
            }
            ViewBag.ReCheck = role;
            return View(BuildOtherReimburse(model));
        }
        #endregion

        /// <summary>
        /// 财务审核页面(出差报销)
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckReimbursementView(PagerInBase pagerInBase, string searchString)
        {
            Session["View"] = true;
            var ac = GetAccountInfo();
            //if (ac.部门!=3 )
            //{
            //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
            //}
            List<TravelReimbursementViewModel> models = new List<TravelReimbursementViewModel>();
            models = BuildTravelReimbursementModels(1);

            if (models.Count() == 0)
            {
                return View();
            }
            //获得数据
            var lis = models.OrderByDescending(p => p.ID).ToList();

            //搜索数据
            var query = searchString != null ?
                lis.Where(t => t.部门.Contains(searchString)).ToList() :
                lis.ToList();
            //默认倒叙排列
            query = query.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            var count = models.Count;
            var res = new PagerResult<TravelReimbursementViewModel>
            {
                Code = 0,
                DataList = data,
                Total = count,
                PageSize = pagerInBase.PageSize,
                PageIndex = pagerInBase.PageIndex,
                RequestUrl = pagerInBase.RequetUrl
            };
            return View(res);

        }

        // <summary>
        /// 财务审核页面(其他报销)
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckOtherReimbursementView(string searchString, PagerInBase pagerInBase)
        {

            Session["View"] = true;
            var ac = GetAccountInfo();
            //if (ac.部门 != 3)
            //{
            //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
            //}
            var models = BuildOtherReimburses(1);
            if (models.Count() == 0)
            {
                return View();
            }
            //获得数据
            var lis = models.OrderByDescending(p => p.ID).ToList();

            //搜索数据
            var query = searchString != null ?
                lis.Where(t => t.报销人.Contains(searchString)).ToList() :
                lis.ToList();
            //默认倒叙排列
            query = query.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            var count = models.Count;
            var res = new PagerResult<OtherReimburseViewModel>
            {
                Code = 0,
                DataList = data,
                Total = count,
                PageSize = pagerInBase.PageSize,
                PageIndex = pagerInBase.PageIndex,
                RequestUrl = pagerInBase.RequetUrl
            };
            return View(res);


        }
        /// <summary>
        /// 财务审核操作
        /// </summary>
        /// <param name="dataType">审核的数据类型（0|1|2=》出差报销|接待报销|其他报销）</param>
        /// <param name="id">数据ID</param>
        /// <param name="result">审核结果</param>
        /// <returns></returns>
        public ActionResult ReimbursementCheck(int dataType, int id, string result)
        {
            var ac = GetAccountInfo();
            //if (ac.部门 != 3)
            //{
            //    return Content("<script>alert('您无权操作页面！');window.history.go(-1);</script>");
            //}
            if (dataType == 0)//出差报销
            {
                var model = travelReimbursementBLL.GetModel(p => p.ID == id);
                string[] property = new string[1];
                property[0] = "审核状态";
                if (result == "通过")
                {
                    model.审核状态 = true;

                }
                if (result == "驳回")
                {
                    model.审核状态 = false;
                }
                try
                {
                    travelReimbursementBLL.Modify(model, property);
                }
                catch (Exception)
                {

                    return Content("<script>alert('提交数据出错！');window.history.go(-1);</script>");
                }

                return RedirectToAction("CheckReimbursementView");



            }
            if (dataType == 1)
            {
                var model = reimbursement_BusinessReceptionBLL.GetModel(p => p.ID == id);
                string[] property = new string[1];
                property[0] = "审核状态";
                if (result == "通过")
                {
                    model.审核状态 = true;

                }
                if (result == "驳回")
                {
                    model.审核状态 = false;
                }
                try
                {
                    reimbursement_BusinessReceptionBLL.Modify(model, property);
                }
                catch (Exception)
                {

                    return Content("<script>alert('提交数据出错！');window.history.go(-1);</script>");
                }

                return RedirectToAction("BusinessRec_IndexView", new { role = true });

            }


            if (dataType == 2)//其它报销申请
            {
                var model = otherReimburseBLL.GetModel(p => p.ID == id);
                if (model == null)
                {
                    return Content("<script>alert('数据库不存在该数据！');window.history.go(-1);</script>");
                }
                if (result == "通过")
                {
                    model.审核状态 = true;
                }
                if (result == "驳回")
                {
                    model.审核状态 = false;
                }

                string[] property = new string[1];
                property[0] = "审核状态";
                try
                {
                    otherReimburseBLL.Modify(model, property);
                }
                catch (Exception e)
                {

                    return Content("<script>alert('提交数据出错！" + e.Message + "');window.history.go(-1);</script>");
                }

                return RedirectToAction("CheckOtherReimbursementView");
            }
            return View();
        }



        /// <summary>
        /// 构造其它报销数据集
        /// </summary>
        /// <param name="role">当前角色（0|1=》普通用户|审查）</param>
        /// <returns></returns>
        private List<OtherReimburseViewModel> BuildOtherReimburses(int role)
        {
            List<财务_其它报销> model = new List<财务_其它报销>();
            var ac = GetAccountInfo();
            if (role == 0)
            {
                model = otherReimburseBLL.GetModels(p => p.报销人ID == ac.用户ID).ToList();
            }
            else if (role == 1)
            {
                model = otherReimburseBLL.GetModels(p => true).ToList();
            }
            else
            {
                return null;
            }
            List<OtherReimburseViewModel> otherReimburseViewModels = new List<OtherReimburseViewModel>();
            foreach (var item in model)
            {
                OtherReimburseViewModel otherReimburseViewModel = new OtherReimburseViewModel();
                otherReimburseViewModel = BuildOtherReimburse(item);
                if (otherReimburseViewModel != null)
                {
                    otherReimburseViewModels.Add(otherReimburseViewModel);
                }

            }

            return otherReimburseViewModels;
        }

        /// <summary>
        /// 构造其它报销单个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private OtherReimburseViewModel BuildOtherReimburse(财务_其它报销 model)
        {
            if (model == null)
            {
                return null;
            }
            var reimburseModels = otherReimburse_DetailsBLL.GetModels(p => p.其他报销ID == model.ID);

            if (reimburseModels.Count() == 0)
            {
                return null;
            }
            OtherReimburseViewModel otherReimburseViewModel = new OtherReimburseViewModel();
            try
            {

                otherReimburseViewModel.ID = model.ID;
                otherReimburseViewModel.会计审核人ID = model.会计审核人ID;
                if (model.会计审核人ID > 0)
                {
                    otherReimburseViewModel.会计审核人 = accountBLL.GetModel(p => p.ID == model.会计审核人ID).姓名;
                }

                otherReimburseViewModel.原借款 = model.原借款;
                otherReimburseViewModel.审核状态 = model.审核状态;
                otherReimburseViewModel.应退余款 = model.应退余款;
                otherReimburseViewModel.报销人ID = model.报销人ID.Value;

                otherReimburseViewModel.报销人 = accountBLL.GetModel(p => p.ID == model.报销人ID).姓名;
                otherReimburseViewModel.报销部门ID = model.报销部门ID.Value;
                otherReimburseViewModel.报销部门 = departmentBLL.GetModel(p => p.ID == model.报销部门ID).名称;
                otherReimburseViewModel.提交日期 = model.提交日期.Value;
                otherReimburseViewModel.汇款银行 = model.汇款银行;
                otherReimburseViewModel.汇款银行卡账号 = model.汇款银行卡账号;

                List<OtherReimburse_DetailsViewModel> therReimburse_DetailsViewModels = new List<OtherReimburse_DetailsViewModel>();
                foreach (var item in reimburseModels)
                {
                    OtherReimburse_DetailsViewModel otherReimburse_DetailsViewModel = new OtherReimburse_DetailsViewModel();
                    otherReimburse_DetailsViewModel.ID = item.ID;
                    otherReimburse_DetailsViewModel.其他报销ID = item.其他报销ID;
                    otherReimburse_DetailsViewModel.提交时间 = item.提交时间;
                    otherReimburse_DetailsViewModel.核算分类ID = item.核算分类ID;
                    otherReimburse_DetailsViewModel.用途 = item.用途;
                    otherReimburse_DetailsViewModel.金额 = item.金额.Value;
                    therReimburse_DetailsViewModels.Add(otherReimburse_DetailsViewModel);
                }
                otherReimburseViewModel.财务_其他报销_副表 = therReimburse_DetailsViewModels;
            }
            catch (Exception e)
            {

                return null;
            }
            return otherReimburseViewModel;
        }

        /// <summary>
        /// 构造出差报销实体集
        /// </summary>
        /// <param name="role">0|1(0为普通，1为审查)</param>
        /// <returns></returns>
        private List<TravelReimbursementViewModel> BuildTravelReimbursementModels(int role)
        {
            var ac = GetAccountInfo();
            List<TravelReimbursementViewModel> travelReimbursementViewModels = new List<TravelReimbursementViewModel>();
            List<财务_出差报销单> models = new List<财务_出差报销单>();
            if (role == 0)//查看自己的
            {
                models = travelReimbursementBLL.GetModels(p => p.报销人ID == ac.用户ID).ToList();
            }
            if (role == 1)//财务审核
            {
                models = travelReimbursementBLL.GetModels(p => true).ToList();
            }
            if (models.Count() <= 0)
            {
                return null;
            }
            foreach (var item in models)
            {

                var model = BuildTravelReimbursementModel(item);
                travelReimbursementViewModels.Add(model);
            }
            return travelReimbursementViewModels;
        }

        /// <summary>
        /// 构造出差报销单单个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private TravelReimbursementViewModel BuildTravelReimbursementModel(财务_出差报销单 model)
        {

            if (model == null)
            {
                return null;
            }
            TravelReimbursementViewModel reimbursementViewModel = new TravelReimbursementViewModel();
            reimbursementViewModel.ID = model.ID;
            reimbursementViewModel.借款金额 = model.借款金额;
            reimbursementViewModel.审核状态 = model.审核状态;
            reimbursementViewModel.应退补金额 = model.应退补金额;
            reimbursementViewModel.报销人ID = model.报销人ID.Value;
            reimbursementViewModel.报销人 = accountBLL.GetModel(p => p.ID == model.报销人ID.Value).姓名;
            reimbursementViewModel.关联出差ID = model.关联出差ID.Value;
            reimbursementViewModel.提交时间 = model.提交时间.Value;
            reimbursementViewModel.汇款账号 = model.汇款账号;
            reimbursementViewModel.汇款银行 = model.汇款银行;
            reimbursementViewModel.户名 = model.户名;
            if (model.财务审核人ID != null)
            {
                reimbursementViewModel.财务审核人ID = model.财务审核人ID.Value;
            }
            reimbursementViewModel.合计 = model.合计;
            reimbursementViewModel.生活补助合计 = model.生活补助合计;
            reimbursementViewModel.交通费合计 = model.交通费合计;
            reimbursementViewModel.住宿费合计 = model.住宿费合计;
            reimbursementViewModel.车船费合计 = model.车船费合计;
            if (model.是否作废 != null)
            {
                reimbursementViewModel.是否作废 = model.是否作废.Value;
            }
            else
            {
                reimbursementViewModel.是否作废 = false;
            }
            var trip = businessTripBLL.GetModel(p => p.ID == model.关联出差ID);
            reimbursementViewModel.出差事由 = trip.出差内容;
            reimbursementViewModel.出差日期 = trip.开始时间.ToString("yyyy-mm-dd") + "至" + trip.结束时间.ToString("yyyy-mm-dd");

            if (model.财务审核人ID != null)
            {
                reimbursementViewModel.财务审核人 = accountBLL.GetModel(p => p.ID == model.财务审核人ID).姓名;
            }
            reimbursementViewModel.部门ID = model.部门ID.Value;
            reimbursementViewModel.部门 = departmentBLL.GetModel(p => p.ID == model.部门ID).名称;
            if (travelReimbursement_DetailsBLL.GetModel(p => p.报销ID == model.ID) != null)
            {
                reimbursementViewModel.财务_出差报销详细 = travelReimbursement_DetailsBLL.GetModels(p => p.报销ID == model.ID).ToList();
            }
            else
            {
                reimbursementViewModel.财务_出差报销详细 = null;

            }

            return reimbursementViewModel;
        }

        /// <summary>
        /// 构建单个出差基本信息
        /// </summary>
        /// <param name="id">出差记录ID</param>
        /// <returns></returns>
        private BusinessTripViewModel BuildBusinessTrip(int? id)
        {
            BusinessTripViewModel businessTripViewModel = new BusinessTripViewModel();
            HR_出差计划 model = new HR_出差计划();
            var account = GetAccountInfo();
            if (id > 0)
            {
                model = businessTripBLL.GetModel(p => p.ID == id);
            }
            else
            {
                model = businessTripBLL.GetModel(p => p.提交人ID == account.用户ID);
            }
            if (model == null)
            {
                return businessTripViewModel = null;
            }
            businessTripViewModel.ID = model.ID;
            businessTripViewModel.关联审核人ID = model.关联审核人ID;
            businessTripViewModel.关联审核人 = accountBLL.GetModel(p => p.ID == model.关联审核人ID).姓名;
            businessTripViewModel.出差内容 = model.出差内容;
            businessTripViewModel.审核状态 = model.审核状态;
            businessTripViewModel.开始时间 = model.开始时间;
            businessTripViewModel.提交时间 = model.提交时间;
            businessTripViewModel.结束时间 = model.结束时间;
            businessTripViewModel.部门ID = account.部门;
            businessTripViewModel.提交人ID = model.提交人ID;
            businessTripViewModel.提交人 = accountBLL.GetModel(p => p.ID == model.提交人ID).姓名;
            return businessTripViewModel;

        }

        /// <summary>
        /// 构造出差对象类型
        /// </summary>
        /// <returns></returns>
        private List<BusinessTrip_TypeViewModel> BusinessTrip_TypeViewModels()
        {

            var lis = businessTrip_TypeBLL.GetModels(p => true);
            List<BusinessTrip_TypeViewModel> businessTrip_TypeViewModels = new List<BusinessTrip_TypeViewModel>();
            foreach (var item in lis)
            {
                BusinessTrip_TypeViewModel businessTrip_TypeViewModel = new BusinessTrip_TypeViewModel();
                businessTrip_TypeViewModel.ID = item.ID;
                businessTrip_TypeViewModel.对象类型 = item.对象类型;
                businessTrip_TypeViewModel.数据表名 = item.数据表名;
                businessTrip_TypeViewModels.Add(businessTrip_TypeViewModel);
            }
            return businessTrip_TypeViewModels;
        }

        /// <summary>
        /// 构造单个商务接待报销
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private BusinessRecReimburseViewModel BuildBusinessRecReimburseModel(财务_接待报销 model)
        {
            BusinessRecReimburseViewModel businessRec = new BusinessRecReimburseViewModel();
            businessRec.ID = model.ID;
            businessRec.事由 = model.事由;
            businessRec.城市 = model.城市;
            businessRec.审核状态 = model.审核状态;
            businessRec.接待计划ID = model.接待计划ID;
            businessRec.提交人ID = model.提交人ID;
            businessRec.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID).姓名;
            businessRec.提交时间 = model.提交时间;
            businessRec.时间 = model.时间;
            businessRec.更新人ID = model.更新人ID;
            businessRec.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID).姓名;
            businessRec.更新时间 = model.更新时间;
            businessRec.经销商 = model.经销商;
            businessRec.财务审核人ID = model.财务审核人ID;
            if (businessRec.财务审核人 != null)
            {
                businessRec.财务审核人 = employeeInformationBLL.GetModel(p => p.用户ID == model.财务审核人ID).姓名;
            }

            businessRec.金额 = model.金额;
            return businessRec;
        }

        /// <summary>
        /// 构建商务接待报销集合
        /// </summary>
        /// <returns></returns>
        private List<BusinessRecReimburseViewModel> BuildBusinessRecReimburseModels()
        {
            var ac = GetAccountInfo();
            var models = reimbursement_BusinessReceptionBLL.GetModels(p => p.提交人ID == ac.用户ID);
            List<BusinessRecReimburseViewModel> lis = new List<BusinessRecReimburseViewModel>();
            foreach (var item in models)
            {
                var model = BuildBusinessRecReimburseModel(item);
                lis.Add(model);

            }
            return lis;
        }

        /// <summary>
        /// 构造单个接待数据
        /// </summary>
        /// <param name="id">接待ID</param>
        /// <returns></returns>
        private BusinessReceiving_RecordsViewModel BuildReceivingRecord(营销_接待计划 model)
        {

            BusinessReceiving_RecordsViewModel receivingRecord = new BusinessReceiving_RecordsViewModel();
            receivingRecord.ID = model.ID;
            receivingRecord.主要沟通内容 = model.主要沟通内容;
            receivingRecord.入住酒店 = model.入住酒店;
            receivingRecord.审核人ID = model.审核人ID;
            if (model.审核人ID != null)
            {
                receivingRecord.审核人 = employeeInformationBLL.GetModel(p => p.用户ID == model.审核人ID).姓名;

            }
            receivingRecord.审核状态 = model.审核状态;

            receivingRecord.抵达日期 = model.抵达日期;
            receivingRecord.提交人ID = model.提交人ID;
            receivingRecord.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID).姓名;
            receivingRecord.提交日期 = model.提交日期;
            receivingRecord.更新人ID = model.更新人ID.Value;
            receivingRecord.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID.Value).姓名;
            receivingRecord.更新日期 = model.更新日期.Value;
            receivingRecord.申请人ID = model.申请人ID;
            receivingRecord.申请人 = employeeInformationBLL.GetModel(p => p.用户ID == model.申请人ID).姓名;
            receivingRecord.申请日期 = model.申请日期;
            receivingRecord.离开日期 = model.离开日期;
            receivingRecord.航班号 = model.航班号;
            receivingRecord.费用总预算 = model.费用总预算;
            receivingRecord.部门 = model.部门;
            receivingRecord.配合部门 = model.配合部门;
            return receivingRecord;

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

        /// <summary>
        /// 获取公司员工信息
        /// </summary>
        /// <returns></returns>
        private List<CompanyStaffViewModel> GetCompanyStaff()
        {
            List<CompanyStaffViewModel> companyStaffViewModel = new List<CompanyStaffViewModel>();

            var accounts = accountBLL.GetModels(p => true).ToList();
            var employees = employeeInformationBLL.GetModels(p => true).ToList();
            for (int i = 0; i < accounts.Count(); i++)
            {
                HR_人员信息表 m = new HR_人员信息表();
                try
                {
                    var id = accounts.ElementAt(i).ID;
                    m = employeeInformationBLL.GetModel(p => p.用户ID == id);
                }
                catch (Exception e)
                {

                    throw e;
                }
                CompanyStaffViewModel ac = new CompanyStaffViewModel();
                if (m != null)
                {
                    ac.ID = m.ID;
                    ac.上级部门 = m.上级部门;
                    ac.姓名 = m.姓名;
                    ac.岗位 = m.岗位;
                    ac.用户ID = m.用户ID;
                    ac.职级 = m.职级;
                    ac.部门 = m.部门;
                    companyStaffViewModel.Add(ac);
                }
            }
            return companyStaffViewModel;
        }
    }
}