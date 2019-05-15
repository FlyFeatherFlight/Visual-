﻿using ChicST_MM.IBLL;
using ChicST_MM.WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicST_MM.WEB.Controllers
{
    public class DevelopmentDocController : Controller
    {

        private IDevelopment_CityBLL development_CityBLL;
        private IAccountBLL accountBLL;
        private IDevelopment_CompetesBLL development_CompetesBLL;
        private IDevelopment_FranchiserBLL development_FranchiserBLL;
        private IDevelopment_MarketBLL development_MarketBLL;
        private IEmployeeInformationBLL employeeInformationBLL;
        private IDevelopment_CompetesAnnualSalesBLL development_CompetesAnnualSalesBLL;
        private IDevelopment_Market_RentalBLL development_Market_RentalBLL;
        private IDevelopment_FranchiserContactsBLL development_FranchiserContactsBLL;

        public DevelopmentDocController(IDevelopment_CityBLL development_CityBLL, IAccountBLL accountBLL, IDevelopment_CompetesBLL development_CompetesBLL, IDevelopment_FranchiserBLL development_FranchiserBLL, IDevelopment_MarketBLL development_MarketBLL, IEmployeeInformationBLL employeeInformationBLL, IDevelopment_CompetesAnnualSalesBLL development_CompetesAnnualSalesBLL, IDevelopment_Market_RentalBLL development_Market_RentalBLL, IDevelopment_FranchiserContactsBLL development_FranchiserContactsBLL)
        {
            this.development_CityBLL = development_CityBLL;
            this.accountBLL = accountBLL;
            this.development_CompetesBLL = development_CompetesBLL;
            this.development_FranchiserBLL = development_FranchiserBLL;
            this.development_MarketBLL = development_MarketBLL;
            this.employeeInformationBLL = employeeInformationBLL;
            this.development_CompetesAnnualSalesBLL = development_CompetesAnnualSalesBLL;
            this.development_Market_RentalBLL = development_Market_RentalBLL;
            this.development_FranchiserContactsBLL = development_FranchiserContactsBLL;
        }








        // GET: DevelopmentDoc
        /// <summary>
        /// 城市档案
        /// </summary>
        /// <returns></returns>
        public ActionResult CityView()
        {
           
            var models = BuildCityModels();
            return View(models);
        }


        /// <summary>
        /// 添加城市操作
        /// </summary>
        /// <param name="cityViewModel"></param>
        /// <returns></returns>
        public ActionResult CityAdd(Development_CityViewModel cityViewModel)
        {
            var ac = GetAccountInfo();
            拓展_城市档案 model = new 拓展_城市档案();
            model.城市分级 = cityViewModel.城市分级;
            model.市 = cityViewModel.市;
            model.录入人ID = ac.用户ID;
            model.是否为目标城市 = cityViewModel.是否为目标城市;
            model.更新时间 = DateTime.Now;
            model.省 = cityViewModel.省;
            try
            {
                model = development_CityBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                return Content("<script>alert('提交失败！');window.history.go(-1);</script>");
            }
            return RedirectToAction("CityView");
        }

        /// <summary>
        /// 城市修改操作
        /// </summary>
        /// <param name="cityViewModel"></param>
        /// <returns></returns>
        public ActionResult CityEdit(Development_CityViewModel cityViewModel)
        {
            var ac = GetAccountInfo();
            var model = development_CityBLL.GetModel(p => p.ID == cityViewModel.ID);
            model.城市分级 = cityViewModel.城市分级;
            model.市 = cityViewModel.市;
            model.录入人ID = ac.用户ID;
            model.是否为目标城市 = cityViewModel.是否为目标城市;
            model.更新时间 = DateTime.Now;
            model.省 = cityViewModel.省;
            try
            {
                development_CityBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功!" });
        }


        /// <summary>
        /// 查询商场视图
        /// </summary>
        /// <param name="cid">城市ID</param>
        /// <returns></returns>
        public ActionResult MarketView(int? cid)
        {
            var models = BuildDevelopment_MarketwModels(cid);
            return View(models);
        }

        /// <summary>
        /// 商场添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MarketAddView() {
            Development_MarketViewModel marketViewModel = new Development_MarketViewModel();
            return View(marketViewModel);
        }
        /// <summary>
        /// 商场添加操作
        /// </summary>
        /// <param name="marketViewModel"></param>
        /// <returns></returns>
        public ActionResult MarketAdd(Development_MarketViewModel marketViewModel)
        {
            var ac = GetAccountInfo();
            拓展_商场档案表 model = new 拓展_商场档案表();
            model.商场名称 = marketViewModel.商场名称;
            model.商场等级 = marketViewModel.商场等级;
            model.地址 = marketViewModel.地址;
            model.城市等级 = marketViewModel.城市等级;
            model.录入人ID = ac.用户ID;
           
            model.所属城市ID = marketViewModel.所属城市ID;
            model.更新日期 = DateTime.Now;
            model.职务 = marketViewModel.职务;
            model.联系方式 = marketViewModel.联系方式;
            model.负责人 = marketViewModel.负责人;
            model.品牌调整时间 = marketViewModel.品牌调整时间;
            model.物业名称 = marketViewModel.物业名称;
            model.经营总面积 = marketViewModel.经营总面积;
            model.续约开始日期1 = marketViewModel.续约开始日期1;
            model.续约开始日期2 = marketViewModel.续约开始日期2;
            try
            {
                model = development_MarketBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "添加成功", data = model });
        }

        /// <summary>
        /// 商场修改操作
        /// </summary>
        /// <param name="marketViewModel"></param>
        /// <returns></returns>
        public JsonResult MarketEdit(Development_MarketViewModel marketViewModel)
        {
            var ac = GetAccountInfo();
            var model = development_MarketBLL.GetModel(p => p.ID == marketViewModel.ID);
            model.商场名称 = marketViewModel.商场名称;
            model.商场等级 = marketViewModel.商场等级;
            model.地址 = marketViewModel.地址;
           
            model.城市等级 = marketViewModel.城市等级;
            model.录入人ID = ac.用户ID;
         
            model.所属城市ID = marketViewModel.所属城市ID;
            model.更新日期 = DateTime.Now;
            model.职务 = marketViewModel.职务;
            model.联系方式 = marketViewModel.联系方式;
            model.负责人 = marketViewModel.负责人;
            model.品牌调整时间 = marketViewModel.品牌调整时间;
           
            model.物业名称 = marketViewModel.物业名称;
            model.经营总面积 = marketViewModel.经营总面积;
            model.续约开始日期1 = marketViewModel.续约开始日期1;
            model.续约开始日期2 = marketViewModel.续约开始日期2;
            try
            {
                development_MarketBLL.Modify(model);
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功!" });
        }

        /// <summary>
        /// 商场_租金档案
        /// </summary>
        /// <param name="id">商场ID</param>
        /// <returns></returns>
        public ActionResult MarketRentalView(int id)
        {
            var models = BuildDevelopment_MarketRentalViewModels(id);
            return View(models);

        }

       /// <summary>
       /// 商场_租金档案添加操作
       /// </summary>
       /// <param name="lis"></param>
       /// <returns></returns>
       [HttpPost]
        public ActionResult MarketRentalAdd(string lis) {
            var list = JsonConvert.DeserializeObject<List<拓展_商场_租金档案>>(lis);
            if (list.Count == 0)
            {
                return Json(new { success = false, msg = "添加失败！" });
            }

            try
            {
                foreach (var item in list)
                {
              development_Market_RentalBLL.AddReturnModel(item);
                }
                
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg="提交失败!"+e.Message});
            }
            return Json(new { success = true, msg = "提交成功!"});
        }

        /// <summary>
        /// 商场_租金修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MarketRentalEdit(拓展_商场_租金档案 model) {
            try
            {
                development_Market_RentalBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg="修改失败！"+e.Message});
            }
            return Json(new { success = true, msg = "修改成功！" });

        }

        /// <summary>
        /// 商场删除操作
        /// </summary>
        /// <param name="id">商场ID</param>
        /// <returns></returns>
        public JsonResult MarketDel(int id) {
           
            var f = development_FranchiserBLL.GetModels(p => p.意向商场ID == id);
            var c = development_CompetesBLL.GetModels(p => p.商场ID == id);
            if (c!=null&&f!=null)
            {
                return Json(new { success = false, msg = "该商场关联有经销商和竞品信息，不可删除!" });
            }
            if (f!=null)
            {
                return Json(new { success = false, msg = "该商场关联有经销商信息，不可删除!" });
            }
            if (c!=null)
            {
                return Json(new { success = false, msg = "该商场关联有竞品信息，不可删除!" });
            }
            try
            {
                development_MarketBLL.DelBy(p=>p.ID==id);
            }
            catch (Exception e)
            {

                return Json(new {success=false,msg="删除失败！该记录有关联数据！"+e.Message });
            }
            return Json(new { success = true, msg = "删除成功!" });
        }

        /// <summary>
        /// 商场_租金删除操作
        /// </summary>
        /// <param name="id">商场_租金ID</param>
        /// <returns></returns>
        public JsonResult MarkestRentalDel(int id) {
            try
            {
                development_Market_RentalBLL.DelBy(p=>p.ID==id);
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = "删除失败！该记录有关联数据！" + e.Message });
            }
            return Json(new { success = true, msg = "删除成功!" });
        }
        /// <summary>
        /// 查询经销商视图
        /// </summary>
        /// <param name="cid">城市ID</param>
        /// <returns></returns>
        public ActionResult FranchiserView(int? cid)
        {
            var models = BuildDevelopment_FranchiserViewModels(cid);
            return View(models);

        }

        /// <summary>
        /// 经销商添加操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult FranchiserAdd(Development_FranchiserViewModel model)
        {
            var ac = GetAccountInfo();
            拓展_经销商档案表 franchiserViewModel = new 拓展_经销商档案表();
            franchiserViewModel.代理品牌 = model.代理品牌;
            franchiserViewModel.企业名称 = model.企业名称;
            franchiserViewModel.入驻商场 = model.入驻商场;
            franchiserViewModel.团队人数 = model.团队人数;
            franchiserViewModel.地址 = model.地址;
            franchiserViewModel.城市ID = model.城市ID;
            franchiserViewModel.备注 = model.备注;
            franchiserViewModel.年销售额 = model.年销售额;
            franchiserViewModel.录入人ID = ac.用户ID;
            franchiserViewModel.意向位置分级 = model.意向位置分级;
            franchiserViewModel.意向商场ID = model.意向商场ID;
            franchiserViewModel.商场等级 = model.商场等级;
            franchiserViewModel.投资预算 = model.投资预算; ;
            franchiserViewModel.是否现代品牌 = model.是否现代品牌;
            franchiserViewModel.更新日期 = DateTime.Now;
            franchiserViewModel.理念描述 = model.理念描述;
            franchiserViewModel.社会资源描述 = model.社会资源描述;
            franchiserViewModel.经营年限 = model.经营年限;
            franchiserViewModel.门店数量 = model.门店数量;
            franchiserViewModel.预算面积 = model.预算面积;
            franchiserViewModel.匹配度 = model.匹配度;
            franchiserViewModel.城市等级 = model.城市等级;
            franchiserViewModel.盈利情况 = model.盈利情况;
            try
            {
                franchiserViewModel = development_FranchiserBLL.AddReturnModel(franchiserViewModel);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "添加失败！" + e.Message });
            }
            return Json(new { success = true, msg = "添加成功！" ,data= franchiserViewModel });
        }

        /// <summary>
        /// 经销商修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult FranchiserEdit(Development_FranchiserViewModel model)
        {
            var ac = GetAccountInfo();
            var franchiserViewModel = development_FranchiserBLL.GetModel(p => p.ID == model.ID);
            franchiserViewModel.代理品牌 = model.代理品牌;
            franchiserViewModel.企业名称 = model.企业名称;
            franchiserViewModel.入驻商场 = model.入驻商场;
            franchiserViewModel.团队人数 = model.团队人数;
            franchiserViewModel.地址 = model.地址;
            franchiserViewModel.城市ID = model.城市ID;
            franchiserViewModel.备注 = model.备注;
            franchiserViewModel.年销售额 = model.年销售额;
            franchiserViewModel.录入人ID = ac.用户ID;
            franchiserViewModel.意向位置分级 = model.意向位置分级;
            franchiserViewModel.意向商场ID = model.意向商场ID;
            franchiserViewModel.商场等级 = model.商场等级;
            franchiserViewModel.投资预算 = model.投资预算; ;
            franchiserViewModel.是否现代品牌 = model.是否现代品牌;
            franchiserViewModel.更新日期 = DateTime.Now;
            franchiserViewModel.理念描述 = model.理念描述;
            franchiserViewModel.社会资源描述 = model.社会资源描述;
            franchiserViewModel.经营年限 = model.经营年限;
            franchiserViewModel.门店数量 = model.门店数量;
            franchiserViewModel.预算面积 = model.预算面积;
            franchiserViewModel.匹配度 = model.匹配度;
            franchiserViewModel.城市等级 = model.城市等级;
            franchiserViewModel.盈利情况 = model.盈利情况;
            
            try
            {
                development_FranchiserBLL.Modify(franchiserViewModel);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功！" });
        }

        /// <summary>
        /// 经销商删除操作
        /// </summary>
        /// <param name="id">经销商ID</param>
        /// <returns></returns>
        public ActionResult FranchiserDel(int id) {
            try
            {
                development_FranchiserBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {


                return Json(new { success = false,  msg = "删除失败！该记录有关联数据！" + e.Message });
            }
            return Json(new { success = true, msg = "删除成功！" });
        }

        /// <summary>
        /// 经销商添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FranchiserAddView() {
            Development_FranchiserViewModel model = new Development_FranchiserViewModel();
            return View(model);
        }

        /// <summary>
        /// 经销商_联系人列表分布视图
        /// </summary>
        /// <param name="id">经销商ID</param>
        /// <returns></returns>
        public ActionResult FranchiserContactsView(int id) {
            var models = BuildDevelopment_FranchiserContactsModels(id);
            return View(models);

        }

        /// <summary>
        /// 经销商_联系人添加操作
        /// </summary>
        /// <param name="lis"></param>
        /// <returns></returns>
        public ActionResult FranchiserContactsAdd(string lis) {
            var list = JsonConvert.DeserializeObject<List<拓展_经销商_联系人>>(lis);
            if (list.Count == 0)
            {
                return Json(new { success = false, msg = "添加失败！" });
            }
            try
            {
                foreach (var item in list)
                {
                    development_FranchiserContactsBLL.Add(item);
                }
                
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "添加失败！"+e.Message });
            }
            return Json(new { success = true, msg = "添加成功！"  });
        }

        /// <summary>
        /// 经销商_联系人修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult FranchiserContactsEdit(拓展_经销商_联系人 model)
        {
            
            try
            {
                
                    development_FranchiserContactsBLL.Modify(model);
               

            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = "修改失败！" + e.Message });
            }
            return Json(new { success = true, msg = "修改成功！" });
        }

        /// <summary>
        /// 经销商_联系人删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult FranchiserContactsDel(int id) {
            try
            {
                development_FranchiserContactsBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {
                return Json(new { success = false, msg = "删除失败！该记录有关联数据！" + e.Message });
             
            }
            return Json(new { success = true, msg = "删除成功！"  });
        }


        /// <summary>
        /// 经销商删除操作
        /// </summary>
        /// <param name="id">经销商ID</param>
        /// <returns></returns>
        public JsonResult FranchiseDel(int id)
        {

           
            var c = development_CompetesBLL.GetModels(p => p.经销商ID == id);
           
            if (c != null)
            {
                return Json(new { success = false, msg = "该经销商关联有竞品信息，不可删除!" });
            }
            try
            {
               development_FranchiserBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success = false,  msg = "删除失败！该记录有关联数据！" + e.Message });
            }
            return Json(new { success = true, msg = "删除成功!" });
        }

        /// <summary>
        /// 查询竞品视图
        /// </summary>
        /// <param name="cid">城市ID</param>
        /// <returns></returns>
        public ActionResult CompetesView(int? cid)
        {
            var models = BuildDevelopment_CompetesModels(cid);
            return View(models);
        }

        /// <summary>
        /// 竞品添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ComeptesAddView() {

            return View();
        }

        /// <summary>
        /// 竞品添加操作
        /// </summary>
        /// <param name="competesViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CompetesAdd(Development_CompetesViewModel competesViewModel)
        {
            var ac = GetAccountInfo();
            拓展_竞品档案表 model = new 拓展_竞品档案表();
            model.位置 = competesViewModel.位置;
            model.入住日期 = competesViewModel.入住日期;
            model.商场ID = competesViewModel.商场ID;
            model.城市ID = competesViewModel.城市ID;
            model.录入人ID = ac.用户ID;
            model.更新日期 = DateTime.Now;
            model.经销商ID = competesViewModel.经销商ID;
            model.面积 = competesViewModel.面积;
            model.备注 = competesViewModel.备注;
            model.楼层 = competesViewModel.楼层;
            model.经营形态 = competesViewModel.经营形态;
            model.月店面客流 = competesViewModel.月店面客流;
            model.形象 = competesViewModel.形象;
            model.折扣 = competesViewModel.折扣;
            try
            {
                model = development_CompetesBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                return Json(new {success=false,msg=e.Message });
            }
            return Json(new { success = true, msg = "添加成功",data=model });
        }

        /// <summary>
        /// 竞品修改操作
        /// </summary>
        /// <param name="competesViewModel"></param>
        /// <returns></returns>
        public JsonResult CompetesEdit(Development_CompetesViewModel competesViewModel)
        {
            var ac = GetAccountInfo();
            var model = development_CompetesBLL.GetModel(p => p.ID == competesViewModel.ID);
            model.位置 = competesViewModel.位置;
            model.入住日期 = competesViewModel.入住日期;
            model.商场ID = competesViewModel.商场ID;
            model.城市ID = competesViewModel.城市ID;
            model.录入人ID = ac.用户ID;
            model.更新日期 = DateTime.Now;
            model.经销商ID = competesViewModel.经销商ID;
            model.面积 = competesViewModel.面积;
            model.经营形态 = competesViewModel.经营形态;
            model.楼层 = competesViewModel.楼层;
            model.备注 = competesViewModel.备注;
            model.折扣 = competesViewModel.折扣;
            model.形象 = competesViewModel.形象;
            model.月店面客流 = competesViewModel.月店面客流;
           
            try
            {
                development_CompetesBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }
            return Json(new { success = true, msg = "修改成功!" });
        }

        /// <summary>
        /// 竞品删除操作
        /// </summary>
        /// <param name="id">竞品ID</param>
        /// <returns></returns>
        public JsonResult CompetesDel(int id)
        {

            try
            {
                development_CompetesBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg =  "删除失败！该记录有关联数据！" + e.Message });
            }
            return Json(new { success = true, msg = "删除成功!" });
        }

        /// <summary>
        /// 竞品_年度销售添加操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CometesAnnualSalesAdd(string lis) {
           var list= JsonConvert.DeserializeObject<List<拓展_竞品档案_年销售档案表>>(lis);
            if (list.Count == 0)
            {
                return Json(new { success = false, msg = "添加失败！" });
            }

            try
            {
                foreach (var item in list)
                {
                 development_CompetesAnnualSalesBLL.Add(item);
                }
                
            }
            catch (Exception e)
            {

                return Json(new { success=false,msg= "添加失败！"+e.Message});
            }
            return Json(new { success = true, msg = "添加成功！" });
        }

        /// <summary>
        /// 竞品_年度销售列表分布视图
        /// </summary>
        /// <param name="id">竞品ID</param>
        /// <returns></returns>
        public ActionResult CometesAnnualSalesView(int id) {
            List<Development_CometesAnnualSalesViewModel> models = new List<Development_CometesAnnualSalesViewModel>();
            try
            {
                models = BuildDevelopment_CompetesAnnualSalesModels(id);
            }
            catch (Exception e)
            {

                return Content("暂无数据!");
            }

            return View(models);
        }

        /// <summary>
        /// 竞品_年度销售列表修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CometesAnnualSalesEdit(拓展_竞品档案_年销售档案表 model) {
            try
            {
                development_CompetesAnnualSalesBLL.Modify(model);
            }
            catch (Exception e)
            {

                return Json(new {success=false,msg=e.Message });
            }
            return Json(new { success = true, msg = "修改成功!" });
        }

        /// <summary>
        /// 竞品_年度销售删除
        /// </summary>
        /// <param name="id">年度销售ID</param>
        /// <returns></returns>
        public ActionResult CometesAnnualSalesDel(int id) {

            try
            {
                development_CompetesAnnualSalesBLL.DelBy(p => p.ID == id);
            }
            catch (Exception e)
            {

               return Json(new { success=false,msg = "删除失败！该记录有关联数据！" + e.Message });
            }
            return Json(new { success = true, msg = "删除成功!" });
        }

        /// <summary>
        /// 获取省
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProvince()
        {
            var regions = development_CityBLL.GetModels(p => true).GroupBy(p => p.省).Select(p => p.FirstOrDefault());

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
            var regions = development_CityBLL.GetModels(p => p.省 == province).GroupBy(p => p.市).Select(p => p.FirstOrDefault());

            var cityes = new ArrayList();
            foreach (var item in regions)
            {
                SimpleCity simpleCity = new SimpleCity();
                simpleCity.ID = item.ID;
                simpleCity.City = item.市;
                simpleCity.Level = item.城市分级;
                var city = new SimpleCity[] { simpleCity };
                cityes.Add(city);

            }
            return Json(cityes);

        }

        /// <summary>
        /// 获取商场
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMarket(int cid)
        {
            var models = development_MarketBLL.GetModels(p => p.所属城市ID == cid);
            var markets = new ArrayList();
            foreach (var item in models)
            {
                SimpleMarket market = new SimpleMarket();
                market.ID = item.ID;
                market.Level = item.商场等级;
                market.Name = item.商场名称;

                var m = new SimpleMarket[] { market };

                markets.Add(m);
            }
            return Json(markets);
        }

        /// <summary>
        /// 获取经销商
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFranchiser(int cid)
        {
            var models = development_FranchiserBLL.GetModels(p => p.城市ID == cid);
            var arr = new ArrayList();
            foreach (var item in models)
            {
                SimpleFranchiser franchiser = new SimpleFranchiser();
                franchiser.ID = item.ID;

                franchiser.Name = item.企业名称;

                var m = new SimpleFranchiser[] { franchiser };

                arr.Add(m);
            }
            return Json(arr);
        }

        /// <summary>
        /// 简化的城市类
        /// </summary>
        private class SimpleCity
        {
            public int ID { get; set; }
            public string City { get; set; }
            public string Level { get; set; }
        }

        /// <summary>
        /// 简化的商场类
        /// </summary>
        private class SimpleMarket
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Level { get; set; }

        }

        /// <summary>
        /// 简化的经销商类
        /// </summary>
        private class SimpleFranchiser
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        #region 构建数据实体

        /// <summary>
        /// 构建单个城市实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Development_CityViewModel BuildCityModel(拓展_城市档案 model)
        {
            Development_CityViewModel cityViewModel = new Development_CityViewModel();
            cityViewModel.ID = model.ID;
            cityViewModel.城市分级 = model.城市分级;
            cityViewModel.市 = model.市;
            cityViewModel.录入人ID = model.录入人ID;
            cityViewModel.录入人 = accountBLL.GetModel(p => p.ID == model.录入人ID).姓名;
            cityViewModel.是否为目标城市 = model.是否为目标城市;
            cityViewModel.更新时间 = model.更新时间;
            cityViewModel.省 = model.省;
            return cityViewModel;
        }
        /// <summary>
        /// 构建城市实体集
        /// </summary>
        /// <returns></returns>
        private List<Development_CityViewModel> BuildCityModels()
        {
            var models = development_CityBLL.GetModels(p => true);
            List<Development_CityViewModel> lis = new List<Development_CityViewModel>();
            foreach (var item in models)
            {
                var model = BuildCityModel(item);
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 构建单个商场实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Development_MarketViewModel BuildDevelopment_MarketwModel(拓展_商场档案表 model)
        {
            Development_MarketViewModel marketViewModel = new Development_MarketViewModel();
            marketViewModel.ID = model.ID;
            marketViewModel.商场名称 = model.商场名称;
            marketViewModel.商场等级 = model.商场等级;
            marketViewModel.地址 = model.地址;
            marketViewModel.城市等级 = model.城市等级;
            marketViewModel.录入人ID = model.录入人ID;
            marketViewModel.录入人 = accountBLL.GetModel(p => p.ID == model.录入人ID).姓名;
            
            marketViewModel.所属城市ID = model.所属城市ID;
            marketViewModel.所属城市 = development_CityBLL.GetModel(p => p.ID == model.所属城市ID).市;
            marketViewModel.更新日期 = model.更新日期;
            marketViewModel.职务 = model.职务;
            marketViewModel.联系方式 = model.联系方式;
            marketViewModel.负责人 = model.负责人;
           
            marketViewModel.物业名称 = model.物业名称;
            marketViewModel.经营总面积 = model.经营总面积;
            marketViewModel.续约开始日期1 = model.续约开始日期1;
            marketViewModel.续约开始日期2 = model.续约开始日期2;
            marketViewModel.品牌调整时间 = model.品牌调整时间;
            return marketViewModel;


        }

        /// <summary>
        /// 构建商场实体集
        /// </summary>
        /// <param name="cid">城市ID</param>
        /// <returns></returns>
        private List<Development_MarketViewModel> BuildDevelopment_MarketwModels(int? cid)
        {
            List<Development_MarketViewModel> lis = new List<Development_MarketViewModel>();
            List<拓展_商场档案表> list = new List<拓展_商场档案表>();
            if (cid > 0)
            {
                var models = development_MarketBLL.GetModels(p => p.所属城市ID == cid);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            else
            {
                var models = development_MarketBLL.GetModels(p => true);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var model = BuildDevelopment_MarketwModel(item);
                    lis.Add(model);
                }
            }

            return lis;

        }

        /// <summary>
        /// 构建商场_租金档案实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Development_MarketRentalViewModel BuildDevelopment_MarketRentalViewModel(拓展_商场_租金档案 model) {
            Development_MarketRentalViewModel rentalViewModel = new Development_MarketRentalViewModel();
            rentalViewModel.ID = model.ID;
            rentalViewModel.商场ID = model.商场ID;
            rentalViewModel.租金年 = model.租金年;
            rentalViewModel.租金金额 = model.租金金额;
            return rentalViewModel;
        }


        /// <summary>
        /// 构建商场_租金档案实体集
        /// </summary>
        /// <param name="id">商场ID</param>
        /// <returns></returns>
        private List<Development_MarketRentalViewModel> BuildDevelopment_MarketRentalViewModels(int id) {
            List<Development_MarketRentalViewModel> lis = new List<Development_MarketRentalViewModel>();
            var models = development_Market_RentalBLL.GetModels(p => p.商场ID == id);
            if (models!=null)
            {
                foreach (var item in models)
                {
                    var model = BuildDevelopment_MarketRentalViewModel(item);
                    lis.Add(model);
                }
            }
            return lis;
        }
        /// <summary>
        /// 构建经销商实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Development_FranchiserViewModel BuildDevelopment_FranchiserViewModel(拓展_经销商档案表 model)
        {
            Development_FranchiserViewModel franchiserViewModel = new Development_FranchiserViewModel();
            franchiserViewModel.ID = model.ID;
            franchiserViewModel.代理品牌 = model.代理品牌;
            franchiserViewModel.企业名称 = model.企业名称;
            franchiserViewModel.入驻商场 = model.入驻商场;
            franchiserViewModel.团队人数 = model.团队人数;
            franchiserViewModel.地址 = model.地址;
            franchiserViewModel.城市ID = model.城市ID;
            franchiserViewModel.城市 = development_CityBLL.GetModel(p => p.ID == model.城市ID).市;
            franchiserViewModel.城市等级 = development_CityBLL.GetModel(p => p.ID == model.城市ID).城市分级;
            franchiserViewModel.备注 = model.备注;
            franchiserViewModel.年销售额 = model.年销售额;
            franchiserViewModel.录入人ID = model.录入人ID;
            franchiserViewModel.录入人 = accountBLL.GetModel(p => p.ID == model.录入人ID).姓名;
            franchiserViewModel.意向位置分级 = model.意向位置分级;
            franchiserViewModel.意向商场ID = model.意向商场ID;
            franchiserViewModel.商场等级 = model.商场等级;
            franchiserViewModel.意向商场 = development_MarketBLL.GetModel(p => p.ID == model.意向商场ID).商场名称;
            franchiserViewModel.投资预算 = model.投资预算; ;
            franchiserViewModel.是否现代品牌 = model.是否现代品牌;
            franchiserViewModel.更新日期 = model.更新日期;
           
            franchiserViewModel.社会资源描述 = model.社会资源描述;
            franchiserViewModel.经营年限 = model.经营年限;
            franchiserViewModel.门店数量 = model.门店数量;
            franchiserViewModel.预算面积 = model.预算面积;
            franchiserViewModel.匹配度 = model.匹配度;
            franchiserViewModel.盈利情况 = model.盈利情况;
            return franchiserViewModel;
        }
        /// <summary>
        /// 构建经销商实体集
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        private List<Development_FranchiserViewModel> BuildDevelopment_FranchiserViewModels(int? cid)
        {
            List<Development_FranchiserViewModel> lis = new List<Development_FranchiserViewModel>();
            List<拓展_经销商档案表> list = new List<拓展_经销商档案表>();
            if (cid > 0)
            {
                var models = development_FranchiserBLL.GetModels(p => p.城市ID == cid);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            else
            {
                var models = development_FranchiserBLL.GetModels(p => true);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var model = BuildDevelopment_FranchiserViewModel(item);
                    lis.Add(model);
                }

            }
            return lis;
        }

        /// <summary>
        /// 构建经销商_联系人
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Development_FranchiserContactsViewModel BuildDevelopment_FranchiserContactsModel(拓展_经销商_联系人 model) {
           Development_FranchiserContactsViewModel contactsViewModel = new Development_FranchiserContactsViewModel();
            contactsViewModel.ID = model.ID;
            contactsViewModel.姓名 = model.姓名;
            contactsViewModel.年龄 = model.年龄;
            contactsViewModel.性别 = model.性别;
            contactsViewModel.经销商档案ID = model.经销商档案ID;
            contactsViewModel.职务 = model.职务;
            contactsViewModel.联系方式 = model.联系方式;
            return contactsViewModel;

        }

        /// <summary>
        /// 构建经销商_联系人实体集
        /// </summary>
        /// <param name="id">经销商ID</param>
        /// <returns></returns>
        private List<Development_FranchiserContactsViewModel> BuildDevelopment_FranchiserContactsModels(int id) {
            List<Development_FranchiserContactsViewModel> lis = new List<Development_FranchiserContactsViewModel>();
            var models = development_FranchiserContactsBLL.GetModels(p => p.经销商档案ID == id);
            if (models!=null)
            {
                foreach (var item in models)
                {
                    var model = BuildDevelopment_FranchiserContactsModel(item);
                    lis.Add(model);
                }

            }
            return lis;
        }
        



        /// <summary>
        /// 构建竞品实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Development_CompetesViewModel BuildDevelopment_CompetesModel(拓展_竞品档案表 model)
        {
            Development_CompetesViewModel competesViewModel = new Development_CompetesViewModel();
            competesViewModel.ID = model.ID;
            competesViewModel.位置 = model.位置;
            competesViewModel.入住日期 = model.入住日期;
            competesViewModel.商场ID = model.商场ID;
            competesViewModel.商场 = development_MarketBLL.GetModel(p => p.ID == model.商场ID).商场名称;
            competesViewModel.城市ID = model.城市ID;
            competesViewModel.城市 = development_CityBLL.GetModel(p => p.ID == model.城市ID).市;
            competesViewModel.录入人ID = model.录入人ID;
            competesViewModel.录入人 = accountBLL.GetModel(p => p.ID == model.录入人ID).姓名;
            competesViewModel.更新日期 = model.更新日期;
            competesViewModel.经销商ID = model.经销商ID;
            competesViewModel.经销商 = development_FranchiserBLL.GetModel(p => p.ID == model.经销商ID).企业名称;
            competesViewModel.面积 = model.面积;
            competesViewModel.楼层 = model.楼层;
            competesViewModel.经营形态 = model.经营形态;
            competesViewModel.备注 = model.备注;
            competesViewModel.月店面客流 = model.月店面客流;
            competesViewModel.形象 = model.形象;
            competesViewModel.折扣 = model.折扣;
            return competesViewModel;
        }

        /// <summary>
        /// 构建竞品实体集
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        private List<Development_CompetesViewModel> BuildDevelopment_CompetesModels(int? cid)
        {

            List<Development_CompetesViewModel> lis = new List<Development_CompetesViewModel>();
            List<拓展_竞品档案表> list = new List<拓展_竞品档案表>();
            if (cid > 0)
            {
                var models = development_CompetesBLL.GetModels(p => p.城市ID == cid);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            else
            {
                var models = development_CompetesBLL.GetModels(p => true);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            if (list.Count() > 0)
            {
                foreach (var item in list)
                {
                    var model = BuildDevelopment_CompetesModel(item);
                    lis.Add(model);
                }

            }
            return lis;
        }

        /// <summary>
        /// 构建竞品_年度销售实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private Development_CometesAnnualSalesViewModel BuildDevelopment_CompetesAnnualSalesModel(拓展_竞品档案_年销售档案表 model) {
            Development_CometesAnnualSalesViewModel annualSalesViewModel = new Development_CometesAnnualSalesViewModel();
            annualSalesViewModel.ID = model.ID;
            annualSalesViewModel.销售额 = model.销售额;
            annualSalesViewModel.销售年 = model.销售年;
            annualSalesViewModel.竞品档案ID = model.竞品档案ID;
            annualSalesViewModel.销售月 = model.销售月;
            return annualSalesViewModel;

        }

        /// <summary>
        /// 构建竞品_年度销售档案实体集
        /// </summary>
        /// <param name="id">竞品档案ID</param>
        /// <returns></returns>
        private List<Development_CometesAnnualSalesViewModel> BuildDevelopment_CompetesAnnualSalesModels(int id) {
            
            List<Development_CometesAnnualSalesViewModel> list = new List<Development_CometesAnnualSalesViewModel>();
            var models = development_CompetesAnnualSalesBLL.GetModels(p => p.竞品档案ID == id);
            if (models!=null)
            {
                foreach (var item in models)
                {
                    var model = BuildDevelopment_CompetesAnnualSalesModel(item);
                    list.Add(model);
                }
            }
            return list;

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
    }
}