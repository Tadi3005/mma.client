using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;

namespace Mma.Client.Infrastructures.Mapper;

public class SqlCalendarMapper
{
    public Reservation Map(SqlReservationDto dto)
    {
        var date = DateTime.Parse(dto.Date);
        var start = TimeSpan.Parse(dto.Start);
        var end = TimeSpan.Parse(dto.End);
        return new Reservation(
            date,
            date + start,
            date + end,
            dto.Summary,
            dto.Description,
            new Room(dto.Room.Id, dto.Room.Name, dto.Room.Capacity),
            new User(dto.User.Matricule, dto.User.FullName, dto.User.Email));
    }
}
