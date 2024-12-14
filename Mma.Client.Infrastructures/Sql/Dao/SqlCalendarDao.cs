using System.Data.Common;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Domains.Data.Dao;
using Mma.Client.Infrastructures.Dto.Sql;
using Mma.Client.Infrastructures.Mapper;
using Serilog;

namespace Mma.Client.Infrastructures.Sql.Dao;

public class SqlCalendarDao(DbConnection connection, SqlCalendarMapper calendarMapper, ILogger logger) : ICalendarDao
{
    public IList<Reservation> FindByRoomIdAndDate(DateTime date, string roomId)
    {
        try
        {
            var reservations = new List<Reservation>();
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
        catch (Exception e)
        {
            logger.Error("Error while finding reservations by room id and date");
            throw new InvalidOperationException("Error while finding reservations by room id and date", e);
        }
    }

    public void Add(ReservationRequest request, string idRoom)
    {
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO reservation (date, time_start, time_end, summary, description, id_room, matricule_user) VALUES (@date, @timeStart, @timeEnd, @summary, @description, @idRoom, @matriculeUser)";
            AddSqlParameter(command, "@date", request.RequestDate.ToString("yyyy-MM-dd"));
            AddSqlParameter(command, "@timeStart", request.TimeStart);
            AddSqlParameter(command, "@timeEnd", request.TimeEnd);
            AddSqlParameter(command, "@summary", request.Description);
            AddSqlParameter(command, "@description", request.Description);
            AddSqlParameter(command, "@idRoom", idRoom);
            AddSqlParameter(command, "@matriculeUser", request.Matricule);
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            logger.Error("Error while adding reservation");
            throw new InvalidOperationException("Error while adding reservation", e);
        }
    }

    public string GetLastInsertedId()
    {
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT LAST_INSERT_ID()";
            return command.ExecuteScalar()?.ToString() ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            logger.Error("Error while getting last inserted id");
            throw new InvalidOperationException("Error while getting last inserted id", e);
        }
    }

    public void Add(string idService, string idReservation)
    {
        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO reservation_service (id_reservation, id_service) VALUES (@idReservation, @idService)";

            AddSqlParameter(command, "@idReservation", idReservation);
            AddSqlParameter(command, "@idService", idService);
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            logger.Error("Error while adding reservation service");
            throw new InvalidOperationException("Error while adding reservation service", e);
        }
    }

    public IList<Service> FindAll()
    {
        try
        {
            var services = new List<Service>();
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
        catch (Exception e)
        {
            logger.Error("Error while finding all services");
            throw new InvalidOperationException("Error while finding all services", e);
        }
    }

    private void AddSqlParameter(DbCommand command, string parameterName, object value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = parameterName;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }
}
