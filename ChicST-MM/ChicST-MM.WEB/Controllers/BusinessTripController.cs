﻿
using ChicST_MM.Model;
using ChicST_MM.WEB.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JPager.Net;
using System.Web;
using System.IO;
using Microsoft.Win32;
using ChicST_MM.IBLL;
using ChicST_MM.WEB.Utils;
using System.Web.Configuration;
using System.Net.Mail;
using Newtonsoft.Json;

namespace ChicST_MM.WEB.Controllers
{
    public class BusinessTripController : Controller
    {
        private readonly IBusinessTripBLL businessTripBLL;
        private readonly IBusinessTrip_DetailsBLL businessTrip_DetailsBLL;
        private readonly IBusinessTrip_TypeBLL businessTrip_TypeBLL;
        private readonly IRegionBLL regionBLL;
        private readonly IDistributorBLL distributorBLL;
        private readonly IStoreBLL storeBLL;
        private readonly IAccountBLL accountBLL;
        private readonly IEmployeeInformationBLL employeeInformationBLL;
        private readonly IDepartmentBLL departmentBLL;
        private readonly IBusinessTrip_ReportBLL businessTrip_ReportBLL;
        private readonly IBusinessTrip_ReportProofBLL businessTrip_ReportProofBLL;
        private readonly IDevelopment_CityBLL development_CityBLL;
        private readonly IDevelopment_CompetesBLL development_CompetesBLL;
        private readonly IDevelopment_FranchiserBLL development_FranchiserBLL;
        private readonly IDevelopment_MarketBLL development_MarketBLL;
        private readonly IBusinessTrip_VisitSubjectBLL businessTrip_VisitSubjectBLL;

        public BusinessTripController(IBusinessTripBLL businessTripBLL, IBusinessTrip_DetailsBLL businessTrip_DetailsBLL, IBusinessTrip_TypeBLL businessTrip_TypeBLL, IRegionBLL regionBLL, IDistributorBLL distributorBLL, IStoreBLL storeBLL, IAccountBLL accountBLL, IEmployeeInformationBLL employeeInformationBLL, IDepartmentBLL departmentBLL, IBusinessTrip_ReportBLL businessTrip_ReportBLL, IBusinessTrip_ReportProofBLL businessTrip_ReportProofBLL, IDevelopment_CityBLL development_CityBLL, IDevelopment_CompetesBLL development_CompetesBLL, IDevelopment_FranchiserBLL development_FranchiserBLL, IDevelopment_MarketBLL development_MarketBLL, IBusinessTrip_VisitSubjectBLL businessTrip_VisitSubjectBLL)
        {
            this.businessTripBLL = businessTripBLL ?? throw new ArgumentNullException(nameof(businessTripBLL));
            this.businessTrip_DetailsBLL = businessTrip_DetailsBLL ?? throw new ArgumentNullException(nameof(businessTrip_DetailsBLL));
            this.businessTrip_TypeBLL = businessTrip_TypeBLL ?? throw new ArgumentNullException(nameof(businessTrip_TypeBLL));
            this.regionBLL = regionBLL ?? throw new ArgumentNullException(nameof(regionBLL));
            this.distributorBLL = distributorBLL ?? throw new ArgumentNullException(nameof(distributorBLL));
            this.storeBLL = storeBLL ?? throw new ArgumentNullException(nameof(storeBLL));
            this.accountBLL = accountBLL ?? throw new ArgumentNullException(nameof(accountBLL));
            this.employeeInformationBLL = employeeInformationBLL ?? throw new ArgumentNullException(nameof(employeeInformationBLL));
            this.departmentBLL = departmentBLL ?? throw new ArgumentNullException(nameof(departmentBLL));
            this.businessTrip_ReportBLL = businessTrip_ReportBLL ?? throw new ArgumentNullException(nameof(businessTrip_ReportBLL));
            this.businessTrip_ReportProofBLL = businessTrip_ReportProofBLL ?? throw new ArgumentNullException(nameof(businessTrip_ReportProofBLL));
            this.development_CityBLL = development_CityBLL ?? throw new ArgumentNullException(nameof(development_CityBLL));
            this.development_CompetesBLL = development_CompetesBLL ?? throw new ArgumentNullException(nameof(development_CompetesBLL));
            this.development_FranchiserBLL = development_FranchiserBLL ?? throw new ArgumentNullException(nameof(development_FranchiserBLL));
            this.development_MarketBLL = development_MarketBLL ?? throw new ArgumentNullException(nameof(development_MarketBLL));
            this.businessTrip_VisitSubjectBLL = businessTrip_VisitSubjectBLL ?? throw new ArgumentNullException(nameof(businessTrip_VisitSubjectBLL));
            this.businessTrip_VisitSubjectBLL = businessTrip_VisitSubjectBLL;
        }


