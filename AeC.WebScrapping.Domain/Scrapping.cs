namespace AeC.WebScrapping.Domain;

public class Scrapping
{
    public int Id { get; private set; }
    public string Url { get; private set; }
    public string Properties { get; private set; }

    public Scrapping(string url, string properties)
    {
        if (!IsValidScrapping(url, properties))
        {
            throw new ArgumentException("Invalid data to create a new web scrapping!")
        }

        Url = url;
        Properties = properties;
    }

    private bool IsValidScrapping(string url, string properties)
    {
        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(properties))
        {
            return false;
        }

        return true;
    }
}
