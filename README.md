# Covid19Worker
Serviço que consome informações brasileiras relacionadas a Covid-19 de APIs públicas,
e sempre que há atualizações, consolida em arquivo xlsx, salva uma cópia no computador (Windows) e envia outras para e-mails cadastrados.

## Set Up
- Database: O banco de dados está configurado em arquivo, junto com a solução. Não é necessário realizar nenhuma ação;

- Tempo: No arquivo Json, é possível configurar o intervalo de tempo (minutos) em que o serviço atualiza as informações;

- É necessário adicionar no arquivo Json as configurações para envio de email:
  -> E-mail do remetente;
  -> Senha do remetente;
  -> Host do serviço (exemplo: hotmail - "smtp.live.com", gmail - "smtp.gmail.com")
  
- É necessário cadastrar no banco de dados os e-mails destinatários.
