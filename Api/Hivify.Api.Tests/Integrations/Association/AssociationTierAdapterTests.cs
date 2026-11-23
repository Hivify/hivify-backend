using Hivify.Api.Integrations.Association;
using Moq;
using Tiering.Application.Contracts;
using Tiering.Application.DTOs;

namespace Hivify.Api.Tests.Integrations.Association
{
    public sealed class AssociationTierAdapterTests
    {
        private readonly Mock<ITierService> _tierServiceMock = new();

        private readonly AssociationTierAdapter _adapter;

        public AssociationTierAdapterTests()
        {
            _adapter = new AssociationTierAdapter(_tierServiceMock.Object);
        }


        [Fact]
        public async Task GetPolicyAsync_WhenTierPolicyExists_ShouldMapPolicyCorrectly()
        {
            // Arrange
            var tierId = Guid.NewGuid();

            var features = new[] { "Invitations", "Reports" };

            var tierPolicy = new TierPolicyDto(
                MaxMembers: 25,
                MaxAdministrators: 3,
                Features: features);

            _tierServiceMock
                .Setup(x => x.GetTierPolicyAsync(
                    tierId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(tierPolicy);

            // Act
            var result = await _adapter.GetPolicyAsync(
                tierId,
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(25, result.MaxMembers);
            Assert.Equal(3, result.MaxAdministrators);
            Assert.Equal(features, result.Features);
        }



        [Fact]
        public async Task GetPolicyAsync_WhenTierPolicyDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var tierId = Guid.NewGuid();

            _tierServiceMock
                .Setup(x => x.GetTierPolicyAsync(
                    tierId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((TierPolicyDto?)null);

            // Act
            var result = await _adapter.GetPolicyAsync(
                tierId,
                CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
