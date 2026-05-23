using Xunit;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CustomerManagement.Tests;

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCustomers_ShouldReturnSuccessAndJSONList()
    {
        // Act
        var response = await _client.GetAsync("/api/customers");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
        Assert.NotNull(customers);
        Assert.True(customers.Count >= 3);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomer_WhenIdIsValid()
    {
        // Act
        var response = await _client.GetAsync("/api/customers/1");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var customer = await response.Content.ReadFromJsonAsync<Customer>();
        Assert.NotNull(customer);
        Assert.Equal(1, customer.Id);
        Assert.Equal("Alice", customer.FirstName);
        Assert.Equal("Smith", customer.LastName);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnNotFound_WhenIdIsInvalid()
    {
        // Act
        var response = await _client.GetAsync("/api/customers/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnCreatedAndPayload_WhenPayloadIsValid()
    {
        // Arrange
        var newCustomer = new Customer 
        { 
            FirstName = "Emily", 
            LastName = "Davis", 
            Email = "emily.davis@ventures.co", 
            Company = "Davis Ventures",
            Status = "Active"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/customers", newCustomer);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<Customer>();
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal("Emily", created.FirstName);
        Assert.Equal("Davis Ventures", created.Company);
    }

    [Fact]
    public async Task CreateCustomer_ShouldReturnBadRequest_WhenEmailIsEmpty()
    {
        // Arrange
        var invalidCustomer = new Customer 
        { 
            FirstName = "Emily", 
            LastName = "Davis", 
            Email = "", // Invalid: Missing Email
            Company = "Davis Ventures",
            Status = "Active"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/customers", invalidCustomer);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldReturnSuccess_WhenIdIsValid()
    {
        // Arrange
        var updateInfo = new Customer 
        { 
            FirstName = "Alice", 
            LastName = "Vanderbilt", 
            Email = "alice.smith@acme.com", 
            Company = "Vanderbilt Ltd",
            Status = "Active"
        };

        // Act
        var response = await _client.PutAsJsonAsync("/api/customers/1", updateInfo);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<Customer>();
        Assert.NotNull(updated);
        Assert.Equal("Vanderbilt", updated.LastName);
        Assert.Equal("Vanderbilt Ltd", updated.Company);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldReturnNoContent_WhenIdIsValid()
    {
        // Act
        var response = await _client.DeleteAsync("/api/customers/3");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify it was actually deleted
        var getResponse = await _client.GetAsync("/api/customers/3");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task AddNote_ShouldReturnSuccess_WhenPayloadIsValid()
    {
        // Arrange
        var newNote = new CustomerNote(0, "Sent invoice for Q2 software licensing.", System.DateTime.UtcNow);

        // Act
        var response = await _client.PostAsJsonAsync("/api/customers/1/notes", newNote);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var customer = await response.Content.ReadFromJsonAsync<Customer>();
        Assert.NotNull(customer);
        Assert.Contains(customer.Notes, n => n.Content == "Sent invoice for Q2 software licensing.");
    }
}
