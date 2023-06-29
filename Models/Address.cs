namespace RedeSocial.Models;

public class Address
{
    public Address(string cep, string logradouro, string complemento, string bairro, string municipio, string uf)
    {
        Cep = cep;
        Logradouro = logradouro;
        Complemento = complemento;
        Bairro = bairro;
        Municipio = municipio;
        Uf = uf;
    }

    public string Cep { get; private set; }
    public string Logradouro { get; private set; }
    public string Complemento { get; private set; }
    public string Bairro { get; private set; }
    public string Municipio { get; private set; }
    public string Uf { get; private set; }
}