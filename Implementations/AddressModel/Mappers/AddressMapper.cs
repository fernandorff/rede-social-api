using RedeSocial.Implementations.AddressModel.Responses;
using RedeSocial.Models;

namespace RedeSocial.Implementations.AddressModel.Mappers;

public class AddressMapper
{
    public static AddressDto? ToDto(Address? domain)
    {
        if (domain == null)
            return null;

        return new AddressDto()
        {
            Cep = domain.Cep,
            Logradouro = domain.Logradouro,
            Complemento = domain.Complemento,
            Bairro = domain.Bairro,
            Municipio = domain.Municipio,
            Uf = domain.Uf
        };
    }
}