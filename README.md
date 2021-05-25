# APIDesafio

API desenvolvida com o intuito de demonstrar o conhecimento adquirido em .Net Core 3.1<br/> 

Foram utilizadas as tecnologias como JWT para login e acesso dos métodos da API, swagger para documentação, EntityFrameworkCore para conexão com o banco Postgresql e FluentValidation para validação customizada.


## Segue fluxo para execução utilizando o Git Bash e estando na pasta do projeto:<br/> 
(antes de executar os comandos é importante apontar corretamente a DataBase no arquivo appsetings.json)<br/> 
dotnet clean<br/> 
dotnet build<br/> 
dotnet ef migrations add Initial<br/> 
dotnet ef database update<br/> 
dotnet run<br/> 
