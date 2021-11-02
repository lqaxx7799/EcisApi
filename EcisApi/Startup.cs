using EcisApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcisApi.Repositories;
using EcisApi.Services;
using EcisApi.Controllers;
using FluentValidation.AspNetCore;
using EcisApi.DTO;
using FluentValidation;
using EcisApi.Helpers;

namespace EcisApi
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
            services.AddDbContext<DataContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAgentRepository, AgentRepository>();
            services.AddTransient<IAgentAssignmentRepository, AgentAssignmentRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICompanyReportRepository, CompanyReportRepository>();
            services.AddTransient<ICompanyReportDocumentRepository, CompanyReportDocumentRepository>();
            services.AddTransient<ICompanyTypeRepository, CompanyTypeRepository>();
            services.AddTransient<ICompanyTypeModificationRepository, CompanyTypeModificationRepository>();
            services.AddTransient<ICriteriaRepository, CriteriaRepository>();
            services.AddTransient<ICriteriaDetailRepository, CriteriaDetailRepository>();
            services.AddTransient<ICriteriaTypeRepository, CriteriaTypeRepository>();
            services.AddTransient<IDocumentReviewRepository, DocumentReviewRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IVerificationConfirmRequirementRepository, VerificationConfirmRequirementRepository>();
            services.AddTransient<IVerificationCriteriaRepository, VerificationCriteriaRepository>();
            services.AddTransient<IVerificationDocumentRepository, VerificationDocumentRepository>();
            services.AddTransient<IVerificationProcessRepository, VerificationProcessRepository>();
            services.AddTransient<IViolationReportRepository, ViolationReportRepository>();
            services.AddTransient<IViolationReportDocumentRepository, ViolationReportDocumentRepository>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAgentService, AgentService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<ICompanyReportService, CompanyReportService>();
            services.AddTransient<ICompanyReportDocumentService, CompanyReportDocumentService>();
            services.AddTransient<ICompanyTypeService, CompanyTypeService>();
            services.AddTransient<ICriteriaService, CriteriaService>();
            services.AddTransient<ICriteriaDetailService, CriteriaDetailService>();
            services.AddTransient<ICriteriaTypeService, CriteriaTypeService>();
            services.AddTransient<IDocumentReviewService, DocumentReviewService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IVerificationConfirmRequirementService, VerificationConfirmRequirementService>();
            services.AddTransient<IVerificationCriteriaService, VerificationCriteriaService>();
            services.AddTransient<IVerificationDocumentService, VerificationDocumentService>();
            services.AddTransient<IVerificationProcessService, VerificationProcessService>();
            services.AddTransient<IViolationReportService, ViolationReportService>();
            services.AddTransient<IViolationReportDocumentService, ViolationReportDocumentService>();

            services.AddTransient<ICloudStorageHelper, CloudStorageHelper>();
            services.AddTransient<IEmailHelper, EmailHelper>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("*")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddControllers()
                .AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcisApi", Version = "v1" });
            });

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddTransient<IValidator<AuthenticateRequestDTO>, AuthenticateRequestDTOValidator>();
            services.AddTransient<IValidator<ChangePasswordDTO>, ChangePasswordDTOValidator>();
            services.AddTransient<IValidator<CompanyRegistrationDTO>, CompanyRegistrationDTOValidator>();
            services.AddTransient<IValidator<VerifyCompanyDTO>, VerifyCompanyDTOValidator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcisApi v1"));
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
