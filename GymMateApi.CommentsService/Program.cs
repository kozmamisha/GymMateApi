using GymMateApi.CommentsService.Application.Extensions;
using GymMateApi.CommentsService.Persistance.Extensions;
using GymMateApi.Shared.Extensions;
using GymMateApi.Shared.Middlewares;
using Microsoft.AspNetCore.CookiePolicy;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCommentsPersistence(builder.Configuration);
builder.Services.AddCommentsApplication();
builder.Services.AddApiAuthentication(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    Secure = CookieSecurePolicy.Always,
    HttpOnly = HttpOnlyPolicy.Always,
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
