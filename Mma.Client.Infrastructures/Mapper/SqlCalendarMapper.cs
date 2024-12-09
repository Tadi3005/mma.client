using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;

namespace Mma.Client.Infrastructures.Mapper;

public class SqlCalendarMapper
{
    public Reservation Map(SqlReservationDto dto) =>
        new(
            dto.Date,
            dto.Date + dto.Start,
            dto.Date + dto.End,
            dto.Summary,
            dto.Description,
            new Room(dto.Room.Id, dto.Room.Name, dto.Room.Capacity),
            new User(dto.User.Matricule, dto.User.FullName, dto.User.Email));
}
