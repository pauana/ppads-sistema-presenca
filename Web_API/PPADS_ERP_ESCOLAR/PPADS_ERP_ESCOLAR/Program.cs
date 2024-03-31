using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddTransient<IAulaRepository, AulaRepository>();
builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();
builder.Services.AddTransient<IDisciplinaRepository, DisciplinaRepository>();
builder.Services.AddTransient<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddTransient<IProfessorRepository, ProfessorRepository>();
builder.Services.AddTransient<IProfessorDisciplinaRepository, ProfessorDisciplinaRepository>();

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
