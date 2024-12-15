using System.Data;
using Mma.Client.Infrastructures.Mapper;
using Mma.Client.Infrastructures.Sql.Dao;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Serilog;

namespace Mma.Client.Infrastructures.Tests;

public class ErrorSelectDbTest
{
    [Test]
    public void FindAllUser_ShouldLogErrorAndThrowInvalidOperationException_WhenExecuteReaderThrowsException()
    {
        // Arrange
        var connection = Substitute.For<IDbConnection>();
        var command = Substitute.For<IDbCommand>();
        var logger = Substitute.For<ILogger>();
        var mapper = Substitute.For<SqlUserMapper>();

        // Simuler le comportement de la connexion et de la commande
        connection.CreateCommand().Returns(command);
        command.ExecuteReader().Throws(new Exception("Database error"));

        // Créez l'instance de SqlUserDao
        var dao = new SqlUserDao(connection, mapper, logger);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => dao.FindAll());
        Assert.That(ex.Message, Is.EqualTo("Error while finding all users"));
        Assert.That(ex.InnerException?.Message, Is.EqualTo("Database error"));

        // Vérifiez que le logger a été appelé
        logger.Received(1).Error("Error while finding all users");
    }

    [Test]
    public void ShouldLogErrorAndThrowInvalidOperationException_WhenExecuteReaderThrowsException()
    {
        // Arrange
        var connection = Substitute.For<IDbConnection>();
        var command = Substitute.For<IDbCommand>();
        var logger = Substitute.For<ILogger>();
        var mapper = Substitute.For<SqlRoomMapper>();

        // Simuler le comportement de la connexion et de la commande
        connection.CreateCommand().Returns(command);
        command.ExecuteReader().Throws(new Exception("Database error"));

        // Créez l'instance de SqlRoomDao
        var dao = new SqlRoomDao(connection, mapper, logger);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => dao.FindById("1"));
        Assert.That(ex.Message, Is.EqualTo("Error while finding room by id"));
        Assert.That(ex.InnerException?.Message, Is.EqualTo("Database error"));

        // Vérifiez que le logger a été appelé
        logger.Received(1).Error(Arg.Is<Exception>(e => e.Message == "Database error"), "Error while finding room by id");
    }

    [Test]
    public void FindAllReservation_ShouldLogErrorAndThrowInvalidOperationException_WhenExecuteReaderThrowsException()
    {
        // Arrange
        var connection = Substitute.For<IDbConnection>();
        var command = Substitute.For<IDbCommand>();
        var reader = Substitute.For<IDataReader>();
        var logger = Substitute.For<ILogger>();
        var calendarMapper = Substitute.For<SqlCalendarMapper>();

        // Simuler le comportement de la connexion et de la commande
        connection.CreateCommand().Returns(command);
        command.ExecuteReader().Returns(reader);

        // Simuler l'exception lorsque ExecuteReader est appelé
        command.ExecuteReader().Throws(new Exception("Database error"));

        // Créez l'instance de SqlServiceDao (ou un nom approprié pour le DAO)
        var dao = new SqlCalendarDao(connection, calendarMapper, logger);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => dao.FindAll());
        Assert.That(ex.Message, Is.EqualTo("Error while finding all services"));
        Assert.That(ex.InnerException?.Message, Is.EqualTo("Database error"));

        // Vérifiez que le logger a été appelé
        logger.Received(1).Error("Error while finding all services");
    }

    [Test]
    public void FindAll_ShouldLogErrorAndThrowInvalidOperationException_WhenExecuteReaderThrowsException()
    {
        // Arrange
        var connection = Substitute.For<IDbConnection>();
        var command = Substitute.For<IDbCommand>();
        var reader = Substitute.For<IDataReader>();
        var logger = Substitute.For<ILogger>();
        var calendarMapper = Substitute.For<SqlCalendarMapper>();

        // Simuler le comportement de la connexion et de la commande
        connection.CreateCommand().Returns(command);
        command.ExecuteReader().Returns(reader);

        // Simuler l'exception lors de l'appel à ExecuteReader
        command.ExecuteReader().Throws(new Exception("Database error"));

        // Créer l'instance de SqlServiceDao (ou le nom approprié pour votre DAO)
        var dao = new SqlCalendarDao(connection, calendarMapper, logger);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => dao.FindAll());
        Assert.That(ex.Message, Is.EqualTo("Error while finding all services"));
        Assert.That(ex.InnerException?.Message, Is.EqualTo("Database error"));

        // Vérifier que le logger a bien été appelé
        logger.Received(1).Error("Error while finding all services");
    }

    [Test]
    public void FindById_ShouldLogErrorAndThrowInvalidOperationException_WhenExecuteReaderThrowsException()
    {
        // Arrange
        var connection = Substitute.For<IDbConnection>();
        var command = Substitute.For<IDbCommand>();
        var logger = Substitute.For<ILogger>();
        var mapper = Substitute.For<SqlRoomMapper>();

        // Simuler le comportement de la connexion et de la commande
        connection.CreateCommand().Returns(command);

        // Simuler l'exception lors de l'appel à ExecuteReader
        command.ExecuteReader().Throws(new Exception("Database error"));

        // Créer une instance de SqlRoomDao (ou le nom approprié pour votre DAO)
        var dao = new SqlRoomDao(connection, mapper, logger);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => dao.FindById("123"));
        Assert.That(ex.Message, Is.EqualTo("Error while finding room by id"));
        Assert.That(ex.InnerException?.Message, Is.EqualTo("Database error"));

        // Vérifier que le logger a bien été appelé
        logger.Received(1).Error(Arg.Any<Exception>(), "Error while finding room by id");
    }

    [Test]
    public void Add_ShouldLogErrorAndThrowInvalidOperationException_WhenExecuteNonQueryThrowsException()
    {
        // Arrange
        var connection = Substitute.For<IDbConnection>();
        var command = Substitute.For<IDbCommand>();
        var logger = Substitute.For<ILogger>();

        // Simuler le comportement de la connexion et de la commande
        connection.CreateCommand().Returns(command);

        // Simuler l'exception lors de l'appel à ExecuteNonQuery
        command.ExecuteNonQuery().Throws(new Exception("Database error"));

        // Créer une instance de SqlCalendarDao (ou le DAO approprié pour votre projet)
        var dao = new SqlCalendarDao(connection, Substitute.For<ISqlCalendarMapper>(), logger);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => dao.Add("service123", "reservation456"));
        Assert.That(ex.Message, Is.EqualTo("Error while adding reservation service"));
        Assert.That(ex.InnerException?.Message, Is.EqualTo("Database error"));

        // Vérifier que le logger a bien été appelé
        logger.Received(1).Error("Error while adding reservation service");
    }


}
