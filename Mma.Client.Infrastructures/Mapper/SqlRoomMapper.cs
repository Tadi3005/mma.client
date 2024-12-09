using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;

namespace Mma.Client.Infrastructures.Mapper;

public class SqlRoomMapper
{
    public Room Map(SqlRoomDto dto) => new (dto.Id, dto.Name, dto.Capacity);
}
