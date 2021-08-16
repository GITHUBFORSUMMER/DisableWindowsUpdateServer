using Commom;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsUpdateServerCatClassLibrary
{
    public class CatJob : IJob
    {
        private static AppLog appLog = AppLog.CreateLogger(typeof(CatJob));
        public void Execute(IJobExecutionContext context)
        {
            //获得服务集合
            var serviceControllers = ServiceController.GetServices();
            //遍历服务集合，打印服务名和服务状态
            var servicesArr = serviceControllers.Where(c => c.ServiceName.Contains("wuauserv"));
            foreach (var services in servicesArr)
            {
                if (services.Status == ServiceControllerStatus.Running)
                {
                    try
                    {
                        services.Stop();
                        appLog.Info($"服务刚刚启动了已帮您将其服务关闭");
                    }
                    catch (Exception ex)
                    {
                        appLog.Info($"服务关不掉,{ex.Message}");
                    }
                }
            }
        }
    }
}
