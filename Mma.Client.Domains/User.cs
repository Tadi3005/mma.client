namespace Mma.Client.Domains;

public record User(string Matricule, string FullName, string Email)
{
    public static User Empty => new(string.Empty, string.Empty, string.Empty);
}
