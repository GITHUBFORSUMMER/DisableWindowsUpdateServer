using Commom;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsUpdateServerCatClassLibrary
{
    public class JobServerBuilder
    {
        //从工厂中获取一个调度器实例化
        private static IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
        private static AppLog appLog = AppLog.CreateLogger(typeof(JobServerBuilder));
        public static void Start()
        {

            appLog.Info($"脚本开始工作");
            //最外层一个 try catch 避免调度器启动就失败而找不到原因 或者使当前脚本无法继续运行
            try
            {
                //开启调度器
                if (!scheduler.IsStarted)
                {
                    scheduler.Start();
                } 
                #region WINDOWS UPDATE CAT
                //CRON表达式 1/2 * * * * ? *
                var windowsUpdateCatJob = JobBuilder.Create<CatJob>().Build();
                var createJobTrigger = TriggerBuilder.Create()
                    .StartNow()
                    .WithCronSchedule(ConfigurationManager.AppSettings["CronText"])
                    .Build();
                scheduler.ScheduleJob(windowsUpdateCatJob, createJobTrigger);
                #endregion
            }
            catch (Exception ex)
            {
                appLog.Info($"开启调度器失败[{ex.Message}]");
            }
        }


        public static void Stop()
        {
            //开启调度器
            if (scheduler.IsStarted)
            {
                scheduler.Shutdown();
                appLog.Info($"脚本停止工作");
            }
        }
    }
}
