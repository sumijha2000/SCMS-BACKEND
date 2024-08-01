
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using COMMON_PROJECT_STRUCTURE_API.services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;

WebHost.CreateDefaultBuilder().
ConfigureServices(s =>
{
    IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    s.AddSingleton<login>();
    s.AddSingleton<Authentication>();
    s.AddSingleton<getSignupInfo>();
    s.AddSingleton<AdminLoginService>();
    s.AddSingleton<usernewaccount>();
    s.AddSingleton<getManagerData>();
    s.AddSingleton<getSupplierData>();
    s.AddSingleton<orderservice>();
    s.AddSingleton<getOrderList>();
    s.AddSingleton<managerProfileService>();
    s.AddSingleton<supplierProfileService>();
    s.AddSingleton<getUsersPofilesData>();
    s.AddSingleton<productService>();
    s.AddSingleton<getNotAvaiableProduct>();
    s.AddSingleton<orderChartService>();
    s.AddSingleton<getUserRolData>();
    s.AddSingleton<getOrderData>();
    s.AddSingleton<getsupplierorderdata>();
    s.AddSingleton<totalUsersService>();



    s.AddCors(options =>
              {
                  options.AddPolicy("AllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:5164", "http://localhost:5173", "https://localhost:5002") // Change this to match your frontend URL
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
              });

    // Add authentication
    s.AddAuthentication("SourceJWT").AddScheme<SourceJwtAuthenticationSchemeOptions, SourceJwtAuthenticationHandler>("SourceJWT", options =>
    {
        options.SecretKey = appsettings["jwt_config:Key"].ToString();
        options.ValidIssuer = appsettings["jwt_config:Issuer"].ToString();
        options.ValidAudience = appsettings["jwt_config:Audience"].ToString();
        options.Subject = appsettings["jwt_config:Subject"].ToString();
    });

    // Add authorization/
    s.AddAuthorization();

    // Add controllers
    s.AddControllers();
})
    .Configure(app =>
    {
        app.UseRouting();

        // Use CORS
        app.UseCors("AllowSpecificOrigins");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseStaticFiles();


        app.UseEndpoints(e =>
        {
            var login = e.ServiceProvider.GetRequiredService<login>();
            var auth = e.ServiceProvider.GetRequiredService<Authentication>();
            var getSignupInfo = e.ServiceProvider.GetRequiredService<getSignupInfo>();
            var adminLoginService = e.ServiceProvider.GetRequiredService<AdminLoginService>();
            var usernewaccount = e.ServiceProvider.GetRequiredService<usernewaccount>();
            var getManagerData = e.ServiceProvider.GetRequiredService<getManagerData>();
            var getSupplierData = e.ServiceProvider.GetRequiredService<getSupplierData>();
            var OrderService = e.ServiceProvider.GetRequiredService<getSupplierData>();
            var orderservice = e.ServiceProvider.GetRequiredService<orderservice>();
            var getOrderList = e.ServiceProvider.GetRequiredService<getOrderList>();
            var managerProfileService = e.ServiceProvider.GetRequiredService<managerProfileService>();
            var supplierProfileService = e.ServiceProvider.GetRequiredService<supplierProfileService>();
            var getUsersPofilesData = e.ServiceProvider.GetRequiredService<getUsersPofilesData>();
            var productService = e.ServiceProvider.GetRequiredService<productService>();
            var getNotAvaiableProduct = e.ServiceProvider.GetRequiredService<getNotAvaiableProduct>();
            var orderChartService = e.ServiceProvider.GetRequiredService<orderChartService>();
            var getOrderData = e.ServiceProvider.GetRequiredService<getOrderData>();
            var getUserRolData = e.ServiceProvider.GetRequiredService<getUserRolData>();
            var getsupplierorderdata = e.ServiceProvider.GetRequiredService<getsupplierorderdata>();
            var totalUsersService = e.ServiceProvider.GetRequiredService<totalUsersService>();






            e.MapPost("login",
     [AllowAnonymous] async (HttpContext http) =>
     {
         var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
         requestData rData = JsonSerializer.Deserialize<requestData>(body);
         if (rData.eventID == "1001") // update
             await http.Response.WriteAsJsonAsync(await login.Login(rData));

     });

            e.MapPost("/signin", async (requestData rData, Authentication auth) =>
                     {
                         return await auth.SignIn(rData);
                     });


            e.MapPost("/deleteAccount", async (requestData rData, Authentication auth) =>
              {
                  return await auth.DeleteAccount(rData);
              });

            e.MapPost("/logoutuser", async (requestData rData, Authentication auth) =>
    {
        return await auth.Logout(rData);
    });


            e.MapPost("getSignupInfo",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await getSignupInfo.GetSignupInfo(rData));

    });

            e.MapPost("usernewaccount",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await usernewaccount.UserNewAccount(rData));

    });
            e.MapPost("updateuseraccount",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await usernewaccount.UpdateUserAccount(rData));

    });
            e.MapPost("deleteuseraccount",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await usernewaccount.DeleteUserAccount(rData));

    });
            e.MapPost("getmanagerdata",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await getManagerData.GetManagerData(rData));

    });
            e.MapPost("getsupplierdata",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await getSupplierData.GetSupplierData(rData));

    });
            e.MapPost("orderservice",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await orderservice.CreateOrder(rData));

    });
            e.MapPost("updateorderstatus",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await orderservice.UpdateOrderStatus(rData));

    });
            e.MapPost("updateorderstatusbyshipmentId",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await orderservice.UpdateOrderStatusByShipmentId(rData));

    });
            e.MapPost("deleteorders",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await orderservice.DeleteOrders(rData));

    });
            e.MapPost("getorderlist",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await getOrderList.GetOrderList(rData));

    });
            e.MapPost("managerprofileservice",
    [AllowAnonymous] async (HttpContext http) =>
    {
        var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
        requestData rData = JsonSerializer.Deserialize<requestData>(body);
        if (rData.eventID == "1001") // update
            await http.Response.WriteAsJsonAsync(await managerProfileService.CreateManagerProfile(rData));

    });
            e.MapPost("managerprofiledata",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await managerProfileService.GetManagerProfileDatal(rData));

           });
            e.MapPost("upadatemanagerdata",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await managerProfileService.UpdateManagerProfile(rData));

           });
            e.MapPost("deletemanagerprofile",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await managerProfileService.DeleteManagerProfile(rData));

           });

            e.MapPost("supplierprofileservice",
            [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await supplierProfileService.CreateSupplierProfile(rData));

           });


            e.MapPost("supplierprofiledata",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await supplierProfileService.GetSupplierProfileData(rData));

           });
            e.MapPost("updatesupplierprofile",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await supplierProfileService.UpdateSupplierProfile(rData));

           });
            e.MapPost("deletesupplierprofile",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await supplierProfileService.DeleteSupplierProfile(rData));

           });


            e.MapPost("getmanagerspofilesdata",
  [AllowAnonymous] async (HttpContext http) =>
  {
      var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
      requestData rData = JsonSerializer.Deserialize<requestData>(body);
      if (rData.eventID == "1001") // update
          await http.Response.WriteAsJsonAsync(await getUsersPofilesData.GetMangersPofilesData(rData));

  });

            e.MapPost("getsupplierspofilesdata",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getUsersPofilesData.GetSuppliersPofilesData(rData));

           });
            e.MapPost("productservice",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await productService.CreateProduct(rData));

           });
            e.MapPost("updateproduct",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await productService.UpdateProduct(rData));

           });
            e.MapPost("deleteproduct",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await productService.DeleteProduct(rData));

           });
            e.MapPost("getproductlist",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await productService.GetProductList(rData));

           });




            e.MapPost("getnotavaiableproduct",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getNotAvaiableProduct.GetNotAvaiableProduct(rData));

           });
            e.MapPost("updateorderstatusbysupplier",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getNotAvaiableProduct.UpdateOrderStatusBySupplier(rData));

           });


            e.MapPost("updateorderbysupplierstatusbyshipmentId",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getNotAvaiableProduct.UpdateOrderBySupplierStatusByShipmentId(rData));

           });

            e.MapPost("confirmeddeliveries",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getNotAvaiableProduct.ConfirmedDeliveries(rData));

           });

            e.MapPost("transactionhistory",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getNotAvaiableProduct.TransactionHistory(rData));

           });
            e.MapPost("transactionhistorysupplier",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getNotAvaiableProduct.TransactionHistorySupplier(rData));

           });

            e.MapPost("orderchartservice",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await orderChartService.GetOrderStatusCounts(rData));

           });
            e.MapPost("getuserrolecounts",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getUserRolData.GetUserRoleCounts(rData));

           });
            e.MapPost("getorderdata",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getOrderData.GetOrderData(rData));

           });
            e.MapPost("getsuppliertransactiondata",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await getsupplierorderdata.GetSupplierTransactionData(rData));

           });

            e.MapPost("totalusersservice",
           [AllowAnonymous] async (HttpContext http) =>
           {
               var body = await new StreamReader(http.Request.Body).ReadToEndAsync();
               requestData rData = JsonSerializer.Deserialize<requestData>(body);
               if (rData.eventID == "1001") // update
                   await http.Response.WriteAsJsonAsync(await totalUsersService.GetTotalUsers(rData));

           });







            e.MapPost("admin/login",
      async context =>
      {
          try
          {
              var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
              var loginRequest = JsonSerializer.Deserialize<LoginRequest>(body);

              var token = await adminLoginService.Authenticate(loginRequest.Username, loginRequest.Password);

              if (token == null)
              {
                  context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                  await context.Response.WriteAsync("Invalid credentials.");
                  return;
              }

              await context.Response.WriteAsJsonAsync(new { token });
          }
          catch (Exception ex)
          {
              context.Response.StatusCode = StatusCodes.Status500InternalServerError;
              await context.Response.WriteAsync($"An error occurred: {ex.Message}");
          }
      });


            e.MapPost("admin/logout",
         async context =>
         {
             await context.Response.WriteAsync("Logged out successfully.");
         });




            IConfiguration appsettings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            e.MapGet("/dbstring",
                  async c =>
                  {
                      dbServices dspoly = new dbServices();
                      await c.Response.WriteAsJsonAsync("{'mongoDatabase':" + appsettings["mongodb:connStr"] + "," + " " + "MYSQLDatabase" + " =>" + appsettings["db:connStrPrimary"]);
                  });

            e.MapGet("/bing",
          async c => await c.Response.WriteAsJsonAsync("{'Name':'Anish','Age':'26','Project':'COMMON_PROJECT_STRUCTURE_API'}"));
        });
    }).Build().Run();
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
public record requestData
{
    [Required]
    public string eventID { get; set; }
    [Required]
    public IDictionary<string, object> addInfo { get; set; }
}

public record responseData
{
    public responseData()
    {
        eventID = "";
        rStatus = 0;
        rData = new Dictionary<string, object>();
    }
    [Required]
    public int rStatus { get; set; } = 0;
    public string eventID { get; set; }
    public IDictionary<string, object> addInfo { get; set; }
    public IDictionary<string, object> rData { get; set; }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}