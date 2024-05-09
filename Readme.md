# CloudHotel

#### Sistema para gestão de Reservas em Hotéis e Pousadas

## Requisitos

| Descrição                                 | Restrição                          | Critério de aceite                 |
|-------------------------------------------|------------------------------------|------------------------------------|
| Cadastrar acomodações                     | O nome ter entre 3 e 45 caracteres | Ver a acomodações após salva-la    |
| Inserir reservas                          | Informar nome do hóspede e período | Ver reserva no calendário          |
| Geração de FNRH                           | Incluir dados do hotel no documento| Gerar documento com todos os dados |
| Calendário para visualizar reservas       | Cores diferentes para cada status  | Exibir todos os quartos e reservas |
| Alterar dados da reserva                  | Check-in igual ou sup. a data atual| Salvar com sucesso após alteraçoes |
| Visualizar chegadas e saídas na home      | Tamanho da fonte grande            | Visualizar dados ao abrir o sistema|

## Como executar

* Criar uma variável de ambiente ou user secrets de nome "PostgresConnectionString" contendo uma connection string para PostgreSQL
* Executar a migration com o comando "dotnet ef database update  -p src/Infrastructure""
* Executar "dotnet run --project src/Infrastructure" e "cd src/Presentation && npm run dev" (já tem o launch pronto para o VSCode)