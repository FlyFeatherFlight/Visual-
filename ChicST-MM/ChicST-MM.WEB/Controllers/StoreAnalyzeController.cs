﻿using ChicST_MM.IBLL;
using ChicST_MM.Model;
using ChicST_MM.WEB.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ChicST_MM.WEB.Controllers
{
    /// <summary>
    /// 门店数据分析
    /// </summary>
    public class StoreAnalyzeController : Controller
    {
        private readonly IStoreEmployeesBLL storeEmployeesBLL;
        private readonly IStoreBLL storeBLL;
        private readonly ITrackGoalBLL trackGoalBLL;
        private readonly ICustomerInfoBLL customerInfoBLL;
        private readonly ICustomerTrackingBLL customerTrackingBLL;
        private static List<StoreRecRecordsViewModel> storeRecViewModels;
        private readonly ISalesRecordBLL salesRecordBLL;

        public StoreAnalyzeController(IStoreEmployeesBLL storeEmployeesBLL, IStoreBLL storeBLL, ITrackGoalBLL trackGoalBLL, ICustomerInfoBLL customerInfoBLL, ICustomerTrackingBLL customerTrackingBLL, ISalesRecordBLL salesRecordBLL)
        {
            this.storeEmployeesBLL = storeEmployeesBLL ?? throw new ArgumentNullException(nameof(storeEmployeesBLL));
            this.storeBLL = storeBLL ?? throw new ArgumentNullException(nameof(storeBLL));
            this.trackGoalBLL = trackGoalBLL ?? throw new ArgumentNullException(nameof(trackGoalBLL));
            this.customerInfoBLL = customerInfoBLL ?? throw new ArgumentNullException(nameof(customerInfoBLL));
            this.customerTrackingBLL = customerTrackingBLL ?? throw new ArgumentNullException(nameof(customerTrackingBLL));
            this.salesRecordBLL = salesRecordBLL ?? throw new ArgumentNullException(nameof(salesRecordBLL));
        }



        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        // GET: StoreAnalyze
        public ActionResult Index(int? storeid, int? emid, string startDate, string EndDate)
        {
            DateTime dt = DateTime.Now;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末

            storeRecViewModels = BuildStoreRec(0, null, startMonth.ToString("yyyy-MM-dd"), endMonth.ToString("yyyy-MM-dd"), out int ExcCount, out int RecCount, out int TrackCount);//默认显示当月数据
            storeRecViewModels = storeRecViewModels.OrderBy(p => p.店铺ID).ThenByDescending(p => p.ID).ToList();//先按照店铺ID排序，再按ID降序排列
            ViewBag.ExcCount = ExcCount;
            ViewBag.RecCount = RecCount;
            ViewBag.TrackCount = TrackCount;

            return View(storeRecViewModels);



        }

        /// <summary>
        /// 单个接待具体信息
        /// </summary>
        /// <param name="id">接待ID</param>
        /// <returns></returns>
        public ActionResult CustomerInfoView(int id)
        {
            var m = customerInfoBLL.GetModel(p => p.ID == id);
            var model = BuildRecRecordsViewModel(m);
            return View(model);
        }
        /// <summary>
        /// 获取门店目录
        /// </summary>
        /// <returns></returns>                                                                                                          
        [HttpGet]
        public ActionResult Getstores()
        {
            var lis = BuildSimpleStores();
            return Json(lis, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 构造接待数据统计
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="emid"></param>
        /// <param name="startDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="ExcCount"></param>
        /// <param name="RecCount"></param>
        /// <param name="TrackCount"></param>
        /// <returns></returns>
        public List<StoreRecRecordsViewModel> BuildStoreRec(int? storeid, int? emid, string startDate, string EndDate, out int ExcCount, out int RecCount, out int TrackCount)
        {
            List<StoreRecRecordsViewModel> models = new List<StoreRecRecordsViewModel>();
            models = BuildCustomerInfos(storeid, emid, startDate, EndDate);
            ExcCount = 0;//该店铺意向确认人数
            RecCount = 0;
            TrackCount = 0;
            if (models.Count()==0)
            {
                return null;
            }
           
            if (storeid > 0)//为0，则查看全国信息
            {
                var m = models.Where(p => p.店铺ID == storeid);
                if (m.Count() > 0)
                {
                    models = m.ToList();
                }
                else
                {

                    return models = null;

                }
            }
            else
            {
                if (models == null)
                {
                    return models;
                }
            }
            RecCount = models.Count();//接待总人数
            TrackCount = models.Where(p => p.跟进人ID != null).Count();//跟进总人数
            ExcCount = models.Where(p => p.是否有意向 == true).Count();
            if (emid > 0)   //根据店员查看
            {
                TrackCount = models.Where(p => p.跟进人ID == emid).Count();//员工跟进人数
                models = models.Where(p => p.接待人ID == emid).ToList();
                models.OrderByDescending(p => p.ID);
                RecCount = models.Count();//员工接待人数
                ExcCount = models.Where(p => p.是否有意向 == true).Count();////员工意向确认人数
            }
            if (startDate != "" && startDate != null)//筛选开始时间
            {
                DateTime startTime = Convert.ToDateTime(startDate);
                models = models.Where(p => p.接待日期 > startTime).ToList();
                RecCount = models.Count();//接待人数
                TrackCount = models.Where(p => p.跟进人ID != null).Count();
                ExcCount = models.Where(p => p.是否有意向 == true).Count();////员工意向确认人数
                //models = startDate != null ?
                //    models.Where(t => t.接待日期>startTime).ToList() :
                //    models.ToList();
            }
            if (EndDate != "" && EndDate != null)//筛选结束时间
            {
                DateTime endTime = Convert.ToDateTime(EndDate);
                models = models.Where(p => p.接待日期 < endTime).ToList();
                RecCount = models.Count();//接待人数
                TrackCount = models.Where(p => p.跟进人ID != null).Count();
                ExcCount = models.Where(p => p.是否有意向 == true).Count();////员工意向确认人数
            }


            return models;

        }

        /// <summary>
        /// 获取门店及员工接待数据
        /// </summary>
        /// <param name="storeid">门店ID</param>
        /// <param name="emid">员工id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRecving(int? storeid, int? emid, string startDate, string EndDate)
        {
            List<StoreRecRecordsViewModel> models = new List<StoreRecRecordsViewModel>();
            int TrackCount = 0;
            int RecCount = 0;
            int ExcCount = 0;
            //该店铺意向确认人数
            try
            {
                models = BuildStoreRec(storeid, emid, startDate, EndDate, out ExcCount, out RecCount, out TrackCount);
            }
            catch (Exception e)
            {

                throw e;
            }

            if (models == null)
            {
                return Json(new { success = false, msg = "查询失败!" });
            }
            return Json(new { data = models.ToArray(), recCount = RecCount, trackCount = TrackCount, excCount = ExcCount });
        }

        /// <summary>
        /// 获得门店员工名录
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public ActionResult GetStoreEmployees(int storeid)
        {
            var models = BuildEmployees(storeid);
            models.Where(p => p.停用标志 != true);
            return Json(models);

        }

        /// <summary>
        ///全国接待数量统计(简单)视图
        /// </summary>
        /// <returns></returns>
        public ActionResult AllStoreRecStatisticsView()
        {
            return View();
        }

        /// <summary>
        /// 全国接待数量统计(简单)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AllStoreRecStatistics(int? storeid, int? emid, string startDate, string EndDate)
        {
            var storeRecViewModels = BuildCustomerInfos(storeid, emid, startDate, EndDate);
            if (storeRecViewModels == null)
            {
                storeRecViewModels = BuildCustomerInfos(storeid, emid, startDate, EndDate);

                storeRecViewModels = storeRecViewModels.OrderBy(p => p.店铺ID).ThenByDescending(p => p.ID).ToList();//先按照店铺ID排序，再按ID降
            }

            var models = BuildSimpleStores();
            for (int i = 0; i < models.Count(); i++)
            {
                var id = models.ElementAt(i).ID;
                if (storeRecViewModels.Where(p => p.店铺ID == id) != null)
                {
                    models.ElementAt(i).Count = storeRecViewModels.Where(p => p.店铺ID == id).Count();
                }
                else
                {
                    models.ElementAt(i).Count = 0;
                }

            }
            models = models.OrderBy(p => p.ID).ToList();
            return Json(models, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 门店数据月报表
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreMonthView()
        {

            return View();
        }

        /// <summary>
        /// 月报数据JSON
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreMonthData(int? storeID, int? emID, int month)
        {
            List<StoreAnalyzeModel> models = new List<StoreAnalyzeModel>();
            try
            {
                models = BuildRecAnalyzeMonthModels(storeID, emID, month);
            }


            catch (Exception e)
            {

                return Json(new { success = false, msg = "查询异常！" + e.Message });
            }
            if (models!=null)
            {
                return Json(new { success = true, msg = "查询成功！", data = models });
            }
            else
            {
                return Json(new { success = false, msg = "查询失败！" });
            }
        }

        /// <summary>
        ///门店数据分析
        /// </summary>
        /// <param name="storeID">门店ID</param>
        /// <param name="emID">销售人员</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAnalyzeJson(int storeID, int? emID, DateTime? startDate, DateTime? endDate)
        {
            List<StoreAnalyzeModel> models = new List<StoreAnalyzeModel>();
            try
            {
                models = BuildRecAnalyzeViewModels(storeID, emID, startDate, endDate);
            }
            catch (Exception e)
            {

                return Json(new { success = false, msg = e.Message });
            }

            return Json(new { success = true, msg = "查询成功!", data = models });
        }


        /// <summary>
        /// 所有门店信息
        /// </summary>
        /// <returns></returns>
        private List<StoreViewModel> BuildStores()
        {
            var models = storeBLL.GetModels(p => true);
            List<StoreViewModel> lis = new List<StoreViewModel>();
            foreach (var item in models)
            {
                var model = BuildStore(item);
                lis.Add(model);
            }
            return lis;


        }


        /// <summary>
        /// 销售成交列表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesRecordView(int? storeid, int? emid, DateTime? startDate, DateTime? EndDate)
        {

            var models = BuildSalesRecordModels(storeid, emid, startDate, EndDate);
            models = models.OrderBy(p => p.店铺ID).ThenByDescending(p => p.销售日期).ToList();
            if (storeid>0)
            {
                ViewBag.StoreName = storeBLL.GetModel(p => p.ID == storeid).名称;
            }
            else
            {
                ViewBag.StoreName = "全国";
            }
            return View(models);
        }

        /// <summary>
        /// 跟进列表视图
        /// </summary>
        /// <param name="storeid">店铺ID</param>
        /// <param name="emid">销售人员ID</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="EndDate">结束时间</param>
        /// <returns></returns>
        public ActionResult CustomerTrackListView(int storeid, int? emid, DateTime? startDate, DateTime? EndDate,string phoneNum) {
            ViewBag.StoreID = storeid;
            if (startDate==null&&EndDate==null)
            {
                DateTime dt = DateTime.Now;
                startDate = dt.AddDays(1 - dt.Day);  //本月月初
                EndDate = startDate.Value.AddMonths(1).AddDays(-1);  //本月月末
            }
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = EndDate;
            ViewBag.PhNum = phoneNum;
            ViewBag.EmID = emid;
            var models = BuildStoreCustomer_TackModels(storeid,emid,startDate,EndDate);
            if (models.Count()>0)
            {
                if (phoneNum!=null&&phoneNum!="")
                {
                    var m = models.Where(p => p.客户电话 ==phoneNum);
                    if (m != null)
                    {
                        models = m.ToList();
                    }
                }
                if (startDate!=null)
                {
                    var m = models.Where(p => p.跟进时间 >= startDate);
                    if (m != null)
                    {
                        models = m.ToList();
                    }
                }
                if (EndDate != null)
                {
                    var m = models.Where(p => p.跟进时间 <= EndDate);
                    if (m != null)
                    {
                        models = m.ToList();
                    }

                }
                
            }
            return View(models);

        }


        /// <summary>
        /// 构建简单门店信息，只有ID/名字
        /// </summary>
        /// <returns></returns>
        private List<StoreClass> BuildSimpleStores()
        {

            var stores = BuildStores();
            StoreClass[] arr = new StoreClass[stores.Count];

            for (int i = 0; i < stores.Count; i++)
            {

                StoreClass myClass = new StoreClass() { };
                myClass.Name = stores.ElementAt(i).名称;
                myClass.ID = stores.ElementAt(i).ID;
                arr[i] = myClass;

            }
            var lis = arr.ToList();
            return lis;
        }

        /// <summary>
        /// 单个门店信息
        /// </summary>
        /// <returns></returns>
        private StoreViewModel BuildStore(销售_店铺档案 model)
        {
            StoreViewModel storeViewModel = new StoreViewModel();
            storeViewModel.ID = model.ID;
            storeViewModel.使用面积 = model.使用面积;
            storeViewModel.停用标志 = model.停用标志;
            storeViewModel.制单人 = model.制单人;
            storeViewModel.制单日期 = model.制单日期;
            storeViewModel.名称 = model.名称;
            storeViewModel.品牌ID = model.品牌ID;
            storeViewModel.商场 = model.商场;
            storeViewModel.地区ID = model.地区ID;
            storeViewModel.地址 = model.地址;
            storeViewModel.密码 = model.密码;
            storeViewModel.收货人 = model.收货人;
            storeViewModel.收货人电话 = model.收货人电话;
            storeViewModel.收货地址 = model.收货地址;
            storeViewModel.等级 = model.等级;
            storeViewModel.经销商ID = model.经销商ID;
            storeViewModel.编号 = model.编号;
            storeViewModel.联系人 = model.联系人;
            storeViewModel.联系人电话 = model.联系人电话;
            storeViewModel.负责人 = model.负责人;
            storeViewModel.负责人电话 = model.负责人电话;

            return storeViewModel;


        }

        /// <summary>
        /// 构造员工信息
        /// </summary>
        /// <param name="storeid">店铺ID</param>
        /// <returns></returns>
        private List<StoreEmployeesViewModel> BuildEmployees(int storeid)
        {
            var models = storeEmployeesBLL.GetModels(p => p.店铺ID == storeid);
            List<StoreEmployeesViewModel> lis = new List<StoreEmployeesViewModel>();
            foreach (var item in models)
            {
                var model = BuildEmployee(item);
                lis.Add(model);
            }
            return lis;
        }


        /// <summary>
        /// 构造单个员工信息
        /// </summary>
        /// <returns></returns>
        private StoreEmployeesViewModel BuildEmployee(销售_店铺员工档案 model)
        {
            StoreEmployeesViewModel employeeViewModel = new StoreEmployeesViewModel();
            employeeViewModel.ID = model.ID;
            employeeViewModel.停用标志 = model.停用标志;
            employeeViewModel.制单人 = model.制单人;
            employeeViewModel.制单日期 = model.制单日期;

            employeeViewModel.姓名 = model.姓名;

            employeeViewModel.店铺ID = model.店铺ID;
            employeeViewModel.店铺 = storeBLL.GetModel(p => p.ID == model.店铺ID).名称;
            employeeViewModel.性别 = model.性别;
            employeeViewModel.是否店长 = model.是否店长;
            employeeViewModel.是否设计师 = model.是否设计师;
            employeeViewModel.是否销售 = model.是否销售;
            employeeViewModel.等级 = model.等级;
            employeeViewModel.编号 = model.编号;
            employeeViewModel.职务ID = model.职务ID;
            employeeViewModel.联系方式 = model.联系方式;
            employeeViewModel.跟进目标计划数 = model.跟进目标计划数;
            return employeeViewModel;
        }

        /// <summary>
        /// 构建单个接待信息
        /// </summary>
        /// <returns></returns>
        private StoreRecRecordsViewModel BuildRecRecordsViewModel(销售_接待记录 model)
        {
            StoreRecRecordsViewModel receivingRecordsViewModel = new StoreRecRecordsViewModel();
            try
            {
                receivingRecordsViewModel.ID = model.ID;
                receivingRecordsViewModel.不喜欢产品 = model.不喜欢产品;
                receivingRecordsViewModel.不喜欢元素 = model.不喜欢元素;
                receivingRecordsViewModel.主导者 = model.主导者;
                receivingRecordsViewModel.主导者喜好风格 = model.主导者喜好风格;
                receivingRecordsViewModel.使用空间 = model.使用空间;
                receivingRecordsViewModel.关闭备注 = model.关闭备注;
                receivingRecordsViewModel.其它空间备注 = model.其它空间备注;
                receivingRecordsViewModel.其它空间家具 = model.其它空间家具;
                receivingRecordsViewModel.其它空间预算 = model.其它空间预算;
                receivingRecordsViewModel.出店时间 = model.出店时间;
                receivingRecordsViewModel.制单日期 = model.制单日期;
                receivingRecordsViewModel.卧室预算备注 = model.卧室预算备注;
                receivingRecordsViewModel.卧室预算家具 = model.卧室预算家具;
                receivingRecordsViewModel.卧室预算金额 = model.卧室预算金额;
                receivingRecordsViewModel.同行人 = model.同行人;
                receivingRecordsViewModel.喜欢产品 = model.喜欢产品;
                receivingRecordsViewModel.喜欢元素 = model.喜欢元素;
                receivingRecordsViewModel.如何得知品牌 = model.如何得知品牌;
                receivingRecordsViewModel.安装地址 = model.安装地址;
                receivingRecordsViewModel.客厅预算备注 = model.客厅预算备注;
                receivingRecordsViewModel.客厅预算家具 = model.客厅预算家具;
                receivingRecordsViewModel.客厅预算金额 = model.客厅预算金额;
                receivingRecordsViewModel.客户姓名 = model.客户姓名;
                receivingRecordsViewModel.客户建议 = model.客户建议;
                receivingRecordsViewModel.客户来源 = model.客户来源;
                receivingRecordsViewModel.客户电话 = model.客户电话;
                receivingRecordsViewModel.客户目的 = model.客户目的;
                receivingRecordsViewModel.客户着装 = model.客户着装;
                receivingRecordsViewModel.客户类别 = model.客户类别;
                receivingRecordsViewModel.客户类型 = model.客户类型;
                receivingRecordsViewModel.客户职业 = model.客户职业;
                receivingRecordsViewModel.家居使用者 = model.家居使用者;
                receivingRecordsViewModel.年龄段 = model.年龄段;
                receivingRecordsViewModel.店铺ID = model.店铺ID;
                receivingRecordsViewModel.店铺 = storeBLL.GetModel(p => p.ID == model.店铺ID).名称;
                receivingRecordsViewModel.性别 = model.性别;
                receivingRecordsViewModel.户型大小 = model.户型大小;
                receivingRecordsViewModel.接待人ID = model.接待人ID;
                if (model.接待人ID != 0)
                {
                    receivingRecordsViewModel.接待人 = storeEmployeesBLL.GetModel(p => p.ID == model.接待人ID).姓名;

                }
                receivingRecordsViewModel.接待序号 = model.接待序号;
                receivingRecordsViewModel.接待日期 = model.接待日期;
                receivingRecordsViewModel.是否关闭 = model.是否关闭;
                receivingRecordsViewModel.是否有意向 = model.是否有意向;
                receivingRecordsViewModel.更新人 = model.更新人;
                if (model.更新人 != null)
                {
                    receivingRecordsViewModel.更新人姓名 = storeEmployeesBLL.GetModel(p => p.ID == model.更新人).姓名;

                }
                receivingRecordsViewModel.更新日期 = model.更新日期;
                receivingRecordsViewModel.来店次数 = model.来店次数;
                receivingRecordsViewModel.比较品牌 = model.比较品牌;
                receivingRecordsViewModel.比较品牌产品 = model.比较品牌产品;
                receivingRecordsViewModel.比较品牌产品备注 = model.比较品牌产品备注;
                receivingRecordsViewModel.特征 = model.特征;
                receivingRecordsViewModel.社交软件 = model.社交软件;
                receivingRecordsViewModel.装修情况 = model.装修情况;
                receivingRecordsViewModel.装修进度 = model.装修进度;
                receivingRecordsViewModel.装修风格 = model.装修风格;
                receivingRecordsViewModel.设计师 = model.设计师;
                receivingRecordsViewModel.跟进人ID = model.跟进人ID;
                if (model.跟进人ID != null)
                {
                    receivingRecordsViewModel.跟进人 = storeEmployeesBLL.GetModel(p => p.ID == model.跟进人ID).姓名;

                }
                receivingRecordsViewModel.返点 = model.返点;
                receivingRecordsViewModel.进店时长 = model.进店时长;
                receivingRecordsViewModel.进店时间 = model.进店时间;
                receivingRecordsViewModel.销售讲解 = model.销售讲解;
                receivingRecordsViewModel.预报价折扣 = model.预报价折扣;
                receivingRecordsViewModel.预算金额 = model.预算金额;
                receivingRecordsViewModel.预计使用时间 = model.预计使用时间;
                receivingRecordsViewModel.餐厅预算备注 = model.餐厅预算备注;
                receivingRecordsViewModel.餐厅预算家具 = model.餐厅预算家具;
                receivingRecordsViewModel.餐厅预算金额 = model.餐厅预算金额;
                var m = salesRecordBLL.GetModels(p => p.接待记录ID == model.ID);
                if (m.Count() > 0)
                {
                    decimal money = 0;
                    foreach (var item in m)
                    {
                        money = money + item.销售金额;
                    }
                    receivingRecordsViewModel.成交金额 = money;
                }
            }
            catch (Exception e)
            {

                throw e;
            }

            return receivingRecordsViewModel;
        }

        /// <summary>
        /// 构造所有店铺客户信息
        /// </summary>
        /// <param name="storeid">店铺ID</param>
        /// <returns></returns>
        private List<StoreRecRecordsViewModel> BuildCustomerInfos(int? storeid, int? emid, string startDate, string EndDate)
        {

            List<StoreRecRecordsViewModel> lis = new List<StoreRecRecordsViewModel>();
            List<销售_接待记录> list = new List<销售_接待记录>();
            if (storeid > 0)
            {
                var models = customerInfoBLL.GetModels(p => p.店铺ID == storeid);
                if (models != null)
                {
                    list = models.ToList();
                }
            }

            else
            {
                var models = customerInfoBLL.GetModels(p => true);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            if (emid > 0 && storeid > 0)
            {
                var models = list.Where(p => p.接待人ID == emid);
                if (models != null)
                {
                    list = models.ToList();
                }

            }
           
            if (startDate != null && startDate != ""&&list.Count()>0)
            {
                DateTime startTime = Convert.ToDateTime(startDate);
                var models = list.Where(p => p.接待日期 >= startTime);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            if (EndDate != null && EndDate != "" && list.Count() > 0)
            {
                DateTime endTime = Convert.ToDateTime(EndDate);
                var models = list.Where(p => p.接待日期 <= endTime);
                if (models != null)
                {
                    list = models.ToList();
                }
            }
            foreach (var item in list)
            {
                var model = BuildRecRecordsViewModel(item);
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 构建单个客户的跟进
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private StoreCustomer_TrackViewModel BuildStoreCustomer_TackModel(销售_意向追踪日志 model)
        {

            StoreCustomer_TrackViewModel storeCustomer = new StoreCustomer_TrackViewModel();
            var m = customerInfoBLL.GetModel(p => p.ID == model.接待记录ID);
            storeCustomer.Id = model.id;
            storeCustomer.备注 = model.备注;
            storeCustomer.跟进结果 = model.跟进结果;
            storeCustomer.跟进时间 = model.跟进时间;
            storeCustomer.跟进方式 = model.跟进方式;
            storeCustomer.跟进内容 = model.跟进内容;
            storeCustomer.店铺ID = model.店铺ID;
            storeCustomer.跟进人ID = model.跟进人;
            storeCustomer.跟进人 = storeEmployeesBLL.GetModel(p => p.ID == model.跟进人).姓名;
            storeCustomer.是否申请设计师 = model.是否申请设计师;
            storeCustomer.接待ID = model.接待记录ID;
            storeCustomer.店长审核 = model.店长审核;
            storeCustomer.客户电话 = m.客户电话;
            storeCustomer.店铺 = storeBLL.GetModel(p => p.ID == model.店铺ID).名称;
            storeCustomer.客户姓名 = m.客户姓名;
            return storeCustomer;
        }

        /// <summary>
        /// 当前店铺所有跟进记录
        /// </summary>
        /// <returns></returns>
        private List<StoreCustomer_TrackViewModel> BuildStoreCustomer_TackModels(int storeID, int? emid, DateTime? startDate, DateTime? EndDate)
        {
            var models = customerTrackingBLL.GetModels(p => p.店铺ID == storeID);
            if (emid > 0)
            {
                models = models.Where(p => p.跟进人 == emid);
            }
            if (startDate!=null)
            {
                models = models.Where(p => p.跟进时间 >= startDate);
            }
            if (EndDate!=null)
            {
                models = models.Where(p => p.跟进时间 <= EndDate);
            }
            if (models==null)
            {
                return null;
            }
            List<StoreCustomer_TrackViewModel> lis = new List<StoreCustomer_TrackViewModel>();
            foreach (var item in models)
            {
                var model = BuildStoreCustomer_TackModel(item);
                lis.Add(model);
            }
            return lis;
        }

        /// <summary>
        /// 构建销售成交单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private SalesRecordViewModel BuildSalesRecordModel(销售_接待成交单 model)
        {
            SalesRecordViewModel salesRecord = new SalesRecordViewModel();
            salesRecord.ID = model.ID;
            salesRecord.制单人员ID = model.制单人员ID;
            salesRecord.制单日期 = model.制单日期;
            salesRecord.合同单号 = model.合同单号;
            salesRecord.备注 = model.备注;
            salesRecord.折扣 = model.折扣;
            salesRecord.接待记录ID = model.接待记录ID;
            salesRecord.是否全款 = model.是否全款;
            salesRecord.更新人ID = model.更新人ID;
            salesRecord.更新日期 = model.更新日期;
            salesRecord.订库样 = model.订库样;
            salesRecord.销售人员ID = model.销售人员ID;
            salesRecord.销售人员 = storeEmployeesBLL.GetModel(p => p.ID == model.销售人员ID).姓名;
            salesRecord.销售单号 = model.销售单号;
            salesRecord.销售日期 = model.销售日期;
            salesRecord.销售金额 = model.销售金额;
            salesRecord.店铺ID = model.店铺ID;
            salesRecord.店铺 = storeBLL.GetModel(p => p.ID == model.店铺ID).名称;
            return salesRecord;


        }

        /// <summary>
        /// 构建成交数据集
        /// </summary>
        /// <param name="id">门店id</param>
        /// <returns></returns>
        private List<SalesRecordViewModel> BuildSalesRecordModels(int? id, int? emid, DateTime? startDate, DateTime? endDate)
        {
            List<销售_接待成交单> lis = new List<销售_接待成交单>();
            List<SalesRecordViewModel> list = new List<SalesRecordViewModel>();
            if (id > 0)
            {
                var models = salesRecordBLL.GetModels(p => p.店铺ID == id);
                if (models.Count() > 0)
                {
                    lis = models.ToList();

                    ////////////////
                    if (emid > 0)
                    {
                        var m = lis.Where(p => p.销售人员ID == emid);
                        if (m != null)
                        {
                            lis = m.ToList();
                        }
                        else
                        {
                            lis = null;

                        }

                    }
                }
            }
            else
            {

                var models = salesRecordBLL.GetModels(p => true);
                if (models.Count() > 0)
                {
                    lis = models.ToList();
                }
            }

            ////////////
            if (startDate != null)
            {
                if (lis.Count() > 0)
                {
                    var m = lis.Where(p => p.销售日期 > startDate);
                    if (m != null)
                    {
                        lis = m.ToList();
                    }
                }
                else
                {
                    lis = null;

                }
            }
            ////////////
            if (endDate != null)
            {
                if (lis.Count() > 0)
                {
                    var m = lis.Where(p => p.销售日期 < endDate);
                    if (m != null)
                    {
                        lis = m.ToList();
                    }
                }
                else
                {
                    lis = null;

                }
            }
            ////////




            if (lis != null)
            {
                foreach (var item in lis)
                {
                    var model = BuildSalesRecordModel(item);
                    list.Add(model);
                }
            }

            return list;
        }

        /// <summary>
        /// 门店数据分析
        /// </summary>
        /// <returns></returns>
        private List<StoreAnalyzeModel> BuildRecAnalyzeViewModels(int? storeID, int? emID, DateTime? startDate, DateTime? endDate)
        {
            List<销售_接待记录> recModels = new List<销售_接待记录>();
            List<StoreAnalyzeModel> lis = new List<StoreAnalyzeModel>();
            List<销售_店铺员工档案> lisem = new List<销售_店铺员工档案>();
            if (storeID > 0)//查询指定店铺
            {
                var models = customerInfoBLL.GetModels(p => p.店铺ID == storeID);//查询当前店铺
                var ems = storeEmployeesBLL.GetModels(p => p.店铺ID == storeID);
                if (ems != null)
                {
                    lisem = ems.ToList();
                }
                else
                {
                    lisem = null;
                }
                if (models.Count() > 0)
                {
                    recModels = models.ToList();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var models = customerInfoBLL.GetModels(p => true);//查询所有店铺
                if (models.Count() > 0)
                {
                    recModels = models.ToList();
                }
                else
                {
                    return null;
                }
                var ems = storeEmployeesBLL.GetModels(p => true);
                if (ems != null)
                {
                    lisem = ems.ToList();
                }
                else
                {
                    lisem = null;
                }
            }

            if (emID > 0)
            {
                var m = recModels.Where(p => p.接待人ID == emID);
                if (m.Count() > 0)
                {
                    recModels = m.ToList();
                }
                else
                {
                    return null;
                }
            }
            if (startDate != null)
            {
                var m = recModels.Where(p => p.接待日期 > startDate);
                if (m.Count() > 0)
                {
                    recModels = m.ToList();
                }
                else
                {
                    return null;
                }
            }
            if (endDate != null)
            {
                var m = recModels.Where(p => p.接待日期 < endDate);
                if (m.Count() > 0)
                {
                    recModels = m.ToList();
                }
                else
                {
                    return null;
                }
            }

            if (recModels != null && lisem != null)//数据合成
            {
                foreach (var item in lisem)
                {

                    var models = recModels.Where(p => p.接待人ID == item.ID);//不同接待人的集合
                    var modeldate = models.GroupBy(p => p.接待日期);//不同接待人接待数据按日期分组
                    foreach (var ite in modeldate)
                    {
                        StoreAnalyzeModel model = new StoreAnalyzeModel();
                        model.接待日期 = ite.Key;
                        model.意向确认人数 = ite.Count(p => p.是否确认 == true);
                        model.成交人数 = ite.Count(p => p.是否成交 == true);
                        model.接待人数 = ite.Count();
                        model.销售ID = item.ID;
                        model.销售姓名 = storeEmployeesBLL.GetModel(p => p.ID == item.ID).姓名;
                        model.店铺ID = ite.FirstOrDefault().店铺ID;
                        model.店铺 = storeBLL.GetModel(p => p.ID == model.店铺ID).名称;
                        List<SalesRecordViewModel> salesRecordsViewModels = new List<SalesRecordViewModel>();
                        foreach (var it in ite)
                        {
                            var sales = salesRecordBLL.GetModels(p => p.接待记录ID == it.ID);
                            foreach (var i in sales)
                            {
                                SalesRecordViewModel salesRecordsView = new SalesRecordViewModel();
                                salesRecordsView.销售日期 = i.销售日期;
                                salesRecordsView.销售金额 = i.销售金额;
                                salesRecordsView.销售人员ID = i.销售人员ID;
                                salesRecordsView.销售日期 = i.销售日期;

                                salesRecordsViewModels.Add(salesRecordsView);
                            }
                        }


                        if (model.接待人数 > 0)
                        {
                            model.确认率 = (model.意向确认人数 / model.接待人数).ToString("0.00%");
                        }
                        if (model.意向确认人数 > 0)
                        {
                            model.成交率 = (model.成交人数 / model.意向确认人数).ToString("0.00%");
                        }
                        else
                        {
                            model.成交率 = "0.00%";
                        }
                        model.达成率 = "0.00%";
                        if (salesRecordsViewModels.Count > 0)
                        {
                            model.成交金额 = salesRecordsViewModels.Where(p => p.销售人员ID == model.销售ID).Sum(p => p.销售金额);//指定日期，销售的成交金额
                            model.最大单值 = salesRecordsViewModels.Max(p => p.销售金额);
                        }

                        lis.Add(model);
                    }

                }
                return lis;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 月报数据
        /// </summary>
        /// <param name="storeID">店铺ID</param>
        /// <param name="emID">销售ID</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        private List<StoreAnalyzeModel> BuildRecAnalyzeMonthModels(int? storeID, int? emID, int month)
        {

            List<销售_接待记录> recModels = new List<销售_接待记录>();
            List<StoreAnalyzeModel> lis = new List<StoreAnalyzeModel>();
            List<销售_店铺员工档案> lisem = new List<销售_店铺员工档案>();
            if (storeID > 0)//查询指定店铺
            {
                var models = customerInfoBLL.GetModels(p => p.店铺ID == storeID && p.接待日期.Month == month);//查询当前店铺
                var ems = storeEmployeesBLL.GetModels(p => p.店铺ID == storeID);
                if (ems != null)
                {
                    lisem = ems.ToList();
                }
                else
                {
                    lisem = null;
                }
                if (models.Count() > 0)
                {
                    recModels = models.ToList();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var models = customerInfoBLL.GetModels(p => p.接待日期.Month == month);//查询所有店铺
                if (models.Count() > 0)
                {
                    recModels = models.ToList();
                }
                else
                {
                    return null;
                }
                var ems = storeEmployeesBLL.GetModels(p => true);
                if (ems != null)
                {
                    lisem = ems.ToList();
                }
                else
                {
                    lisem = null;
                }
            }

            if (emID > 0 && storeID > 0)
            {
                var m = recModels.Where(p => p.接待人ID == emID);
                if (m.Count() > 0)
                {
                    recModels = m.ToList();
                }
                else
                {
                    return null;
                }
            }

            var modeldate = recModels.GroupBy(p => p.店铺ID);//不同接待人接待数据按日期分组
            foreach (var item in modeldate)
            {
                StoreAnalyzeModel storeAnalyzeModel = new StoreAnalyzeModel();
                storeAnalyzeModel.接待月份 = month;
                storeAnalyzeModel.接待人数 = item.Count();
                storeAnalyzeModel.店铺ID = item.Key;
                storeAnalyzeModel.意向确认人数 = item.Count(p => p.是否确认 == true);
                storeAnalyzeModel.成交人数 = item.Count(p => p.是否成交 == true);
                storeAnalyzeModel.店铺 = storeBLL.GetModel(p => p.ID == item.Key).名称;
                if (storeAnalyzeModel.接待人数 > 0)
                {
                    storeAnalyzeModel.确认率 = (Convert.ToDouble(storeAnalyzeModel.意向确认人数) / Convert.ToDouble(storeAnalyzeModel.接待人数)).ToString("0.00%");
                }
                if (storeAnalyzeModel.意向确认人数 > 0)
                {
                    storeAnalyzeModel.成交率 = (Convert.ToDouble(storeAnalyzeModel.成交人数) / Convert.ToDouble(storeAnalyzeModel.意向确认人数)).ToString("0.00%");
                }
                else
                {
                    storeAnalyzeModel.成交率 = "0.00%";
                }
                storeAnalyzeModel.达成率 = "0.00%";
                List<SalesRecordViewModel> salesRecordsViewModels = new List<SalesRecordViewModel>();
                foreach (var it in item)
                {
                    var sales = salesRecordBLL.GetModels(p => p.店铺ID == it.店铺ID && p.销售日期.Month == month);
                    foreach (var i in sales)
                    {
                        SalesRecordViewModel salesRecordsView = new SalesRecordViewModel();
                        salesRecordsView.销售日期 = i.销售日期;
                        salesRecordsView.销售金额 = i.销售金额;
                        salesRecordsView.销售人员ID = i.销售人员ID;
                        salesRecordsView.销售日期 = i.销售日期;
                        salesRecordsView.店铺ID = i.店铺ID;
                        salesRecordsViewModels.Add(salesRecordsView);
                    }
                }
                if (salesRecordsViewModels.Count() > 0)
                {
                    storeAnalyzeModel.成交金额 = salesRecordsViewModels.Sum(p => p.销售金额);//指定月份，销售的成交金额
                    storeAnalyzeModel.最大单值 = salesRecordsViewModels.Max(p => p.销售金额);//指定月份，销售的成交最大金额
                }
                var employeeSales = salesRecordsViewModels.Where(p => p.是否业务业绩 == true);
                var selfSales = salesRecordsViewModels.Where(p => p.是否自营业绩 == true);
                if (employeeSales != null)
                {
                    storeAnalyzeModel.门店业务业绩 = employeeSales.Sum(p => p.销售金额);
                }
                if (employeeSales != null)
                {
                    storeAnalyzeModel.门店自营业绩 = selfSales.Sum(p => p.销售金额);
                }

                lis.Add(storeAnalyzeModel);

            }
            return lis;
        }

        /// <summary>
        /// 门店对象
        /// </summary>
        public class StoreClass
        {
            public string Name { get; set; }
            public int ID { get; set; }
            public int Count { get; set; }

        }
    }
}