async function cadastrarUsuario() {

    const nome = document.getElementById("nome").value;
    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;

    const resposta = await fetch("/api/users", {

        method: "POST",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify({
            nome: nome,
            email: email,
            senha: senha
        })

    });

    if (resposta.ok) {

        alert("Usuario cadastrado!");

        window.location.href = "login.html";

    } else {

        alert("Erro ao cadastrar usuario");

    }
}



async function login() {

    const email = document.getElementById("email").value;
    const senha = document.getElementById("senha").value;

    const resposta = await fetch(`/api/auth/login?email=${email}&senha=${senha}`, {

        method: "POST",

        credentials: "include"

    });

    if (resposta.ok) {

        alert("Login realizado!");

        window.location.href = "tarefas.html";

    } else {

        alert("Email ou senha invalidos");

    }
}



async function carregarTarefas() {

    const resposta = await fetch("/api/tarefas/usuario", {

        credentials: "include"

    });

    if (!resposta.ok) {

        alert("Faca login primeiro");

        window.location.href = "login.html";

        return;
    }

    const tarefas = await resposta.json();

    const div = document.getElementById("listaTarefas");

    div.innerHTML = "";

    tarefas.forEach(tarefa => {

        div.innerHTML += `
            <div>
                <p><strong>${tarefa.tarefa}</strong></p>
                <p>Status: ${tarefa.status ? "Concluida" : "Pendente"}</p>
                <hr>
            </div>
        `;
    });
}



async function criarTarefa() {

    const descricao = document.getElementById("descricao").value;

    const status = document.getElementById("status").value === "true";

    const resposta = await fetch("/api/tarefas", {

        method: "POST",

        credentials: "include",

        headers: {
            "Content-Type": "application/json"
        },

        body: JSON.stringify({
            descricao: descricao,
            status: status
        })

    });

    if (resposta.ok) {

        alert("Tarefa cadastrada!");

        carregarTarefas();

    } else {

        alert("Erro ao cadastrar tarefa");

    }
}



async function logout() {

    await fetch("/api/auth/logout", {

        method: "POST",

        credentials: "include"

    });

    alert("Logout realizado!");

    window.location.href = "login.html";
}