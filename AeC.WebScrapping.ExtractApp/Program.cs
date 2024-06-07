using AeC.WebScrapping.Application.Services;
using AeC.WebScrapping.Domain;

Console.WriteLine("Digite a URL do site que você quer fazer scraping:");
string url = Console.ReadLine();

var fields = new Dictionary<string, string>();
string fieldName, fieldSelector;

do
{
    Console.WriteLine("Digite o nome do campo (ou deixe em branco para terminar):");
    fieldName = Console.ReadLine();
    if (!string.IsNullOrEmpty(fieldName))
    {
        Console.WriteLine($"Digite o seletor índice para o campo {fieldName}:");
        fieldSelector = Console.ReadLine();
        fields.Add(fieldName, fieldSelector);
    }
} while (!string.IsNullOrEmpty(fieldName));

var request = new Scrapping
{
    Url = url,
    Fields = fields
};

var webScrapingService = new ScrappingService();
var webScrapingUseCase = new ScrappingService(webScrapingService);

var results = await webScrapingUseCase.ExecuteAsync(request);

Console.WriteLine("Resultados:");
foreach (var result in results)
{
    Console.WriteLine($"{result.Key}: {result.Value}");
}