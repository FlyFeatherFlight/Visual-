using ChicStoreManagement.Model;
using ChicStoreManagement.IBLL;
using StoreAnalyze.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using MoreLinq;
using System;

namespace StoreAnalyze.Controllers
{
    public class HomeController : Controller
    {
        private IStoreEmployeesBLL storeEmployeesBLL;
        private IStoreBLL storeBLL;
        private ITrackGoalBLL trackGoalBLL;
        private ICustomerInfoBLL customerInfoBLL;
        private static List<ReceivingRecordsViewModel> storeRecViewModels;

        public HomeController(IStoreEmployeesBLL storeEmployeesBLL, IStoreBLL storeBLL, ITrackGoalBLL trackGoalBLL, ICustomerInfoBLL customerInfoBLL)
        {
            this.storeEmployeesBLL = storeEmployeesBLL;
            this.storeBLL = storeBLL;
            this.trackGoalBLL = trackGoalBLL;
            this.customerInfoBLL = customerInfoBLL;
            
        }

        public ActionResult Index()
        {
            storeRecViewModels = BuildCustomerInfos();
            return View();
        }

        #region 门店测试
        public class StoreClass
        {
            public string Name { get; set; }
            public int ID { get; set; }

        }
        [HttpGet]
        public ActionResult getstores() {
            var stores = BuildStores();
           

            StoreClass[]  arr= new StoreClass[stores.Count];
         
            for (int i = 0; i < stores.Count; i++)
            {

                StoreClass myClass = new StoreClass() { };
                    myClass.Name= stores.ElementAt(i).名称;
                    myClass.ID = stores.ElementAt(i).ID;
                    arr[i] =myClass;
               
            }
            var lis = arr.ToList();
            return Json(lis,JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region 门店接待数据
        /// <summary>
        /// 获得门店接待数据
        /// </summary>
        /// <param name="storeid">门店ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetStoreRece(int?storeid) {
            
            var models = storeRecViewModels.Where(p => p.店铺ID == storeid);
            var names = models.DistinctBy(p=>p.接待人ID);
            var dates = models.DistinctBy(p => p.接待日期);
            List<StoreRece> emlis = new List<StoreRece>();
            List<DateCount> storelis = new List<DateCount>();
            for (int i = 0; i < names.Count(); i++)
            {
                StoreRece storeRece = new StoreRece();
                List<DateCount> list = new List<DateCount>();
                storeRece.Name = names.ElementAt(i).接待人;
                
                for (int j = 0; j < dates.Count(); j++)
                {
                    DateCount dateCount = new DateCount();
                  
                    dateCount.Date = dates.ElementAt(j).接待日期;
                   
                    int recID = names.ElementAt(i).接待人ID;
                    var m = models.Where(p => p.接待人ID == recID);
                    dateCount.Count = m.Where(p => p.接待日期 == dateCount.Date).Count();
                    list.Add(dateCount);
                }
                storeRece.dateCounts = list;
                emlis.Add(storeRece);
            }
            foreach (var item in dates)
            {
                DateCount storedate = new DateCount();
                storedate.Date = item.接待日期;
                storedate.Count = models.Where(p => p.接待日期 == item.接待日期).Count();
                storelis.Add(storedate);
            }
            

          
            return Json(new { emdata=emlis.ToArray(),storedata= storelis.ToArray() }, JsonRequestBehavior.AllowGet );
        }
        public class StoreRece {

          public   string Name { get; set; }
          public List<DateCount> dateCounts { get; set; }
        }
        public class DateCount {
            public  int Count { get; set; }
            public DateTime Date { get; set; }

        }
        #endregion
        /// <summary>
        /// 所有门店信息
        /// </summary>
        /// <returns></returns>
        public List<StoreViewModel> BuildStores() {
            var models = storeBLL.GetModels(p => true);
            List<StoreViewModel> lis = new List<StoreViewModel>();
            foreach (var item in models)
            {
               var model= BuildStore(item);
                lis.Add(model);
            }
            return lis;


        }

        /// <summary>
        /// 单个门店信息
        /// </summary>
        /// <returns></returns>
        public StoreViewModel BuildStore(销售_店铺档案 model) {
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
        public List<EmployeeViewModel> BuildEmployees(int storeid) {
            var models = storeEmployeesBLL.GetModels(p => p.店铺ID == storeid);
            List<EmployeeViewModel> lis = new List<EmployeeViewModel>();
            foreach (var item in models)
            {
                var model=BuildEmployee(item);
                lis.Add(model);
            }
            return lis;
        }


        /// <summary>
        /// 构造单个员工信息
        /// </summary>
        /// <returns></returns>
        public EmployeeViewModel BuildEmployee(销售_店铺员工档案 model) {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel();
            employeeViewModel.ID = model.ID;
            employeeViewModel.停用标志 = model.停用标志;
            employeeViewModel.制单人 = model.制单人;
            employeeViewModel.制单日期 = model.制单日期;

            employeeViewModel.姓名 = model.姓名;
            employeeViewModel.密码 = model.密码;
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
        /// 构造单个客户信息
        /// </summary>
        /// <returns></returns>
        public ReceivingRecordsViewModel BuildCustomerInfo(销售_接待记录  model) {
            ReceivingRecordsViewModel receivingRecordsViewModel = new ReceivingRecordsViewModel();
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
                receivingRecordsViewModel.家庭成员 = model.家庭成员;
                receivingRecordsViewModel.年龄段 = model.年龄段;
                receivingRecordsViewModel.店铺ID = model.店铺ID;
                receivingRecordsViewModel.店铺 = storeBLL.GetModel(p => p.ID == model.店铺ID).名称;
                receivingRecordsViewModel.性别 = model.性别;
                receivingRecordsViewModel.户型大小 = model.户型大小;
                receivingRecordsViewModel.接待人ID = model.接待人ID;
                if (model.接待人ID!=0)
                {
                    receivingRecordsViewModel.接待人 = storeEmployeesBLL.GetModel(p => p.ID == model.接待人ID).姓名;

                }
                receivingRecordsViewModel.接待序号 = model.接待序号;
                receivingRecordsViewModel.接待日期 = model.接待日期;
                receivingRecordsViewModel.是否关闭 = model.是否关闭;
                receivingRecordsViewModel.是否有意向 = model.是否有意向;
                receivingRecordsViewModel.更新人 = model.更新人;
                if (model.更新人!=null)
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
                if (model.跟进人ID!=null)
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
            }
            catch (Exception e)
            {

                throw e;
            }
           
            return receivingRecordsViewModel;
        }

        /// <summary>
        /// 构造该店铺客户信息
        /// </summary>
        /// <param name="storeid">店铺ID</param>
        /// <returns></returns>
        public List<ReceivingRecordsViewModel> BuildCustomerInfos() {
            var models = customerInfoBLL.GetModels(p => true);
                
            List<ReceivingRecordsViewModel> lis = new List<ReceivingRecordsViewModel>();
            foreach (var item in models)
            {
                var model = BuildCustomerInfo(item);
                lis.Add(model);
            }
            return lis;
        }
    }
}