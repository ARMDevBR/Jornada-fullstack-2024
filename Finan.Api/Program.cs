using Finan.Api;
using Finan.Api.Common.Api;
using Finan.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

/*// Versão antiga inicial
//builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString)); // Usa AddScoped.

//builder.Services.AddScoped // Uma instância do banco por requisição.
//builder.Services.AddSingleton // Uma instância do banco para toda a aplicação após a 1ª requisição.
//builder.Services.AddTransient // Uma instância do banco por item a ser resolvido.

//builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
//builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();*/

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();

app.Run();
