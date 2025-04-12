using OrganizationTree.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.AddApplicationServices();
builder.AddData();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
