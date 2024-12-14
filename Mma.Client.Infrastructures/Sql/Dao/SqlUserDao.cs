using System.Data;
using System.Data.Common;
using Mma.Client.Domains;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;
using Serilog;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlUserDao(DbConnection connection, SqlUserMapper mapper, ILogger logger) : IUserDao
{
    public IList<User> FindAll()
    {
        try
        {
            var users = new List<User>();
            using IDbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM member";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var dto = new SqlUserDto((string) reader["matricule"], (string) reader["fullname"], (string) reader["email"]);
                var user = mapper.Map(dto);
                users.Add(user);
            }

            return users;
        }
        catch (Exception e)
        {
            logger.Error("Error while finding all users");
            throw new InvalidOperationException("Error while finding all users", e);
        }
    }
}
