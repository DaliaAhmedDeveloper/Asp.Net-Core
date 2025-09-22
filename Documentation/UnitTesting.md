# What is Unit Testing?

Unit testing is the process of testing small pieces of code (usually methods/classes) in isolation to ensure they work as expected.

In ASP.NET Core 8, we use unit testing to verify business logic, services, controllers, etc.

The goal: catch bugs early, improve code quality, and make refactoring safer.

# What is Moq?

Moq is a mocking library for .NET.

It allows you to create fake versions of interfaces or classes so you can test your code without calling the real implementation.

This is extremely useful in unit testing, where you want to isolate the class under test.

# Why use Moq?

Suppose your class depends on a database or an external API:

```csharp
public class OrderHelper
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderHelper(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Order> GetOrder(int id)
    {
        return await _unitOfWork.Order.GetByIdAsync(id);
    }
}
```

If you call _unitOfWork.Order.GetByIdAsync(id) in a test, it would hit the database.

We don’t want that in unit tests — it makes tests slow, fragile, and dependent on external state.

With Moq, you can fake _unitOfWork:

```csharp
var mockUnitOfWork = new Mock<IUnitOfWork>();
mockUnitOfWork.Setup(u => u.Order.GetByIdAsync(1))
              .ReturnsAsync(new Order { Id = 1, TotalAmount = 100 });

var helper = new OrderHelper(mockUnitOfWork.Object);

var order = await helper.GetOrder(1);

// order is now the fake order we set up
```

# Tools for Unit Testing in ASP.NET Core

- xUnit (most common, modern, default template in .NET)

- NUnit (older, still popular)

- MSTest (Microsoft’s built-in, but less used now)

# Setup for Unit Testing

1. Navigate to your main project folder
   cd path/to/MyApp

2. dotnet add package xunit
   dotnet add package xunit.runner.visualstudio

   xunit → contains the [Fact] and [Theory] attributes.
   xunit.runner.visualstudio → allows Visual Studio / dotnet test to discover and run tests.

3. Create an xUnit test project inside the project folder
   dotnet new xunit -n Tests

4. Add a reference to the main project
   dotnet add Tests/Tests.csproj reference MyApp.csproj

5. dotnet add package Moq

6. Build and run tests
dotnet build
dotnet test

____________________________________________________________

# Attributes : 

1. Fact Attribute

[Fact] is used to mark a method as a test method.

It represents a fixed test case with no parameters.

```csharp
[Fact]
public void Add_ShouldReturnSum()
{
    int result = 2 + 3;
    Assert.Equal(5, result);
}
```

2. Theory Attribute

[Theory] allows parameterized tests.

You can run the same test with multiple inputs using [InlineData].

```csharp
[Theory]
[InlineData(2, 3, 5)]
[InlineData(10, 20, 30)]
public void Add_ShouldReturnCorrectSum(int a, int b, int expected)
{
    Assert.Equal(expected, a + b);
}
```
3. Assertions

Assertions are checks that verify the result of your code.

Common Assertions:

- Assert.Equal(expected, actual) → values are equal
- Assert.NotEqual(expected, actual) → values are not equal
- Assert.True(condition) / Assert.False(condition) → condition is true/false
- Assert.Null(obj) / Assert.NotNull(obj) → object is null/not null
- Assert.Contains(item, collection) / Assert.DoesNotContain(item, collection) →  checks if a collection contains an item 
- Assert.Empty(collection) / Assert.NotEmpty(collection) → collection is empty/not empty
- Assert.Throws<TException>(() => code) → checks if code throws an exception
- Assert.IsType<T>(obj) → object is of a specific type 

4. Mocking

Use libraries like Moq to fake dependencies.
This allows you to isolate the method under test from external systems (DB, APIs).

```csharp
var mockService = new Mock<IMyService>();
mockService.Setup(s => s.GetValue()).Returns(10);

var sut = new MyClass(mockService.Object);
Assert.Equal(10, sut.UseService());
```
5. Arrange → Act → Assert Pattern

A common structure for tests:

- Arrange: Prepare objects, data, mocks.

- Act: Call the method under test.

- Assert: Verify the result.

6. Test Naming

Use clear names to describe the scenario and expected outcome.
Format: MethodName_Scenario_ExpectedResult

7. Setup and Teardown

Use constructor for setup.
Use Dispose for cleanup.

```csharp
public class MyTests : IDisposable
{
    public MyTests() { /* Arrange common data */ }
    public void Dispose() { /* Cleanup */ }
}
```

8. Collections and Class Fixtures
  Share context between multiple tests using IClassFixture<T>.