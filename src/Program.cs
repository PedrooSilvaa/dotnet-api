internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                builder.Configuration.GetConnectionString("IWantDb"),
                new MySqlServerVersion(new Version(8, 0, 30)) // Altere para a vers�o do seu MySQL
            )
        );
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddAuthorization(options => {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
              .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
              .RequireAuthenticatedUser()
              .Build();
            options.AddPolicy("EmployeePolicy", p =>
               p.RequireAuthenticatedUser().RequireClaim("EmployeeCode"));
            options.AddPolicy("CpfPolicy", p =>
                p.RequireAuthenticatedUser().RequireClaim("Cpf"));
        });
        builder.Services.AddAuthentication(x => {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters() {
                ValidateActor = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
            };
        });

        builder.Services.AddScoped<QueryAllUsersWithClaimName>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
        app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle);
        app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);
        app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);
        app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle);
        app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle);

        app.UseExceptionHandler("/error");
        app.Map("/error", (HttpContext http) => {
            return Results.Problem(title: "An error ocurred", statusCode: 500);
        });

        app.Run();
    }
}