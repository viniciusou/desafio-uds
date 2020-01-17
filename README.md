# Acais.API

API REST para o sistema de uma loja de açaís personalizados. Teste de desenvolvimento proposto pela UDS Tecnologia.


## Pré-requisitos

Sistema operacional `Linux`, `Windows` ou `macOS`. Assegure-se de ter [Docker](https://docs.docker.com/install/) instalado na máquina utlizada.


## Documentação

A documentação da API pode ser acessada em `https://explore.postman.com/templates/5048/acaisapi`.


## Instalação e Deploy

Clone o projeto em sua máquina. No terminal acesse o diretório `Acais.API`. Execute o comando `docker-compose up`. Acesse a coleção de requisições disponíveis em `https://explore.postman.com/templates/5048/acaisapi` para testar a API.


## Executando testes

No terminal acesse o diretório `Acais.API.UnitTests`. Execute o comando `dotnet test` para rodar os testes unitários.


## Desenvolvido com

* [.NET Core](https://dotnet.microsoft.com/) - Framework utilizado para desenvolver a API
* [SQL Server](https://www.microsoft.com/sql-server) - Banco de dados utilizado
* [NUnit](https://nunit.org/) - Framework de testes unitários utilizado
* [Moq](https://github.com/Moq/moq4/) - Framework de mocking utilizado
* [Docker](https://www.docker.com/) - Ferramenta utilizada para facilitar o deploy da aplicação