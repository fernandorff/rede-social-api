using RedeSocial.Models;

namespace RedeSocial.Contracts.Repositories;

public interface IAddressRepository
{
    Task<Address?> ConsultCep(string cep);
}