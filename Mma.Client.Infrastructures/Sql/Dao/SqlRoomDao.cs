using System.Data;
using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;
using Serilog;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlRoomDao(IDbConnection connection, SqlRoomMapper mapper, ILogger logger) : IRoomDao
{
    public Room FindById(string roomId)
    {
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM room WHERE id = @id";
            var idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = roomId;
            command.Parameters.Add(idParameter);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                logger.Error("Room not found");
                throw new Exception("Room not found");
            }

            var dto = new SqlRoomDto(
                (string)reader["id"],
                (string)reader["name"],
                (int)reader["capacity"]);
            return mapper.Map(dto);
        }
        catch (Exception e)
        {
            logger.Error(e, "Error while finding room by id");
            throw new InvalidOperationException("Error while finding room by id", e);
        }
    }
}
