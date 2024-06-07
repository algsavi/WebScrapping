using AeC.WebScrapping.Domain;
using AeC.WebScrapping.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Dynamic;
using System.Net.Http.Json;
using System.Reflection.Metadata;

namespace AeC.WebScrapping.Infra.WebExtractor;

public class WebExtractor : IWebExtractor
{
    private readonly IWebDriver _webDriver;
    public WebExtractor(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public async Task<List<dynamic>> ScrapeAsync(string json)
    {
        List<dynamic> extractedItems = new List<dynamic>();

        try
        {
            JObject jObject = JObject.Parse(json);

            string url = jObject["url"].ToString();

            _webDriver.Navigate().GoToUrl(url);

            foreach (var action in jObject["actions"])
            {
                string actionType = action["action"].ToString();

                try
                {
                    switch (actionType)
                    {
                        case "type":
                            var typeElement = _webDriver.FindElement(By.XPath(action["selector"].ToString()));
                            typeElement.SendKeys(action["text"].ToString());
                            break;
                        case "click":
                            var clickElement = _webDriver.FindElement(By.XPath(action["selector"].ToString()));
                            clickElement.Click();
                            break;
                        case "wait":
                            int waitTime = (int)action["seconds"];

                            Thread.Sleep(waitTime * 1000);

                            break;
                        case "extract":
                            var elements = _webDriver.FindElements(By.XPath(action["selector"].ToString()));

                            foreach (var element in elements)
                            {
                                dynamic extractedItem = new ExpandoObject();

                                var properties = action["properties"].ToObject<JObject>();
                                
                                foreach (var property in properties)
                                {
                                    var subElement = element.FindElement(By.XPath(property.Value.ToString()));

                                    if (property.Key == "url")
                                        ((IDictionary<string, object>)extractedItem).Add(property.Key, subElement.GetAttribute("href"));
                                    else
                                        ((IDictionary<string, object>)extractedItem).Add(property.Key, subElement.Text);

                                    extractedItems.Add(extractedItem);
                                }
                            }
                            break;
                        case "navigateAndExtract":
                            string filter = action["filter"]?.ToString();

                            var searchResults = _webDriver.FindElements(By.XPath(action["selector"].ToString()));

                            foreach (var result in searchResults)
                            {
                                var linkElement = result.FindElement(By.XPath(action["linkSelector"].ToString()));
                                var linkURL = linkElement.GetAttribute("href");
                                var linkTitle = linkElement.Text;

                                if (string.IsNullOrEmpty(filter) || linkTitle.StartsWith(filter))
                                {
                                    _webDriver.Navigate().GoToUrl(linkURL);

                                    dynamic extractedItem = new ExpandoObject();

                                    foreach (var property in action["properties"].ToObject<JObject>())
                                    {
                                        var element = _webDriver.FindElement(By.XPath(property.Value.ToString()));

                                        ((IDictionary<string, object>)extractedItem).Add(property.Key, element.Text);

                                        extractedItems.Add(extractedItem);
                                    }

                                    _webDriver.Navigate().Back();
                                }
                            }

                            break;
                        default:
                            throw new NotSupportedException($"Action '{actionType}' is not supported.");
                    }
                }
                catch (StaleElementReferenceException)
                {
                    continue;
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
                catch(JavaScriptException)
                {
                    continue;
                }
                catch(Exception)
                {
                    continue;
                }
            }
        }
        catch (JsonReaderException)
        {
            throw new Exception($"Invalid JSON");
        }
        catch(WebDriverException)
        {
            throw new Exception($"Disconnected");
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return extractedItems;
    }
}
