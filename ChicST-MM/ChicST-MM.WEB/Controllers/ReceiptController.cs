using ChicST_MM.IBLL;
using ChicST_MM.WEB.Models;
using JPager.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicST_MM.WEB.Controllers
{
    /// <summary>
    /// 收款单控制器
    /// </summary>
    public class ReceiptController : Controller
    {
        private readonly IAccountBLL accountBLL;
        private readonly IEmployeeInformationBLL employeeInformationBLL;
        private readonly IReceiptBLL receiptBLL;

        public ReceiptController(IAccountBLL accountBLL, IEmployeeInformationBLL employeeInformationBLL, IReceiptBLL receiptBLL)
        {
            this.accountBLL = accountBLL;
            this.employeeInformationBLL = employeeInformationBLL;
            this.receiptBLL = receiptBLL;
        }

        /// <summary>
        /// 首页，显示收款记录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string searchString, PagerInBase pagerInBase)
        {
            Session["View"] = true;
            var models = BuildReceiptModels();
            //根据条件检索
            var query = searchString != null ?
                models.Where(t => t.付款方.Contains(searchString)).ToList() :
                models.ToList();
            //默认倒叙排列

            query = query.OrderByDescending(p => p.ID).ToList();
            //分页数据
            var data = query.Skip(pagerInBase.Skip).Take(pagerInBase.PageSize);

            //总页数
            var count = models.Count;
            var res = new PagerResult<ReceiptViewModel>
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
        /// 添加收款记录视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiptAddView()
        {
            Session["View"] = true;
           
            return View();
        }

        /// <summary>
        /// 收款记录添加操作
        /// </summary>
        /// <returns></returns>
        public ActionResult AddReceipt(ReceiptViewModel receiptViewModel)
        {
            if (Session["View"].ToString() == "false")
            {
                return Content("<script>alert('重复操作！');window.history.go(-1);</script>");
            }
            
            var ac = GetAccountInfo();
            receiptViewModel.提交人员 = ac.姓名;
            receiptViewModel.提交人员ID = ac.用户ID;
            财务_收款单 model = new 财务_收款单();
            model.付款方 = receiptViewModel.付款方;
            model.付款时间 = receiptViewModel.付款时间;
            model.付款账号 = receiptViewModel.付款账号;
            model.付款银行 = receiptViewModel.付款银行;
            model.到账时间 = receiptViewModel.到账时间;
            model.备注 = receiptViewModel.备注;
            model.提交人 = ac.用户ID;
            model.提交时间 = DateTime.Now;
            model.收款方 = receiptViewModel.收款方;
            model.收款账号 = receiptViewModel.收款账号;
            model.收款银行 = receiptViewModel.收款银行;
            model.款项类型 = receiptViewModel.款项类型;
            try
            {
                model=receiptBLL.AddReturnModel(model);
            }
            catch (Exception e)
            {

                throw e;
            }
           
            return RedirectToAction("ReceiptInfoView",new { id = model.ID});
        }

        /// <summary>
        /// 收款详情
        /// </summary>
        /// <param name="id">收款单ID</param>
        /// <returns></returns>
        public ActionResult ReceiptInfoView(int id)
        {
            var model = receiptBLL.GetModel(p => p.ID==id);
            var rmodel = BuildReceiptModel(model);
            return View(rmodel);
        }

        /// <summary>
        /// 收款记录审查视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiptCheckView() { return View(); }


        /// <summary>
        /// 收款审查操作
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckReceipt(ReceiptViewModel receiptViewModel)
        {
            return View();

        }

        /// <summary>
        /// 构建单个收款单实体
        /// </summary>
        /// <returns></returns>
        public ReceiptViewModel BuildReceiptModel(财务_收款单 model)
        {
            if (model == null)
            {
                return null;
            }
           
            ReceiptViewModel receiptViewModel = new ReceiptViewModel();
            receiptViewModel.ID = model.ID;
            receiptViewModel.付款方 = model.付款方;
            receiptViewModel.付款时间 = model.付款时间;
            receiptViewModel.付款账号 = model.付款账号;
            receiptViewModel.付款银行 = model.付款银行;
            if (model.到账时间 != null)
            {
                receiptViewModel.到账时间 = model.到账时间.Value;
            }
            receiptViewModel.提交人员ID = model.提交人;
            receiptViewModel.提交人员 = employeeInformationBLL.GetModel(p=>p.用户ID==model.提交人).姓名;
            receiptViewModel.备注 = model.备注;
            if (model.审核人 != null)
            {
                var em = GetCompanyStaff();
                receiptViewModel.审核人 = model.审核人.Value;

                receiptViewModel.审核人员 = em.FirstOrDefault(p => p.ID == receiptViewModel.审核人).姓名;
                receiptViewModel.审核时间 = model.审核时间.Value;
                receiptViewModel.审核结果 = model.审核结果;
            }
            receiptViewModel.提交时间 = model.提交时间;
            receiptViewModel.收款方 = model.收款方;
            receiptViewModel.收款账号 = model.收款账号;
            receiptViewModel.收款银行 = model.收款银行;
            receiptViewModel.款项类型 = model.款项类型;
            return receiptViewModel;
        }

        /// <summary>
        /// 构建收款实体集
        /// </summary>
        /// <returns></returns>
        public List<ReceiptViewModel> BuildReceiptModels() {
            var models = receiptBLL.GetModels(p=>true);
            List<ReceiptViewModel> receiptViewModels = new List<ReceiptViewModel>();
            ReceiptViewModel receiptViewModel = new ReceiptViewModel();
            foreach (var item in models)
            {
                receiptViewModel = BuildReceiptModel(item);
                if (receiptViewModel!=null)
                {
                    receiptViewModels.Add(receiptViewModel);
                }
            }
            return receiptViewModels;
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