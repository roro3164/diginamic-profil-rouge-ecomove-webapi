using System.Text;
using Ecomove.Api.DTOs.AdressDTOs;

public class OpenStreetMapHttpRequest
{
    private const string _baseUrl = "https://nominatim.openstreetmap.org/search";
    private readonly IHttpClientFactory _httpClientFactory;

    public OpenStreetMapHttpRequest(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<HttpResponseMessage?> GetAdress(CarpoolAddressDTO carpoolAddressDTO)
    {
        string Number = carpoolAddressDTO.Number;
        string City = carpoolAddressDTO.City;
        string Country = carpoolAddressDTO.Country;
        string Street = carpoolAddressDTO.Street;
        string PostalCode = carpoolAddressDTO.PostalCode;

        HttpClient? client = _httpClientFactory.CreateClient();

        Dictionary<string, string> queryParams = new Dictionary<string, string> {
                    {"street", $"{Number} {Street}"},
                    {"city", City},
                    {"country",Country},
                    {"postalcode",PostalCode},
                    {"format","jsonv2"}
                };

        StringBuilder urlBuilder = new StringBuilder(_baseUrl);

        urlBuilder.Append("?");

        foreach (var (key, value) in queryParams)
        {
            urlBuilder.Append($"{key}={value}&");
        }

        urlBuilder.Remove(urlBuilder.Length - 1, 1);

        string url = urlBuilder.ToString();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

        request.Headers.Add("User-Agent", ".NET Client");

        HttpResponseMessage response = await client.SendAsync(request);

        return response.IsSuccessStatusCode ? response : null;
    }

}