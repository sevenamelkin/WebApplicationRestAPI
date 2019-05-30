using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWebApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
//using WebApplicationRestAPI.Models;

namespace WebApplicationRestAPI
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            //AwardsContext ac;
            string con1 = "Server=(localdb)\\mssqllocaldb;Database=usersdbstore;Trusted_Connection=True;MultipleActiveResultSets=true";
           
            services.AddDbContext<UsersContext>(options => options.UseSqlServer(con1));
            services.AddDbContext<AwardsContext>(options => options.UseSqlServer(con1));
            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
