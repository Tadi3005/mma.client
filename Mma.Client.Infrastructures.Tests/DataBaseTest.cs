using System.Data.SQLite;
using Mma.Client.Domains;
using Mma.Client.Domains.AskReservation;
using Mma.Client.Infrastructures.Sql;

namespace Mma.Client.Infrastructures.Tests;

public class Tests
{
    private SqlDataStorageFactory _dataStorageFactory;
    private SqlService _service;

    private const string ConnectionString = """Data Source="../../../../Mma.Client.Infrastructures.Tests/Resources/mmaClient.sqlite";Version=3;""";
    private const string FalseConnectionString = "False Data Source";
    [SetUp]
    public void Setup()
    {
        try
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            const string createTablesScript = """
              create table if not exists member (matricule varchar(50) not null primary key, fullname varchar(50) not null, email varchar(100) not null);
              create table if not exists room (id varchar(50) not null primary key, name varchar(50) not null, capacity int not null);
              create table if not exists reservation (id_reservation integer primary key autoincrement, date date not null, time_start time not null, time_end time not null, summary varchar(100) not null, description varchar(200) not null, id_room varchar(50) not null, matricule_user varchar(50) not null, constraint Reservation_Member_matricule_fk foreign key (matricule_user) references member (matricule), constraint Reservation_Room_id_fk foreign key (id_room) references room (id));
              create table if not exists service (id integer primary key autoincrement, name varchar(50) not null);
              create table if not exists reservation_service (id integer primary key autoincrement, id_reservation int not null, id_service int not null, constraint Reservation_Service_Service_id_fk foreign key (id_service) references service (id), constraint reservation_service_reservation_id_reservation_fk foreign key (id_reservation) references reservation (id_reservation));

              insert into member (matricule, fullname, email) values ('M001', 'Alice Dupont', 'alice.dupont@example.com');
              insert into member (matricule, fullname, email) values ('M002', 'Bob Martin', 'bob.martin@example.com');

              insert into room (id, name, capacity) values ('R001', 'Salle Réunion A', 20);
              insert into room (id, name, capacity) values ('R002', 'Salle Réunion B', 10);

              insert into reservation (date, time_start, time_end, summary, description, id_room, matricule_user)
              values ('2022-01-01', '09:00', '11:00', 'Réunion Projet A', 'Discussion sur le projet A', 'R001', 'M001');
              insert into reservation (date, time_start, time_end, summary, description, id_room, matricule_user)
              values ('2022-01-01', '14:00', '15:30', 'Atelier B', 'Atelier pratique', 'R002', 'M002');

              insert into service (name) values ('Service Catering');
              insert into service (name) values ('Service Technique');

              insert into reservation_service (id_reservation, id_service) values (1, 1);
              insert into reservation_service (id_reservation, id_service) values (2, 2);
              """;

            using var command = new SQLiteCommand(createTablesScript, connection);
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        _dataStorageFactory = new SqlDataStorageFactory(ConnectionString, "System.Data.SQLite", SQLiteFactory.Instance);
        _service = new SqlService(_dataStorageFactory.CreateDataStorage());
    }

    [Test]
    public void ShouldFindAllUserWhenThereIsTwoUser()
    {
        var users = _service.FindAllServices();
        Console.WriteLine(users.Count);
        Assert.That(users, Has.Count.EqualTo(2));
    }

    [Test]
    public void ShouldFindAllReservationsWhenThereIsOneReservation()
    {
        var reservations = _service.FindReservations(DateTime.Parse("2022-01-01"), "R001");
        Assert.That(reservations, Has.Count.EqualTo(1));
    }

    [Test]
    public void ShouldFindRoomByIdWhenRoomExists()
    {
        // Arrange
        const string roomId = "R001";

        // Act
        var room = _service.FindRoomById(roomId);

        // Assert
        Assert.That(room, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(room.Id, Is.EqualTo(roomId));
            Assert.That(room.Name, Is.EqualTo("Salle Réunion A"));
            Assert.That(room.Capacity, Is.EqualTo(20));
        });
    }

    [Test]
    public void ShouldReturnEmptyListWhenNoReservationsExistForDateAndRoom()
    {
        // Arrange
        var date = DateTime.Parse("2023-01-01");
        const string roomId = "R001";

        // Act
        var reservations = _service.FindReservations(date, roomId);

        // Assert
        Assert.That(reservations, Is.Not.Null);
        Assert.That(reservations, Has.Count.EqualTo(0));
    }

    [Test]
    public void ShouldAddReservationSuccessfully()
    {
        // Arrange
        var request = new ReservationRequest("M001", DateTime.Parse("2022-01-02"), DateTime.Parse("2022-01-02 10:00:00"), DateTime.Parse("2022-01-02 12:00:00"), 10, "Nouvelle Réunion", new List<Service>());

        const string roomId = "R001";

        // Act
        _service.AddReservation(request, roomId);

        // Assert
        var reservations = _service.FindReservations(request.RequestDate, roomId);
        Assert.That(reservations, Has.Count.EqualTo(1));
        Assert.That(reservations[0].Summary, Is.EqualTo("Nouvelle Réunion"));
    }

    [Test]
    public void ShouldThrowExceptionWhenAddingReservationWithInvalidRoomId()
    {
        // Arrange
        var factory = new SqlDataStorageFactory(FalseConnectionString, "System.Data.SQLite", SQLiteFactory.Instance);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            factory.CreateDataStorage();
        }, "Expected an exception when creating data storage with an invalid connection string");
    }

    [Test]
    public void ShouldAllUsersWhenThereIsTwoUsers()
    {
        var users = _service.FindAllUsers();
        Assert.That(users, Has.Count.EqualTo(2));
    }

    [Test]
    public void ShouldAllServicesWhenThereIsTwoServices()
    {
        var services = _service.FindAllServices();
        Assert.That(services, Has.Count.EqualTo(2));
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();

            const string dropTablesScript = """
                                            drop table if exists reservation_service;
                                            drop table if exists service;
                                            drop table if exists reservation;
                                            drop table if exists room;
                                            drop table if exists member
                                            """;

            using var command = new SQLiteCommand(dropTablesScript, connection);
            command.ExecuteNonQuery();
            connection.Close();
        } catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


        _dataStorageFactory.Dispose();
    }
}
