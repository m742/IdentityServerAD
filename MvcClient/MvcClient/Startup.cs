using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MvcClient
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
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";

                // Add Package Microsoft.AspNetCore.Authentication.OpenIdConnect
            })
                .AddCookie("Cookies")
               
                .AddOpenIdConnect("oidc", options =>
            { // depois de autenticar no identity, grava no cookies as informações
                options.SignInScheme = "Cookies";
                options.RequireHttpsMetadata = false;
                options.Authority = "http://localhost:5000"; // servidor identity
                options.ClientId = "mvc.implicit"; // tem que estar cadastrado no servidor de identidade
                //options.ResponseMode = "form_post";//
                options.ResponseType = "id_token token"; // se você quer o access token para acessar a API, coloca o token
                                                          // no caso que só quero o ID, coloco a nomeclatura acima
                options.SaveTokens = true; // coloca o token no cookies
                options.Scope.Add("NotaFiscal");
                options.Scope.Add("openid");
                options.Scope.Add("profile");
            });
                
                

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
          

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
