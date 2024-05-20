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
        if (alunos.length > 0){
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
        } else {
            const container = document.getElementById('lista-alunos-container');
            container.innerHTML = ''; // Limpar conteúdo existente

            const msg = document.createElement('h4');
            msg.innerHTML = 'Não existem alunos matriculados na Turma selecionada';

            container.appendChild(msg);
        }
    } catch (error) {
        alert(`Professor(a) ${localStorage.getItem('username')} não leciona na Turma selecionada`);
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


