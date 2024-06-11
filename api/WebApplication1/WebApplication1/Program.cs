using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPshbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//JSON Serializer : 'tradutor' dos dados da web para o codigo

builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
    options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

//builder.Services.AddControllers() : Este método é usado para adicionar os serviços necessários para controllers na aplicação Ele é uma parte do sistema de injeção de dependência do ASP.NET Core.

//.AddNewtonsoftJson(options => : Este método é usado para configurar o Newtonsoft.Json como o serializador JSON padrão para a aplicação ASP.NET Core.

//options.SerializerSettings.ReferenceLoopHandling : Newtonsoft.Json.ReferenceLoopHandling.Ignore : configura como lidar com referências circulares ao serializar objetos JSON. Aqui, ReferenceLoopHandling.
//Ignore diz ao serializador para ignorar essas referências circulares ao invés de lançar uma exceção.

//.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()) : configura o resolvedor de contratos para o serializador JSON.
//O DefaultContractResolver é responsável por determinar quais propriedades de um objeto serão incluídas na serialização JSON.Configurá-lo desta forma significa que todas as propriedades públicas do objeto serão
//incluídas na serialização por padrão.



var app = builder.Build();


//Enable CORS(Cross-Origin Resource Sharing): permitir solicitações de qualquer origem, com qualquer método HTTP e com qualquer cabeçalho

app.UseCors(c=>c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

//app.UseCors() : ste método adiciona o middleware de CORS à pipeline de solicitação. Ele permite que você especifique políticas CORS para sua aplicação.

//c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod() : Esta parte define a política CORS

//AllowAnyHeader() : Permite que solicitações incluam qualquer cabeçalho. Isso significa que a aplicação permitirá solicitações com qualquer cabeçalho HTTP

//AllowAnyOrigin() : Permite solicitações de qualquer origem. Isso significa que a aplicação aceitará solicitações de qualquer domínio, sem restrições de origem.

//AllowAnyMethod() : Permite solicitações usando qualquer método HTTP (GET, POST, PUT, DELETE, etc.). Isso significa que a aplicação aceitará solicitações usando qualquer método HTTP.



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
