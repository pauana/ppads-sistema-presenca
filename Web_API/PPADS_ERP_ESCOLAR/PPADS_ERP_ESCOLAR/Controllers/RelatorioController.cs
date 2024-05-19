using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPADS_ERP_ESCOLAR.Infra;
using PPADS_ERP_ESCOLAR.Models;
using System;
using System.Linq;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.IO;

namespace PPADS_ERP_ESCOLAR.Controllers;

[ApiController]
[Route("api/v1")]
public class DiretoController : ControllerBase
{
    private readonly DBConnection _context;

    public DiretoController(DBConnection context)
    {
        _context = context;
    }

    [HttpPost("relatorio")]
    public String Relatorio([FromBody] Filtros filtros){
        string json = "";    

        if (filtros.Agrupar == "turma")
        {
            var query = from turma in _context.Turmas
                        join matricula in _context.Matriculas on turma.idTurma equals matricula.idTurma
                        join aluno in _context.Alunos on matricula.idAluno equals aluno.idAluno
                        select new
                        {
                            idTurma = turma.idTurma,
                            turma = turma.nome,
                            matricula.chamada,
                            aluno = matricula.chamada + " - " + aluno.nome + "(" + aluno.ra + ")",
                            aulas = _context.Aulas.Count(a => a.idTurma == turma.idTurma && 
                            (!filtros.DataIni.HasValue || a.data >= filtros.DataIni.Value) &&
                            (!filtros.DataFim.HasValue || a.data <= filtros.DataFim.Value) &&
                            (a.data.Year == filtros.AnoLetivo) &&
                            (string.IsNullOrEmpty(filtros.Periodo) || a.periodo == filtros.Periodo)),
                            faltas = _context.RegistrosPresenca
                                            .Where(rp => rp.idMatricula == matricula.idMatricula && rp.presenca == "F")
                                            .Join(_context.Aulas, rp => rp.idAula, a => a.idAula, (rp, a) => new { rp, a })
                                            .Count(j => (!filtros.DataIni.HasValue || j.a.data >= filtros.DataIni.Value) &&
                                                    (!filtros.DataFim.HasValue || j.a.data <= filtros.DataFim.Value) &&
                                                    (j.a.data.Year == filtros.AnoLetivo) &&
                                                    (string.IsNullOrEmpty(filtros.Periodo) || j.a.periodo == filtros.Periodo)),
                        } into result
                        where result.aulas > 0
                        select new
                        {
                            result.idTurma,
                            result.turma,
                            result.chamada,
                            result.aluno,
                            result.aulas,
                            result.faltas,
                            freq = Math.Round(((double)(result.aulas - result.faltas) / result.aulas) * 100, 0)
                        };

            if (filtros.Turma.HasValue)    
            {
                if (filtros.Turma.Value != 0){
                    query = query.Where(q => q.idTurma == filtros.Turma.Value);
                }
            }

            var orderedQuery = query.OrderBy(r => r.turma).ThenBy(r => r.chamada);
            json = JsonConvert.SerializeObject(orderedQuery, Formatting.Indented);

        } else if (filtros.Agrupar == "professor")
        {
            var query = from professor in _context.Professores
                        join turmaProfessor in _context.TurmasProfessor on professor.idProfessor equals turmaProfessor.idProfessor
                        join turma in _context.Turmas on turmaProfessor.idTurma equals turma.idTurma
                        join matricula in _context.Matriculas on turma.idTurma equals matricula.idTurma
                        join aluno in _context.Alunos on matricula.idAluno equals aluno.idAluno
                        select new
                        {
                            professor = professor.nome,
                            idTurma = turma.idTurma,
                            turma = turma.nome,
                            chamada = matricula.chamada,
                            aluno = matricula.chamada + " - " + aluno.nome + " (" + aluno.ra + ")",
                            aulas = _context.Aulas.Count(a => a.idTurma == turma.idTurma && a.idProfessor == professor.idProfessor && 
                            (!filtros.DataIni.HasValue || a.data >= filtros.DataIni.Value) &&
                            (!filtros.DataFim.HasValue || a.data <= filtros.DataFim.Value) &&
                            (a.data.Year == filtros.AnoLetivo) &&
                            (string.IsNullOrEmpty(filtros.Periodo) || a.periodo == filtros.Periodo)),
                            faltas = _context.RegistrosPresenca
                                                .Where(rp => rp.idMatricula == matricula.idMatricula && rp.presenca == "F")
                                                .Join(_context.Aulas, rp => rp.idAula, a => a.idAula, (rp, a) => new {rp, a})
                                                .Count(j => j.a.idProfessor == professor.idProfessor &&
                                                    (!filtros.DataIni.HasValue || j.a.data >= filtros.DataIni.Value) &&
                                                    (!filtros.DataFim.HasValue || j.a.data <= filtros.DataFim.Value) &&
                                                    (j.a.data.Year == filtros.AnoLetivo) &&
                                                    (string.IsNullOrEmpty(filtros.Periodo) || j.a.periodo == filtros.Periodo)),
                        } into result
                        where result.aulas > 0
                        select new
                        {
                            result.professor,
                            result.idTurma,
                            result.turma,
                            result.chamada,
                            result.aluno,
                            result.aulas,
                            result.faltas,
                            freq = result.aulas > 0 ? Math.Round((double)(result.aulas - result.faltas) / result.aulas * 100, 0) : 0
                        };
            
            if (filtros.Turma.HasValue)    
            {
                if (filtros.Turma.Value != 0){
                    query = query.Where(q => q.idTurma == filtros.Turma.Value);
                }
            }

            var orderedQuery = query.OrderBy(r => r.professor).ThenBy(r => r.turma).ThenBy(r => r.chamada);
            json = JsonConvert.SerializeObject(orderedQuery, Formatting.Indented);

        } else if (filtros.Agrupar == "disciplina")
        {
            var query = from disciplina in _context.Disciplinas
                        join professorDisciplina in _context.ProfessoresDisciplina on disciplina.idDisciplina equals professorDisciplina.idDisciplina
                        join professor in _context.Professores on professorDisciplina.idProfessor equals professor.idProfessor
                        join aula in _context.Aulas on professor.idProfessor equals aula.idProfessor
                        join turma in _context.Turmas on aula.idTurma equals turma.idTurma
                        join matricula in _context.Matriculas on turma.idTurma equals matricula.idTurma
                        join aluno in _context.Alunos on matricula.idAluno equals aluno.idAluno
                        select new
                        {
                            disciplina = disciplina.materia + " (" + professor.nome + ")",
                            idTurma = turma.idTurma,
                            turma = turma.nome,
                            chamada = matricula.chamada,
                            aluno = matricula.chamada + " - " + aluno.nome + " (" + aluno.ra + ")",
                            aulas = _context.Aulas.Count(a => a.idTurma == turma.idTurma && a.idProfessor == professor.idProfessor && 
                            (!filtros.DataIni.HasValue || a.data >= filtros.DataIni.Value) &&
                            (!filtros.DataFim.HasValue || a.data <= filtros.DataFim.Value) &&
                            (a.data.Year == filtros.AnoLetivo) &&
                            (string.IsNullOrEmpty(filtros.Periodo) || a.periodo == filtros.Periodo)),
                            faltas = _context.RegistrosPresenca
                                                .Where(rp => rp.idMatricula == matricula.idMatricula && rp.presenca == "F")
                                                .Join(_context.Aulas, rp => rp.idAula, a => a.idAula, (rp, a) => new {rp, a})
                                                .Count(j => j.a.idProfessor == professor.idProfessor &&
                                                    (!filtros.DataIni.HasValue || j.a.data >= filtros.DataIni.Value) &&
                                                    (!filtros.DataFim.HasValue || j.a.data <= filtros.DataFim.Value) &&
                                                    (j.a.data.Year == filtros.AnoLetivo) &&
                                                    (string.IsNullOrEmpty(filtros.Periodo) || j.a.periodo == filtros.Periodo)),
                        } into result
                        where result.aulas > 0
                        select new
                        {
                            result.disciplina,
                            result.idTurma,
                            result.turma,
                            result.chamada,
                            result.aluno,
                            result.aulas,
                            result.faltas,
                            freq = result.aulas > 0 ? Math.Round((double)(result.aulas - result.faltas) / result.aulas * 100, 0) : 0
                        };

            if (filtros.Turma.HasValue)    
            {
                if (filtros.Turma.Value != 0){
                    query = query.Where(q => q.idTurma == filtros.Turma.Value);
                }
            }

            var orderedQuery = query.OrderBy(r => r.disciplina).ThenBy(r => r.turma).ThenBy(r => r.chamada);
            json = JsonConvert.SerializeObject(orderedQuery, Formatting.Indented);

        } else if (filtros.Agrupar == "aluno")
        {
            var query = from aluno in _context.Alunos
                        join matricula in _context.Matriculas on aluno.idAluno equals matricula.idAluno
                        join turma in _context.Turmas on matricula.idTurma equals turma.idTurma
                        join turmaProfessor in _context.TurmasProfessor on turma.idTurma equals turmaProfessor.idTurma
                        join professor in _context.Professores on turmaProfessor.idProfessor equals professor.idProfessor
                        join professorDisciplina in _context.ProfessoresDisciplina on professor.idProfessor equals professorDisciplina.idProfessor
                        join disciplina in _context.Disciplinas on professorDisciplina.idDisciplina equals disciplina.idDisciplina
                        where professor.ativo == "A"
                        select new
                        {
                            aluno = aluno.nome + " (" + aluno.ra + ") - " + turma.nome,
                            idTurma = turma.idTurma,
                            disciplina = disciplina.materia,
                            professor = professor.nome,
                            aulas = _context.Aulas.Count(a => a.idTurma == turma.idTurma && a.idProfessor == professor.idProfessor && 
                            (!filtros.DataIni.HasValue || a.data >= filtros.DataIni.Value) &&
                            (!filtros.DataFim.HasValue || a.data <= filtros.DataFim.Value) &&
                            (a.data.Year == filtros.AnoLetivo) &&
                            (string.IsNullOrEmpty(filtros.Periodo) || a.periodo == filtros.Periodo)),
                            faltas = _context.RegistrosPresenca
                                                .Where(rp => rp.idMatricula == matricula.idMatricula && rp.presenca == "F")
                                                .Join(_context.Aulas, rp => rp.idAula, a => a.idAula, (rp, a) => new {rp, a})
                                                .Count(j => j.a.idProfessor == professor.idProfessor &&
                                                    (!filtros.DataIni.HasValue || j.a.data >= filtros.DataIni.Value) &&
                                                    (!filtros.DataFim.HasValue || j.a.data <= filtros.DataFim.Value) &&
                                                    (j.a.data.Year == filtros.AnoLetivo) &&
                                                    (string.IsNullOrEmpty(filtros.Periodo) || j.a.periodo == filtros.Periodo)),
                        } into result
                        where result.aulas > 0
                        select new
                        {
                            result.aluno,
                            result.idTurma,
                            result.disciplina,
                            result.professor,
                            result.aulas,
                            result.faltas,
                            freq = Math.Round((double)(result.aulas - result.faltas) / result.aulas * 100, 0)
                        };

            if (filtros.Turma.HasValue)    
            {
                if (filtros.Turma.Value != 0){
                    query = query.Where(q => q.idTurma == filtros.Turma.Value);
                }
            }

            var orderedQuery = query.OrderBy(r => r.aluno).ThenBy(r => r.disciplina).ToList();
            json = JsonConvert.SerializeObject(orderedQuery, Formatting.Indented);
        }
        return json;
    }

