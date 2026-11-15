using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Application.Services;
using Moq;
using SharedKernel.ValueObjects;

namespace Application.Tests
{
    public class AssociationMemberCapacityCheckerTests
    {
        private readonly Mock<IAssociationRepository> _associationRepositoryMock = new();
        private readonly Mock<IAssociationInvitationRepository> _invitationRepositoryMock = new();
        private readonly Mock<IAssociationTierPort> _tierPortMock = new();

        private readonly AssociationMemberCapacityChecker _checker;

        public AssociationMemberCapacityCheckerTests()
        {
            _checker = new AssociationMemberCapacityChecker(
                _associationRepositoryMock.Object,
                _invitationRepositoryMock.Object,
                _tierPortMock.Object);
        }


        [Fact]
        public async Task EnsureCapacityAvailableAsync_WhenTierPolicyDoesNotExist_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var associationId = new AssociationID(Guid.NewGuid());
            var tierId = Guid.NewGuid();

            _tierPortMock
                .Setup(x => x.GetPolicyAsync(
                    tierId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((AssociationTierPolicy?)null);

            // Act
            var act = async () => await _checker.EnsureCapacityAvailableAsync(
                associationId,
                tierId,
                CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);

            _associationRepositoryMock.Verify(
                x => x.GetMemberCountAsync(
                    It.IsAny<AssociationID>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);

            _invitationRepositoryMock.Verify(
                x => x.GetPendingInvitationCountAsync(
                    It.IsAny<AssociationID>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task EnsureCapacityAvailableAsync_WhenCapacityIsAvailable_ShouldNotThrow()
        {
            // Arrange
            var associationId = new AssociationID(Guid.NewGuid());
            var tierId = Guid.NewGuid();

            _tierPortMock
                .Setup(x => x.GetPolicyAsync(
                    tierId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AssociationTierPolicy(
                    MaxMembers: 5,
                    MaxAdministrators: 1,
                    Features: []));

            _associationRepositoryMock
                .Setup(x => x.GetMemberCountAsync(
                    associationId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(3);

            _invitationRepositoryMock
                .Setup(x => x.GetPendingInvitationCountAsync(
                    associationId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var act = async () => await _checker.EnsureCapacityAvailableAsync(
                associationId,
                tierId,
                CancellationToken.None);

            // Assert
            await _checker.EnsureCapacityAvailableAsync(
                associationId,
                tierId,
                CancellationToken.None);
        }

        [Fact]
        public async Task EnsureCapacityAvailableAsync_WhenCapacityIsExactlyReached_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var associationId = new AssociationID(Guid.NewGuid());
            var tierId = Guid.NewGuid();

            _tierPortMock
                .Setup(x => x.GetPolicyAsync(
                    tierId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AssociationTierPolicy(
                    MaxMembers: 5,
                    MaxAdministrators: 1,
                    Features: []));

            _associationRepositoryMock
                .Setup(x => x.GetMemberCountAsync(
                    associationId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(3);

            _invitationRepositoryMock
                .Setup(x => x.GetPendingInvitationCountAsync(
                    associationId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            // Act
            var act = async () => await _checker.EnsureCapacityAvailableAsync(
                associationId,
                tierId,
                CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact]
        public async Task EnsureCapacityAvailableAsync_WhenCapacityIsExceeded_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var associationId = new AssociationID(Guid.NewGuid());
            var tierId = Guid.NewGuid();

            _tierPortMock
                .Setup(x => x.GetPolicyAsync(
                    tierId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AssociationTierPolicy(
                    MaxMembers: 5,
                    MaxAdministrators: 1,
                    Features: []));

            _associationRepositoryMock
                .Setup(x => x.GetMemberCountAsync(
                    associationId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(4);

            _invitationRepositoryMock
                .Setup(x => x.GetPendingInvitationCountAsync(
                    associationId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            // Act
            var act = async () => await _checker.EnsureCapacityAvailableAsync(
                associationId,
                tierId,
                CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }
    }
}
