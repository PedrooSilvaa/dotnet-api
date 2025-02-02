# dotnet-api

API desenvolvida com **C#**, **.NET** e **Entity Framework**. Esta API utiliza **JWT Bearer Authentication**, **MySQL** como banco de dados e implementa políticas de autorização baseadas em claims, permitindo um controle de acesso robusto.

## 🚀 Funcionalidades

- **Autenticação JWT**: A API usa JWT Bearer para autenticação, validando tokens em cada requisição.
- **Autorização Baseada em Claims**: As requisições podem ser filtradas com base em claims, como "EmployeeCode" e "Cpf".
- **CRUD Básico com Entity Framework**: Implementação de operações CRUD básicas utilizando **Entity Framework** para persistência de dados.
- **Política de Senha Personalizada**: A configuração de identidade foi ajustada para aceitar senhas simples com poucos requisitos.
- **Swagger**: Utiliza Swagger para a documentação e testes da API em ambiente de desenvolvimento.

## 🚀 Tecnologias Utilizadas

- **C#**: Linguagem de programação principal.
- **.NET 6+**: Framework de desenvolvimento para criar APIs RESTful.
- **Entity Framework Core**: ORM para interação com o banco de dados.
- **MySQL**: Banco de dados utilizado para persistir as informações.
- **JWT**: Autenticação baseada em tokens JWT para proteger as rotas da API.
- **Swagger**: Ferramenta para documentação e testes da API.

## 🛠️ Como Executar o Projeto

Para executar o projeto localmente, siga os passos abaixo:

1. Clone o repositório:

```bash
git clone https://github.com/SeuUsuario/dotnet-api.git
