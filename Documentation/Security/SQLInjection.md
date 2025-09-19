# How SQL Injection Can Harm Your Code (with Examples)

## What is SQL Injection?

SQL Injection happens when:

You let user input go directly into a SQL statement without validation or parameters, and the attacker uses it to inject SQL code that you didn't intend to run.

## What Can Happen? (Dangers)

1. Bypass Login (Authentication Bypass)
Let’s say you write this:

```csharp
string sql = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
```
An attacker enters:

username: admin
password: ' OR '1'='1

SQL becomes:

```sql
SELECT * FROM Users WHERE Username = 'admin' AND Password = '' OR '1'='1'
Result? Login always succeeds because '1'='1' is always true.
```

2. Read All Data from the Table
If you allow:

```csharp
string sql = $"SELECT * FROM Products WHERE Name = '{name}'";
```
And the attacker types:
name: ' OR 1=1 --

It becomes:

```sql
SELECT * FROM Products WHERE Name = '' OR 1=1 --'
```
The -- means SQL ignores the rest.
So the query returns all products, even hidden or restricted ones.

3. Delete Data

If you use dynamic SQL like this:

```csharp
string sql = $"DELETE FROM Users WHERE Id = {userId}";
```
And someone passes:
userId = 1; DROP TABLE Users

It becomes:

```sql
DELETE FROM Users WHERE Id = 1; DROP TABLE Users
```
Boom! The entire Users table is gone.

4. Modify Data

```csharp
string sql = $"UPDATE Orders SET Status = 'Shipped' WHERE OrderId = {orderId}";
```
Attacker enters:

orderId = 5; UPDATE Orders SET Status = 'Cancelled'

# How to Protect Your Code

NEVER build SQL with string concatenation + user input

Always use parameterized queries

```csharp
var sql = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
var users = _context.Users
    .FromSqlRaw(sql, new SqlParameter("@username", username), new SqlParameter("@password", password))
    .ToList();

```
Or better: Use Entity Framework Linq:

```csharp
var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
```