using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------
// 1. Service Registration & API Configuration
// ----------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Customer Management API",
        Description = "A premium starter Web API custom-prepared for customer management workflows under HVE-Core.",
        Contact = new OpenApiContact
        {
            Name = "Vicente Teo",
            Email = "vicenteteo@example.com"
        }
    });
});

// Configure CORS for local development interfaces
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register state as a singleton for development database simulation
builder.Services.AddSingleton<CustomerRepository>();

var app = builder.Build();

// ----------------------------------------------------
// 2. HTTP Request Pipeline Configuration
// ----------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Management API v1");
        c.RoutePrefix = "swagger";
    });
}

// Enable serving static assets if present
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("AllowAll");

// ----------------------------------------------------
// 3. REST API Endpoints Routing
// ----------------------------------------------------
var api = app.MapGroup("/api");

api.MapGet("/customers", (CustomerRepository repo) =>
{
    return Results.Ok(repo.GetAll());
})
.WithName("GetCustomers")
.WithOpenApi(operation => new(operation)
{
    Summary = "Retrieve all tracked customers",
    Description = "Returns a comprehensive list of active, inactive, lead, and pending customers."
});

api.MapGet("/customers/{id:int}", (int id, CustomerRepository repo) =>
{
    var customer = repo.GetById(id);
    return customer is not null 
        ? Results.Ok(customer) 
        : Results.NotFound(new { Message = $"Customer with ID {id} not found." });
})
.WithName("GetCustomerById")
.WithOpenApi(operation => new(operation)
{
    Summary = "Retrieve customer details by ID"
});

api.MapPost("/customers", (Customer newCustomer, CustomerRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(newCustomer.FirstName) || string.IsNullOrWhiteSpace(newCustomer.LastName))
    {
        return Results.BadRequest(new { Error = "First name and last name are required." });
    }
    if (string.IsNullOrWhiteSpace(newCustomer.Email))
    {
        return Results.BadRequest(new { Error = "Email address is required." });
    }
    
    var created = repo.Add(newCustomer);
    return Results.Created($"/api/customers/{created.Id}", created);
})
.WithName("CreateCustomer")
.WithOpenApi(operation => new(operation)
{
    Summary = "Create a new customer profile"
});

api.MapPut("/customers/{id:int}", (int id, Customer updatedCustomer, CustomerRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(updatedCustomer.FirstName) || string.IsNullOrWhiteSpace(updatedCustomer.LastName))
    {
        return Results.BadRequest(new { Error = "First name and last name are required." });
    }
    
    var success = repo.Update(id, updatedCustomer);
    return success 
        ? Results.Ok(repo.GetById(id))
        : Results.NotFound(new { Message = $"Customer with ID {id} not found." });
})
.WithName("UpdateCustomer")
.WithOpenApi(operation => new(operation)
{
    Summary = "Update an existing customer profile details"
});

api.MapDelete("/customers/{id:int}", (int id, CustomerRepository repo) =>
{
    var success = repo.Delete(id);
    return success 
        ? Results.NoContent()
        : Results.NotFound(new { Message = $"Customer with ID {id} not found." });
})
.WithName("DeleteCustomer")
.WithOpenApi(operation => new(operation)
{
    Summary = "Delete a customer profile"
});

api.MapPost("/customers/{id:int}/notes", (int id, CustomerNote note, CustomerRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(note.Content))
    {
        return Results.BadRequest(new { Error = "Note content is required." });
    }
    
    var success = repo.AddNote(id, note);
    return success 
        ? Results.Ok(repo.GetById(id))
        : Results.NotFound(new { Message = $"Customer with ID {id} not found." });
})
.WithName("RecordCustomerNote")
.WithOpenApi(operation => new(operation)
{
    Summary = "Record a note for a customer interaction",
    Description = "Adds a time-stamped note log detailing interactions, support details, or lead tracking."
});

app.Run();

// ----------------------------------------------------
// 4. Domain Models & Mock Repository
// ----------------------------------------------------
public record CustomerNote(int Id, string Content, DateTime CreatedAt);

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Status { get; set; } = "Lead"; // Active, Inactive, Lead, Pending
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<CustomerNote> Notes { get; set; } = new();
}

public class CustomerRepository
{
    private readonly List<Customer> _customers = new();
    private int _customerIdCounter = 1;
    private int _noteIdCounter = 1;

    public CustomerRepository()
    {
        // Seed default high-quality customer data
        var c1 = new Customer 
        { 
            Id = _customerIdCounter++, 
            FirstName = "Alice", 
            LastName = "Smith", 
            Email = "alice.smith@acme.com",
            Company = "Acme Corporation",
            Status = "Active"
        };
        c1.Notes.Add(new CustomerNote(_noteIdCounter++, "Initial onboarding complete. Very pleased with standard integration speed.", DateTime.UtcNow.AddDays(-5)));
        c1.Notes.Add(new CustomerNote(_noteIdCounter++, "Sent follow-up regarding feature request for exports.", DateTime.UtcNow.AddDays(-2)));

        var c2 = new Customer 
        { 
            Id = _customerIdCounter++, 
            FirstName = "Bob", 
            LastName = "Jones", 
            Email = "bob@jonesventures.io",
            Company = "Jones Ventures",
            Status = "Lead"
        };
        c2.Notes.Add(new CustomerNote(_noteIdCounter++, "Expressed high interest in product lines during conference.", DateTime.UtcNow.AddDays(-1)));

        var c3 = new Customer 
        { 
            Id = _customerIdCounter++, 
            FirstName = "Charlie", 
            LastName = "Brown", 
            Email = "charlie@snoopy.net",
            Company = "Peanuts LLC",
            Status = "Pending"
        };

        _customers.Add(c1);
        _customers.Add(c2);
        _customers.Add(c3);
    }

    public IEnumerable<Customer> GetAll() => _customers;

    public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);

    public Customer Add(Customer customer)
    {
        customer.Id = _customerIdCounter++;
        customer.CreatedAt = DateTime.UtcNow;
        _customers.Add(customer);
        return customer;
    }

    public bool Update(int id, Customer updated)
    {
        var existing = GetById(id);
        if (existing is null) return false;

        existing.FirstName = updated.FirstName;
        existing.LastName = updated.LastName;
        existing.Email = updated.Email;
        existing.Company = updated.Company;
        existing.Status = updated.Status;
        return true;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing is null) return false;
        
        _customers.Remove(existing);
        return true;
    }

    public bool AddNote(int customerId, CustomerNote note)
    {
        var customer = GetById(customerId);
        if (customer is null) return false;
        
        var normalizedNote = note with { Id = _noteIdCounter++, CreatedAt = DateTime.UtcNow };
        customer.Notes.Add(normalizedNote);
        return true;
    }
}

public partial class Program { }
