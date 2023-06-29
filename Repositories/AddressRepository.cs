using RedeSocial.Contracts.Repositories;
using RedeSocial.Implementations.AddressModel.Responses;
using RedeSocial.Models;
using System.Text.Json;

namespace RedeSocial.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly HttpClient _httpClient;

    public AddressRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Address?> ConsultCep(string cep)
    {
        var requestUri = $"/ws/{cep}/json";

        try
        {
            using var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            var cepResponse = JsonSerializer.Deserialize<AddressDto>(content);

            return ToDomain(cepResponse);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro na consulta de CEPs: {e}");
        }

        return null;
    }

    private static Address? ToDomain(AddressDto? response)
    {
        if (response == null)
            return null;

        return new Address(response.Cep, response.Logradouro, response.Complemento, response.Bairro,
            response.Localidade, response.Uf);
    }
}