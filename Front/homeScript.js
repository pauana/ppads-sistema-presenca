const API = 'https://ppads-canudoproximo.azurewebsites.net/api/v1/';

document.addEventListener('DOMContentLoaded', async function() {
    const username = localStorage.getItem('username');
    if (username) {
        document.getElementById('user-name').textContent = username;
    }

    const token = localStorage.getItem('token');

    if (!token) {
        window.location.href = 'login.html';
        return;
    }

    const response = await fetch('https://ppads-canudoproximo.azurewebsites.net/api/v1/verify-token', {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });

    if (!response.ok) {
        localStorage.removeItem('token');  // Remove o token inválido
        window.location.href = 'login.html';
    }

    var turmaSelect = document.getElementById('turmaSelect');

    fetch(API + 'turma/lista', {
        headers: {
            'Authorization': `Bearer ${token}`  // Incluir o token no cabeçalho
        }
    })
    .then(response => response.json())
    .then(turmas => {
        turmas.forEach(turma => {
            var option = new Option(turma.nome, turma.idTurma);
            turmaSelect.add(option);
        });
    })
    .catch(error => console.error('Erro ao buscar turmas:', error));

    var anoSelect = document.getElementById('anoSelect');

    fetch(API + 'serie/anosletivos', {
        headers: {
            'Authorization': `Bearer ${token}`  // Incluir o token no cabeçalho
        }
    })
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
    const token = localStorage.getItem('token');  // Obter o token do localStorage

    const requestData = {
        dataini: dataIni ? new Date(dataIni) : null,
        datafim: dataFim ? new Date(dataFim) : null,
        anoletivo: parseInt(anoLetivo),
        periodo: periodo,
        turma: turma ? parseInt(turma) : 0,
        agrupar: agrupar
    };

    const url = API + button;

    if (button == 'relatorio'){
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`  // Incluir o token no cabeçalho
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
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`  // Incluir o token no cabeçalho
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

document.getElementById('registrar_aula').addEventListener('click', async function() {
    const turmaId = document.getElementById('turmaSelect').value;
    const professorId = localStorage.getItem('idProfessor');
    const data = document.getElementById('diaInput').value;
    const periodo = document.getElementById('periodoSelect').value;
    const token = localStorage.getItem('token');  // Obter o token do localStorage

    const form = document.getElementById('filter-form');

    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    try {
        const response = await fetch(`${API}aula/lista_alunos_com_registro_presenca?turmaId=${turmaId}&professorId=${professorId}&data=${data}&periodo=${periodo}`, {
            method: 'GET',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        const alunos = await response.json();
        renderizarListaDeAlunos(alunos);
        // Verificar se existe algum registro de presença para decidir qual botão exibir
        const existeRegistroPresenca = alunos.some(aluno => aluno.idRegistroPresenca !== null);
        if (existeRegistroPresenca) {
            document.getElementById('atualizar_presenca').style.display = 'inline-block';
            document.getElementById('salvar_presenca').style.display = 'none';
        } else {
            document.getElementById('salvar_presenca').style.display = 'inline-block';
            document.getElementById('atualizar_presenca').style.display = 'none';
        }
    } catch (error) {
        console.error('Erro ao buscar a lista de alunos:', error);
    }
});

document.getElementById('salvar_presenca').addEventListener('click', async function() {
    const token = localStorage.getItem('token');  // Obter o token do localStorage
    const registros = [];
    const turmaId = document.getElementById('turmaSelect').value;
    const professorId = localStorage.getItem('idProfessor'); // ID do professor. Você pode ajustar isso conforme necessário.
    const data = document.getElementById('diaInput').value;
    const periodo = document.getElementById('periodoSelect').value;


    document.querySelectorAll('#lista-alunos-container tr').forEach(tr => {
        const idMatricula = tr.dataset.idMatricula;
        const selectElement = tr.querySelector('select');
        if (selectElement) {
            const presenca = selectElement.value;
            registros.push({ idMatricula, presenca, idTurma: turmaId, idProfessor: professorId, data, periodo });
        }
    });

    try {
        const response = await fetch(`${API}aula/salvar_presenca`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(registros)
        });

        if (response.ok) {
            alert('Presenças salvas com sucesso!');
        } else {
            alert('Erro ao salvar presenças.');
        }
    } catch (error) {
        console.error('Erro ao salvar presenças:', error);
    }
});

document.getElementById('atualizar_presenca').addEventListener('click', async function() {
    const token = localStorage.getItem('token');  // Obter o token do localStorage
    const registros = [];

    document.querySelectorAll('#lista-alunos-container tr').forEach(tr => {
        const idRegistroPresenca = tr.dataset.idRegistroPresenca;
        const selectElement = tr.querySelector('select');
        if (selectElement) {
            const presenca = selectElement.value;
            registros.push({ idRegistroPresenca, presenca });
        }
    });

    try {
        const response = await fetch(`${API}aula/atualizar_presenca`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(registros)
        });

        if (response.ok) {
            alert('Presenças atualizadas com sucesso!');
        } else {
            alert('Erro ao atualizar presenças.');
        }
    } catch (error) {
        console.error('Erro ao atualizar presenças:', error);
    }
});

function renderizarListaDeAlunos(alunos) {
    const container = document.getElementById('lista-alunos-container');
    container.innerHTML = ''; // Limpar conteúdo existente

    const tabela = document.createElement('table');
    tabela.className = 'table table-bordered';

    const thead = document.createElement('thead');
    thead.innerHTML = '<tr><th>Aluno</th><th>Presença</th></tr>';
    tabela.appendChild(thead);

    const tbody = document.createElement('tbody');
    alunos.forEach(aluno => {
        const tr = document.createElement('tr');
        tr.dataset.idMatricula = aluno.idMatricula;
        if (aluno.idRegistroPresenca) {
            tr.dataset.idRegistroPresenca = aluno.idRegistroPresenca;
        }

        const tdAluno = document.createElement('td');
        tdAluno.textContent = aluno.aluno;
        tr.appendChild(tdAluno);

        const tdPresenca = document.createElement('td');
        const selectPresenca = document.createElement('select');
        selectPresenca.className = 'form-control';
        selectPresenca.innerHTML = `
            <option value="P" ${aluno.presenca === 'P' ? 'selected' : ''}>Presença</option>
            <option value="F" ${aluno.presenca === 'F' ? 'selected' : ''}>Falta</option>
        `;
        tdPresenca.appendChild(selectPresenca);
        tr.appendChild(tdPresenca);

        tbody.appendChild(tr);
    });

    tabela.appendChild(tbody);
    container.appendChild(tabela);
}


