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
        private readonly IBusinessRec_CarReimBLL businessRec_CarReimBLL;
        private readonly IBusinessRec_HotelReimBLL businessRec_HotelReimBLL;
        private readonly IHotelReimSharingBLL hotelReimSharingBLL;
        private readonly ICarReim_DetailsBLL carReim_DetailsBLL;
        private readonly ICarReim_SharingBLL carReim_SharingBLL;
        private readonly IBusinessReceptionBLL businessReceptionBLL;
        private readonly IReimbursement_BusinessRecDetailsBLL reimbursement_BusinessRecDetailsBLL;

        public ReimbursementController(IBusinessTripBLL businessTripBLL, IAccountBLL accountBLL, IEmployeeInformationBLL employeeInformationBLL, IDepartmentBLL departmentBLL, IOtherReimburseBLL otherReimburseBLL, IOtherReimburse_DetailsBLL otherReimburse_DetailsBLL, ITravelReimbursementBLL travelReimbursementBLL, ITravelReimbursement_DetailsBLL travelReimbursement_DetailsBLL, IBusinessTrip_TypeBLL businessTrip_TypeBLL, IRegionBLL regionBLL, IDistributorBLL distributorBLL, IStoreBLL storeBLL, IReimbursement_BusinessReceptionBLL reimbursement_BusinessReceptionBLL, IReimbursement_ReceptionSharingBLL reimbursement_ReceptionSharingBLL, IBusinessRec_CarReimBLL businessRec_CarReimBLL, IBusinessRec_HotelReimBLL businessRec_HotelReimBLL, IHotelReimSharingBLL hotelReimSharingBLL, ICarReim_DetailsBLL carReim_DetailsBLL, ICarReim_SharingBLL carReim_SharingBLL, IBusinessReceptionBLL businessReceptionBLL, IReimbursement_BusinessRecDetailsBLL reimbursement_BusinessRecDetailsBLL)
        {
            this.businessTripBLL = businessTripBLL;
            this.accountBLL = accountBLL;
            this.employeeInformationBLL = employeeInformationBLL;
            this.departmentBLL = departmentBLL;
            this.otherReimburseBLL = otherReimburseBLL;
            this.otherReimburse_DetailsBLL = otherReimburse_DetailsBLL;
            this.travelReimbursementBLL = travelReimbursementBLL;
            this.travelReimbursement_DetailsBLL = travelReimbursement_DetailsBLL;
            this.businessTrip_TypeBLL = businessTrip_TypeBLL;
            this.regionBLL = regionBLL;
            this.distributorBLL = distributorBLL;
            this.storeBLL = storeBLL;
            this.reimbursement_BusinessReceptionBLL = reimbursement_BusinessReceptionBLL;
            this.reimbursement_ReceptionSharingBLL = reimbursement_ReceptionSharingBLL;
            this.businessRec_CarReimBLL = businessRec_CarReimBLL;
            this.businessRec_HotelReimBLL = businessRec_HotelReimBLL;
            this.hotelReimSharingBLL = hotelReimSharingBLL;
            this.carReim_DetailsBLL = carReim_DetailsBLL;
            this.carReim_SharingBLL = carReim_SharingBLL;
            this.businessReceptionBLL = businessReceptionBLL;
            this.reimbursement_BusinessRecDetailsBLL = reimbursement_BusinessRecDetailsBLL;
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
        /// 出差报销修改
        /// </summary>
        /// <returns></returns>
        public ActionResult TracelReimbursementEdit(TravelReimbursementViewModel travelReimbursementViewModel) {
            var ac = GetAccountInfo();
            财务_出差报销单 model = new 财务_出差报销单();
            try
            {

            model = travelReimbursementBLL.GetModel(p=>p.ID==travelReimbursementViewModel.ID);
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
          
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg=e.Message});
            }
            return Json(new { success=true,msg="提交成功！"});
        }

        /// <summary>
        /// 出差报销副表提交操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult TravelReimbursement_DetailsApply(财务_出差报销详细 model)
        {
            
            var ac = GetAccountInfo();
            


                var m = travelReimbursementBLL.GetModel(p => p.ID == model.报销ID);
                if (m == null)
                {
                    return Json(new { success = false, msg = "添加失败！"});
                }
            model.提交人ID = ac.用户ID;
            model.提交时间 = DateTime.Now;

                try
                {
                   model = travelReimbursement_DetailsBLL.AddReturnModel(model);


                    Session["View"] = false;
                }
                catch (Exception e)
                {

                    return Json(new { success = false, msg = e.Message });
                }
           
            return Json(new { success = true, msg = "提交成功！",data=model });
        }


        /// <summary>
        /// 出差报销主表ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TravelReimbursement_DetailsInfo(int id) {
            List<财务_出差报销详细> models = new List<财务_出差报销详细>();
            try
            {
               var  model = travelReimbursement_DetailsBLL.GetModels(p => p.报销ID == id);
                if (model!=null)
                {
                    models = model.ToList();
                }
                else
                {
                    return Json(new { success = false, msg = "暂无出差报销详细数据！请添加！" });
                }
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg=e.Message});
            }
            return Json(new { success = true, msg = "查询成功！", data = models });



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
        public ActionResult TravelReimbursementDetailsInfoView(int id,bool isEdit, bool? role)
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
            ViewBag.IsEdit = isEdit;
            return View(model);
        }
        /// <summary>
        /// 出差报销副表修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult TravelReimbursement_DetailsEdit(财务_出差报销详细 model) {
            try
            {
                travelReimbursement_DetailsBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success=false ,msg=e.Message});
            }
            return Json(new { success = true, msg = "修改成功！", data = model });


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
        /// 商务接待列表
        /// </summary>
        /// <param name="role">是否是审核</param>
        /// <param name="searchString">姓名查找</param>
        /// <param name="pagerInBase"></param>
        /// <returns></returns>
        public ActionResult BusinessRec_ReimIndexView(bool role, string searchString, PagerInBase pagerInBase)
        {
            ViewBag.ReCheck = role;//是否是审核
            List<BusinessReceiving_RecordsViewModel> models = new List<BusinessReceiving_RecordsViewModel>();
            if (role == true)
            {
                var ac = GetAccountInfo();
                if (ac.部门 != 3)
                {
                    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                }

            }
            models = BuildReceivingRecords(role);
            if (models == null)
            {
                var res = new PagerResult<BusinessReceiving_RecordsViewModel>
                {
                    Code = 0,
                    DataList = null,
                    Total = 0,
                    PageSize = pagerInBase.PageSize,
                    PageIndex = pagerInBase.PageIndex,
                    RequestUrl = pagerInBase.RequetUrl
                };
                return View(res);
            }
            else
            { //搜索数据
                var query = searchString != null ?
                    models.Where(t => t.提交人.Contains(searchString)).ToList() :
                    models.ToList();
                //默认倒叙排列
                query = query.OrderByDescending(p => p.ID).ToList();
                //分页数据
                var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

                var count = models.Count;
                var res = new PagerResult<BusinessReceiving_RecordsViewModel>
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

        }

        /// <summary>
        /// 商务接待报销-其它报销页面
        /// </summary>
        /// <param name="recID">商务接待ID</param>
        /// <param name="searchString">根据城市查询</param>
        /// <param name="pagerInBase"></param>
        /// <returns></returns>
        public ActionResult BusinessRec_IndexView(int recID, string searchString, PagerInBase pagerInBase, bool role)
        {
            ViewBag.ReCheck = role;
            ViewBag.recID = recID;
            List<BusinessRecReimburseViewModel> models = new List<BusinessRecReimburseViewModel>();
            if (role == true)
            {
                var ac = GetAccountInfo();
                //if (ac.部门 != 3)
                //{
                //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                //}

            }
            models = BuildBusinessRecReimburseModels(recID);
            //获得数据
            if (models == null)
            {
                var res = new PagerResult<BusinessRecReimburseViewModel>
                {
                    Code = 0,
                    DataList = null,
                    Total = 0,
                    PageSize = pagerInBase.PageSize,
                    PageIndex = pagerInBase.PageIndex,
                    RequestUrl = pagerInBase.RequetUrl
                };
                return View(res);
            }
            else
            {
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
            //搜索数据


        }

        /// <summary>
        /// 商务接待报销增加
        /// </summary>
        /// <param name="id">商务接待ID</param>
        /// <returns></returns>
        public ActionResult BusinessRec_AddView(int id)
        {
            var ac = GetAccountInfo();
            var mo = reimbursement_BusinessReceptionBLL.GetModels(p => p.接待计划ID == id).FirstOrDefault();
            if (mo != null)
            {
                return Content("<script> alert('当前接待已有数据不可添加！'); window.history.go(-1);</script> ");
            }
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

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "添加成功！", data = m });
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

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！", data = m });
        }

        /// <summary>
        /// 商务接待详情页
        /// </summary>
        /// <param name="id">商务接待ID</param>
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
            var mo = reimbursement_BusinessReceptionBLL.GetModels(p => p.接待计划ID == id).FirstOrDefault();
            if (mo != null)
            {
                ViewBag.IsHave = true;//是否已经存在数据
            }
            BusinessRecReimburseViewModel model = new BusinessRecReimburseViewModel();
            model.接待计划ID = id;


            return View(model);
        }

        /// <summary>
        /// 获取商务接待报销JSON数据
        /// </summary>
        /// <param name="id">接待计划ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetBusinessRec_Info(int id)
        {
            try
            {
                var model = reimbursement_BusinessReceptionBLL.GetModel(p => p.接待计划ID == id);
                if (model != null)
                {
                    var m = BuildBusinessRecReimburseModel(model);
                    m.RecSharingList = reimbursement_ReceptionSharingBLL.GetModels(p => p.接待报销ID == m.ID).ToList();

                    return Json(new { success = true, msg = "查询成功！", data = m }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, msg = "不存在当前接待的接待报销，请添加！", data = 0 }, JsonRequestBehavior.AllowGet);
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

        /// <summary>
        /// 接待报销明细查询
        /// </summary>
        /// <param name="id">接待报销ID</param>
        /// <returns></returns>
        public ActionResult BusinessRecReim_DetailsGet(int id) {
            List<BusinessRecReim_DetailsViewModel> models = new List<BusinessRecReim_DetailsViewModel>();
            try
            {
               models = BuildBusinessRecReim_DetailsModels(id);
                if (models.Count()<=0)
                {
                    return Json(new { success = true, msg = "当前接待报销明细为空！" });
                }
            }
            catch (Exception e)
            {

                return Json(new { success=true,msg=e.Message});
            }
            
            return Json(new { success = true, msg = "查询成功！", data = models });
        }

        /// <summary>
        /// 添加接待报销明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessRecReim_DetailsAdd(BusinessRecReim_DetailsViewModel model) {

            var ac = GetAccountInfo();
            财务_接待报销明细 recReim_DetailsViewModel = new 财务_接待报销明细();
            
            recReim_DetailsViewModel.事由 = model.事由;
            recReim_DetailsViewModel.城市 = model.城市;
            recReim_DetailsViewModel.接待报销ID = model.接待报销ID;
            recReim_DetailsViewModel.提交人ID = ac.用户ID;
            
            recReim_DetailsViewModel.提交日期 = DateTime.Now;
            recReim_DetailsViewModel.日期 = model.日期;
            recReim_DetailsViewModel.更新人ID =ac.用户ID;
            recReim_DetailsViewModel.更新日期 = DateTime.Now;
            recReim_DetailsViewModel.身份 = model.身份;
            recReim_DetailsViewModel.费用 = model.费用;
            recReim_DetailsViewModel.姓名 = model.姓名;
            try
            {
                recReim_DetailsViewModel =  reimbursement_BusinessRecDetailsBLL.AddReturnModel(recReim_DetailsViewModel);
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg=e.Message}) ;
            }
            return Json(new { success = true, msg = "添加成功！", data = recReim_DetailsViewModel });
        }

        /// <summary>
        /// 修改接待报销明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessRecReim_DetailsEdit(BusinessRecReim_DetailsViewModel model)
        {

            var ac = GetAccountInfo();
            财务_接待报销明细 recReim_DetailsViewModel = new 财务_接待报销明细();
            recReim_DetailsViewModel = reimbursement_BusinessRecDetailsBLL.GetModel(p => p.ID == model.ID);
            recReim_DetailsViewModel.事由 = model.事由;
            recReim_DetailsViewModel.城市 = model.城市;
            recReim_DetailsViewModel.接待报销ID = model.接待报销ID;
           
            recReim_DetailsViewModel.日期 = model.日期;
            recReim_DetailsViewModel.更新人ID = ac.用户ID;
            recReim_DetailsViewModel.更新日期 = DateTime.Now;
            recReim_DetailsViewModel.身份 = model.身份;
            recReim_DetailsViewModel.费用 = model.费用;
            recReim_DetailsViewModel.姓名 = model.姓名;
            try
            {
                reimbursement_BusinessRecDetailsBLL.Modify(recReim_DetailsViewModel);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！", data = recReim_DetailsViewModel });
        }

        /// <summary>
        /// 接待报销明细删除
        /// </summary>
        /// <param name="id">接待报销明细ID</param>
        /// <returns></returns>
        public ActionResult BusinessRecReim_DetailsDel(int id)
        {

           
            try
            {
                 reimbursement_BusinessRecDetailsBLL.DelBy(p=>p.ID==id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "删除成功！"});
        }


        #endregion


        #region 接待住宿报销
        /// <summary>
        /// 住宿报销列表
        /// </summary>
        /// <param name="id">商务接待ID</param>
        /// <param name="searchString"></param>
        /// <param name="pagerInBase"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult HotelReimIndexView(int id, string searchString, PagerInBase pagerInBase, bool role)
        {
            ViewBag.ReCheck = role;
            ViewBag.id = id;
            List<HotelReimViewModel> models = new List<HotelReimViewModel>();
            if (role == true)
            {
                var ac = GetAccountInfo();

                //if (ac.部门 != 3)
                //{
                //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                //}
                //               else
                //{

                //}
            }
            var lis = businessRec_HotelReimBLL.GetModels(p => p.商务接待ID == id);
            if (lis.Count() > 0)//是否为空
            {
                foreach (var item in lis)
                {
                    var model = BuildHotelReimViewModel(item);
                    models.Add(model);
                }

                //获得数据

                //搜索数据
                var query = searchString != null ?
                    models.Where(t => t.住宿人员.Contains(searchString)).ToList() :
                    models.ToList();
                //默认倒叙排列
                query = query.OrderByDescending(p => p.ID).ToList();
                //分页数据
                var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

                var count = models.Count;
                var res = new PagerResult<HotelReimViewModel>
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

            else
            {
                var res = new PagerResult<HotelReimViewModel>
                {
                    Code = 0,
                    DataList = null,
                    Total = 0,
                    PageSize = pagerInBase.PageSize,
                    PageIndex = pagerInBase.PageIndex,
                    RequestUrl = pagerInBase.RequetUrl
                };
                return View(res);
            }
        }

        /// <summary>
        /// 添加住宿报销主表视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult HotelReimAddView(int id)
        {
            HotelReimViewModel model = new HotelReimViewModel();
            model.商务接待ID = id;
            return View(model);

        }

        /// <summary>
        /// 住宿报销主表添加操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult HotelReimAdd(HotelReimViewModel model)
        {
            var ac = GetAccountInfo();
            财务_接待住宿报销 m = new 财务_接待住宿报销();
            m.住宿人员 = model.住宿人员;
            m.住宿时间 = model.住宿时间;
            m.商务接待ID = model.商务接待ID;
            m.提交人ID = ac.用户ID;
            m.提交时间 = DateTime.Now;
            m.更新人ID = ac.用户ID;
            m.更新日期 = DateTime.Now;
            m.部门 = employeeInformationBLL.GetModel(p => p.用户ID == ac.用户ID).部门;
            m.酒店名称 = model.酒店名称;
            m.金额 = model.金额;
            try
            {
                m = businessRec_HotelReimBLL.AddReturnModel(m);
            }
            catch (Exception e)
            {

                return Content(e.Message);
            }
            return RedirectToAction("HotelReimInfoView", new { id = m.商务接待ID, isEdit = false, role=false });
        }

        /// <summary>
        /// 住宿报销详情
        /// </summary>
        /// <param name="id">商务接待IDID</param>
        /// <returns></returns>
        public ActionResult HotelReimInfoView(int id, bool isEdit, bool role)
        {
            var model = businessRec_HotelReimBLL.GetModels(p => p.商务接待ID == id).FirstOrDefault();
            HotelReimViewModel m = new HotelReimViewModel();
            if (model != null)
            {
                ViewBag.IsHave = true;//是否已存在住宿报销
                m = BuildHotelReimViewModel(model);
            }
            ViewBag.IsEdit = isEdit;
            ViewBag.Role = role;

            m.商务接待ID = id;
            return View(m);
        }

        /// <summary>
        /// 住宿报销主表修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult HotelReimInfoEdit(HotelReimViewModel model)
        {
            var ac = GetAccountInfo();
            var m = businessRec_HotelReimBLL.GetModel(p => p.ID == model.ID);
            m.住宿人员 = model.住宿人员;
            m.住宿时间 = model.住宿时间;
            m.更新人ID = ac.用户ID;
            m.更新日期 = DateTime.Now;
            m.酒店名称 = model.酒店名称;
            m.金额 = model.金额;
            try
            {
                businessRec_HotelReimBLL.Modify(m);
            }
            catch (Exception e)
            {

                return Content("<script>alert('" + e.Message + "');window.history.go(-1);</script>");
            }
            return RedirectToAction("HotelReimInfoView", new { id = model.商务接待ID, isEdit = false,role=false });

        }
        /// <summary>
        /// 住宿报销分摊查询
        /// </summary>
        /// <param name="id">住宿报销ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HotelReimSharingJson(int id)
        {

            var models = BuildHotelReimSharingViewModels(id);

            return Json(new { data = models });
        }

        /// <summary>
        /// 住宿报销分摊添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HotelReimSharingAdd(HotelReimSharingViewModel model)
        {
            var ac = GetAccountInfo();
            财务_接待住宿报销分摊 m = new 财务_接待住宿报销分摊();
            m.住宿人员 = model.住宿人员;
            m.住宿开始时间 = model.住宿开始时间;
            m.住宿截止时间 = model.住宿截止时间;
            m.住宿房间 = model.住宿房间;
            m.分摊金额 = model.分摊金额;
            m.城市 = model.城市;
            m.备注 = model.备注;
            m.对象类型 = model.对象类型;
            m.接待住宿报销ID = model.接待住宿报销ID;
            m.提交人ID = ac.用户ID;
            m.提交时间 = DateTime.Now;
            m.更新人ID = ac.用户ID;
            m.更新时间 = DateTime.Now;
            try
            {
                m = hotelReimSharingBLL.AddReturnModel(m);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "提交成功！", data = m });
        }

        /// <summary>
        /// 住宿报销分摊修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HotelReimSharingEdit(HotelReimSharingViewModel model)
        {
            var ac = GetAccountInfo();
            var m = hotelReimSharingBLL.GetModel(p => p.ID == model.ID);
            m.住宿人员 = model.住宿人员;
            m.住宿开始时间 = model.住宿开始时间;
            m.住宿截止时间 = model.住宿截止时间;
            m.住宿房间 = model.住宿房间;
            m.分摊金额 = model.分摊金额;
            m.城市 = model.城市;
            m.备注 = model.备注;
            m.对象类型 = model.对象类型;
            m.更新人ID = ac.用户ID;
            m.更新时间 = DateTime.Now;
            try
            {
                hotelReimSharingBLL.Modify(m);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！", data = m });
        }

        /// <summary>
        /// 住宿报销分摊删除
        /// </summary>
        /// <param name="id">住宿报销分摊ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HotelReimSharingDel(int id)
        {
            try
            {
                hotelReimSharingBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "删除成功！" });
        }
        #endregion



        #region 接待车辆报销
        /// <summary>
        /// 车辆报销列表
        /// </summary>
        /// <param name="id">商务接待ID</param>
        /// <param name="searchString">按提交人姓名查找</param>
        /// <param name="pagerInBase"></param>
        /// <param name="role">是否审查角色</param>
        /// <returns></returns>
        public ActionResult CarReimIndexView(int id, string searchString, PagerInBase pagerInBase, bool role)
        {
            ViewBag.ReCheck = role;
            ViewBag.id = id;
            List<CarReimViewModel> models = new List<CarReimViewModel>();
            if (role == true)
            {
                var ac = GetAccountInfo();

                //if (ac.部门 != 3)
                //{
                //    return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
                //}
                //               else
                //{

                //}
            }
            models = BuildCarReimModels(id);
            if (models.Count() > 0)//是否为空
            {

                //获得数据

                //搜索数据
                var query = searchString != null ?
                    models.Where(t => t.提交人.Contains(searchString)).ToList() :
                    models.ToList();
                //默认倒叙排列
                query = query.OrderByDescending(p => p.ID).ToList();
                //分页数据
                var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

                var count = models.Count;
                var res = new PagerResult<CarReimViewModel>
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

            else
            {
                var res = new PagerResult<CarReimViewModel>
                {
                    Code = 0,
                    DataList = null,
                    Total = 0,
                    PageSize = pagerInBase.PageSize,
                    PageIndex = pagerInBase.PageIndex,
                    RequestUrl = pagerInBase.RequetUrl
                };
                return View(res);
            }
        }

        /// <summary>
        /// 车辆报销详情页
        /// </summary>
        /// <param name="id">商务接待ID</param>
        /// <param name="isEdit">是否修改</param>
        /// <param name="role">是否审查角色</param>
        /// <returns></returns>
        public ActionResult CarReimInfoView(int id, bool isEdit, bool role)
        {
            if (role == true)
            {
                //var ac = GetAccountInfo();
                //if (ac.部门!=3)
                //{
                //    return Content(" <script> alert('您无权进入当前页面！'); window.history.go(-1);</script> ");
                //}
            }
            var model = businessRec_CarReimBLL.GetModels(p => p.商务接待ID == id).FirstOrDefault();
            CarReimViewModel m = new CarReimViewModel();
            if (model != null)
            {
                ViewBag.IsHave = true;
                m = BuildCarReimModel(model);
            }

            ViewBag.IsEdit = isEdit;
            ViewBag.Role = role;
            m.商务接待ID = id;
            return View(m);

        }

        /// <summary>
        /// 车辆报销主表添加页面
        /// </summary>
        /// <param name="id">商务接待ID</param>
        /// <returns></returns>
        public ActionResult CarReimAddView(int id)
        {
            CarReimViewModel carReimViewModel = new CarReimViewModel();
            carReimViewModel.商务接待ID = id;
            return View(carReimViewModel);

        }

        /// <summary>
        /// 车辆报销主表添加操作
        /// </summary>
        /// <param name="carReimViewModel"></param>
        /// <returns></returns>

        public ActionResult CarReimAdd(CarReimViewModel carReimViewModel)
        {
            var ac = GetAccountInfo();
            财务_接待车辆报销 model = new 财务_接待车辆报销();
            model.商务接待ID = carReimViewModel.商务接待ID;
            model.备注 = carReimViewModel.备注;
            model.总计花费金额 = carReimViewModel.总计花费金额;
            model.提交人ID = ac.用户ID;
            model.提交时间 = DateTime.Now;
            model.搭乘人员 = carReimViewModel.搭乘人员;
            model.更新人ID = ac.用户ID;
            model.更新时间 = DateTime.Now;
            model.用车开始时间 = carReimViewModel.用车开始时间;
            model.用车截止时间 = carReimViewModel.用车截止时间;
            model.车辆数 = carReimViewModel.车辆数;
            try
            {
                model = businessRec_CarReimBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "添加成功！", data = model });
        }

        /// <summary>
        /// 车辆报销主表修改操作
        /// </summary>
        /// <param name="carReimViewModel"></param>
        /// <returns></returns>
        public ActionResult CarReimEdit(CarReimViewModel carReimViewModel)
        {
            var model = businessRec_CarReimBLL.GetModel(p => p.ID == carReimViewModel.ID);
            var ac = GetAccountInfo();
            model.商务接待ID = carReimViewModel.商务接待ID;
            model.备注 = carReimViewModel.备注;
            model.总计花费金额 = carReimViewModel.总计花费金额;

            model.搭乘人员 = carReimViewModel.搭乘人员;
            model.更新人ID = ac.用户ID;
            model.更新时间 = DateTime.Now;
            model.用车开始时间 = carReimViewModel.用车开始时间;
            model.用车截止时间 = carReimViewModel.用车截止时间;
            model.车辆数 = carReimViewModel.车辆数;
            try
            {
                businessRec_CarReimBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！", data = model });
        }

        /// <summary>
        /// 车辆报销明细查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CarReimDetailsInfo(int id)
        {
            var models = BuildCarReimDetailsModels(id);
            return Json(models);

        }

        /// <summary>
        /// 车辆报销
        /// </summary>
        /// <param name="carReimDetails"></param>
        /// <returns></returns>
        public ActionResult CarReimDetailsAdd(CarReimDetailsViewModel carReimDetails)
        {
            var ac = GetAccountInfo();
            财务_车辆报销明细 model = new 财务_车辆报销明细();
            model.备注 = carReimDetails.备注;
            model.报销类型 = carReimDetails.报销类型;
            model.更新人ID = ac.用户ID;
            model.更新时间 = DateTime.Now;
            model.车辆报销ID = carReimDetails.车辆报销ID;
            model.金额 = carReimDetails.金额;
            try
            {
                model = carReim_DetailsBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "添加成功！", data = model });
        }

        /// <summary>
        /// 车辆报销明细修改
        /// </summary>
        /// <param name="carReimDetails"></param>
        /// <returns></returns>
        public ActionResult CarReimDetailsEdit(CarReimDetailsViewModel carReimDetails)
        {
            var ac = GetAccountInfo();
            var model = carReim_DetailsBLL.GetModel(p => p.ID == carReimDetails.ID);
            model.备注 = carReimDetails.备注;
            model.报销类型 = carReimDetails.报销类型;
            model.更新人ID = ac.用户ID;
            model.更新时间 = DateTime.Now;
            model.车辆报销ID = carReimDetails.车辆报销ID;
            model.金额 = carReimDetails.金额;
            try
            {
                carReim_DetailsBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！", data = model });
        }

        /// <summary>
        /// 车辆报销明细删除
        /// </summary>
        /// <param name="id">车辆报销明细ID</param>
        /// <returns></returns>
        public ActionResult CarReimDetailsDel(int id)
        {
            try
            {
                carReim_DetailsBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "提交成功！" });
        }

        /// <summary>
        /// 车辆报销分摊数据
        /// </summary>
        /// <param name="id">车辆报销ID</param>
        /// <returns></returns>
        public ActionResult CarReimSharingInfo(int id)
        {
            var models = carReim_SharingBLL.GetModels(p => p.车辆报销ID == id);
            return Json(new { success = true, msg = "查询成功！", data = models });
        }

        /// <summary>
        /// 车辆报销分摊修改
        /// </summary>
        /// <param name="carReimSharing"></param>
        /// <returns></returns>
        public ActionResult CarReimSharingEdit(CarReimSharingViewModel carReimSharing)
        {
            var model = carReim_SharingBLL.GetModel(p => p.ID == carReimSharing.ID);
            var ac = GetAccountInfo();
            model.分摊金额 = carReimSharing.分摊金额;
            model.城市 = carReimSharing.城市;
            model.备注 = carReimSharing.备注;
            model.对象名称 = carReimSharing.对象名称;
            model.对象类型 = carReimSharing.对象类型;
            model.更新人ID = ac.用户ID;
            model.更新时间 = DateTime.Now;
            model.车辆报销ID = carReimSharing.车辆报销ID;
            try
            {
                carReim_SharingBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功!", data = model });
        }

        /// <summary>
        /// 车辆报销分摊添加操作
        /// </summary>
        /// <param name="carReimSharing"></param>
        /// <returns></returns>
        public ActionResult CarReimSharingAdd(CarReimSharingViewModel carReimSharing)
        {
            财务_车辆报销分摊 model = new 财务_车辆报销分摊();
            var ac = GetAccountInfo();

            model.分摊金额 = carReimSharing.分摊金额;
            model.城市 = carReimSharing.城市;
            model.备注 = carReimSharing.备注;
            model.对象名称 = carReimSharing.对象名称;
            model.对象类型 = carReimSharing.对象类型;
            model.更新人ID = ac.用户ID;
            model.更新时间 = DateTime.Now;
            model.车辆报销ID = carReimSharing.车辆报销ID;
            try
            {
                model = carReim_SharingBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "添加成功!", data = model });
        }

        /// <summary>
        /// 车辆报销分摊删除
        /// </summary>
        /// <param name="id">车辆报销分摊ID</param>
        /// <returns></returns>
        public ActionResult CarReimSharingDel(int id)
        {

            try
            {
                carReim_SharingBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "删除成功！" });
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

        #region 财务审核

        /// <summary>
        /// 财务审核页面(出差报销)
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckReimbursementView(PagerInBase pagerInBase, string searchString)
        {
            Session["View"] = true;
            var ac = GetAccountInfo();
            if (ac.部门 != 3)
            {
                return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
            }
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
            if (ac.部门 != 3)
            {
                return Content("<script>alert('您无权进入当前页面！');window.history.go(-1);</script>");
            }
            var models = BuildOtherReimburses(1);
            if (models.Count() == 0)
            {
                
                var res = new PagerResult<OtherReimburseViewModel>
                {
                    Code = 0,
                    DataList = null,
                    Total = 0,
                    PageSize = pagerInBase.PageSize,
                    PageIndex = pagerInBase.PageIndex,
                    RequestUrl = pagerInBase.RequetUrl
                };
                return View(res);
            }
            else { 
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

        }
        /// <summary>
        /// 财务审核操作
        /// </summary>
        /// <param name="dataType">审核的数据类型（0|1|2|3|4=》出差报销|接待报销|其他报销|住宿报销|车辆报销审核）</param>
        /// <param name="id">数据ID</param>
        /// <param name="result">审核结果</param>
        /// <returns></returns>
        public ActionResult ReimbursementCheck(int dataType, int id, bool result)
        {
            var ac = GetAccountInfo();
            if (ac.部门 != 3)
            {
                return Content("<script>alert('您无权操作页面！');window.history.go(-1);</script>");
            }
            if (dataType == 0)//出差报销
            {
                var model = travelReimbursementBLL.GetModel(p => p.ID == id);
                string[] property = new string[2];
                property[0] = "审核状态";
                property[1] = "财务审核人ID";
                model.审核状态 = result;
                model.财务审核人ID = ac.用户ID;
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
            if (dataType == 1)//接待报销审核
            {
                var model = reimbursement_BusinessReceptionBLL.GetModel(p => p.ID == id);
                string[] property = new string[2];
                property[0] = "审核状态";
                property[1] = "财务审核人ID";
                model.审核状态 = result;
                model.财务审核人ID = ac.用户ID;
                try
                {
                    reimbursement_BusinessReceptionBLL.Modify(model, property);
                }
                catch (Exception)
                {

                    return Content("<script>alert('提交数据出错！');window.history.go(-1);</script>");
                }

                return RedirectToAction("BusinessRec_InfoView", new {id=model.接待计划ID, role = true });

            }
            if (dataType == 3)//住宿报销审核
            {
                var model = businessRec_HotelReimBLL.GetModel(p => p.ID == id);
                string[] property = new string[2];
                property[0] = "审核状态";
                property[1] = "财务审核人ID";
                model.审核状态 = result;
                model.财务审核人ID = ac.用户ID;

                try
                {
                    businessRec_HotelReimBLL.Modify(model, property);
                }
                catch (Exception)
                {

                    return Content("<script>alert('提交数据出错！');window.history.go(-1);</script>");
                }

                return RedirectToAction("HotelReimInfoView", new { id=model.商务接待ID,isEdit=false,role = true });

            }
            if (dataType == 4)//车辆报销审核
            {
                var model = businessRec_CarReimBLL.GetModel(p => p.ID == id);
                string[] property = new string[2];
                property[0] = "审核状态";
                property[1] = "财务审核人ID";

                model.审核状态 = result;
                model.财务审核人ID = ac.用户ID;


                try
                {
                    businessRec_CarReimBLL.Modify(model, property);
                }
                catch (Exception)
                {

                    return Content("<script>alert('提交数据出错！');window.history.go(-1);</script>");
                }

                return RedirectToAction("CarReimInfoView", new { id= model.商务接待ID,isEdit=false, role = true });

            }

            if (dataType == 2)//其它报销申请
            {
                var model = otherReimburseBLL.GetModel(p => p.ID == id);
                if (model == null)
                {
                    return Content("<script>alert('数据库不存在该数据！');window.history.go(-1);</script>");
                }

                model.审核状态 = result;
                model.会计审核人ID = ac.用户ID;


                string[] property = new string[2];
                property[0] = "审核状态";
                property[1] = "会计审核人ID";
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


        #endregion




        #region 出差及其他报销数据
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
        #endregion

        #region 商务接待及商务接待报销
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
        private List<BusinessRecReimburseViewModel> BuildBusinessRecReimburseModels(int recID)
        {
            var ac = GetAccountInfo();
            List<财务_接待报销> models = new List<财务_接待报销>();


            var m = reimbursement_BusinessReceptionBLL.GetModels(p => p.接待计划ID == recID);
            if (m.Count() <= 0)
            {
                return null;
            }
            models = m.ToList();


            List<BusinessRecReimburseViewModel> lis = new List<BusinessRecReimburseViewModel>();
            foreach (var item in models)
            {
                var model = BuildBusinessRecReimburseModel(item);
                lis.Add(model);

            }
            return lis;
        }

        /// <summary>
        /// 构建接待报销明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private BusinessRecReim_DetailsViewModel BuildBusinessRecReim_DetailsModel(财务_接待报销明细 model) {

            BusinessRecReim_DetailsViewModel recReim_DetailsViewModel = new BusinessRecReim_DetailsViewModel();
            recReim_DetailsViewModel.ID = model.ID;
            recReim_DetailsViewModel.事由 = model.事由;
            recReim_DetailsViewModel.城市 = model.城市;
            recReim_DetailsViewModel.接待报销ID = model.接待报销ID;
            recReim_DetailsViewModel.提交人ID = model.提交人ID;
            recReim_DetailsViewModel.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID).姓名;
            recReim_DetailsViewModel.提交日期 = model.提交日期;
            recReim_DetailsViewModel.日期 = model.日期;
            recReim_DetailsViewModel.更新人ID = model.更新人ID;
            recReim_DetailsViewModel.更新日期 = model.更新日期;
            recReim_DetailsViewModel.身份 = model.身份;
            recReim_DetailsViewModel.费用 = model.费用;
            recReim_DetailsViewModel.姓名 = model.姓名;
            return recReim_DetailsViewModel;
        }

        /// <summary>
        /// 接待报销明细数据集
        /// </summary>
        /// <param name="id">接待报销ID</param>
        /// <returns></returns>
        private List<BusinessRecReim_DetailsViewModel> BuildBusinessRecReim_DetailsModels(int id) {
            var models = reimbursement_BusinessRecDetailsBLL.GetModels(p => p.接待报销ID == id);
            List<BusinessRecReim_DetailsViewModel> lis = new List<BusinessRecReim_DetailsViewModel>();
            foreach (var item in models)
            {
                var model = BuildBusinessRecReim_DetailsModel(item);
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
        /// 构造接待数据集
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private List<BusinessReceiving_RecordsViewModel> BuildReceivingRecords(bool role)
        {
            List<营销_接待计划> models = new List<营销_接待计划>();
            var ac = GetAccountInfo();
            if (role == true)
            {
                var m = businessReceptionBLL.GetModels(p => true);
                if (m.Count() <= 0)
                {
                    return null;
                }
                models = m.ToList();
            }
            else
            {
                var m = businessReceptionBLL.GetModels(p => p.提交人ID == ac.用户ID);
                if (m.Count() <= 0)
                {
                    return null;
                }
                models = m.ToList();
            }
            List<BusinessReceiving_RecordsViewModel> lis = new List<BusinessReceiving_RecordsViewModel>();
            foreach (var item in models)
            {
                var model = BuildReceivingRecord(item);
                lis.Add(model);
            }
            return lis;
        }
        #endregion

        #region 住宿报销数据构建
        /// <summary>
        /// 构造单个住宿报销数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private HotelReimViewModel BuildHotelReimViewModel(财务_接待住宿报销 model)
        {
            HotelReimViewModel hotelReimViewModel = new HotelReimViewModel();
            hotelReimViewModel.ID = model.ID;
            hotelReimViewModel.住宿人员 = model.住宿人员;
            hotelReimViewModel.住宿时间 = model.住宿时间;
            hotelReimViewModel.商务接待ID = model.商务接待ID;
            hotelReimViewModel.提交人ID = model.提交人ID;
            hotelReimViewModel.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID).姓名;
            hotelReimViewModel.提交时间 = model.提交时间;
            hotelReimViewModel.更新人ID = model.更新人ID;
            hotelReimViewModel.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID).姓名;
            hotelReimViewModel.更新日期 = model.更新日期;
            hotelReimViewModel.部门 = model.部门;
            hotelReimViewModel.酒店名称 = model.酒店名称;
            hotelReimViewModel.金额 = model.金额;
            hotelReimViewModel.审核状态 = model.审核状态;
            hotelReimViewModel.财务审核人ID = model.财务审核人ID;
            if (hotelReimViewModel.财务审核人ID != null)
            {
                hotelReimViewModel.财务审核人 = employeeInformationBLL.GetModel(p => p.用户ID == model.财务审核人ID).姓名;
            }
            return hotelReimViewModel;
        }



        /// <summary>
        /// 构建单个住宿报销分摊数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private HotelReimSharingViewModel BuildHotelReimSharingViewModel(财务_接待住宿报销分摊 model)
        {
            HotelReimSharingViewModel hotelReimSharing = new HotelReimSharingViewModel();
            hotelReimSharing.ID = model.ID;
            hotelReimSharing.住宿人员 = model.住宿人员;
            hotelReimSharing.住宿开始时间 = model.住宿开始时间;
            hotelReimSharing.住宿截止时间 = model.住宿截止时间;
            hotelReimSharing.住宿房间 = model.住宿房间;
            hotelReimSharing.分摊金额 = model.分摊金额;
            hotelReimSharing.城市 = model.城市;
            hotelReimSharing.备注 = model.备注;
            hotelReimSharing.对象类型 = model.对象类型;
            hotelReimSharing.接待住宿报销ID = model.接待住宿报销ID;
            hotelReimSharing.提交人ID = model.提交人ID;
            hotelReimSharing.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID).姓名;
            hotelReimSharing.提交时间 = model.提交时间;
            hotelReimSharing.更新人ID = model.更新人ID;
            hotelReimSharing.更新时间 = model.更新时间;
            hotelReimSharing.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID).姓名;
            return hotelReimSharing;

        }

        /// <summary>
        /// 构建住宿报销分摊数据集
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private List<HotelReimSharingViewModel> BuildHotelReimSharingViewModels(int id)
        {
            var ac = GetAccountInfo();
            List<HotelReimSharingViewModel> lis = new List<HotelReimSharingViewModel>();


            var li = hotelReimSharingBLL.GetModels(p => p.接待住宿报销ID == id);


            if (li.Count() <= 0)
            {
                return null;
            }
            foreach (var item in li)
            {
                var model = BuildHotelReimSharingViewModel(item);
                lis.Add(model);
            }
            return lis;
        }
        #endregion


        #region 车辆报销数据构建
        /// <summary>
        /// 构造 车辆报销主表单个数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CarReimViewModel BuildCarReimModel(财务_接待车辆报销 model)
        {
            CarReimViewModel carReim = new CarReimViewModel();
            carReim.ID = model.ID;
            carReim.商务接待ID = model.商务接待ID;
            carReim.备注 = model.备注;
            carReim.总计花费金额 = model.总计花费金额;
            carReim.提交人ID = model.提交人ID;
            carReim.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID).姓名;
            carReim.提交时间 = model.提交时间;
            carReim.搭乘人员 = model.搭乘人员;
            carReim.更新人ID = model.更新人ID;
            carReim.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID).姓名;
            carReim.更新时间 = model.更新时间;
            carReim.用车开始时间 = model.用车开始时间;
            carReim.用车截止时间 = model.用车截止时间;
            carReim.车辆数 = model.车辆数;
            carReim.财务审核人ID = model.财务审核人ID;
            carReim.审核状态 = model.审核状态;
            if (carReim.财务审核人ID != null)
            {
                carReim.财务审核人 = employeeInformationBLL.GetModel(p => p.用户ID == carReim.财务审核人ID).姓名;

            }
            return carReim;
        }

        /// <summary>
        /// 构造车辆报销数据集
        /// </summary>
        /// <param name="id">商务接待ID</param>
        /// <returns></returns>
        private List<CarReimViewModel> BuildCarReimModels(int id)
        {
            var models = businessRec_CarReimBLL.GetModels(p => p.商务接待ID == id);
            List<CarReimViewModel> lis = new List<CarReimViewModel>();
            foreach (var item in models)
            {
                var m = BuildCarReimModel(item);
                lis.Add(m);
            }
            return lis;
        }

        /// <summary>
        /// 构造车辆报销明细单个数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CarReimDetailsViewModel BuildCarReimDetailsModel(财务_车辆报销明细 model)
        {
            CarReimDetailsViewModel carReimDetails = new CarReimDetailsViewModel();
            carReimDetails.ID = model.ID;
            carReimDetails.备注 = model.备注;
            carReimDetails.报销类型 = model.报销类型;
            carReimDetails.更新人ID = model.更新人ID;
            carReimDetails.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID).姓名;
            carReimDetails.更新时间 = model.更新时间;
            carReimDetails.车辆报销ID = model.车辆报销ID;
            carReimDetails.金额 = model.金额;
            return carReimDetails;
        }
        /// <summary>
        /// 构造车辆报销明细数据集
        /// </summary>
        /// <param name="id">车辆报销ID</param>
        /// <returns></returns>
        private List<CarReimDetailsViewModel> BuildCarReimDetailsModels(int id)
        {
            var models = carReim_DetailsBLL.GetModels(p => p.车辆报销ID == id);
            List<CarReimDetailsViewModel> lis = new List<CarReimDetailsViewModel>();
            foreach (var item in models)
            {
                var m = BuildCarReimDetailsModel(item);
                lis.Add(m);
            }
            return lis;
        }


        /// <summary>
        /// 构造车辆报销分摊单个数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private CarReimSharingViewModel BuildCarReimSharingModel(财务_车辆报销分摊 model)
        {
            CarReimSharingViewModel carReimSharing = new CarReimSharingViewModel();
            carReimSharing.ID = model.ID;
            carReimSharing.分摊金额 = model.分摊金额;
            carReimSharing.城市 = model.城市;
            carReimSharing.备注 = model.备注;
            carReimSharing.对象名称 = model.对象名称;
            carReimSharing.对象类型 = model.对象类型;
            carReimSharing.更新人ID = model.更新人ID;
            carReimSharing.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID).姓名;
            carReimSharing.更新时间 = model.更新时间;
            carReimSharing.车辆报销ID = model.车辆报销ID;
            return carReimSharing;
        }

        /// <summary>
        /// 构造车辆报销数据集
        /// </summary>
        /// <param name="id">车辆报销ID</param>
        /// <returns></returns>
        private List<CarReimSharingViewModel> BuildCarReimSharingModels(int id)
        {
            var models = carReim_SharingBLL.GetModels(p => p.车辆报销ID == id);
            List<CarReimSharingViewModel> lis = new List<CarReimSharingViewModel>();
            foreach (var item in models)
            {
                var m = BuildCarReimSharingModel(item);
                lis.Add(m);
            }
            return lis;

        }
        #endregion

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