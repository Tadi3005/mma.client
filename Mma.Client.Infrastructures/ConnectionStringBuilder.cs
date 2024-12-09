namespace Mma.Client.Infrastructures;

public class ConnectionStringBuilder
{
    public string ConnectionString { get; }

    public ConnectionStringBuilder(string connectionString)
    {
        ConnectionString = connectionString.Replace(" ", string.Empty);
    }

    private string[] NormalizedConnectionParts => ConnectionString.Split(';');

    public string DbServer
    {
        get
        {
            var mainPart = NormalizedConnectionParts.FirstOrDefault(p => p.Contains('@'));
            return mainPart?.Split('@')[1].Split(':')[0] ?? string.Empty;
        }
    }

    public string DbPort
    {
        get
        {
            var mainPart = NormalizedConnectionParts.FirstOrDefault(p => p.Contains('@'));
            return mainPart?.Split(':').LastOrDefault() ?? string.Empty;
        }
    }

    public string DbUser
    {
        get
        {
            var userPart = NormalizedConnectionParts.FirstOrDefault(p => p.StartsWith("user="));
            return userPart?.Split('=')[1] ?? string.Empty;
        }
    }

    public string DbPassword
    {
        get
        {
            var passwordPart = NormalizedConnectionParts.FirstOrDefault(p => p.StartsWith("pwd="));
            return passwordPart?.Split('=')[1] ?? string.Empty;
        }
    }

    public string DbDataBase
    {
        get
        {
            var mainPart = NormalizedConnectionParts.FirstOrDefault(p => p.Contains('@'));
            return mainPart?.Split('@')[0] ?? string.Empty;
        }
    }
}
