using Mma.Client.Domains;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;
using MySql.Data.MySqlClient;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlRoomDao(MySqlConnection connection, SqlRoomMapper mapper) : IRoomDao
{
    public Room FindById(string roomId)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM room WHERE id = @id";
        command.Parameters.AddWithValue("@id", roomId);

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            throw new InvalidOperationException("Room not found.");
        }

        var dto = new SqlRoomDto(
            reader.GetString("id"),
            reader.GetString("name"),
            reader.GetInt32("capacity"));
        return mapper.Map(dto);
    }
}
