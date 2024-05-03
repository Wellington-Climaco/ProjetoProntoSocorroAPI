# API - Sistema de Pronto Socorro

## 📘 Sobre o Projeto

É uma API para ser utilizada em sistemas de pronto socorros. Onde conseguimos ter o controle do numero de pacientes na fila, chamar os próximos, encaminhar paciente para outro especialista e etc...

O Sistema conta com os seguintes funcionários : recepção, triagem, clínico, ortopedista, enfermaria e um admin. Onde determinados funcionarios tem limitações de acesso à alguns endpoints


## 🧱 Arquitetura

Optei por utilizar a Clean Architecture neste projeto, arquitetura que venho estudando e aplicando os conceitos em projetos pessoais como este.
O projeto conta com a seguinte estrutura:

- **Application**
  
Camada onde é implementada a lógica de negócio da aplicação, é uma camada responsavel por "orquestrar" como os dados são movidos entre as camadas por meio de DTOs, fazendo com que nossa camada de dóminio não seja exposta.

- **Domain**


Camada que guarda o coração da aplicação, como as entidades que são os objetos que carregam as regras de negócio mais importantes dela, a camada de dóminio não tem dependencia nenhuma de outras camadas, não sendo afetadas por mudanças externas na aplicação.

- **Infra.Data**

  
Camada de infraestrutura.Data contém implementações para serviços externos como banco de dados e serviço de emails por exemplo. e também guardam os Repositories, que são responsáveis por persistir as entidades em um banco de dados.

- **Infra.IOC**

  
Camada que tem a responsabilidade de fazer a inversão de controle, onde registramos nossos serviços em um único ponto da aplicação.

- **Api**

  
Camada com as controllers, responsáveis por mostrar nossos endpoints, local onde conseguimos interagir com a aplicação.


## ♻️ Ideia de Fluxo para aplicação

![image](https://github.com/Wellington-Climaco/ProjetoProntoSocorroAPI/assets/142629826/534cbd5b-e001-4397-b0c3-aeb1a9eacde9)



## ⚙️ Tecnologias e Padrões utilizados

- .NET 8
- C#
- ASP.NET
- Autenticação e autorização
- Repository Pattern
- Clean Architecture
- RESTful API
- Banco de dados: SQL Server

## ❓ Como executar o projeto
Para executar a aplicação localmente é necessário:

- Necessário ter o Sql Server instalado, aconselho instalação via Docker, utilizando conteiner do Sql server.

### Inserindo ConnectionString

modifique o valor da chave ```DefaultConnection``` no arquivo ```appsettings.json``` para sua connectionString

```json
   "ConnectionStrings": {
   "DefaultConnection": "string de conexão"
 },
```

### Execução do Migrations

Com os comandos abaixo você irá criar as tabelas do banco de dados.

Execute os seguintes comandos:

```
dotnet tool install --global dotnet-ef
```
```
dotnet ef migrations add CriandoDataBase
```

```
dotnet ef database update
```

### Executar a aplicação

Se for executar via terminal, certifique-se de estar dentro do diretório da API e executar

```
dotnet run
```

Se não abrir coloque :  ```/swagger/index.html``` após seu link do localhost. Exemplo: **localhost:5041/swagger/index.html**

###







