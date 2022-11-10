namespace DotNet.Testcontainers.Tests.Unit
{
  using System.Collections.Generic;
  using System.Linq;
  using DotNet.Testcontainers.Clients;
  using DotNet.Testcontainers.Configurations;
  using Xunit;

  public static class TestcontainersConfigurationConverterTest
  {
    private const string Port = "2375";

    public sealed class ExposedPorts
    {
      [Fact]
      public void ShouldAddTcpPortSuffix()
      {
        // Given
        var containerConfiguration = new TestcontainersConfiguration(exposedPorts: new Dictionary<string, string> { { Port, null } });

        // When
        var exposedPort = new TestcontainersConfigurationConverter(containerConfiguration).ExposedPorts.Single().Key;

        // Then
        Assert.Equal($"{Port}/tcp", exposedPort);
      }

      [Theory]
      [InlineData("UDP")]
      [InlineData("TCP")]
      [InlineData("SCTP")]
      public void ShouldKeepPortSuffix(string portSuffix)
      {
        // Given
        var qualifiedPort = $"{Port}/{portSuffix}";

        var containerConfiguration = new TestcontainersConfiguration(exposedPorts: new Dictionary<string, string> { { qualifiedPort, null } });

        // When
        var exposedPort = new TestcontainersConfigurationConverter(containerConfiguration).ExposedPorts.Single().Key;

        // Then
        Assert.Equal($"{Port}/{portSuffix}".ToLowerInvariant(), exposedPort);
      }
    }

    public sealed class PortBindings
    {
      [Fact]
      public void ShouldAddTcpPortSuffix()
      {
        // Given
        var containerConfiguration = new TestcontainersConfiguration(portBindings: new Dictionary<string, string> { { Port, Port } });

        // When
        var portBinding = new TestcontainersConfigurationConverter(containerConfiguration).PortBindings.Single().Key;

        // Then
        Assert.Equal($"{Port}/tcp", portBinding);
      }

      [Theory]
      [InlineData("UDP")]
      [InlineData("TCP")]
      [InlineData("SCTP")]
      public void ShouldKeepPortSuffix(string portSuffix)
      {
        // Given
        var qualifiedPort = $"{Port}/{portSuffix}";

        var containerConfiguration = new TestcontainersConfiguration(portBindings: new Dictionary<string, string> { { qualifiedPort, Port } });

        // When
        var portBinding = new TestcontainersConfigurationConverter(containerConfiguration).PortBindings.Single().Key;

        // Then
        Assert.Equal($"{Port}/{portSuffix}".ToLowerInvariant(), portBinding);
      }
    }
  }
}
