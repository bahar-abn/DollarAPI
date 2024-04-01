using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


[Route("api/[controller]")]
[ApiController]
public class dollarController : ControllerBase
{
    private static readonly HttpClient client = new HttpClient();
    private const string ApiUrl = "https://api.exchangerate-api.com/usd";

    
    [HttpGet] 
    public async Task<IActionResult> GetDollarPrice()
    {
        
            HttpResponseMessage response = await client.GetAsync(ApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                
                dynamic jsonData = JsonConvert.DeserializeObject(responseBody);
                decimal dollarPrice = jsonData.rate;

                return Ok($"Current dollar price: {dollarPrice}");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to retrieve dollar price from the external API.");
            }
        }
       
          
    }

