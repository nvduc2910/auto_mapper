using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using System.Reflection;
using AutoMapper_Demo.Infrastructure.Mapping;

namespace AutoMapper_Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();

            var maperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                var types = Assembly.GetExecutingAssembly().GetExportedTypes();
                LoadStandardMappings(types);
                LoadCustomMappings(types, cfg);

            });

            

            var mapper = maperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        public void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types

                        from i in t.GetInterfaces()
                        where i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                        !t.IsAbstract &&
                        !t.IsInterface

                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach(var map in maps)
            {
                Mapper.Map(map.Source, map.Destination);
            }
        }

        public void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression configuration)
        {
            var maps = (from t in types

                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                        !t.IsAbstract &&
                        !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(configuration);
            }
        }


    }
}
