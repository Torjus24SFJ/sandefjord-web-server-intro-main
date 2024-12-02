var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Library library = new Library();

Book martian = new Book("Martian", "Jack Black", new DateTime(2002, 10, 10));
Book foundation = new Book("Foundation", "Jane Doe", new DateTime(1940, 04, 05));
Book LordOfTheRings = new Book("LordOfTheRings", "JRR Tolken", new DateTime(1954, 12, 24));
library.AddNewBook(martian);
library.AddNewBook(foundation);
library.AddNewBook(LordOfTheRings);

app.MapGet("/book", () =>
{
  return library.ListAllBooks();
});

app.MapGet("/book/available", () =>
{
  return library.ListAvailableBooks();
});

app.MapPost("/book/borrow", (BorrowRequest request) =>
{
  Book? book = library.BorrowBook(request.Title);

  if (book == null)
  {
    return Results.NotFound();
  }
  else
  {
    return Results.Ok(book);
  }
});

app.Run();
