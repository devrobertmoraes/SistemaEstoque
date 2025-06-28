using SistemaEstoque.Core.Enums;

namespace SistemaEstoque.Core.Entities;

/// <summary>
/// Representa um usuário do sistema.
/// </summary>

public class Usuario
{
    /// <summary>
    /// Identificador único do usuário
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Nome completo do usuário
    /// </summary>
    public string NomeCompleto { get; private set; }

    /// <summary>
    /// Nome de usuario para login.
    /// </summary>
    public string NomeDeUsuario { get; private set; }

    /// <summary>
    /// Hash da senha do usuário
    /// </summary>
    public string SenhaHash { get; private set; }

    /// <summary>
    /// Cargo do usuário no sistema (Admin, Operador, etc...)
    /// </summary>
    public CargoEnum Cargo { get; private set; }

    /// <summary>
    /// Propriedade de navegação: Um usuário pode ter feito várias movimentações
    /// </summary>
    public List<MovimentacaoEstoque> MovimentacoesEstoque { get; private set; }

    /// <summary>
    /// Construtor da classe Usuario
    /// </summary>
    /// <param name="nomeCompleto">Nome completo do usuário</param>
    /// <param name="nomeDeUsuario">Nome de usuário para Login</param>
    /// <param name="senhaHash">Hash da senha do usuário</param>
    /// <param name="cargo">Cargo do usuário no sistema</param>
    public Usuario(string nomeCompleto, string nomeDeUsuario, string senhaHash, CargoEnum cargo)
    {
        Id = Guid.NewGuid();
        NomeCompleto = nomeCompleto;
        NomeDeUsuario = nomeDeUsuario;
        SenhaHash = senhaHash;
        Cargo = cargo;
        MovimentacoesEstoque = new List<MovimentacaoEstoque>();
    }
}