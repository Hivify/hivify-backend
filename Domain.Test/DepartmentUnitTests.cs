using Department.Domain.Departments;
using SharedKernel.ValueObjects;
using System;
using System.Xml.Linq;
using Xunit;

namespace Domain.Tests;

public class DepartmentTests
{
    private static DepartmentEntity CreateValidDepartment()
    {
        return DepartmentEntity.Create(
            new Name("Test Department"));
    }

    [Fact]
    public void Create_WithValidName_ShouldSetProperties()
    {
        // Arrange
        var name = new Name("Test Department");

        // Act
        var department = DepartmentEntity.Create(name);

        // Assert
        Assert.NotEqual(Guid.Empty, department.Id.Value);
        Assert.Equal(name, department.Name);
        Assert.Empty(department.StaffMembers);
    }

    [Fact]
    public void Create_CalledTwice_ShouldGenerateDifferentIds()
    {
        // Act
        var firstDepartment = CreateValidDepartment();
        var secondDepartment = CreateValidDepartment();

        // Assert
        Assert.NotEqual(
            firstDepartment.Id.Value,
            secondDepartment.Id.Value);
    }
}