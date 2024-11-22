/*using System;
using System.Collections.Generic;

public enum AvailabilityStatus
{
    Available,
    CheckedOut
}

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string ISBN { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }

    public Book(string title, string author, string genre, string isbn)
    {
        Title = title;
        Author = author;
        Genre = genre;
        ISBN = isbn;
        AvailabilityStatus = AvailabilityStatus.Available;
    }

    public void ChangeAvailabilityStatus(AvailabilityStatus status)
    {
        AvailabilityStatus = status;
    }

    public void GetBookInfo()
    {
        Console.WriteLine($"Атауы: {Title}, Авторы: {Author}, Жанры: {Genre}, ISBN: {ISBN}, Статус: {AvailabilityStatus}");
    }
}

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public virtual void Register() { }
    public virtual void Login() { }
}

public class Reader : User
{
    public Reader(string id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public void BorrowBook(Book book)
    {
        if (book.AvailabilityStatus == AvailabilityStatus.Available)
        {
            book.ChangeAvailabilityStatus(AvailabilityStatus.CheckedOut);
            Console.WriteLine($"{Name} кітапты алды: {book.Title}");
        }
        else
        {
            Console.WriteLine($"Кітап {book.Title} қолжетімді емес.");
        }
    }

    public void ReturnBook(Book book)
    {
        book.ChangeAvailabilityStatus(AvailabilityStatus.Available);
        Console.WriteLine($"{Name} кітапты қайтарды: {book.Title}");
    }
}

public class Librarian : User
{
    public Librarian(string id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public void IssueBook(Book book, Reader reader)
    {
        reader.BorrowBook(book);
    }

    public void ReturnBook(Book book, Reader reader)
    {
        reader.ReturnBook(book);
    }
}

public class Loan
{
    public Book Book { get; set; }
    public Reader Reader { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Loan(Book book, Reader reader)
    {
        Book = book;
        Reader = reader;
        LoanDate = DateTime.Now;
    }

    public void IssueLoan()
    {
        Console.WriteLine($"Кітапқа {Book.Title} оқырманға {Reader.Name} берілді.");
    }

    public void ReturnLoan()
    {
        ReturnDate = DateTime.Now;
        Console.WriteLine($"Кітап қайтарылды: {Book.Title}, оқырман: {Reader.Name}");
    }
}

public class Library
{
    public List<Book> Books { get; set; }
    public List<User> Users { get; set; }
    public List<Loan> Loans { get; set; }

    public Library()
    {
        Books = new List<Book>();
        Users = new List<User>();
        Loans = new List<Loan>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"Кітап қосылды: {book.Title}");
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
        Console.WriteLine($"Кітап жойылды: {book.Title}");
    }

    public Book SearchBook(string query)
    {
        foreach (var book in Books)
        {
            if (book.Title.Contains(query) || book.Author.Contains(query))
            {
                return book;
            }
        }
        return null;
    }

    public void GenerateReport()
    {
        Console.WriteLine("Кітапхана есебін жасау...");
    }
}

public interface IBookOperations
{
    void ChangeAvailabilityStatus(AvailabilityStatus status);
    void GetBookInfo();
}

public interface IUserOperations
{
    void Register();
    void Login();
}

public interface ILoanOperations
{
    void IssueLoan();
    void ReturnLoan();
}

public interface ILibraryOperations
{
    void AddBook(Book book);
    void RemoveBook(Book book);
    Book SearchBook(string query);
    void GenerateReport();
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        Book book1 = new Book("Cанлык сахабалар", "А Каримов", "сахабалар", "54615163");
        Book book2 = new Book("Олар пайгамбарымызды осылай суйген", "Нурсултан устаз", "Омирбаян", "541268");
        library.AddBook(book1);
        library.AddBook(book2);

        Reader reader = new Reader("1", "раушан", "raushab@gmail.com");
        Librarian librarian = new Librarian("2", "данияр", "dani@bk.com");

        librarian.IssueBook(book1, reader);
        reader.ReturnBook(book1);

        var foundBook = library.SearchBook("сахабалар");
        if (foundBook != null)
        {
            foundBook.GetBookInfo();
        }

        library.GenerateReport();
    }
}*/


using System;
using System.Collections.Generic;

public interface IBookOperations
{
    void GetBookInfo();
    void ChangeStatus(AvailabilityStatus status);
}

public interface ILoanSystem
{
    void IssueLoan(Reader reader, Book book);
    void ReturnLoan(Reader reader, Book book);
}

public interface ICatalog
{
    Book SearchBook(string query);
    List<Book> FilterBooks(string genre);
}

