using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;

namespace Mma.Client.Infrastructures.Mapper;

public class SqlServiceMapper
{
    public Service Map(SqlServiceDto dto) => new(dto.Id, dto.Name);
}
