using BlazorDemoApp.Admin;
using BlazorDemoApp.Admin.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Blazorise 기본 설정
builder.Services
    .AddBlazorise(options => { options.Immediate = true; })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

//json 데이터 처리 기본 설정 
var appData = new AppContextData
{
    Common      = JsonHelper.Load<Global>("data/global.json"),
    ChartCommon = JsonHelper.Load<ChartsDataSet>("data/chartData.json"),
};
builder.Services.AddSingleton(appData);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
