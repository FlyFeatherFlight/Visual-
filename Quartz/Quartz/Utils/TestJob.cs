using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace QuartzTest.Utils
{
    public class TestJob:IJob
    {
        public int Str { get; set; }
        public void Execute(IJobExecutionContext context)
        {
            //var reportDirectory = string.Format("~/reports/{0}/", DateTime.Now.ToString("yyyy-MM"));
            //reportDirectory = System.Web.Hosting.HostingEnvironment.MapPath(reportDirectory);
            //if (!Directory.Exists(reportDirectory))
            //{
            //    Directory.CreateDirectory(reportDirectory);
            //}
            //var dailyReportFullPath = string.Format("{0}report_{1}.log", reportDirectory, DateTime.Now.Day);
            //var logContent = string.Format("{0}==>>{1}{2}", DateTime.Now, "create new log.", Environment.NewLine);
            //File.AppendAllText(dailyReportFullPath, logContent);


            MailHelper mailHelper = new MailHelper();


            mailHelper.Host = "smtp.exmail.qq.com";
            mailHelper.MailFrom = "system-message@chiccasa.com.cn";
            mailHelper.MailPwd = "Chic123456";

            mailHelper.MailSubject = "test";
            mailHelper.MailBody = "调度测试！";
            string emailBox = "saihua_chen@yeah.net";
            mailHelper.MailToArray = new string[1] { emailBox };
            try
            {
                mailHelper.Send();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}