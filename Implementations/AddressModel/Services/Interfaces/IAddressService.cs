using RedeSocial.Implementations.AddressModel.Responses;

namespace RedeSocial.Implementations.AddressModel.Services.Interfaces;

public interface IAddressService
{
    Task<AddressDto?> ConsultCep(string cep);
}