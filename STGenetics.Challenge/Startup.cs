using STGenetics.Challenge.App.Middlewares;
using STGenetics.Challenge.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using STGenetics.Challenge.Infra.Context;
using STGenetics.Challenge.Infra.Extensions;
using STGenetics.Challenge.Middlewares;
using System.Reflection;
using FluentValidation;
using MediatR;
using STGenetics.Challenge.Business.Features.ItemsMenu.Commands.Create;
using STGenetics.Challenge.Business.Features.Discounts.Queries.GetAll;

namespace STGenetics.Challenge.App
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true);
                }
                    );
            });

            services.AddHttpClient();
            var b = Configuration.GetConnectionString("DefaultConnection");
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "STGenetics Challenge API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autenticação JWT. Digite 'Bearer' [space] e insira o token abaixo. Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference{
                                                        Type = ReferenceType.SecurityScheme,
                                                        Id = "Bearer"
                                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header,
                                },
                            new List<string>()
                        }
                    });
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
            services.AddAutoMapper(typeof(GetAllDiscountDto).Assembly);
            services.AddServices();
            services.AddAuthentication(Configuration);
            services.AddAuthorization();
            services.AddRepositories();
            services.AddScoped<ExceptionHandlerMiddleware>();
            services.AddValidatorsFromAssemblyContaining<CreateMenuItemCommandValidator>();
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<CreateMenuItemCommand>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowAll");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());

        }
    }
}