        /// <summary>
        /// 出差列表页
        /// </summary>
        /// role大于零则为审核人员操作
        /// <returns></returns>
        // GET: BusinessTrip
        [HttpGet]
        public ViewResult Index(int? role, string searchString, PagerInBase pagerInBase)
        {

            Session["View"] = true;
            var model = BuildBusinessTripList(role);

            //根据条件检索
            model = searchString != null ?
                model.Where(t => t.出差人姓名.Contains(searchString)).ToList() :
                model.ToList();

            //默认倒叙排列

            model = model.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = model.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            //总页数
            var count = model.Count;
            var res = new PagerResult<BusinessTripViewModel>
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
        /// 展示出差详细
        /// </summary>
        /// <param name="id">出差记录ID</param>
        /// <returns></returns>
        public ActionResult BusinessTrip_DetailsView(int id, bool? role, bool? isEdit)
        {
            Session["View"] = true;
            var ac = GetAccountInfo();
            HR_出差计划 m = new HR_出差计划();
            if (id > 0)
            {
                m = businessTripBLL.GetModel(p => p.ID == id);
            }
            else
            {
                m = businessTripBLL.GetModel(p => p.提交人ID == ac.用户ID);
            }
            var model = BuildBusinessTrip(m);
            //if (isEdit == true && model.审核状态 == true)
            //{
            //    return Content("<script>alert('该出差已审核通过，不可修改！');window.history.go(-1);</script>");
            //}
            if (model == null)
            {
                return Content("<script>alert('数据查询出错。');window.history.go(-1);</script>");
            }
            ViewBag.Role = role;
            if (role == true)
            {
                if (model.关联审核人ID != ac.用户ID)
                {
                    return Content("<script>alert('非法操作！');window.history.go(-1);</script>");
                }
            }
            ViewBag.IsEdit = isEdit;
            return View(model);


        }
        /// <summary>
        /// 出差主表json数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessTripModel(int id)
        {
            BusinessTripViewModel m = new BusinessTripViewModel();
            try
            {
                var model = businessTripBLL.GetModel(p => p.ID == id);
                m = BuildBusinessTrip(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "查询出差计划主表失败！"+e.Message });
            }


            return Json(new { success = true, msg = "查询成功！", data = m });
        }



        /// <summary>
        /// 出差记录审核页
        /// </summary>
        /// <param name="selectKey"></param>
        /// <param name="orderKey"></param>
        /// <returns></returns>
        public ViewResult BusinerssTrip_CheckView(string searchString, PagerInBase pagerInBase)
        {


            Session["View"] = true;
            var ac = GetAccountInfo();
            var model = BuildBusinessTripList(1);


            //根据条件检索
            model = searchString != null ?
                model.Where(t => t.出差人姓名.Contains(searchString)).ToList() :
                model.ToList();
            model = model.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = model.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            //总页数
            var count = model.Count;

            var res = new PagerResult<BusinessTripViewModel>
            {
                Code = 0,
                DataList = data,
                Total = count,
                PageSize = 10,
                PageIndex = 0,
                RequestUrl = ""
            };
            return View(res);
        }

        /// <summary>
        /// 添加出差记录页面
        /// </summary>
        /// <returns></returns>
        public ViewResult AddBusinessTripView()
        {
            Session["View"] = true;
            var ac = GetAccountInfo();
            BusinessTripViewModel model = new BusinessTripViewModel();
            model.出差人 = ac.用户ID;
            model.出差人姓名 = ac.姓名;
            model.部门ID = ac.部门;
            model.部门 = departmentBLL.GetModel(p => p.ID == ac.部门).名称;
            return View(model);
        }

        /// <summary>
        /// 添加出差记录操作
        /// </summary>
        /// <param name="model">传入出差记录</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessTripAdd(BusinessTripViewModel model)
        {
            if (Session["View"].ToString() == "false")
            {
                return Content("<script>alert('重复操作！');window.history.go(-1);</script>");
            }

            var ac = GetAccountInfo();
            var businessTrip = new HR_出差计划();

           
            businessTrip.住宿费预计 = model.住宿费预计;
            businessTrip.出差人 = ac.用户ID;
            businessTrip.出差内容 = model.出差内容;
            businessTrip.合计 = model.合计;
            businessTrip.备注 = model.备注;
            businessTrip.审核时间 = model.审核时间;
            businessTrip.审核状态 = model.审核状态;
            businessTrip.开始时间 = model.开始时间;
            businessTrip.提交人ID = ac.用户ID;
            businessTrip.提交时间 = DateTime.Now;
            businessTrip.结束时间 = model.结束时间;
            businessTrip.车船费预计 = model.车船费预计;
            businessTrip.部门ID = ac.部门;
            businessTrip.餐补费预计 = model.餐补费预计;
            businessTrip.关联审核人ID = model.关联审核人ID;

            if (ModelState.IsValid)
            {
                businessTrip = businessTripBLL.AddReturnModel(businessTrip);
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
            return RedirectToAction("AddBusinessTrip_DetailsView", new { id = businessTrip.ID , isDetail = false, isEdit = false });
        }

        /// <summary>
        /// 出差记录修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessTripEdit(BusinessTripViewModel model)
        {

            var ac = GetAccountInfo();
            var businessTrip = businessTripBLL.GetModel(p => p.ID == model.ID);

            var em = GetCompanyStaff();
            businessTrip.住宿费预计 = model.住宿费预计;

            businessTrip.出差内容 = model.出差内容;
            businessTrip.合计 = model.合计;
            businessTrip.备注 = model.备注;
            businessTrip.审核时间 = model.审核时间;
            businessTrip.审核状态 = model.审核状态;
            businessTrip.开始时间 = model.开始时间;
            businessTrip.结束时间 = model.结束时间;
            businessTrip.车船费预计 = model.车船费预计;
            businessTrip.餐补费预计 = model.餐补费预计;
            businessTrip.关联审核人ID = model.关联审核人ID;
            try
            {
                businessTripBLL.Modify(businessTrip);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "修改失败！" + e.Message });
            }
            return Json(new { success = true, msg = "修改成功!" });
        }

