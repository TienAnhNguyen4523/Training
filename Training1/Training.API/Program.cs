using MediatR;
using Microsoft.EntityFrameworkCore;
using Training.Data.DBContext;
using Training.Data.Infrastructure.Implementation;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Implementation.User;
using Tranining.Domain.Service.Interface.User;
using System.Reflection;
using Tranining.Domain.Service.Interface.Role;
using Training.Entity.EntityModel;
using Tranining.Domain.Service.Implementation.Role;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Service.Implementation.UserRole;
using Tranining.Domain.Command.UserRoles;
using Tranining.Domain.Service.Interface.UserRole;
using Training.Authentication.TokenValidators;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Training.Authentication.Handlers;
using Training.Authentication.Requirements;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tranining.Domain.Service.Interface.Author;
using Tranining.Domain.Service.Implementation.Author;
using Tranining.Domain.Service.Interface.Category;
using Tranining.Domain.Service.Implementation.Category;
using Tranining.Domain.Service.Implementation.Book;
using Tranining.Domain.Service.Interface.Book;
using Tranining.Domain.Service.Interface.IProductRoleService;
using Tranining.Domain.Service.Implementation.ProductRole;
using System.Security;
using Tranining.Domain.Service.Interface.Permission;
using Tranining.Domain.Service.Implementation.Permission;
using Tranining.Domain.Service.Implementation.UserPermission;
using Tranining.Domain.Service.Interface.UserPermission;
using Tranining.Domain.Service.Interface.BorrowAndReturnBook;
using Tranining.Domain.Service.Implementation.BorrowAndReturnBook;
using Tranining.Domain.Service.Interface.ExportBill;
using Tranining.Domain.Service.Implementation.ExportBill;
using Tranining.Domain.Service.Interface.Statistic;
using Tranining.Domain.Service.Implementation.Statistic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp", builder =>
	{
		builder.WithOrigins("http://localhost:3000") 
			   .AllowAnyMethod()
			   .AllowAnyHeader()
			   .AllowCredentials(); 
	});
});
// Add services to the container.
AddServices(builder);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","Training.API v1"));
//}
app.UseCors("AllowReactApp");
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Training.API v1"));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
void AddServices(WebApplicationBuilder build)
{
	//connect DB SQL
	build.Services.AddDbContext<TrainingDbContext>(options => options.UseSqlServer(build.Configuration.GetConnectionString("DefaultConnection")),ServiceLifetime.Transient);
	build.Services.AddScoped<DbContext, TrainingDbContext>();
	//Injection configuration
	build.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
	build.Services.AddTransient<IUnitOfWork, UnitOfWork>();
	//Authen Swagger
	// Configure the JwtModel settings using the "appJwt" section from the configuration.
	build.Services.Configure<JwtModel>(build.Configuration.GetSection("appJwt"));

	// Build the service provider to retrieve configured services.
	ServiceProvider? servicesProvider = build.Services.BuildServiceProvider();

	// Retrieve the JwtModel configuration values.
	var jwtBearerSettings = servicesProvider.GetService<IOptions<JwtModel>>().Value;

	// Configure authentication using JWT Bearer scheme.
	var authenticationBuilder = build.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
	authenticationBuilder.AddJwtBearer(o =>
	{
		// Clear any existing security token validators.
		o.SecurityTokenValidators.Clear();

		// Add custom JWT Bearer validator.
		o.SecurityTokenValidators.Add(new JwtBearerValidator());

		// Set up token validation parameters.
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidAudience = jwtBearerSettings.Audience,  // Set the valid audience.
			ValidIssuer = jwtBearerSettings.Issuer,  // Set the valid issuer.
			IssuerSigningKey = jwtBearerSettings.SigningKey  // Set the signing key.
		};

		// Assign the token validation parameters to the options.
		o.TokenValidationParameters = tokenValidationParameters;

		// Define events for JWT Bearer authentication.
		o.Events = new JwtBearerEvents
		{
			// Handle the MessageReceived event to retrieve the token from the query string for specific paths.
			OnMessageReceived = context =>
			{
				if (context.Request.Path.ToString()
					.StartsWith("/HUB/", StringComparison.InvariantCultureIgnoreCase))
				{
					context.Token = context.Request.Query["access_token"];
				}
				return Task.CompletedTask;
			}
		};
	});
	build.Services.AddHttpContextAccessor();
	build.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
	// Add MVC services with a requirement for authenticated users.
	build.Services.AddMvc(mvcOptions =>
	{
		// Create a policy that requires authenticated users and uses JWT Bearer authentication scheme.
		var policy = new AuthorizationPolicyBuilder()
			.RequireAuthenticatedUser()
			.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
			.AddRequirements(new SolidAccountRequirement())
			.Build();

		// Add an authorization filter using the created policy.
		mvcOptions.Filters.Add(new AuthorizeFilter(policy));
	});

	// Add Swagger services for API documentation.
	build.Services.AddSwaggerGen(c =>
	{
		// Define a JWT security scheme for Swagger.
		var jwtSecurityScheme = new OpenApiSecurityScheme
		{
			Scheme = "bearer",
			BearerFormat = "JWT",
			Name = "JWT Authentication",
			In = ParameterLocation.Header,
			Type = SecuritySchemeType.Http,
			Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

			// Set a reference to the JWT Bearer authentication scheme.
			Reference = new OpenApiReference
			{
				Id = JwtBearerDefaults.AuthenticationScheme,
				Type = ReferenceType.SecurityScheme
			}
		};

		// Add the Swagger document.
		c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheCodeBuzz-Service", Version = "v1" });

		// Add the JWT security definition to Swagger.
		c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

		// Add a security requirement to Swagger that uses the defined JWT security scheme.
		c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{ jwtSecurityScheme, Array.Empty<string>() }
	});
	});

	// Authen
	build.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
	build.Services.AddScoped<IProfileService, ProfileService>();

	// Requirement handler.
	build.Services.AddScoped<IAuthorizationHandler, SolidAccountRequirementHandler>();
	build.Services.AddScoped<IAuthorizationHandler, RoleRequirementHandler>();

	//Add service
	build.Services.AddTransient<IUserService, UserService>();
	build.Services.AddTransient<IRoleServices,RoleServices>();
	build.Services.AddTransient<IUserRoleService, UserRoleService>();
	build.Services.AddTransient<IAuthorService, AuthorService>();
	build.Services.AddTransient<ICategoryService, CategoryService>();
	build.Services.AddTransient<IBookService, BookService>();
	build.Services.AddTransient<IProductRoleService, ProductRoleService>();
	build.Services.AddTransient<IPermissionService, PermissionService>();
	build.Services.AddTransient<IUserPermissionService, UserPermissionService>();
	build.Services.AddTransient<IBorrowAndReturnBookService, BorrowAndReturnBookService>();
	build.Services.AddTransient<IExportBillService, ExportBillService>();
	build.Services.AddTransient<IStatisticService, StatisticService>();
	//Add MediatR
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(),typeof(CreateUserCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(UpdateUserCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(DeleteUserCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateRoleCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(DeleteRoleCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(UpdateRoleCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateUserRoleCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(UpdateUserRoleCommand).Assembly);
	build.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(DeleteUserRoleCommand).Assembly);
}