    [HttpPost("exportar")]
    public IActionResult ExportarRelatorio([FromBody] Filtros filtros){
        string json = Relatorio(filtros);

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Relatório");

        // Adicionar cabeçalhos e ajustar estilo

        if (filtros.Agrupar == "turma")
        {
            var data = JsonConvert.DeserializeObject<List<TurmaItem>>(json);
            
            var headers = new[] { "Turma", "Aluno", "Total de Aulas", "Total de Faltas", "Frequência" }; 
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Adicionar dados
            if (data.Count > 0)
            {
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.Turma;
                    worksheet.Cell(row, 2).Value = item.Aluno;
                    worksheet.Cell(row, 3).Value = item.Aulas;
                    worksheet.Cell(row, 4).Value = item.Faltas;
                    worksheet.Cell(row, 5).Value = item.Freq;
                    row++;
                } 
            } 
        } else if (filtros.Agrupar == "professor")
        {
            var data = JsonConvert.DeserializeObject<List<ProfessorItem>>(json);

            var headers = new[] { "Professor", "Turma", "Aluno", "Total de Aulas", "Total de Faltas", "Frequência" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Adicionar dados   
            if (data.Count > 0)
            {
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.Professor;
                    worksheet.Cell(row, 2).Value = item.Turma;
                    worksheet.Cell(row, 3).Value = item.Aluno;
                    worksheet.Cell(row, 4).Value = item.Aulas;
                    worksheet.Cell(row, 5).Value = item.Faltas;
                    worksheet.Cell(row, 6).Value = item.Freq;
                    row++;
                } 
            } 
        } else if (filtros.Agrupar == "disciplina")
        {
            var data = JsonConvert.DeserializeObject<List<DisciplinaItem>>(json);
            var headers = new[] { "Disciplina", "Turma", "Aluno", "Total de Aulas", "Total de Faltas", "Frequência" }; 
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Adicionar dados   
            if (data.Count > 0)
            {
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.Disciplina;
                    worksheet.Cell(row, 2).Value = item.Turma;
                    worksheet.Cell(row, 3).Value = item.Aluno;
                    worksheet.Cell(row, 4).Value = item.Aulas;
                    worksheet.Cell(row, 5).Value = item.Faltas;
                    worksheet.Cell(row, 6).Value = item.Freq;
                    row++;
                } 
            } 
        } else if (filtros.Agrupar == "aluno")
        {
            var data = JsonConvert.DeserializeObject<List<AlunoItem>>(json);
            var headers = new[] {"Aluno", "Disciplina", "Professor", "Total de Aulas", "Total de Faltas", "Frequência" };
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
            }

            // Adicionar dados   
            if (data.Count > 0)
            {
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cell(row, 1).Value = item.Aluno;
                    worksheet.Cell(row, 2).Value = item.Disciplina;
                    worksheet.Cell(row, 3).Value = item.Professor;
                    worksheet.Cell(row, 4).Value = item.Aulas;
                    worksheet.Cell(row, 5).Value = item.Faltas;
                    worksheet.Cell(row, 6).Value = item.Freq;
                    row++;
                } 
            } 
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Relatorio.xlsx");
    }
}