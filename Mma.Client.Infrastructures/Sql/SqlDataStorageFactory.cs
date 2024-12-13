using System.Data.Common;
using Mma.Client.Domains.Data;

namespace Mma.Client.Infrastructures.Sql;

public sealed class SqlDataStorageFactory : IDataStorageFactory, IDisposable
{
    private readonly string _connexionString;
    private readonly string _provider;
    private DbConnection? _connection;

    // Ajouter l'instance du provider
    public SqlDataStorageFactory(string connexionstring, string provider, DbProviderFactory factory)
    {
        DbProviderFactories.RegisterFactory(provider, factory);
        _provider = provider;
        _connexionString = connexionstring;
    }

    public IDataStorage CreateDataStorage()
    {
        _connection?.Dispose();
        _connection = DbProviderFactories.GetFactory(_provider).CreateConnection();

        if (_connection == null)
        {
            throw new InvalidOperationException("Connection is null");
        }

        _connection.ConnectionString = _connexionString;
        _connection.Open();
        return new SqlDataStorage(_connection);
    }

    public void Dispose() => _connection?.Dispose();
}
