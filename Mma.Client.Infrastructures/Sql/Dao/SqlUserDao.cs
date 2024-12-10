using Mma.Client.Domains;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;
using MySql.Data.MySqlClient;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlUserDao(MySqlConnection connection, SqlUserMapper sqlUserMapper) : IUserDao
{
    public IList<User> FindAll()
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM member";

        using var reader = command.ExecuteReader();
        var users = new List<User>();

        while (reader.Read())
        {
            var dto = new SqlUserDto(
                reader.GetString("matricule"),
                reader.GetString("fullname"),
                reader.GetString("email"));
            users.Add(sqlUserMapper.Map(dto));
        }

        return users;
    }
}
