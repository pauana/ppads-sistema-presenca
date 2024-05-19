const API = 'http://localhost:5217/api/v1/';

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
        window.location.href = 'index.html';
    } else {
        const result = await response.json();
        alert(result.message);
    }
});

