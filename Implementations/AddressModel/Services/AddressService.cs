using RedeSocial.Contracts.Repositories;
using RedeSocial.Implementations.AddressModel.Mappers;
using RedeSocial.Implementations.AddressModel.Responses;
using RedeSocial.Implementations.AddressModel.Services.Interfaces;

namespace RedeSocial.Implementations.AddressModel.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<AddressDto?> ConsultCep(string cep)
    {
        var addressModel = await _addressRepository.ConsultCep(cep);

        return AddressMapper.ToDto(addressModel);
    }
}