# REST API

 
🚩 O  objetivo desse projeto é a construção de uma API implementando o padrão REST (ao menos até o [nível 2 de maturidade](https://martinfowler.com/articles/richardsonMaturityModel.html), onde são especificados verbos HTTP para operações em cada recurso e coleção).

📚 O projeto está estruturado de acordo com as [boas práticas de design da API Web RESTful específicadas pela microsoft](https://docs.microsoft.com/pt-br/azure/architecture/best-practices/api-design) - acessado em 📅 23/06/2022.

### Descrição do projeto
Essa API representa um projeto CRUD para cadastro de funcionários (diferenciados pelo contrato CLT e PJ).

Alguns conceitos e padrões foram requisitados na proposta do projeto:

▶ Clean Code
▶ SOLID
▶ POO - Herança, Polimorfismo e Encapsulamento
▶ Design Pattern - Template Method

### Para rodar o projeto
É necessário estar na pasta raíz da API (\REST-API) e primeiramente preparar o banco de dados com o comando:
```
dotnet ef database update
```
Em seguida rodar a api com:
```
dotnet run
```

### Endpoints
Documentação da API com Swagger:
> [http://localhost:5229/swagger/index.html](http://localhost:5229/swagger/index.html)

Endpoints para requisições HTTP:
> [http://localhost:5229/api/v1/colaboradores](http://localhost:5229/api/v1/colaboradores) - GET / POST
> [http://localhost:5229/api/v1/colaboradores/id](http://localhost:5229/api/v1/colaboradores/id) - GET / PUT / DELETE
