What is Pagination?
Pagination is the process of dividing a large set of data into smaller chunks (pages), so the user or client can view the data page by page instead of loading everything at once.

Why Use Pagination?
Improves performance: Loads only a subset of data at a time, reducing memory and network usage.

Better user experience: Makes it easier to navigate large datasets.

Reduces server load: Limits database queries and bandwidth.

Common Pagination Concepts
Term	Description
Page Number	The current page index (e.g., 1, 2, 3...)
Page Size	Number of items per page (e.g., 10, 20, 50)
Total Records	Total number of items in the dataset
Total Pages	Total pages = ceil(Total Records / Page Size)

How Pagination Works (Basic Logic)
csharp
Copy
Edit
int pageNumber = 1; // e.g., from query string
int pageSize = 10;

var pagedData = dbContext.Items
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();
Skip: Skips the records before the current page.

Take: Takes only the number of records for the current page.

Pagination in APIs
When building an API, you typically:

Accept pageNumber and pageSize as query parameters.

Return the current page data along with metadata like total pages or total records.

Example response:

json
Copy
Edit
{
  "data": [...], // items for this page
  "pageNumber": 1,
  "pageSize": 10,
  "totalRecords": 125,
  "totalPages": 13
}
Types of Pagination
Offset Pagination (most common): uses Skip/Take or OFFSET/FETCH in SQL.

Cursor Pagination: uses a pointer or token for the last fetched record, more efficient for large datasets.

Keyset Pagination: based on indexed columns for faster queries.