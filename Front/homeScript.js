document.addEventListener('DOMContentLoaded', function() {
    var turmaSelect = document.getElementById('turmaSelect');

    fetch('http://localhost:5217/api/v1/turma/lista')
        .then(response => response.json())
        .then(turmas => {
            turmas.forEach(turma => {
                var option = new Option(turma.nome, turma.idTurma);
                turmaSelect.add(option);
            });
        })
        .catch(error => console.error('Erro ao buscar turmas:', error));
});