        /// <summary>
        /// 添加出差计划详细的页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddBusinessTrip_DetailsView(int id,bool isDetail, bool? isEdit)
        {
            Session["View"] = true;
            BusinessTrip_DetailsViewModel model = new BusinessTrip_DetailsViewModel();


            ViewBag.IsDetail = isDetail;
            ViewBag.isEdit = isEdit;
            model.出差记录ID = id;
            var business = businessTripBLL.GetModel(p => p.ID == model.出差记录ID);
            if (business.审核状态 != null)
            {
                return Content("该出差已审核不可操作！");
            }
            var models = BuildBusinessTrip_DetailsViewModels(id);
            ViewBag.ID = id;
            return View(models);
        }

        /// <summary>
        /// 添加出差计划详细
        /// </summary>
        /// <param name="model">传入出差计划详细</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessTrip_DetailsAdd(BusinessTrip_DetailsViewModel model)
        {

            Session["View"] = false;
            var business = businessTripBLL.GetModel(p => p.ID == model.出差记录ID);
            if (business.审核状态 != null)
            {
                return Json(new { success = false, msg = "该出差已审核不可操作！" });
            }
            HR_出差计划详细 businessTrip_Details = new HR_出差计划详细();
            businessTrip_Details.出差记录ID = model.出差记录ID;
            businessTrip_Details.出差时间 = model.出差时间;
            businessTrip_Details.城市ID = model.城市ID;
            businessTrip_Details.同行人员 = model.同行人员;


            businessTrip_Details.备注 = model.备注;
            businessTrip_Details.巡店目的 = model.巡店目的;
       
            businessTrip_Details.计划内容 = model.计划内容;
            businessTrip_Details.计划方案 = model.计划方案;
            businessTrip_Details.提交时间 = DateTime.Now;
            businessTrip_Details.城市ID = model.城市ID;


            if (ModelState.IsValid)
            {
                businessTrip_Details = businessTrip_DetailsBLL.AddReturnModel(businessTrip_Details);
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
                string s = null;
                foreach (var item in sb)
                {
                    s += item.ToString() + "<br/>";
                }
                return Json(new { Success = false, msg = s });
            }

            return Json(new { Success = true, businessid = businessTrip_Details.ID });
        }

