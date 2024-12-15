using System.Data.Common;
using Serilog;

namespace Mma.Client.Infrastructures.Sql;

public sealed class SqlDataStorageFactory : IDataStorageFactory, IDisposable
{
    private readonly string _connexionString;
    private readonly string _provider;
    private DbConnection? _connection;

    private static readonly ILogger Logger = new LoggerConfiguration()
        .WriteTo
        .Console()
        .CreateLogger();

    public SqlDataStorageFactory(string connexionstring, string provider, DbProviderFactory factory)
    {
        DbProviderFactories.RegisterFactory(provider, factory);
        _provider = provider;
        _connexionString = connexionstring;
    }

    public IDataStorage CreateDataStorage()
    {
        try
        {
            _connection?.Dispose();
            _connection = DbProviderFactories.GetFactory(_provider).CreateConnection();

            if (_connection == null)
            {
                throw new InvalidOperationException("Connection is null");
            }

            _connection.ConnectionString = _connexionString;
            _connection.Open();
            return new SqlDataStorage(_connection, Logger);
        }
        catch (Exception e)
        {
            Logger.Error("Error while creating data storage");
            throw new InvalidOperationException("Error while creating data storage", e);
        }
    }

    public void Dispose() => _connection?.Dispose();
}
