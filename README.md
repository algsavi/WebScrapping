--Para a utilização do sistema, precisa primeiramente rodar a migration.
A connection string está no "appsettings.json" do projeto Web

Para rodar a migration usar como default o projeto Infra:
update-database

No projeto AeC.WebScrapping.IoC, caso deseje que o chrome não seja aberto, remover o comentário da linha 33 do arquivo "DependencyInjection.cs"

Para testar o projeto, pode criar o JSON conforme desejar (não consegui testar inumeros sites, mas criei da forma mais dinâmica possível, dentro do tempo que tive para realizar o teste, que foi de 2 dias conciliando com meu trabalho atual)

Exemplo de JSON da Alura (foco principal do teste):

{
  "url": "https://www.alura.com.br",
  "actions": [
    {
      "action": "type",
      "selector": "/html/body/main/section[1]/header/div/nav/div[2]/form/input",
      "text": "rpa"
    },
    {
      "action": "click",
      "selector": "/html/body/main/section[1]/header/div/nav/div[2]/form/button"
    },
	{
		"action": "wait",
		"seconds": 60
	},
	{
      "action": "navigateAndExtract",
	  "selector": "//li[@class='busca-resultado']",
	  "linkSelector": ".//a[@class='busca-resultado-link']",
	  "filter": "Formação",
      "properties": {
        "title": "/html/body/main/section[1]/article/div/div/div[2]/h1",
        "teacher": "//*[@id='instrutores']/div/ul/li[2]/div/h3",
        "duration": "//div[@class='formacao__info-conclusao']//div[@class='formacao__info-destaque']"
		}
	}
  ]
}

Aqui na ALURA eu coloquei um wait de 1 minuto porque o site deles é muito lento.

Exemplo de JSON para Amazon (o caso da Amazon é um pouco mais complicado, pois realizam bloqueios recorrentes pra evitar extração, mas nas primeiras tentativas irá funcionar):

{
  "url": "https://www.amazon.com.br",
  "actions": [
    {
      "action": "type",
      "selector": "//*[@id="twotabsearchtextbox']",
      "text": "echo dot"
    },
    {
      "action": "click",
      "selector": "//*[@id='nav-search-submit-button']"
    },
    {
      "action": "extract",
	  "selector": "//div[contains(@class, 's-result-item')1]",
      "properties": {
        "description": "//span[contains(@class, 'a-size-base-plus') and contains(@class, 'a-color-base') and contains(@class, 'a-text-normal')]",
        "price": "//span[@id='priceblock_ourprice']"
		}
	}
  ]
}

Exemplo de JSON para Google:

{
  "url": "https://www.google.com.br",
  "actions": [
    {
      "action": "type",
      "selector": "//*[@id='APjFqb']",
      "text": "AeC Relacionamento"
    },
	{
		"action": "wait",
		"seconds": 2
	},
    {
      "action": "click",
      "selector": "//input[@name='btnK']"
    },
	{
		"action": "wait",
		"seconds": 2
	},
    {
      "action": "extract",
	  "selector": "//div[@class='g']//h3",
      "properties": {
        "title": "//h3",
        "url": "//div[@class='g']//a[@href]"
		}
	}
  ]
}

O inglês foi usado como linguagem padrão pois se trata de um projeto com possibilidade de escala global.
O resultado é armazenado no banco de dados em formato JSON, como foi criado um projeto dinâmico, foi a melhor decisão de arquitetura,
pois assim temos flexibilidade na hora de obter os dados. Para visualizar em HTML ou relatório PDF (ou para futuras exportações) fica bem fácil e flexivel também.

Tentei simular o GitFlow, mesmo sendo mais complicado por estar começando tudo do zero e ser apenas um desenvolvedor, mas criei comentário de bug usando uma branch hotfix e depois criei um comentário de feature...Simulando a feature indo pra develop e depois o merge com a master.

O projeto foi criado de uma forma que possa ser separado os serviços para que assim possa ser realizado builds independentes, a arquitetura usada foi a Clean Architecture.

Melhorias futuras:
	- Para escalar um produto assim, precisaria adicionar um serviço assincrone do WebExtractor, deixando ele rodar em plano de fundo. Poderia usar tanto o Quartz quanto um serviço de messageria como o RabbitMQ (pensando em escala, seria a melhor opção).
	- Melhorias na usabilidade da interface web, com retornos de erro
	- Melhorias no fluxo de funcionamento do WebExtractor, para que assim seja possível realizar logs e tratamentos mais adequados
 	- Gerenciamento de acessos (tanto na parte de acesso ao sistema de extração, quanto na parte de logins de acesso caso o site que deve ser lido, tenha login)
  	- Controle de paginações de resultados
   - Flexibilidade de uso de seletores (atualmente está apenas com xpath)
   	- ... Essas foram as que pensei no momento...
 
Qualquer dúvida estou a disposição.
