# TaskManager
 Sistema de gerenciamento de tarefas (https://meteor-ocelot-f0d.notion.site/NET-C-5281edbec2e4480d98552e5ca0242c5b).
## Pré-requisitos
- Docker e docker-compose
## Executando o Projeto
### Passo 1: Clonar o Repositório
- git clone https://github.com/hig0r/taskmanager.git
- cd taskmanager
### Passo 2: Usar Docker Compose
`docker-compose up --build -d`
### Passo 3: Acessando Aplicação
Acessar no link `http://localhost:1234`.

## Refinamento
- Existe alguma categorização ou filtragem específica que deveríamos implementar para a listagem de projetos (por data de criação, status, prioridade, etc)?
- Existe algum tipo de ordenação padrão desejado para a listagem de tarefas (por data de criação, prioridade, status, etc)?
- A exclusão de tarefas deve ser auditada de alguma forma? Se sim, quais informações devem ser registradas? Ou talvez um soft delete já seria o suficiente?
- Os comentários devem ser editáveis?
- Os comentários podem ser excluídos?
## Possivéis melhorias
- Implementar mais testes (testes de integração / unitários)
- Documentação
- CI/CD
- Cache
- Logs
- Global error handler
- DTOs para o retorno de cada endpoint
