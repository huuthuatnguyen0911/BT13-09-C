using System;
using System.Collections.Generic;

// Định nghĩa giao diện IBook
public interface IBook
{
    string Title { get; set; }
    string Author { get; set; }
    string Publisher { get; set; }
    int Year { get; set; }
    string ISBN { get; set; }
    List<string> Chapters { get; set; }
}

// Định nghĩa lớp Book kế thừa từ IBook
public class Book : IBook
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public string ISBN { get; set; }
    public List<string> Chapters { get; set; }

    public Book(string title, string author, string publisher, int year, string isbn)
    {
        Title = title;
        Author = author;
        Publisher = publisher;
        Year = year;
        ISBN = isbn;
        Chapters = new List<string>();
    }
}

// Định nghĩa lớp BookList để quản lý danh sách các cuốn sách
public class BookList
{
    private List<IBook> books;

    public BookList()
    {
        books = new List<IBook>();
    }

    public void AddBook(IBook book)
    {
        books.Add(book);
    }

    public void DisplayBooks()
    {
        Console.WriteLine("Danh sach cuon sach:");
        foreach (var book in books)
        {
            Console.WriteLine($"Tieu de: {book.Title}");
            Console.WriteLine($"Tac gia: {book.Author}");
            Console.WriteLine($"Nha xuat ban: {book.Publisher}");
            Console.WriteLine($"Nam xuat ban: {book.Year}");
            Console.WriteLine($"ISBN: {book.ISBN}");
            Console.WriteLine("Chuong sach:");
            foreach (var chapter in book.Chapters)
            {
                Console.WriteLine($"- {chapter}");
            }
            Console.WriteLine();
        }
    }

    public void SortBooksByAuthor()
    {
        books.Sort((book1, book2) => book1.Author.CompareTo(book2.Author));
    }

    public void SortBooksByTitle()
    {
        books.Sort((book1, book2) => book1.Title.CompareTo(book2.Title));
    }

    public void SortBooksByYear()
    {
        books.Sort((book1, book2) => book1.Year.CompareTo(book2.Year));
    }
}

class Program
{
    static void Main(string[] args)
    {
        BookList bookList = new BookList();

        // Nhập thông tin cho các cuốn sách
        Console.Write("Nhap so luong cuon sach: ");
        int numBooks = int.Parse(Console.ReadLine());

        for (int i = 0; i < numBooks; i++)
        {
            Console.WriteLine($"Nhap thong tin cho cuon sach thu {i + 1}:");
            Console.Write("Tieu de: ");
            string title = Console.ReadLine();
            Console.Write("Tac gia: ");
            string author = Console.ReadLine();
            Console.Write("Nha xuat ban: ");
            string publisher = Console.ReadLine();
            Console.Write("Nam xuat ban: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("ISBN: ");
            string isbn = Console.ReadLine();

            Book book = new Book(title, author, publisher, year, isbn);

            Console.Write("Nhap so luong chuong sach: ");
            int numChapters = int.Parse(Console.ReadLine());

            for (int j = 0; j < numChapters; j++)
            {
                Console.Write($"Nhap ten chuong sach thu {j + 1}: ");
                string chapter = Console.ReadLine();
                book.Chapters.Add(chapter);
            }

            bookList.AddBook(book);
            Console.WriteLine();
        }

        // Xuất danh sách thông tin cuốn sách
        bookList.DisplayBooks();

        // Sắp xếp và xuất danh sách theo các tiêu chí
        Console.WriteLine("Danh sach sau khi sap xep:");
        Console.WriteLine("Sap xep theo ten tac gia:");
        bookList.SortBooksByAuthor();
        bookList.DisplayBooks();

        Console.WriteLine("Sap xep theo tieu de sach:");
        bookList.SortBooksByTitle();
        bookList.DisplayBooks();

        Console.WriteLine("Sap xep theo nam xuat ban:");
        bookList.SortBooksByYear();
        bookList.DisplayBooks();
    }
}
