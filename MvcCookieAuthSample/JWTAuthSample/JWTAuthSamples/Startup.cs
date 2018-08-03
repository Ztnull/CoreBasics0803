using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTAuthSamples
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
            //加入JWT参数的配置

            #region 加入JWT参数的配置+jwtsettings and AddAuthentication

            //获取appsettings中的JwtSettings节点配置：方便AuthorizeController中构造函数_jwtSettingsAccess获取值
            services.Configure<Models.JwtSettings>(Configuration.GetSection("JwtSettings"));
            var jwtsettings = new Models.JwtSettings();
            Configuration.Bind("JwtSettings", jwtsettings);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtsettings.Issuser,//token的颁发者
                    ValidAudience = jwtsettings.Audience,//指定的客户端
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtsettings.SecreKey))//加密方式


                };
            });//同样加上Authentication

            #endregion

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();//加上Authentication不然前面的特性标记[Authorize]不可用
            app.UseMvc();
        }
    }
}
