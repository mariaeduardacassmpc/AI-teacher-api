# AI Teacher Workspace

API REST para uma plataforma SaaS de organização pedagógica que utiliza inteligência artificial para auxiliar professores no planejamento de aulas, na criação de questões e avaliações e na organização de materiais didáticos.

O diferencial do projeto é a construção de um assistente de IA contextualizado com os dados e fluxos de trabalho da professora. Em vez de funcionar como um chatbot genérico, a proposta é utilizar informações de turmas, planejamentos, materiais, banco de questões e diário de aula para gerar sugestões relevantes, com referências às fontes utilizadas.

O backend está sendo desenvolvido com C# e .NET 9, seguindo uma arquitetura organizada em camadas, com foco em separação de responsabilidades, regras de negócio, integração com serviços externos e testabilidade.

## Tecnologias

- **C# / .NET 9** — desenvolvimento da API.
- **ASP.NET Core Web API** — construção dos endpoints REST.
- **Entity Framework Core** — mapeamento objeto-relacional e persistência de dados.
- **PostgreSQL** — banco de dados relacional.
- **JWT** — autenticação e autorização.
- **Apache Kafka** — processamento assíncrono e comunicação orientada a eventos.
- **Swagger / OpenAPI** — documentação e exploração dos endpoints.
- **xUnit** — testes automatizados.
- **Moq** — simulação de dependências nos testes unitários.
- **Docker** — conteinerização e padronização do ambiente de execução.

## Funcionalidades da API

### Gestão de planejamento de aulas
- Cadastro, consulta, edição, reagendamento e exclusão de planejamentos.
- Organização das aulas por data, turma e disciplina.
- Registro de objetivos, duração, conteúdos, atividades e materiais vinculados.
- Preparação para geração de planos de aula com IA.

### Gestão de disciplinas e conteúdos
- Cadastro de disciplinas e conteúdos programáticos.
- Associação de disciplinas às turmas.
- Acompanhamento do progresso dos conteúdos ministrados.
- Controle de tópicos concluídos e pendentes.

### Gestão de turmas
- Cadastro e manutenção de turmas.
- Associação de múltiplas disciplinas a uma turma.
- Registro de quantidade de alunos e horários.
- Organização de informações para acompanhamento pedagógico.

### Biblioteca de materiais
- Organização de materiais didáticos, incluindo documentos, apresentações, imagens, links e textos.
- Busca e filtragem por tipo e conteúdo.
- Preparação para recuperação de informações relevantes para a IA.

### Banco de questões
- Cadastro e consulta de questões reutilizáveis.
- Filtros por disciplina, assunto, dificuldade e tipo.
- Suporte a questões objetivas, discursivas, verdadeiro ou falso e programação.
- Registro da quantidade de utilizações de cada questão.
- Preparação para geração de questões por IA.

### Diário de aula
- Registro dos conteúdos ministrados.
- Armazenamento de observações e dificuldades identificadas.
- Consulta por turma e período.
- Utilização dos registros como contexto para recomendações pedagógicas futuras.

### Assistente de inteligência artificial
- Integração planejada com um provedor de IA.
- Recuperação de contexto a partir dos dados pedagógicos.
- Geração de planos de aula, questões e atividades de revisão.
- Sugestões para aulas futuras e análise de avaliações.
- Respostas com referências às fontes utilizadas, permitindo rastrear a origem das informações.

### Dashboard e acompanhamento
- Endpoints para consolidação das aulas do dia e das próximas aulas.
- Indicadores de turmas, desempenho e progresso dos planejamentos.
- Resumo semanal das atividades pedagógicas.
- Consulta de sugestões geradas a partir do contexto disponível.
