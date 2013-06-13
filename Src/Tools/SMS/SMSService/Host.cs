using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;


namespace SMSService
{
    class Host
    {
        static void Main(string[] args)
        {
            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.Service<SMSService>(serviceConfigurator =>
                {
                    serviceConfigurator.ConstructUsing(() => new SMSService());
                    serviceConfigurator.WhenStarted(myService => myService.Start());
                    serviceConfigurator.WhenStopped(myService => myService.Stop());
                });

                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.StartAutomatically();
                hostConfigurator.SetDisplayName("DVSMSService");
                hostConfigurator.SetDescription("service to process sms requests");
                hostConfigurator.SetServiceName("DVSMSService");
            });
        }
    }
}
