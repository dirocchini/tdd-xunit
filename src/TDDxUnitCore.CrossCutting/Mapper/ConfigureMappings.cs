using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TDDxUnitCore.Domain.Audiences;

namespace TDDxUnitCore.CrossCutting.Mapper
{
    public class ConfigureMappings
    {
        public static void Configure(IServiceCollection services)
        {
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "TDDxUnitCore.CrossCutting.Mapper");

            Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
            {
                return
                    assembly.GetTypes()
                        .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                        .ToArray();
            }

            services.AddAutoMapper(typelist);


            //services.AddScoped(provider => new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile(new StudentMappingProfile());
            //}));
        }
    }
}
