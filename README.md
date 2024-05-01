# Projeto de Sistema de Presenças escolar

-- Pré-requisitos já instalados no computador -- 
• VSCode 
• Git Bash 2.45.0

-- O que é preciso para a API rodar no computador --

• .NET SDK 7.0.48

• MySQL Community Server 8.3.0 (Estamos usando para testes com o MySQL o Wampserver 3.3.5 - é necessário ter todos os Visual C++ da Microsoft)

-- Passo a Passo para rodar a API --
No computador, clique a tecla Windows, digite env e entre em "Editar as variáveis de ambiente do sistema" e adicione no PATH o dotnet e o git:

    >> C:\Program Files\dotnet
    >> C:\Program Files\Git\cmd.
    >> (Se estiver usando o Wampserver) C:\wamp64\bin\mysql\mysql8.3.0\bin

Execute o programa WampServer (se estiver usando ele como servidor de MySQL)

Abra o Git Bash em uma pasta que será clonado o repositório:

• (Git Bash) `git clone https://github.com/pauana/ppads-sistema-presenca.git`
• (Git Bash) `cd ppads-sistema-presenca/`
• (Git Bash) `code .`

Abrirá o projeto no VSCode 

Instale as extensões: C# DEv Kit | Live Server

! Antes de executar, abra o Prompt de Comando do computador e execute os comandos (substituindo as variáveis conforme está no seu computador):

    >> mysql -u username -p 
    >> CREATE DATABASE escolaoctogono;
    >> EXIT;

    >> mysql -u username -p escolaoctogono < caminho_para_o_arquivo_sql

No terminal no VS, vá até a pasta ppads-sistema-presenca\Web_API\PPADS_ERP_ESCOLAR\PPADS_ERP_ESCOLAR e rode `dotnet run` para compilar o projeto da API

