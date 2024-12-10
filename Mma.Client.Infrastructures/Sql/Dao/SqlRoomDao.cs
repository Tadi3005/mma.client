using System.Data;
using System.Data.Common;
using Mma.Client.Domains;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlRoomDao(DbConnection connection, SqlRoomMapper mapper) : IRoomDao
{
    public Room FindById(string roomId)
    {
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM room WHERE id = @id";
        var idParameter = command.CreateParameter();
        idParameter.ParameterName = "@id";
        idParameter.Value = roomId;
        command.Parameters.Add(idParameter);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            var dto = new SqlRoomDto(
                (string)reader["id"],
                (string)reader["name"],
                (int)reader["capacity"]);
            return mapper.Map(dto);
        }
        else
        {
            throw new Exception("Room not found");
        }
    }
}
