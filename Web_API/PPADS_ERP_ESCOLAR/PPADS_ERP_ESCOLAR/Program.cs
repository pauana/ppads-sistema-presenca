using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:5501")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddTransient<IAulaRepository, AulaRepository>();
builder.Services.AddTransient<IAlunoRepository, AlunoRepository>();
builder.Services.AddTransient<IDisciplinaRepository, DisciplinaRepository>();
builder.Services.AddTransient<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddTransient<IProfessorRepository, ProfessorRepository>();
builder.Services.AddTransient<IProfessorDisciplinaRepository, ProfessorDisciplinaRepository>();
builder.Services.AddTransient<IResponsavelRepository, ResponsavelRepository>();
builder.Services.AddTransient<IResponsavelAlunoRepository, ResponsavelAlunoRepository>();
builder.Services.AddTransient<ISerieRepository, SerieRepository>();
builder.Services.AddTransient<ITurmaRepository, TurmaRepository>();
builder.Services.AddTransient<ITurmaProfessorRepository, TurmaProfessorRepository>();
builder.Services.AddTransient<IRegistroPresencaRepository, RegistroPresencaRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
