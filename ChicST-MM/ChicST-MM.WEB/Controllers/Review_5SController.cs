﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChicST_MM.IBLL;
using ChicST_MM.WEB.Models;
using UploadManager;
using Newtonsoft.Json;
using System.IO;
using JPager.Net;
using Microsoft.Win32;
using ChicST_MM.Model;

namespace ChicST_MM.WEB.Controllers
{
    public class Review_5SController : Controller
    {
        private readonly IReview_5SBLL review_5SBLL;
        private readonly IReview_5S_RcordBLL review_5S_RcordBLL;
        private readonly IReview_5S_RecordDetailsBLL review_5S_RecordDetailsBLL;
        private readonly IReview_5S_RecordProofBLL review_5S_RecordProofBLL;
        private readonly IDistributorBLL distributorBLL;
        private readonly IStoreBLL storeBLL;
        private readonly IEmployeeInformationBLL employeeInformationBLL;
        private readonly IAccountBLL accountBLL;

        public Review_5SController(IReview_5SBLL review_5SBLL, IReview_5S_RcordBLL review_5S_RcordBLL, IReview_5S_RecordDetailsBLL review_5S_RecordDetailsBLL, IReview_5S_RecordProofBLL review_5S_RecordProofBLL, IDistributorBLL distributorBLL, IStoreBLL storeBLL, IEmployeeInformationBLL employeeInformationBLL, IAccountBLL accountBLL)
        {
            this.review_5SBLL = review_5SBLL ?? throw new ArgumentNullException(nameof(review_5SBLL));
            this.review_5S_RcordBLL = review_5S_RcordBLL ?? throw new ArgumentNullException(nameof(review_5S_RcordBLL));
            this.review_5S_RecordDetailsBLL = review_5S_RecordDetailsBLL ?? throw new ArgumentNullException(nameof(review_5S_RecordDetailsBLL));
            this.review_5S_RecordProofBLL = review_5S_RecordProofBLL ?? throw new ArgumentNullException(nameof(review_5S_RecordProofBLL));
            this.distributorBLL = distributorBLL ?? throw new ArgumentNullException(nameof(distributorBLL));
            this.storeBLL = storeBLL ?? throw new ArgumentNullException(nameof(storeBLL));
            this.employeeInformationBLL = employeeInformationBLL ?? throw new ArgumentNullException(nameof(employeeInformationBLL));
            this.accountBLL = accountBLL ?? throw new ArgumentNullException(nameof(accountBLL));
        }









        /// <summary>
        /// 评审标准
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var models = review_5SBLL.GetModels(p => true).ToList();

            List<Review_5SViewModel> lis = new List<Review_5SViewModel>();
            foreach (var item in models)
            {
                Review_5SViewModel review_5SViewModel = new Review_5SViewModel();
                review_5SViewModel.ID = item.ID;
                review_5SViewModel.具体内容 = item.具体内容;
                review_5SViewModel.分值 = item.分值.Value;
                review_5SViewModel.扣分标准 = item.扣分标准;
                review_5SViewModel.评估标准 = item.评估标准;
                review_5SViewModel.评估项目 = item.评估项目;
                lis.Add(review_5SViewModel);
            }
            return View(lis);
        }

        /// <summary>
        /// 审查主表添加视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ReviewRecordAddView()
        {
            Session["View"] = true;
            ReviewRecordViewModel reviewRecordViewModel = new ReviewRecordViewModel();
            var models = review_5SBLL.GetModels(p => true).ToList();
            List<C5S_评审标准> lis = new List<C5S_评审标准>();
            foreach (var item in models)
            {
                C5S_评审标准 m = new C5S_评审标准();
                m.ID = item.ID;
                m.具体内容 = item.具体内容;
                m.分值 = item.分值;
                m.扣分标准 = item.扣分标准;
                m.评估标准 = item.评估标准;
                m.评估项目 = item.评估项目;

                lis.Add(m);
            }
            reviewRecordViewModel.ReviewRecordModels = lis;
            return View(reviewRecordViewModel);
        }

        /// <summary>
        /// 5S审查主表提交操作
        /// </summary>
        /// <param name="reviewRecordViewModel"></param>
        /// <returns></returns>
        public ActionResult AddReviewRecord(ReviewRecordViewModel reviewRecordViewModel)
        {

            if (Session["View"].ToString() == "false")
            {
                return Content("<script>alert('重复操作！');window.history.go(-1);</script>");
            }
            var ac = GetAccountInfo();
            C5S_评审记录 model = new C5S_评审记录();
            model.奖惩制度提出意见 = reviewRecordViewModel.奖惩制度提出意见;
            model.展厅 = reviewRecordViewModel.展厅;
            model.巡店人员 = ac.用户ID;
            model.巡店日期 = reviewRecordViewModel.巡店日期;
            model.陈列负责人ID = reviewRecordViewModel.陈列负责人ID;
            model.运营负责人ID = reviewRecordViewModel.运营负责人ID;
            model.提交人 = ac.用户ID;
            model.提交日期 = DateTime.Now;
            model.经销商 = reviewRecordViewModel.经销商;
            model.陈列建议 = reviewRecordViewModel.陈列建议;

            try
            {
                model = review_5S_RcordBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                throw e;
            }

            return Json(model);

        }

