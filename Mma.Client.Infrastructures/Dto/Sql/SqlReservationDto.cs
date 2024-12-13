namespace Mma.Client.Infrastructures.Dto.Sql;

public record SqlReservationDto(int Id, string? Date, string? Start, string? End, string Summary, string Description, SqlRoomDto Room, SqlUserDto User);