public interface IUserOperations
{
    void BorrowBook(Book book);
    void ReturnBook(Book book);
}

public class Book : IBookOperations
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public string ISBN { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }

    public Book(string title, string author, string genre, string isbn)
    {
        Title = title;
        Author = author;
        Genre = genre;
        ISBN = isbn;
        AvailabilityStatus = AvailabilityStatus.Available;
    }

    public void GetBookInfo()
    {
        Console.WriteLine($"Атауы: {Title}, Автор: {Author}, Жанры: {Genre}, ISBN: {ISBN}, Мәртебесі: {AvailabilityStatus}");
    }

    public void ChangeStatus(AvailabilityStatus status)
    {
        AvailabilityStatus = status;
    }
}

public class Reader : IUserOperations
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string TicketNumber { get; set; }

    public Reader(string name, string surname, string ticketNumber)
    {
        Name = name;
        Surname = surname;
        TicketNumber = ticketNumber;
    }

    public void BorrowBook(Book book)
    {
        if (book.AvailabilityStatus == AvailabilityStatus.Available)
        {
            book.ChangeStatus(AvailabilityStatus.CheckedOut);
            Console.WriteLine($"{Name} {Surname} кітапты алды: {book.Title}");
        }
        else
        {
            Console.WriteLine($"Кітап {book.Title} қолжетімсіз.");
        }
    }

    public void ReturnBook(Book book)
    {
        book.ChangeStatus(AvailabilityStatus.Available);
        Console.WriteLine($"{Name} {Surname} кітапты қайтарды: {book.Title}");
    }
}

public class Librarian : IUserOperations
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public Librarian(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public void BorrowBook(Book book)
    {
        Console.WriteLine("Кітапханашылар кітап ала алмайды.");
    }

    public void ReturnBook(Book book)
    {
        Console.WriteLine("Кітапханашылар кітап қайтармайды.");
    }

    public void IssueBook(Reader reader, Book book)
    {
        reader.BorrowBook(book);
    }

    public void ReturnBook(Reader reader, Book book)
    {
        reader.ReturnBook(book);
    }
}

public class LoanSystem : ILoanSystem
{
    public void IssueLoan(Reader reader, Book book)
    {
        reader.BorrowBook(book);
    }

    public void ReturnLoan(Reader reader, Book book)
    {
        reader.ReturnBook(book);
    }
}

public class Catalog : ICatalog
{
    private List<Book> _books;

    public Catalog()
    {
        _books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public Book SearchBook(string query)
    {
        foreach (var book in _books)
        {
            if (book.Title.Contains(query) || book.Author.Contains(query))
            {
                return book;
            }
        }
        return null;
    }

    public List<Book> FilterBooks(string genre)
    {
        var filteredBooks = new List<Book>();
        foreach (var book in _books)
        {
            if (book.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
            {
                filteredBooks.Add(book);
            }
        }
        return filteredBooks;
    }
}

public enum AvailabilityStatus
{
    Available,
    CheckedOut
}

public class Library
{
    public Catalog Catalog { get; set; }
    public LoanSystem LoanSystem { get; set; }
    public List<Book> Books { get; set; }
    public List<Reader> Readers { get; set; }
    public List<Librarian> Librarians { get; set; }

    public Library()
    {
        Catalog = new Catalog();
        LoanSystem = new LoanSystem();
        Books = new List<Book>();
        Readers = new List<Reader>();
        Librarians = new List<Librarian>();
    }

    public void AddBook(Book book)
    {
        Catalog.AddBook(book);
        Books.Add(book);
    }

    public void AddReader(Reader reader)
    {
        Readers.Add(reader);
    }

    public void AddLibrarian(Librarian librarian)
    {
        Librarians.Add(librarian);
    }

    public void GetReport()
    {
        Console.WriteLine("Кітапхана туралы есеп дайындалды.");
    }
}

class Program
{
    static void Main()
    {
        var library = new Library();

        var book1 = new Book("любов", "Раушан ким", "баевик", "518651");
        var book2 = new Book("бузау", "нурик щин", "романтикау", "54846");

        library.AddBook(book1);
        library.AddBook(book2);

        var reader1 = new Reader("Досбол", "ахметов", "R001");
        var librarian = new Librarian("Айдын", "жанмодаев");

        library.AddReader(reader1);
        library.AddLibrarian(librarian);

        librarian.IssueBook(reader1, book1);
        reader1.ReturnBook(book1);

        var foundBook = library.Catalog.SearchBook("бузау");
        if (foundBook != null)
        {
            foundBook.GetBookInfo();
        }

        library.GetReport();
    }
}

