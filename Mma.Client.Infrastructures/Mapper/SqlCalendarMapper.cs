using Mma.Client.Domains;
using Mma.Client.Infrastructures.Dto.Sql;

namespace Mma.Client.Infrastructures.Mapper;

public class SqlCalendarMapper : ISqlCalendarMapper
{
    public Reservation Map(SqlReservationDto dto)
    {
        if (dto is not { Date: not null, Start: not null, End: not null })
        {
            throw new ArgumentException("Date, Start and End must not be null");
        }

        var date = DateTime.Parse(dto.Date);
        var startWithoutDate = DateTime.Parse(dto.Start);
        var endWithourDate = DateTime.Parse(dto.End);
        var start = date.AddHours(startWithoutDate.Hour).AddMinutes(startWithoutDate.Minute);
        var end = date.AddHours(endWithourDate.Hour).AddMinutes(endWithourDate.Minute);
        return new Reservation(
            date,
            start,
            end,
            dto.Summary,
            dto.Description,
            new Room(dto.Room.Id, dto.Room.Name, dto.Room.Capacity),
            new User(dto.User.Matricule, dto.User.FullName, dto.User.Email));
    }

    public Service MapService(SqlServiceDto dto) => new(dto.Id, dto.Name);
}
