document.getElementById('loginForm').onsubmit = function(event) {
    event.preventDefault();

    var username = document.getElementById('username').value;
    var password = document.getElementById('password').value;

    if (username === 'ADMIN' && password === 'admin') {
        window.location.href = 'index.html'
    } else {
        alert('Acesso negado! Verifique o usuário e a senha.');
    }
};
