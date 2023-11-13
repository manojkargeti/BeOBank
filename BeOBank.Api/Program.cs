using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.CustomerAccountService;
using BeOBank.Web.Api.Services.TransactionsService;
 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, "BeOBankAPISpecification.xml");

    if (File.Exists(xmlCommentsFilePath))
    {
        c.IncludeXmlComments(xmlCommentsFilePath);
    }
    else
    {
         //added for integrationTest
        Console.WriteLine($"Warning: XML comments file not found at {xmlCommentsFilePath}");
    }
});

var customers = new List<CustomerDetails>
{
    new CustomerDetails { CustomerId = 1, Name = "Manoj", Surname = "Kargeti" },
    new CustomerDetails { CustomerId = 2, Name = "Michell", Surname = "Jackson" },
    new CustomerDetails { CustomerId = 3, Name = "James", Surname = "Jonson" },
    new CustomerDetails { CustomerId = 4, Name = "Bond", Surname = "Decole" }
};

builder.Services.AddSingleton<IList<CustomerDetails>>(customers);

builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();