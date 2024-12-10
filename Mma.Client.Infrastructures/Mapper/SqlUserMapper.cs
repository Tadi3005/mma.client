using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;

namespace Mma.Client.Infrastructures.Mapper;

public class SqlUserMapper
{
    public User Map(SqlUserDto dto) => new(dto.Matricule, dto.FullName, dto.Email);
}
