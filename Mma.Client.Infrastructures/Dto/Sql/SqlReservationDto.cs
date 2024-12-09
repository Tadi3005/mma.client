namespace Mma.Client.Infrastructures.Dto.Sql;

public record SqlReservationDto(int Id, DateTime Date, TimeSpan Start, TimeSpan End, string Summary, string Description, SqlRoomDto Room, SqlUserDto User);
