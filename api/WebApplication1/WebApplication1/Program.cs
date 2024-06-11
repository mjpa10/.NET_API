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

//builder.Services.AddControllers() : Este m�todo � usado para adicionar os servi�os necess�rios para controllers na aplica��o Ele � uma parte do sistema de inje��o de depend�ncia do ASP.NET Core.

//.AddNewtonsoftJson(options => : Este m�todo � usado para configurar o Newtonsoft.Json como o serializador JSON padr�o para a aplica��o ASP.NET Core.

//options.SerializerSettings.ReferenceLoopHandling : Newtonsoft.Json.ReferenceLoopHandling.Ignore : configura como lidar com refer�ncias circulares ao serializar objetos JSON. Aqui, ReferenceLoopHandling.
//Ignore diz ao serializador para ignorar essas refer�ncias circulares ao inv�s de lan�ar uma exce��o.

//.AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()) : configura o resolvedor de contratos para o serializador JSON.
//O DefaultContractResolver � respons�vel por determinar quais propriedades de um objeto ser�o inclu�das na serializa��o JSON.Configur�-lo desta forma significa que todas as propriedades p�blicas do objeto ser�o
//inclu�das na serializa��o por padr�o.



var app = builder.Build();


//Enable CORS(Cross-Origin Resource Sharing): permitir solicita��es de qualquer origem, com qualquer m�todo HTTP e com qualquer cabe�alho

app.UseCors(c=>c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

//app.UseCors() : ste m�todo adiciona o middleware de CORS � pipeline de solicita��o. Ele permite que voc� especifique pol�ticas CORS para sua aplica��o.

//c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod() : Esta parte define a pol�tica CORS

//AllowAnyHeader() : Permite que solicita��es incluam qualquer cabe�alho. Isso significa que a aplica��o permitir� solicita��es com qualquer cabe�alho HTTP

//AllowAnyOrigin() : Permite solicita��es de qualquer origem. Isso significa que a aplica��o aceitar� solicita��es de qualquer dom�nio, sem restri��es de origem.

//AllowAnyMethod() : Permite solicita��es usando qualquer m�todo HTTP (GET, POST, PUT, DELETE, etc.). Isso significa que a aplica��o aceitar� solicita��es usando qualquer m�todo HTTP.



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
