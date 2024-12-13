using System.Data.Common;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlCalendarDao(DbConnection connection, SqlCalendarMapper calendarMapper) : ICalendarDao
{
    public IList<Reservation> FindByRoomIdAndDate(DateTime date, string roomId)
    {
        IList<Reservation> reservations = new List<Reservation>();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM reservation JOIN room ON reservation.id_room = room.id JOIN member ON reservation.matricule_user = member.matricule WHERE id_room = @roomId AND date = @date";
        AddSqlParameter(command, "@roomId", roomId);
        AddSqlParameter(command, "@date", date.ToString("yyyy-MM-dd"));
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var dto = new SqlReservationDto((int)reader["id_reservation"], reader.GetValue(reader.GetOrdinal("date")).ToString(),
                reader.GetValue(reader.GetOrdinal("time_start")).ToString(), reader.GetValue(reader.GetOrdinal("time_end")).ToString(), (string)reader["summary"], (string)reader["description"],
                new SqlRoomDto((string)reader["id"], (string)reader["name"], (int)reader["capacity"]),
                new SqlUserDto((string)reader["matricule"], (string)reader["fullname"], (string)reader["email"]));
            var reservation = calendarMapper.Map(dto);
            reservations.Add(reservation);
        }

        return reservations;
    }

    public void Add(ReservationRequest request, string idRoom)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO reservation (date, time_start, time_end, summary, description, id_room, matricule_user) VALUES (@date, @timeStart, @timeEnd, @summary, @description, @idRoom, @matriculeUser)";
        AddSqlParameter(command, "@date", request.Date.ToString("yyyy-MM-dd"));
        AddSqlParameter(command, "@timeStart", request.TimeStart);
        AddSqlParameter(command, "@timeEnd", request.TimeEnd);
        AddSqlParameter(command, "@summary", request.Description);
        AddSqlParameter(command, "@description", request.Description);
        AddSqlParameter(command, "@idRoom", idRoom);
        AddSqlParameter(command, "@matriculeUser", request.Matricule);
        command.ExecuteNonQuery();
    }

    public string GetLastInsertedId()
    {
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT LAST_INSERT_ID()";
        return command.ExecuteScalar()?.ToString() ?? throw new InvalidOperationException();
    }

    public void Add(IList<Service> services, string idReservation)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO reservation_service (id_reservation, id_service) VALUES (@idReservation, @idService)";
        AddSqlParameter(command, "@idReservation", idReservation);
        var idServiceParameter = command.CreateParameter();
        idServiceParameter.ParameterName = "@idService";
        foreach (var service in services)
        {
            idServiceParameter.Value = service.Id;
            command.Parameters.Add(idServiceParameter);
            command.ExecuteNonQuery();
        }
    }

    public IList<Service> FindAll()
    {
        IList<Service> services = new List<Service>();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM service";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var dto = new SqlServiceDto((int)reader["id"], (string)reader["name"]);
            var service = calendarMapper.MapService(dto);
            services.Add(service);
        }

        return services;
    }

    private void AddSqlParameter(DbCommand command, string parameterName, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = parameterName;
        parameter.Value = value ?? DBNull.Value;
        command.Parameters.Add(parameter);
    }
}
