using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()      // Permite qualquer origem
              .AllowAnyMethod()      // Permite qualquer método HTTP
              .AllowAnyHeader();     // Permite qualquer cabeçalho
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Banco>(options =>
    options.UseMySql("server=localhost;port=3306;database=planner;user=root;password=Jf66t4Rgi",
    new MySqlServerVersion(new Version(8, 0, 33)))); // Configuração do MySQL

var app = builder.Build();

// Habilitar o CORS globalmente
app.UseCors("PermitirTudo");

app.UseSwagger();
app.UseSwaggerUI();

// Mapear APIs
app.MapGet("/", () => "Loja da UP");
app.MapLivroApi();
app.MapClienteApi();
app.MapPedidoApi();
app.MapAutorApi();
app.MapPagamentoApi();

app.Run();

/*app.MapGet("/", () => "Loja da UP");

// Endpoint GET para listar todos os livros de forma assíncrona
app.MapGet("/livros", async ([FromServices] AppDbContext db) =>
    await db.Livros.ToListAsync());

// Endpoint GET para buscar por ID
app.MapGet("/livros/{id}", async (int id, [FromServices] AppDbContext db) =>
    await db.Livros.FindAsync(id) is Livro livro
        ? Results.Ok(livro)
        : Results.NotFound());

// Endpoint POST para criar um novo livro (recebendo o objeto do corpo da requisição)
app.MapPost("/livros", async ([FromBody] Livro livro, [FromServices] AppDbContext db) => {
    db.Livros.Add(livro);
    await db.SaveChangesAsync();
    return Results.Created($"/livros/{livro.Id}", livro);
});

// Endpoint PUT para atualizar um livro
app.MapPut("/livros/{id}", async (int id, [FromBody] Livro livroAlterado, [FromServices] AppDbContext db) =>
{
    var livro = await db.Livros.FindAsync(id);
    if (livro is null) return Results.NotFound();

    livro.Nome = livroAlterado.Nome;
    livro.Autor = livroAlterado.Autor;
    livro.LeituraCompleta = livroAlterado.LeituraCompleta;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

// Endpoint DELETE para deletar um livro
app.MapDelete("/livros/{id}", async (int id, [FromServices] AppDbContext db) =>
{
    if (await db.Livros.FindAsync(id) is Livro livro)
    {
        db.Livros.Remove(livro);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});*/
