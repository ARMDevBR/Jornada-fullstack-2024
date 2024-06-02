using Finan.Api;
using Finan.Api.Common.Api;
using Finan.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

/*// Vers�o antiga inicial
//builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString)); // Usa AddScoped.

//builder.Services.AddScoped // Uma inst�ncia do banco por requisi��o.
//builder.Services.AddSingleton // Uma inst�ncia do banco para toda a aplica��o ap�s a 1� requisi��o.
//builder.Services.AddTransient // Uma inst�ncia do banco por item a ser resolvido.

//builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
//builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();*/

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();

app.Run();