        /// <summary>
        /// 5S审查副表提交
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public ActionResult AddReviewRecord_Details(string reviewRecord_DetailsViewModel)
        {
            var ac = GetAccountInfo();
            int reviewID = 0;
            var lis = JsonConvert.DeserializeObject<List<ReviewRecord_DetailsViewModel>>(reviewRecord_DetailsViewModel);
            if (lis.Count() <= 0)
            {
                return Json(new { Success = false, Message = "数据异常！" });
            }
            int score = 0;
            foreach (var item in lis)
            {
                C5S_评审记录详细 model = new C5S_评审记录详细();

                model.分值 = item.分值;
                model.备注 = item.备注;
                model.得分 = item.得分;
                model.扣分 = item.扣分;
                model.扣分标准 = item.扣分标准;
                model.日期 = DateTime.Now;
                model.评估标准 = item.评估标准;
                model.评估项目 = item.评估项目;
                model.评审人 = ac.用户ID;
                model.评审表ID = item.评审表ID;
                model.门店 = item.门店;
                model.评分标准序号 = item.评分标准序号;
                model.具体内容 = item.具体内容;
                model = review_5S_RecordDetailsBLL.AddReturnModel(model);
                if (model == null)
                {

                    return Json("失败!");
                }
                score = score + model.得分;
               reviewID = model.评审表ID;
            }
            
            try
            {
                var mo = review_5SBLL.GetModel(p=>p.ID== reviewID);
                mo.分值 = score;
                review_5SBLL.Modify(mo);
            }
            catch (Exception e)
            {


                throw e;

            }
            return Json(reviewID);
        }

        /// <summary>
        /// 评审详情视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ReviewRecordInfo(int id)
        {
            var model = review_5S_RcordBLL.GetModel(p => p.ID == id);
            var model_Detail = BuildReviewModel(model);
            return View(model_Detail);

        }

