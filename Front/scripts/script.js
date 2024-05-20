const API = 'https://ppads-canudoproximo.azurewebsites.net/api/v1/';

document.getElementById('loginForm').addEventListener('submit', async function(event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;

    const response = await fetch(`${API}login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ username, password })
    });

    if (response.ok) {
        const data = await response.json();
        localStorage.setItem('token', data.token);  // Armazena o token no localStorage
        localStorage.setItem('username', data.username);  // Armazena o nome do usuário
        localStorage.setItem('idProfessor', data.idProfessor);  // Armazena o ID do professor
        window.location.href = 'index.html';
    } else {
        const result = await response.json();
        alert(result.message);
    }
});
