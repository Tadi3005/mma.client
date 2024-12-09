using Mma.Client.Domains;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;
using MySql.Data.MySqlClient;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlCalendarDao(MySqlConnection connection, SqlCalendarMapper mapper) : ICalendarDao
{
    public IList<Reservation> FindByRoomIdAndDate(DateTime date, string roomId)
    {
        var reservations = new List<Reservation>();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM reservation JOIN room ON reservation.id_room = room.id JOIN member ON reservation.matricule_user = member.matricule WHERE reservation.id_room = @roomId AND reservation.date = @date";
        command.Parameters.AddWithValue("@roomId", roomId);
        command.Parameters.AddWithValue("@date", date.Date);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var dto = new SqlReservationDto(
                reader.GetInt32("id_reservation"),
                reader.GetDateTime("date"),
                reader.GetTimeSpan("time_start"),
                reader.GetTimeSpan("time_end"),
                reader.GetString("summary"),
                reader.GetString("description"),
                new SqlRoomDto(reader.GetString("id"), reader.GetString("name"), reader.GetInt32("capacity")),
                new SqlUserDto(reader.GetString("matricule"), reader.GetString("fullname"), reader.GetString("email")));
            var reservation = mapper.Map(dto);
            reservations.Add(reservation);
        }

        return reservations;
    }
}
