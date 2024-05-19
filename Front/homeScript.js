const API = 'http://localhost:5217/api/v1/';

document.addEventListener('DOMContentLoaded', function() {
    var turmaSelect = document.getElementById('turmaSelect');

    fetch(API + 'turma/lista')
        .then(response => response.json())
        .then(turmas => {
            turmas.forEach(turma => {
                var option = new Option(turma.nome, turma.idTurma);
                turmaSelect.add(option);
            });
        })
        .catch(error => console.error('Erro ao buscar turmas:', error));
});

document.addEventListener('DOMContentLoaded', function() {
    var anoSelect = document.getElementById('anoSelect');

    fetch(API + 'serie/anosletivos')
        .then(response => response.json())
        .then(anos => {
            anos.forEach(ano => {
                var option = new Option(ano, ano);
                anoSelect.add(option);
            });
        })
        .catch(error => console.error('Erro ao buscar anos letivos:', error));
});

document.getElementById('generate-report').addEventListener('click', generateReport);
document.getElementById('export-report').addEventListener('click', exportReport);

function generateReport() {
    getReport('relatorio');
}

function exportReport(){
    getReport('exportar');
}

function getReport(button){
    const form = document.getElementById('filter-form');

    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    const dataIni = document.getElementById('diaIniInput').value;
    const dataFim = document.getElementById('diaFimInput').value;
    const anoLetivo = document.getElementById('anoSelect').value;
    const periodo = document.getElementById('periodoSelect').value;
    const turma = document.getElementById('turmaSelect').value;
    const agrupar = document.getElementById('agrupaSelect').value;

    const requestData = {
        dataini: dataIni ? new Date(dataIni) : null,
        datafim: dataFim ? new Date(dataFim) : null,
        anoletivo: parseInt(anoLetivo),
        periodo: periodo,
        turma: turma ? parseInt(turma) : 0,
        agrupar: agrupar
    };

    url = API + button;

    if (button == 'relatorio'){
        fetch(url , {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestData)
        })
        .then(response => response.json())
        .then(data => {
            console.log('Resposta recebida:', data);
            if (data.length == 0){
                const container = document.getElementById('report-container');
                container.innerHTML = '';
    
                const msgContainer = document.createElement('h4');
                msgContainer.innerHTML = 'Não existem informações';
                container.appendChild(msgContainer);
            } else{
                criarTabela(data);
            }
        })
        .catch(error => console.error('Erro ao enviar dados:', error));
    } else {
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestData)
        })
        .then(response => response.blob())
        .then(async blob => {
            const options = {
                suggestedName: 'Relatorio.xlsx',
                types: [
                    {
                        description: 'Excel File',
                        accept: {
                            'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet': ['.xlsx']
                        }
                    }
                ]
            };
    
            try {
                const handle = await window.showSaveFilePicker(options);
                const writable = await handle.createWritable();
                await writable.write(blob);
                await writable.close();
            } catch (error) {
                console.error('Erro ao salvar o arquivo:', error);
            }
        })
        .catch(error => console.error('Erro ao exportar relatório:', error));
    }
}

function criarTabela(dados) {
    const agruparPor = document.getElementById('agrupaSelect').value;
    const agrupados = agruparDados(dados, agruparPor);
    const container = document.getElementById('report-container');
    container.innerHTML = '';

    Object.entries(agrupados).forEach(([turma, alunos]) => {
        const turmaContainer = document.createElement('div');
        turmaContainer.className = 'turma-container';

        const turmaHeader = document.createElement('div');
        turmaHeader.className = 'turma-header';
        turmaHeader.innerHTML = `<button class="expand-button">▶</button> ${turma}`;
        turmaHeader.addEventListener('click', () => {
            turmaBody.classList.toggle('hidden');
        });

        const turmaBody = document.createElement('div');
        turmaBody.className = 'turma-body hidden';

        const tabela = document.createElement('table');
        tabela.className = 'table table-bordered';

        const thead = document.createElement('thead');
        if (agruparPor == 'turma') {
            thead.innerHTML = '<tr><th>Alunos</th><th>Total Aulas</th><th>Total Faltas</th><th>Frequência (%)</th></tr>';
        } else if (agruparPor == 'professor') {
            thead.innerHTML = '<tr><th>Turmas</th><th>Alunos</th><th>Total Aulas</th><th>Total Faltas</th><th>Frequência (%)</th></tr>';
        } else if (agruparPor == 'disciplina') {
            thead.innerHTML = '<tr><th>Turmas</th><th>Alunos</th><th>Total Aulas</th><th>Total Faltas</th><th>Frequência (%)</th></tr>';
        } else if (agruparPor == 'aluno') {
            thead.innerHTML = '<tr><th>Disciplina</th><th>Professor</th><th>Total Aulas</th><th>Total Faltas</th><th>Frequência (%)</th></tr>';        
        }
        
        tabela.appendChild(thead);

        const tbody = document.createElement('tbody');
        if (agruparPor == 'turma') {
            alunos.forEach(aluno => {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>${aluno.aluno}</td><td>${aluno.aulas}</td><td>${aluno.faltas}</td><td>${aluno.freq}</td>`;
                tbody.appendChild(tr);
            });
        } else if (agruparPor == 'professor') {
            alunos.forEach(aluno => {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>${aluno.turma}</td><td>${aluno.aluno}</td><td>${aluno.aulas}</td><td>${aluno.faltas}</td><td>${aluno.freq}</td>`;
                tbody.appendChild(tr);
            }); 
        } else if (agruparPor == 'disciplina') {
            alunos.forEach(aluno => {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>${aluno.turma}</td><td>${aluno.aluno}</td><td>${aluno.aulas}</td><td>${aluno.faltas}</td><td>${aluno.freq}</td>`;
                tbody.appendChild(tr);
            }); 
        } else if (agruparPor == 'aluno') {
            alunos.forEach(disciplina => {
                const tr = document.createElement('tr');
                tr.innerHTML = `<td>${disciplina.disciplina}</td><td>${disciplina.professor}</td><td>${disciplina.aulas}</td><td>${disciplina.faltas}</td><td>${disciplina.freq}</td>`;
                tbody.appendChild(tr);
            });        
        }
        tabela.appendChild(tbody);
        turmaBody.appendChild(tabela);
        turmaContainer.appendChild(turmaHeader);
        turmaContainer.appendChild(turmaBody);
        container.appendChild(turmaContainer);
    });
}

function agruparDados(dados, chave) {
    return dados.reduce((acc, item) => {
        const key = item[chave];
        if (!acc[key]) {
            acc[key] = [];
        }
        acc[key].push(item);
        return acc;
    }, {});
}