        /// <summary>
        /// 当前用户评审记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ReviewRecordsView(string searchString, PagerInBase pagerInBase)
        {
            var models = BuildReviewModels(null);

            //搜索数据
            var query = searchString != null ?
                models.Where(t => t.展厅名称.Contains(searchString)).ToList() :
                models.ToList();
            //默认倒叙排列
            query = query.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            var count = models.Count;
            var res = new PagerResult<ReviewRecordViewModel>
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
        /// 文件上传
        /// </summary>
        /// <param name="UploadStream">文件流</param>
        /// <param name="storeid">店铺ID</param>
        /// <param name="rowid">评分项序号</param>
        /// <param name="reviewID">主表ID</param>
        /// <returns></returns>
        public ActionResult UpFiles(HttpPostedFileBase UploadStream, string storeid, string rowid, string reviewID)
        {
            var ac = GetAccountInfo();
            if (UploadStream == null)
            {
                return Json(new { success = false, Message = "上传文件为空" });
            }
            string code = reviewID + "-" + rowid;
            string fileName = UploadStream.FileName;
            var path = UploadManager.UploadManager.SaveFile(UploadStream, "5S评审记录", storeid, DateTime.Now.ToString("yyyy-MM-dd"), code, fileName);

            C5S_评审记录_凭证 model = new C5S_评审记录_凭证();
            model.路径 = path;
            model.时间 = DateTime.Now;
            model.提交人 = ac.用户ID;
            model.评分标准序号 = int.Parse(rowid);
            model.评分主表ID = int.Parse(reviewID);
            model = review_5S_RecordProofBLL.AddReturnModel(model);

            return Json(new { Success = true, Message = "上传成功！" });
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="rowid">ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DownLoadFiles(int id)
        {



            var path = review_5S_RecordProofBLL.GetModel(p => p.ID == id).路径;

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
        /// 获取经销商
        /// </summary>
        /// <returns></returns>
        public ActionResult BuildDistributor()
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
        /// 获得店铺
        /// </summary>
        /// <param name="id">经销商ID</param>
        /// <returns></returns>
        public ActionResult GetStore(int id)
        {
            var stores = storeBLL.GetModels(p => p.经销商ID == id);
            var tripObj = new ArrayList();
            foreach (var item in stores)
            {

                var store = new string[] { item.ID.ToString(), item.名称 };
                tripObj.Add(store);
            }
            return Json(tripObj);
        }

        /// <summary>
        /// 获得公司员工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStaff() {
            var models = GetCompanyStaff();
            return Json(models);
        }

        /// <summary>
        /// 构建单个评审记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReviewRecordViewModel BuildReviewModel(C5S_评审记录 model)
        {
            if (model == null)
            {
                return null;
            }
            ReviewRecordViewModel reviewRecordView = new ReviewRecordViewModel();
            reviewRecordView.ID = model.ID;

           if (model.分数!=null)
            {
                reviewRecordView.分数 = model.分数.Value;
            }
            

            reviewRecordView.奖惩制度提出意见 = model.奖惩制度提出意见;
            reviewRecordView.展厅 = model.展厅;
            reviewRecordView.巡店人员 = model.巡店人员;
            reviewRecordView.巡店人员姓名 = employeeInformationBLL.GetModel(p => p.用户ID == model.巡店人员).姓名;
           
            reviewRecordView.提交人 = model.提交人;
            reviewRecordView.提交人姓名 = employeeInformationBLL.GetModel(p => p.用户ID == model.提交人).姓名;
            reviewRecordView.提交日期 = model.提交日期;
            reviewRecordView.经销商 = model.经销商;
            reviewRecordView.经销商名字 = distributorBLL.GetModel(p => p.ID == model.经销商).名称;
            reviewRecordView.陈列建议 = model.陈列建议;
           
            if (model.陈列负责人ID != null)
            {
                reviewRecordView.陈列负责人ID = model.陈列负责人ID.Value;
                reviewRecordView.门店负责人 = employeeInformationBLL.GetModel(p => p.用户ID == reviewRecordView.陈列负责人ID).姓名;
            }
           
            if (model.运营负责人ID!=null)
            {
                reviewRecordView.运营负责人ID = model.运营负责人ID.Value;
                reviewRecordView.运营负责人 = employeeInformationBLL.GetModel(p => p.用户ID == reviewRecordView.运营负责人ID).姓名;
            }
            reviewRecordView.展厅名称 = storeBLL.GetModel(p => p.ID == model.展厅).名称;
            var reviewRecord_Details = review_5S_RecordDetailsBLL.GetModels(p => p.评审表ID == model.ID);
            List<ReviewRecord_DetailsViewModel> lis = new List<ReviewRecord_DetailsViewModel>();

            foreach (var item in reviewRecord_Details)
            {
                List<int> ps = new List<int>();
                ReviewRecord_DetailsViewModel r = new ReviewRecord_DetailsViewModel();
                r.ID = item.ID;

                r.分值 = item.分值;
                r.备注 = item.备注;
                r.得分 = item.得分;
                r.扣分 = item.扣分;
                r.扣分标准 = item.扣分标准;
                r.日期 = item.日期;
                r.评估标准 = item.评估标准;
                r.评估项目 = item.评估项目;
                r.评审人 = item.评审人;
                r.评审人姓名 = employeeInformationBLL.GetModel(p => p.用户ID == item.评审人).姓名;
                r.评审表ID = item.评审表ID;
                r.门店 = item.门店;
                r.门店名称 = storeBLL.GetModel(p => p.ID == item.门店).名称;
                r.具体内容 = item.具体内容;
                r.评分标准序号 = item.评分标准序号;
                var lisp = review_5S_RecordProofBLL.GetModels(p => p.评分主表ID == item.评审表ID);
                lisp = lisp.Where(p => p.评分标准序号 == item.评分标准序号);
                if (lisp.Count() != 0)
                {
                    foreach (var ite in lisp)
                    {
                        ps.Add(ite.ID);
                    }
                    r.proofs = ps;
                }

                lis.Add(r);

            }
            reviewRecordView.ReviewRecord_DetailsViewModels = lis;
            return reviewRecordView;
        }



        /// <summary>
        /// 构建评审记录集
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        public List<ReviewRecordViewModel> BuildReviewModels(int? storeid)
        {
            var ac = GetAccountInfo();
            var models = review_5S_RcordBLL.GetModels(p => true);
            List<ReviewRecordViewModel> lis = new List<ReviewRecordViewModel>();
            if (storeid != null && storeid != 0)
            {
                models = models.Where(p => p.展厅 == storeid);
            }
            else
            {
                models = models.Where(p => p.提交人 == ac.用户ID);
            }
            foreach (var item in models)
            {
                var model = BuildReviewModel(item);
                lis.Add(model);
            }
            return lis;
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