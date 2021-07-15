using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using mystrongbox.core;

using mystrongboxwebapi.core;

namespace  mystrongboxwebapi.telemetry
{
    public class DependencyInjection
    {
        public static bool _registerservice = false;
        public static bool _registerapp = false;
        public static bool _registerswagger = false;
        public static ResultAction<object> RegisterServices(string configurationfile, IServiceCollection services, ILogger logger, bool forceregister)
        {
            ResultAction<object> res = new ResultAction<object>();
            if (!_registerservice || forceregister)
            {

                services.AddMvc().AddApplicationPart(typeof(Program).GetTypeInfo().Assembly).AddControllersAsServices();
                services.AddMediatR(typeof(Program).GetTypeInfo().Assembly);
                ResultAction<IncommingTelemetry> resc = ConfigBuilder.BuildConfiguration<IncommingTelemetry>("telemetry", configurationfile, logger);
                res.CopyStatusFrom(resc);
                if (!res.IsOk) return res;
                Configuration.incommingtelemetry = resc.datas;

                // telemetry providers are ok ?
                if (!Configuration.incommingtelemetry.IsValid(logger))
                {
                    Environment.Exit(-1);
                }
                logger.LogInformation($"{typeof(DependencyInjection).FullName} RegisterService ok");
                _registerservice = true;
            }
            return res;
        }
        public static ResultAction<List<SwaggerFile>> RegisterSwagger(string configurationfile, IServiceCollection services, ILogger logger)
        {

            ResultAction<List<SwaggerFile>> ret = new ResultAction<List<SwaggerFile>>();
            return ret;
        }
        public string  GetRole()
        {
            return "";
        }

    }
}
