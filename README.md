AI Teacher Workspace
SaaS para professores no qual a inteligência artificial está ligada aos dados e aos fluxos de trabalho de quem ensina — e não é um chatbot genérico. O projeto é um frontend completo, desenvolvido com React e TypeScript, com camada de services preparada para consumir uma API REST em .NET no futuro.

Cada sugestão da IA parte do que a professora já tem: planejamentos, materiais, banco de questões, avaliações e diário de aula. E cita de onde veio.

Tecnologias
C#
.NET 9
ASP.NET Core Web API
Entity Framework Core
PostgreSQL
JWT (JSON Web Token)
Apache Kafka
Swagger / OpenAPI
xUnit
Moq
Docker

Funcionalidades
Dashboard
Abre com o dia da professora:

Aulas de hoje, turmas ativas, média geral e avaliações abertas
Próximas aulas
Sugestões da IA, cada uma com a fonte (diário ou banco de questões)
Andamento dos planejamentos da semana
Materiais recentes

Meu Planejamento
Calendário de aulas por data, com navegação por semana:
Cada aula traz conteúdo, objetivo, duração, turma, status, atividades numeradas e materiais vinculados
Adicionar, editar, mover de data e excluir aulas
Botão de gerar plano com IA
Sugestão de atividades de revisão

Disciplinas
Conteúdos programáticos com progresso:
Lista de turmas ligadas, quantidade de conteúdos e percentual concluído
Cada tópico marcado como dado ou pendente
Criação de novas disciplinas com nome, descrição e conteúdos

Turmas
Um cartão por turma:
Média, frequência e andamento do programa
Horário fixo e próxima aula
Uma turma pode ter mais de uma disciplina
Criação de novas turmas com nome, disciplinas, número de alunos e horário

Materiais
Biblioteca de PDFs, DOCX, PPTX, imagens, links e textos:
Envio de arquivos e registro de links ou textos
Busca por texto e filtro por tipo
Busca por assunto com IA

Banco de Questões
Questões reutilizáveis com filtros:
Filtros por disciplina, assunto, dificuldade e tipo (múltipla escolha, discursiva, verdadeiro/falso e código)
Criação e exclusão de questões
Contagem de quantas vezes cada questão já foi usada
Geração de questões com IA

Diário de Aula
Registro contínuo do que foi dado:
Dificuldades observadas e observações da professora
Filtro por turma e por data
Cada entrada vira contexto para as próximas sugestões da IA

Assistente IA
Conversa contextual com o trabalho da professora:
Mostra o contexto ativo (turmas, planejamentos, materiais, questões, avaliações e registros de diário)
Responde com fontes citadas
Exemplos: gerar plano de aula, gerar questões, analisar uma avaliação, sugerir a próxima aula, encontrar materiais por assunto e sugerir atividades de revisão

Configurações
Perfil da professora 
Resumo semanal
