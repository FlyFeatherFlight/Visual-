using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace QuartzTest.Utils
{
    public class TestScheduler
    {
        public static  void TrackWarningMail_Start() {

            try
            {
                var properties = new NameValueCollection();
                var sched = new StdSchedulerFactory(properties);
                IScheduler scheduler = sched.GetDefaultScheduler();
                scheduler.Start();
                IJobDetail job = JobBuilder.Create<TestJob>().Build();
                ITrigger trigger = TriggerBuilder.Create()
                  .WithIdentity("triggerName", "groupName")
                  .WithCronSchedule("0 32 11 ?* Friday")//每周五的11点32，可更改
                     .Build();

                scheduler.ScheduleJob(job, trigger);

            }
            catch (Exception e) {

                throw e;
            }
        }
    }
}