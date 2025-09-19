# The Warning
EF Core shows a decimal precision warning like this:

“The property 'X' is of type 'decimal' which will be mapped to a database column with default precision and scale. Consider using HasPrecision to specify the precision explicitly to avoid rounding or truncation.”

Even if the default is decimal(18,2), EF Core still warns you. Why?

# Why the warning happens:
Because EF Core wants you to be explicit, not rely on the database provider's defaults, which may vary (especially across SQL Server, PostgreSQL, SQLite, etc.).

Even though:
SQL Server usually defaults to decimal(18, 2)

You're assigning values like 100.50m

EF Core can't guarantee that the database will store it as 18,2 unless you explicitly set it.

# How to fix it
To remove the warning, explicitly tell EF Core what precision you want:

builder.Property(p => p.Price).HasPrecision(18, 2);

You can do this in:
The OnModelCreating method

# What if I don’t do it?

EF Core will still generate the column as decimal(18, 2) on most databases (like SQL Server).

But:

You might get rounding if your value has more than 2 decimal places.

Or, on a different provider or future EF version, you might get something unexpected.