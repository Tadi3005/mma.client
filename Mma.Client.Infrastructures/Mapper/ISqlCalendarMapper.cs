using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;

namespace Mma.Client.Infrastructures.Mapper;

public interface ISqlCalendarMapper
{
    Reservation Map(SqlReservationDto dto);

    Service MapService(SqlServiceDto dto);
}
