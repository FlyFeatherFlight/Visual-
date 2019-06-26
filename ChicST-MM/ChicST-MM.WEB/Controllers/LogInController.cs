﻿using ChicST_MM.IBLL;
using ChicST_MM.WEB.Models;
using EncryptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChicST_MM.WEB.Controllers
{
    [AllowAnonymous]
    public class LogInController : Controller
    {
        private IAccountBLL accountBLL;
        private IEmployeeInformationBLL employeeInformationBLL;
        private IDepartmentBLL departmentBLL;
        private ISys_AuthorityBLL sys_AuthorityBLL;
        private ISys_FunctionBLL sys_FunctionBLL;

        public LogInController(IAccountBLL accountBLL, IEmployeeInformationBLL employeeInformationBLL, IDepartmentBLL departmentBLL, ISys_AuthorityBLL sys_AuthorityBLL, ISys_FunctionBLL sys_FunctionBLL)
        {
            this.accountBLL = accountBLL ?? throw new ArgumentNullException(nameof(accountBLL));
            this.employeeInformationBLL = employeeInformationBLL ?? throw new ArgumentNullException(nameof(employeeInformationBLL));
            this.departmentBLL = departmentBLL ?? throw new ArgumentNullException(nameof(departmentBLL));
            this.sys_AuthorityBLL = sys_AuthorityBLL ?? throw new ArgumentNullException(nameof(sys_AuthorityBLL));
            this.sys_FunctionBLL = sys_FunctionBLL ?? throw new ArgumentNullException(nameof(sys_FunctionBLL));
        }






        // GET: LogIn
        public ActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="pw">密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string code,string pw) {
            AcountInformation acount = new AcountInformation();
            
            acount = BuildAccountInformation(code, pw);
            if (acount != null)
            {
                FormsAuthentication.SetAuthCookie(acount.编号, false);
                HttpCookie aCookie = new HttpCookie("userName")
                {
                    Value = acount.编号,
                    Expires = DateTime.Now.AddHours(3)
                };
                Response.Cookies.Add(aCookie);
                
                Session["Account"] = acount;
                return Content(" <script>alert(' \t 欢迎使用CHIC营销管理系统,祝您工作愉快! \t '); window.location.href = '/Home/Index';</script> ");
            }
            else
            {
                TempData["msg"] = "用户名或密码错误！";

                return RedirectToAction("SignIn", "LogIn");
            }

        }

        /// <summary>
        /// 注销
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        /// <summary>
        /// 初始化登录人员信息
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="pw">密码</param>
        /// <returns></returns>
        private AcountInformation BuildAccountInformation(string code,string pw) {
            AcountInformation acountInformation = new AcountInformation();
            
            var password=MD5Helper.MD5Encrypt(pw);//md5加密
            var account = accountBLL.GetModel(p => p.编号 == code);
            if (account == null)
            {
                return acountInformation = null;
            }
            if (account.密码!=password)
            {
                return acountInformation = null;
            }
           
            else
            {
                acountInformation.用户ID = account.ID;
                acountInformation.编号 = account.编号;
                acountInformation.停用标志 = account.停用标志;
              
            }
           
            var employee = employeeInformationBLL.GetModel(p => p.用户ID == acountInformation.用户ID);
            if (employee == null)
            {
                return acountInformation= null;
            }
            else
            {
                var department = departmentBLL.GetModel(p => p.名称 == employee.部门);
                acountInformation.ID = employee.ID;
                acountInformation.姓名 = employee.姓名;
                acountInformation.部门 = department.ID;
                acountInformation.管理者 = department.管理者;
                acountInformation.岗位 = employee.岗位;
            }
            var m=sys_AuthorityBLL.GetModel(p => p.用户ID == acountInformation.用户ID && p.功能ID == 74);
            if (m!=null)
            {
                acountInformation.经销价折扣 = m.ID;
            }
            return acountInformation;
        }
    }
}