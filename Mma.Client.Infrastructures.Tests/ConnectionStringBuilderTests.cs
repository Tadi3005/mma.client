namespace Mma.Client.Infrastructures.Tests
{
    public class ConnectionStringBuilderTests
    {
        [Test]
        public void DbServer_ShouldReturnCorrectValue()
        {
            // Arrange
            var connectionString = "Q123456@192.168.100.200:3306;user=admin;pwd=password123";
            var builder = new ConnectionStringBuilder(connectionString);

            // Act
            var dbServer = builder.DbServer;

            // Assert
            Assert.That(dbServer, Is.EqualTo("192.168.100.200"));
        }

        [Test]
        public void DbPort_ShouldReturnCorrectValue()
        {
            // Arrange
            var connectionString = "Q123456@192.168.100.200:3306;user=admin;pwd=password123";
            var builder = new ConnectionStringBuilder(connectionString);

            // Act
            var dbPort = builder.DbPort;

            // Assert
            Assert.That(dbPort, Is.EqualTo("3306"));
        }

        [Test]
        public void DbUser_ShouldReturnCorrectValue()
        {
            // Arrange
            var connectionString = "Q123456@192.168.100.200:3306;user=admin;pwd=password123";
            var builder = new ConnectionStringBuilder(connectionString);

            // Act
            var dbUser = builder.DbUser;

            // Assert
            Assert.That(dbUser, Is.EqualTo("admin"));
        }

        [Test]
        public void DbPassword_ShouldReturnCorrectValue()
        {
            // Arrange
            var connectionString = "Q123456@192.168.100.200:3306;user=admin;pwd=password123";
            var builder = new ConnectionStringBuilder(connectionString);

            // Act
            var dbPassword = builder.DbPassword;

            // Assert
            Assert.That(dbPassword, Is.EqualTo("password123"));
        }

        [Test]
        public void DbDataBase_ShouldReturnCorrectValue()
        {
            // Arrange
            var connectionString = "Q123456@192.168.100.200:3306;user=admin;pwd=password123";
            var builder = new ConnectionStringBuilder(connectionString);

            // Act
            var dbDatabase = builder.DbDataBase;

            // Assert
            Assert.That(dbDatabase, Is.EqualTo("Q123456"));
        }

        [Test]
        public void NormalizedConnectionParts_ShouldRemoveWhitespace()
        {
            // Arrange
            var connectionString = "  Q123456@192.168.100.200:3306 ; user = admin ; pwd = password123  ";
            var builder = new ConnectionStringBuilder(connectionString);

            // Act
            var dbServer = builder.DbServer;
            var dbPort = builder.DbPort;

            // Assert
            Assert.That(dbServer, Is.EqualTo("192.168.100.200"));
            Assert.That(dbPort, Is.EqualTo("3306"));
        }

        [Test]
        public void InvalidConnectionString_ShouldHandleGracefully()
        {
            // Arrange
            var connectionString = "invalidConnectionString";
            var builder = new ConnectionStringBuilder(connectionString);

            // Act
            var dbServer = builder.DbServer;
            var dbPort = builder.DbPort;
            var dbUser = builder.DbUser;
            var dbPassword = builder.DbPassword;
            var dbDatabase = builder.DbDataBase;

            // Assert
            Assert.That(dbServer, Is.Empty);
            Assert.That(dbPort, Is.Empty);
            Assert.That(dbUser, Is.Empty);
            Assert.That(dbPassword, Is.Empty);
            Assert.That(dbDatabase, Is.Empty);
        }
    }
}
