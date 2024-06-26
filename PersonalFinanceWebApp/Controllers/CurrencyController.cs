using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
namespace PersonalFinanceWebApp.Controllers
{
[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public CurrencyController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("getDolarExchangeRate")]
    public async Task<IActionResult> GetDolarExchangeRate()
    {
        try
        {
            var response = await _httpClient.GetStringAsync("http://localhost:5075/api/currency/usd-to-try");
            var data = Newtonsoft.Json.Linq.JObject.Parse(response);
            Console.WriteLine(data);
            var exchangeRate = data["usD_TO_TRY"];

            if (exchangeRate == null)
            {
                return NotFound("Exchange rate not found in response.");
            }

            return Ok(new { USD_TO_TRY = exchangeRate.Value<decimal>() });
        }
        catch (HttpRequestException e)
        {
            // Log the exception (e.g., using a logging framework)
            return StatusCode(500, "Error fetching data from the other API: " + e.Message);
        }
        catch (Newtonsoft.Json.JsonException e)
        {
            // Log the exception (e.g., using a logging framework)
            return StatusCode(500, "Error parsing JSON response: " + e.Message);
        }
        catch (Exception e)
        {
            // Log the exception (e.g., using a logging framework)
            return StatusCode(500, "An unexpected error occurred: " + e.Message);
        }
    }
}}