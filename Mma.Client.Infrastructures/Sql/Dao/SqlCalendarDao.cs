using System.Data.Common;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlCalendarDao(DbConnection connection, SqlCalendarMapper mapper) : ICalendarDao
{
    public IList<Reservation> FindByRoomIdAndDate(DateTime date, string roomId)
    {
        IList<Reservation> reservations = new List<Reservation>();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM reservation JOIN room ON reservation.id_room = room.id JOIN member ON reservation.matricule_user = member.matricule WHERE id_room = @roomId AND date = @date";
        var idParameter = command.CreateParameter();
        idParameter.ParameterName = "@roomId";
        idParameter.Value = roomId;
        command.Parameters.Add(idParameter);
        var dateParameter = command.CreateParameter();
        dateParameter.ParameterName = "@date";
        dateParameter.Value = date.ToString("yyyy-MM-dd");
        command.Parameters.Add(dateParameter);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var dto = new SqlReservationDto((int)reader["id_reservation"], reader.GetValue(reader.GetOrdinal("date")).ToString(),
                reader.GetValue(reader.GetOrdinal("time_start")).ToString(), reader.GetValue(reader.GetOrdinal("time_end")).ToString(), (string)reader["summary"], (string)reader["description"],
                new SqlRoomDto((string)reader["id"], (string)reader["name"], (int)reader["capacity"]),
                new SqlUserDto((string)reader["matricule"], (string)reader["fullname"], (string)reader["email"]));
            var reservation = mapper.Map(dto);
            reservations.Add(reservation);
        }

        return reservations;
    }

    public void Add(ReservationRequest request) => throw new NotImplementedException();
}
