using Xunit;

namespace CustomerManagement.Tests;

public class UnitTests
{
    [Fact]
    public void GetAll_ReturnsSeededCustomers()
    {
        // Arrange
        var repo = new CustomerRepository();

        // Act
        var customers = repo.GetAll();

        // Assert
        Assert.NotNull(customers);
        Assert.Equal(3, customers.Count());
    }

    [Fact]
    public void GetById_WithValidId_ReturnsCustomer()
    {
        // Arrange
        var repo = new CustomerRepository();

        // Act
        var customer = repo.GetById(1);

        // Assert
        Assert.NotNull(customer);
        Assert.Equal("Alice", customer.FirstName);
        Assert.Equal("Smith", customer.LastName);
    }

    [Fact]
    public void GetById_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var repo = new CustomerRepository();

        // Act
        var customer = repo.GetById(99);

        // Assert
        Assert.Null(customer);
    }

    [Fact]
    public void Add_AddsCustomerSuccessfully()
    {
        // Arrange
        var repo = new CustomerRepository();
        var newCustomer = new Customer
        {
            FirstName = "David",
            LastName = "Miller",
            Email = "david.miller@tech.org",
            Company = "TechOrg LLC",
            Status = "Active"
        };

        // Act
        var created = repo.Add(newCustomer);
        var fetched = repo.GetById(created.Id);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 3);
        Assert.NotNull(fetched);
        Assert.Equal("David", fetched.FirstName);
        Assert.Equal("david.miller@tech.org", fetched.Email);
    }

    [Fact]
    public void Update_ModifiesCustomerDetails()
    {
        // Arrange
        var repo = new CustomerRepository();
        var updatedData = new Customer
        {
            FirstName = "Alice",
            LastName = "Johnson",
            Email = "alice.johnson@acme.com",
            Company = "Acme Global",
            Status = "Inactive"
        };

        // Act
        var success = repo.Update(1, updatedData);
        var fetched = repo.GetById(1);

        // Assert
        Assert.True(success);
        Assert.NotNull(fetched);
        Assert.Equal("Johnson", fetched.LastName);
        Assert.Equal("Acme Global", fetched.Company);
        Assert.Equal("Inactive", fetched.Status);
    }

    [Fact]
    public void Delete_RemovesCustomer()
    {
        // Arrange
        var repo = new CustomerRepository();

        // Act
        var success = repo.Delete(2);
        var fetched = repo.GetById(2);

        // Assert
        Assert.True(success);
        Assert.Null(fetched);
        Assert.Equal(2, repo.GetAll().Count());
    }

    [Fact]
    public void AddNote_AppendsNoteToCustomer()
    {
        // Arrange
        var repo = new CustomerRepository();
        var note = new CustomerNote(0, "Sent promotional catalog.", DateTime.UtcNow);

        // Act
        var success = repo.AddNote(1, note);
        var customer = repo.GetById(1);

        // Assert
        Assert.True(success);
        Assert.NotNull(customer);
        Assert.Equal(3, customer.Notes.Count);
        Assert.Equal("Sent promotional catalog.", customer.Notes.Last().Content);
    }
}