        /// <summary>
        /// 出差计划详细修改
        /// </summary>
        /// <param name="model">传入出差计划详细</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessTrip_DetailsEdit(BusinessTrip_DetailsViewModel model)
        {
           
            var business = businessTrip_DetailsBLL.GetModel(p => p.出差记录ID == model.出差记录ID);
            //if (business.审核状态 != null)
            //{
            //    return Json(new { success = false, msg = "该出差已审核不可修改！" });
            //}
            business.ID = model.ID;

            business.出差时间 = model.出差时间;
            business.城市ID = model.城市ID;

            business.同行人员 = model.同行人员;


            business.备注 = model.备注;
            business.巡店目的 = model.巡店目的;


            business.计划内容 = model.计划内容;
            business.计划方案 = model.计划方案;
         
            try
            {
                businessTrip_DetailsBLL.Modify(business);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }

            return Json(new { success = true, msg = "提交成功！", data = model });

        }
        /// <summary>
        /// 出差计划详细删除
        /// </summary>
        /// <param name="id">出差计划详细ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BusinessTrip_DetailsDel(int id)
        {
            try
            {
                businessTrip_DetailsBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "删除成功！" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 出差计划详细查询
        /// </summary>
        /// <param name="id">出差ID</param>
        /// <returns></returns>
        public ActionResult GetBusinessTrip_Details(int id)
        {


            var lis = BuildBusinessTrip_DetailsViewModels(id);


            return Json(lis);
        }

        /// <summary>
        /// 出差拜访对象列表
        /// </summary>
        /// <param name="id">出差ID</param>
        /// <returns></returns>
        public ActionResult BusinessTrip_VisitSubjectView(int id,bool isDetail) {
            var models = BuildBusinessTrip_VisitSubjectModels(id);
            ViewBag.IsDetail = isDetail;
            return View(models);
        }

        /// <summary>
        /// 添加出差拜访对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BusinessTrip_VisitSubjectAdd(string lis) {
            var list = JsonConvert.DeserializeObject<List<HR_出差_拜访对象>>(lis);
            if (list.Count == 0)
            {
                return Json(new { success = false, msg = "添加失败！" });
            }
            try
            {
                foreach (var item in list)
                {
                    HR_出差_拜访对象 m = new HR_出差_拜访对象();
                    m.出差计划详细ID = item.出差计划详细ID;
                    m.拜访对象ID = item.拜访对象ID;
                    m.拜访对象类型 = item.拜访对象类型;
                 businessTrip_VisitSubjectBLL.Add(m);
                }
              
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg="添加失败!"+e.Message});
            }
            return Json(new { success = true, msg = "添加成功！"});
        }

        /// <summary>
        /// 修改出差拜访对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult BusinessTrip_VisitSubjectEdit(HR_出差_拜访对象 model)
        {
            var m = businessTrip_VisitSubjectBLL.GetModel(p => p.ID == model.ID);
            m.拜访对象ID = model.拜访对象ID;
            m.拜访对象类型 = model.拜访对象类型;
            try
            {
                 businessTrip_VisitSubjectBLL.Modify(m);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "修改失败!" + e.Message });
            }
            return Json(new { success = true, msg = "修改成功！", data = m });
        }

        /// <summary>
        /// 拜访对象类型删除
        /// </summary>
        /// <param name="id">拜访对象类型ID</param>
        /// <returns></returns>
        public ActionResult BusinessTrip_VisitSubjectDel(int id) {
            try
            {
                businessTrip_VisitSubjectBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg="删除失败！"+e.Message});
            }
            return Json(new { success = true, msg = "删除成功！" });
        }

        /// <summary>
        /// 出差汇报添加视图
        /// </summary>
        /// <param name="tripID">出差ID</param>
        /// <param name="">详情ID</param>
        /// <returns></returns>
        public ActionResult BusinessTrip_ReportAddView(int tripID, int detailsID)
        {
            BusinessTrip_ReportViewModel model = new BusinessTrip_ReportViewModel();
            model.出差记录ID = tripID;
            model.出差关联计划项ID = detailsID;

            var m = businessTrip_ReportBLL.GetModel(p => p.出差记录ID == tripID && p.出差关联计划项ID == detailsID);
            if (m != null)
            {
                return Content("<script>alert('该汇报项已汇报！');window.history.go(-1);</script>");
            }
            return View(model);
        }



        /// <summary>
        /// 添加出差汇报
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBusinessTrip_Report(BusinessTrip_ReportViewModel reportViewModel)
        {
            var ac = GetAccountInfo();
            HR_出差汇报 model = new HR_出差汇报() { };
            model.出差关联计划项ID = reportViewModel.出差关联计划项ID;
            model.出差记录ID = reportViewModel.出差记录ID;
            model.备注 = reportViewModel.备注;
            model.实际遇到的问题 = reportViewModel.实际遇到的问题;
            model.巡店开始时间 = reportViewModel.巡店开始时间;
            model.巡店结束时间 = reportViewModel.巡店结束时间;
            model.工作成果 = reportViewModel.工作成果;
            model.提交人ID = ac.用户ID;
            model.提交时间 = DateTime.Now;
            model.更新人ID = ac.用户ID;
            model.是否为新增项 = reportViewModel.是否为新增项;
            model.是否包含附件 = reportViewModel.是否包含附件;
            model.更新时间 = DateTime.Now;
            model.解决的方案 = reportViewModel.解决的方案;
            model.需要的协助 = reportViewModel.需要的协助;
            model.预计完成时间 = reportViewModel.预计完成时间;

            try
            {
                model = businessTrip_ReportBLL.AddReturnModel(model);

            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "提交成功", data = model });
        }

        /// <summary>
        /// 出差汇报修改 视图
        /// </summary>
        /// <param name="id">汇报ID</param>
        /// <returns></returns>
        public ActionResult EditBusinessTrip_ReportView(int id)
        {
            var model = businessTrip_ReportBLL.GetModel(p => p.ID == id);
            var ite = BuildBusinessTrip_ReportViewModel(model);
            return View(ite);
        }

        /// <summary>
        /// 修改出差汇报
        /// </summary>
        /// <param name="reportViewModel">出差报告实体</param>
        /// <returns></returns>
        public ActionResult EditBusinessTrip_Report(BusinessTrip_ReportViewModel reportViewModel)
        {

            var ac = GetAccountInfo();
            var m = businessTrip_ReportBLL.GetModel(p => p.ID == reportViewModel.ID);
            HR_出差汇报 model = new HR_出差汇报() { };
            model = m;
            model.备注 = reportViewModel.备注;
            model.实际遇到的问题 = reportViewModel.实际遇到的问题;
            model.巡店开始时间 = reportViewModel.巡店开始时间;
            model.巡店结束时间 = reportViewModel.巡店结束时间;
            model.工作成果 = reportViewModel.工作成果;
            model.更新人ID = ac.用户ID;
            model.是否为新增项 = reportViewModel.是否为新增项;
            model.是否包含附件 = reportViewModel.是否包含附件;
            model.更新时间 = DateTime.Now;
            model.解决的方案 = reportViewModel.解决的方案;
            model.需要的协助 = reportViewModel.需要的协助;
            model.预计完成时间 = reportViewModel.预计完成时间;
            try
            {
                businessTrip_ReportBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！" });
        }

        /// <summary>
        /// 出差汇报分布页视图
        /// </summary>
        /// <param name="tripID"></param>
        /// <returns></returns>
        public ActionResult BusinessTrip_RportListView(int tripID)
        {
            ViewBag.ID = tripID;
            var lis = BuildBusinessTrip_ReportViewModels(tripID);
            return View(lis);
        }

        /// <summary>
        /// 出差汇报JSON数据
        /// </summary>
        /// <param name="tripID">出差ID</param>
        /// <returns></returns>
        public ActionResult BusinessTrip_RportList(int tripID)
        {
            var lis = BuildBusinessTrip_ReportViewModels(tripID);
            return Json(lis);
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="UploadStream">文件流</param>
        /// <param name="storeid">店铺ID</param>
        /// <param name="tripID">出差ID</param>
        /// <param name="tripreporID">出差汇报项ID</param>
        /// <returns></returns>
        public ActionResult UpFiles(HttpPostedFileBase UploadStream, string tripID, string tripreporID)
        {
            var ac = GetAccountInfo();
            if (UploadStream == null)
            {
                return Json(new { success = false, Message = "上传文件为空!" });
            }
            string code = tripID + "-" + tripreporID;
            string fileName = UploadStream.FileName;
            var path = UploadManager.UploadManager.SaveFile(UploadStream, "出差汇报图", ac.姓名, DateTime.Now.ToString("yyyy-MM-dd"), code, fileName);
            HR_出差汇报凭证 model = new HR_出差汇报凭证() { };
            model.出差计划项ID = int.Parse(tripreporID);
            model.出差记录ID = int.Parse(tripID);
            model.时间 = DateTime.Now;
            model.路径 = path;
            try
            {
                model = businessTrip_ReportProofBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                return Json(new { Success = false, Message = e.Message });
            }


            if (model == null)
            {
                return Json(new { Success = false, Message = "上传失败！" });
            }
            return Json(new { Success = true, Message = "上传成功！" });
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="rowid">凭证ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DownLoadFiles(int id)
        {



            var path = businessTrip_ReportProofBLL.GetModel(p => p.ID == id).路径;

            string msg = "";
            if (!System.IO.File.Exists(path))
            {


                msg = "<script>alert('文件下载失败！');parent.location.href='Index';</script>";
                return Content(msg);
            }
            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            FileInfo fi = new FileInfo(path);
            //**********处理可以解决文件类型问题
            string fileextname = fi.Extension;
            string DEFAULT_CONTENT_TYPE = "application/unknown";
            RegistryKey regkey, fileextkey;
            string filecontenttype;
            try
            {
                regkey = Registry.ClassesRoot;
                fileextkey = regkey.OpenSubKey(fileextname);
                filecontenttype = fileextkey.GetValue("Content Type", DEFAULT_CONTENT_TYPE).ToString();
            }
            catch
            {
                filecontenttype = DEFAULT_CONTENT_TYPE;
            }
            //**********end
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.ContentType = filecontenttype;
            try
            {

                Response.AddHeader("Content-Disposition", "attachment; filename=" + Guid.NewGuid().ToString().Substring(0, 5) + ".JPG");//设置文件信息
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception E)
            {

                throw E;
            }

            return new EmptyResult();




        }
        /// <summary>
        /// 出差审核
        /// </summary>
        /// <param name="id">出差记录ID</param>
        /// <param name="checkType">审核状态</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BusinessTrip_Check(int id, bool checkType)
        {
            var ac = GetAccountInfo();
            var model = businessTripBLL.GetModel(p => p.ID == id);

            if (ac.用户ID != model.关联审核人ID)
            {
                return Content("<script>alert('违规操作!');window.history.go(-1);</script>");
            }
            else
            {
                model.审核状态 = checkType;
                string[] property = new string[1];
                property[0] = "审核状态";
                businessTripBLL.Modify(model, property);
                return RedirectToAction("BusinerssTrip_CheckView");
            }

        }

        /// <summary>
        /// 出差审核页面
        /// </summary>
        /// <param name="id">出差ID</param>
        /// <returns></returns>
        public ActionResult BusinessCheckView(int id)
        {
            var ac = GetAccountInfo();
            var model = businessTripBLL.GetModel(p => p.ID == id);
            var m = BuildBusinessTrip(model);
            if (ac.用户ID != model.关联审核人ID)
            {
                return Content("<script>alert('违规操作!');window.history.go(-1);</script>");
            }

            return View(m);
        }



        #region 邮件发送
        /// <summary>
        /// 邮件发送操作
        /// </summary>
        /// <param name="mailInfoModel">邮件类</param>
        /// <param name="planID">计划项ID</param>
        /// <param name="tripID">出差ID</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SendMail(MailInfoModel mailInfoModel,int tripID,int planID)
        {
            //从配置文件中获取发送邮箱配置

            string emailAddress = System.Configuration.ConfigurationManager.AppSettings["MailAddress"];
            string emailPwd = System.Configuration.ConfigurationManager.AppSettings["MailPwd"];
            string emailHost = System.Configuration.ConfigurationManager.AppSettings["Host"];

            MailHelper mailHelper = new MailHelper();
            mailHelper.Host = emailHost;
            
            mailHelper.MailFrom = emailAddress;
            mailHelper.MailPwd = emailPwd;
            mailHelper.IsbodyHtml = true;
            mailHelper.MailSubject = mailInfoModel.MailSubject;
          
          
            try
            {

                string content = "如果您邮件客户端不支持HTML格式，请切换到“普通文本”视图，将看到此内容";
                mailHelper.MyMail = new MailMessage();
                mailHelper.MyMail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, null, "text/plain"));
               

                ///查询图片

                string imagesPath = "";
                var models = businessTrip_ReportProofBLL.GetModels(p => p.出差计划项ID == planID && p.出差记录ID == tripID);
               // Dictionary<int, string> pairs = new Dictionary<int, string>();
                foreach (var item in models)
                {
                    string str = "<img style=\'heigth:300px;width:300px;' src =\'" + item.路径 +"'>";

                    imagesPath += str;
                }
                mailInfoModel.MailBody += "<br />" + imagesPath; //注意此处嵌入的图片资源 

                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(mailInfoModel.MailBody, null, "text/html");

     
                //LinkedResource lrImage = new LinkedResource("E:\\Visual项目\\ChicST-MM\\ChicST-MM.WEB\\images\\1.jpg", "image/gif");
                //lrImage.ContentId = "weblogo"; //此处的ContentId 对应 htmlBodyContent 内容中的 cid: ，如果设置不正确，请不会显示图片 
                //htmlBody.LinkedResources.Add(lrImage);
               
               mailHelper.MyMail.AlternateViews.Add(htmlBody);
               mailHelper.MailToArray = mailInfoModel.MailToArray;
               mailHelper.MailCcArray = mailInfoModel.MailCcArray;
               mailHelper.Send();
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e});
            }
            return Json(new { success = true, msg = "发送邮件成功！" });
        }

        /// <summary>
        /// 邮件发送界面
        /// </summary>
        /// <param name="id">汇报项ID</param>
        /// <returns></returns>
        public ActionResult SendMailView(int id)
        {
            var rmodel = businessTrip_ReportBLL.GetModel(p => p.ID == id);
            var model = BuildBusinessTrip_ReportViewModel(rmodel);
            return View(model);
        }

        /// <summary>
        /// 出差计划明细数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BusinessDetailsJson(int id)
        {
            var pmodel = businessTrip_DetailsBLL.GetModel(p => p.ID == id);

            return Json(pmodel);
        }
        #endregion





        /// <summary>
        /// 获取省
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProvince()
        {
            var regions = regionBLL.GetModels(p => true).GroupBy(p => p.省).Select(p => p.FirstOrDefault());

            List<String> provinces = new List<String>();
            foreach (var item in regions)
            {
                provinces.Add(item.省);
            }

            return Json(provinces);

        }
        /// <summary>
        /// 获取市
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRgion(string province)
        {
            var regions = regionBLL.GetModels(p => p.省 == province).GroupBy(p => p.市).Select(p => p.FirstOrDefault());

            var cityes = new ArrayList();
            foreach (var item in regions)
            {

                var city = new string[] { item.区号, item.市 };
                cityes.Add(city);

            }
            return Json(cityes);

        }

        /// <summary>
        /// 获取地区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetArea(string code)
        {
            var areaList = regionBLL.GetModels(p => p.区号 == code).ToList();
            var area = new ArrayList();
            foreach (var item in areaList)
            {

                var city = new string[] { item.ID.ToString(), item.区县 };
                area.Add(city);

            }
            return Json(area);
        }
        /// <summary>
        /// 获得门店
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTripObj()
        {

            var stores = storeBLL.GetModels(p => true);
            var tripObj = new ArrayList();
            foreach (var item in stores)
            {

                var store = new string[] { item.ID.ToString(), item.名称 };
                tripObj.Add(store);
            }
            return Json(tripObj);

        }

        /// <summary>
        /// 获得门店
        /// </summary>
        /// <param name="city">市</param>
        /// <param name="pro">省</param>
        /// <returns></returns>
       [HttpPost]
        public JsonResult GetTripObj(int id)
        {
            string pro;
            string city;
            var n = development_CityBLL.GetModel(p => p.ID == id);
            if (n == null)
            {
                return Json(new { success = false, msg = "不存在该城市！" });
            }
            pro = n.省;
            city = n.市;
            var m = regionBLL.GetModels(p => p.省 == pro && p.市 == city);
           
            if (m==null)
            {
                return Json(new { success = false, msg = "不存在该城市！" });
            }
            
            var stores = storeBLL.GetModels(p => true);
           
            if (stores==null)
            {
                return Json(new { success = false, msg = "数据库中不存在门店！" });
            }

            var tripObj = new ArrayList();
            foreach (var item in stores)
            {
                var model=regionBLL.GetModel(p => p.ID == item.地区ID);
                if (model.省==pro&&model.市==city)
                {
                var store = new string[] { item.ID.ToString(), item.名称 };
                tripObj.Add(store);
                }
               
            }
            return Json(new { success=true,data= tripObj ,msg="查询成功!"});

        }
        /// <summary>
        /// 获得经销商
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDistripObj()
        {

            var distributors = distributorBLL.GetModels(p => true).ToList();
            var tripObj = new ArrayList();


            foreach (var item in distributors)
            {

                var distributor = new string[] { item.ID.ToString(), item.名称 };
                tripObj.Add(distributor);

            }
            return Json(tripObj);


        }
        /// <summary>
        /// 获得商场
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMarketObj()
        {

            var stores = storeBLL.GetModels(p => true);
            var tripObj = new ArrayList();
            foreach (var item in stores)
            {

                var store = new string[] { item.ID.ToString(), item.商场 };
                tripObj.Add(store);
            }
            return Json(tripObj);


        }

        /// <summary>
        /// 获取公司职员信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAssociatedAuditor()
        {
            var models = GetCompanyStaff();
            if (models.Count() <= 0)
            {
                return Json("查询失败!");
            }
            var tripObj = new ArrayList();
            foreach (var item in models)
            {
                var Auditor = new string[] { item.用户ID.ToString(), item.姓名 ,item.邮箱地址};
                tripObj.Add(Auditor);
            }
            return Json(tripObj);
        }

        /// <summary>
        /// 查询出差计划详情
        /// </summary>
        /// <param name="id">出差记录ID</param>
        /// <returns></returns>
        private List<BusinessTrip_DetailsViewModel> BuildBusinessTrip_DetailsViewModels(int id)
        {
            var businessTrip = businessTripBLL.GetModel(p => p.ID == id);
            var businessTripDetails = businessTrip_DetailsBLL.GetModels(p => p.出差记录ID == id);
            if (businessTripDetails == null)
            {
                return null;
            }
            List<BusinessTrip_DetailsViewModel> lis = new List<BusinessTrip_DetailsViewModel>() { };
            foreach (var item in businessTripDetails)
            {
                BusinessTrip_DetailsViewModel model = new BusinessTrip_DetailsViewModel();
                model.ID = item.ID;
                model.出差记录ID = item.出差记录ID;
                model.出差时间 = item.出差时间;
                model.同行人员 = item.同行人员;
               

                model.巡店目的 = item.巡店目的;
                model.提交时间 = item.提交时间;
            

                model.计划内容 = item.计划内容;
                model.计划方案 = item.计划方案;
             

                model.城市ID = item.城市ID;
                model.城市 = development_CityBLL.GetModel(p => p.ID == item.城市ID).市;
                model.备注 = item.备注;
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 构建拜访对象实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private BusinessTrip_VisitSubjectViewModel BuildBusinessTrip_VisitSubjectModel(HR_出差_拜访对象 model) {
            BusinessTrip_VisitSubjectViewModel _VisitSubjectViewModel = new BusinessTrip_VisitSubjectViewModel();
            _VisitSubjectViewModel.ID = model.ID;
            _VisitSubjectViewModel.出差计划详细ID = model.出差计划详细ID;
            _VisitSubjectViewModel.拜访对象类型 = model.拜访对象类型;
            _VisitSubjectViewModel.拜访对象ID = model.拜访对象ID;
            if (_VisitSubjectViewModel.拜访对象类型=="经销商")
            {
                _VisitSubjectViewModel.拜访对象名称 = development_FranchiserBLL.GetModel(P => P.ID == _VisitSubjectViewModel.拜访对象ID).企业名称;
            }
            if (_VisitSubjectViewModel.拜访对象类型 == "商场")
            {
                _VisitSubjectViewModel.拜访对象名称 = development_MarketBLL.GetModel(P => P.ID == _VisitSubjectViewModel.拜访对象ID).商场名称;
            }
            if (_VisitSubjectViewModel.拜访对象类型 == "门店")
            {
                _VisitSubjectViewModel.拜访对象名称 = storeBLL.GetModel(P => P.ID == _VisitSubjectViewModel.拜访对象ID).名称;
            }
            return _VisitSubjectViewModel;
        }

        /// <summary>
        /// 构建拜访对象实体集
        /// </summary>
        /// <param name="id">出差计划详细ID</param>
        /// <returns></returns>
        private List<BusinessTrip_VisitSubjectViewModel> BuildBusinessTrip_VisitSubjectModels(int id) {
            var models = businessTrip_VisitSubjectBLL.GetModels(p => p.出差计划详细ID == id);
            if (models==null)
            {
                return null;
            }
            List<BusinessTrip_VisitSubjectViewModel> lis = new List<BusinessTrip_VisitSubjectViewModel>();
            foreach (var item in models)
            {
                var model = BuildBusinessTrip_VisitSubjectModel(item);
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 构建出差汇报实体集
        /// </summary>
        /// <param name="tripID"></param>
        /// <returns></returns>
        private List<BusinessTrip_ReportViewModel> BuildBusinessTrip_ReportViewModels(int tripID)
        {
            var models = businessTrip_ReportBLL.GetModels(p => p.出差记录ID == tripID);
            List<BusinessTrip_ReportViewModel> lis = new List<BusinessTrip_ReportViewModel>();
            foreach (var item in models)
            {
                var model = BuildBusinessTrip_ReportViewModel(item);
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 构建出差汇报实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private BusinessTrip_ReportViewModel BuildBusinessTrip_ReportViewModel(HR_出差汇报 model)
        {
            BusinessTrip_ReportViewModel trip_ReportViewModel = new BusinessTrip_ReportViewModel();
            trip_ReportViewModel.ID = model.ID;
            trip_ReportViewModel.出差关联计划项ID = model.出差关联计划项ID;
            trip_ReportViewModel.出差记录ID = model.出差记录ID;
            trip_ReportViewModel.备注 = model.备注;
            trip_ReportViewModel.预计完成时间 = model.预计完成时间;
            trip_ReportViewModel.实际遇到的问题 = model.实际遇到的问题;
            trip_ReportViewModel.解决的方案 = model.解决的方案;
            trip_ReportViewModel.需要的协助 = model.需要的协助;
            trip_ReportViewModel.巡店开始时间 = model.巡店开始时间.Value;
            trip_ReportViewModel.巡店结束时间 = model.巡店结束时间.Value;
            trip_ReportViewModel.工作成果 = model.工作成果;
            trip_ReportViewModel.提交人ID = model.提交人ID.Value;
            trip_ReportViewModel.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID.Value).姓名;
            trip_ReportViewModel.提交时间 = model.提交时间.Value;
            trip_ReportViewModel.是否为新增项 = model.是否为新增项.Value;
            trip_ReportViewModel.是否包含附件 = model.是否包含附件.Value;
            trip_ReportViewModel.更新人ID = model.更新人ID.Value;
            trip_ReportViewModel.更新人 = employeeInformationBLL.GetModel(p => p.用户ID == model.更新人ID.Value).姓名;
            trip_ReportViewModel.更新时间 = model.更新时间.Value;
            List<HR_出差汇报凭证> lis = new List<HR_出差汇报凭证>() { };
            var models = businessTrip_ReportProofBLL.GetModels(p => p.出差记录ID == model.出差记录ID);
            models = models.Where(p => p.出差计划项ID == model.出差关联计划项ID);
            foreach (var item in models)
            {
                HR_出差汇报凭证 m = new HR_出差汇报凭证();
                m.ID = item.ID;
                m.出差记录ID = item.出差记录ID;
                m.路径 = item.路径;
                m.出差计划项ID = item.出差计划项ID;
                lis.Add(m);
            }
            trip_ReportViewModel.HR_出差汇报凭证 = lis;
            return trip_ReportViewModel;
        }

        /// <summary>
        /// 构建单个出差基本信息
        /// </summary>
        /// <param name="id">出差记录ID</param>
        /// <returns></returns>
        private BusinessTripViewModel BuildBusinessTrip(HR_出差计划 model)
        {
            BusinessTripViewModel businessTripViewModel = new BusinessTripViewModel();

            var account = GetAccountInfo();

            businessTripViewModel.ID = model.ID;
            businessTripViewModel.关联审核人ID = model.关联审核人ID;
            businessTripViewModel.关联审核人 = accountBLL.GetModel(p => p.ID == model.关联审核人ID).姓名;
            businessTripViewModel.出差内容 = model.出差内容;
            businessTripViewModel.住宿费预计 = model.住宿费预计;
            businessTripViewModel.出差人 = model.出差人.Value;
            businessTripViewModel.出差人姓名 = employeeInformationBLL.GetModel(p => p.用户ID == model.出差人).姓名;
            businessTripViewModel.出差内容 = model.出差内容;
            businessTripViewModel.合计 = model.合计;
            businessTripViewModel.备注 = model.备注;
            businessTripViewModel.审核状态 = model.审核状态;
            businessTripViewModel.提交人ID = model.提交人ID;
            businessTripViewModel.提交人 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人ID).姓名;
            businessTripViewModel.车船费预计 = model.车船费预计;
            businessTripViewModel.餐补费预计 = model.餐补费预计;
            businessTripViewModel.部门 = employeeInformationBLL.GetModel(p => p.用户ID == businessTripViewModel.出差人).部门;
            businessTripViewModel.开始时间 = model.开始时间;
            businessTripViewModel.提交时间 = model.提交时间;
            businessTripViewModel.结束时间 = model.结束时间;
            businessTripViewModel.部门ID = model.部门ID;
            //List<BusinessTrip_DetailsViewModel> lis = new List<BusinessTrip_DetailsViewModel>() { };
            //lis = BuildBusinessTrip_DetailsViewModels(businessTripViewModel.ID);
            //businessTripViewModel.BusinessTrip_DetailsViewModels = lis;
            return businessTripViewModel;

        }

        /// <summary>
        /// 出差申请列表
        /// </summary>
        /// <param name="role">当前用户角色</param>
        /// <returns></returns>
        private List<BusinessTripViewModel> BuildBusinessTripList(int? role)
        {
            List<BusinessTripViewModel> businessTripViewModels = new List<BusinessTripViewModel>();
            var em = GetAccountInfo();
            if (role > 0)//大于零则为审核人员操作
            {     

                var models = businessTripBLL.GetModels(p => p.关联审核人ID == em.用户ID);
                if (models==null)
                {
                    return null;
                }
                foreach (var item in models)
                {
                    businessTripViewModels.Add(BuildBusinessTrip(item));
                }
            }
            else
            {
                var models = businessTripBLL.GetModels(p => p.提交人ID == em.用户ID);//否则为当前用户提交的出差申请
                if (models == null)
                {
                    return null;
                }
                foreach (var item in models)
                {
                    businessTripViewModels.Add(BuildBusinessTrip(item));
                }
            }

            return businessTripViewModels;
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
                var em = accounts.ElementAt(i);
                try
                {
                    
                    
                    m = employeeInformationBLL.GetModel(p => p.用户ID == em.ID);
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
                    ac.邮箱地址 = em.邮箱;
                    companyStaffViewModel.Add(ac);
                }
            }
            return companyStaffViewModel;
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