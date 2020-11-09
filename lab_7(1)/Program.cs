using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//+Создать иерархию классов исключений(собственных) – 3 типа и более.
//+Сделать наследование пользовательских типов исключений от стандартных
//+классов.Net(например, Exception, IndexOutofRange).
//Сгенерировать и обработать как минимум пять различных исключительных
//ситуаций на основе своих и стандартных исключений.Например, не позволять при
//инициализации объектов передавать неверные данные, обрабатывать ошибки при
//работе с памятью и ошибки работы с файлами, деление на ноль, неверный индекс,
//нулевой указатель и т.д.
//В конце поставить универсальный обработчик catch.
//Обработку исключений вынести в main.При обработке выводить
//специфическую информацию о месте, диагностику и причине исключения.
//Последним должен быть блок, который отлавливает все исключения (finally).
//Добавьте код в одной из функций макрос Assert.Объясните что он проверяет, как
//будет выполняться программа в случае не выполнения условия.Объясните
//назначение Assert.

namespace lab_7_1_
{
    class EmptyTitle : NullReferenceException //не  ввели название книги
    {
        public EmptyTitle(string message) : base(message)
        { }
    }
    class ImpossibleYear : ArgumentOutOfRangeException //этот год еще не наступил
    {
        public ImpossibleYear(string message) : base(message)
        { }
    }
    class DeleteElementException : Exception //попытка удаления несуществующего элемента
    {
        public DeleteElementException(string message) : base(message)
        { }
    }
    abstract class GeneralInfo
    {
        public string Title { get; set; }
        public string Country { get; set; }
        public int CurrentYear = DateTime.Now.Year;
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Cover { get; set; }
        public double Price { get; set; }
    }
    interface ICommentable
    {
        void Comment();
    }
    abstract class Comments
    {
        public abstract void Comment();
    }
    partial class PrintEdition
    {
        public string NameOfEdition { get; set; }
        public int HashCode { get; set; }
        public PrintEdition(string name)
        {
            NameOfEdition = name;
            HashCode = GetHashCode();
        }
        public override string ToString()
        {
            return NameOfEdition + "(hash code: " + HashCode + ")";
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            PrintEdition print = (PrintEdition)obj;
            return (this.NameOfEdition == print.NameOfEdition);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //Get type переписать нельзя
    }
    class Book : GeneralInfo
    {
        public Book(string title, string country, int year, int pages, string cover, double price)
        {
            if (title == "" || title == null)
            {
                throw new EmptyTitle("You didn't write title of the book(");
            }
            else
            {
                Title = title;
            }
            Country = country;
            if (year > CurrentYear)
            {
                throw new ImpossibleYear("Are you sure? This year has not come yet...");
            }
            Year = year;
            Pages = pages;
            Cover = cover;
            Price = price;
        }
        public override string ToString()
        {
            return "~~~~~~~~~~Information about book~~~~~~~~~~\nTitle: " + Title + "\nYear: " + Year + "\nPages: " + Pages + "\nCountry: " + Country + "\nPrice: " + Price;
        }
    }
    class Magazin : GeneralInfo
    {
        public Magazin(string title, string country, int year, int pages, string cover, double price)
        {
            if (title == "" || title == null)
            {
                throw new EmptyTitle("You didn't write title of the magazin(");
            }
            else
            {
                Title = title;
            }
            Country = country;
            if (year > CurrentYear)
            {
                throw new ImpossibleYear("Are you sure? This year has not come yet...");
            }
            Pages = pages;
            Cover = cover;
            Price = price;
        }
        public override string ToString()
        {
            return "~~~~~~~~~~Information about magazin~~~~~~~~~~\nTitle: " + Title + "\nYear: " + Year + "\nPages: " + Pages + "\nCountry: " + Country + "\nPrice: " + Price;
        }
    }
    class SchoolBook : GeneralInfo
    {
        public SchoolBook(string title, string country, int year, int pages, string cover, double price)
        {
            if (title == "" || title == null)
            {
                throw new EmptyTitle("You didn't write title of the school book(");
            }
            else
            {
                Title = title;
            }
            Country = country;
            if (year > CurrentYear)
            {
                throw new ImpossibleYear("Are you sure? This year has not come yet...");
            }
            Pages = pages;
            Cover = cover;
            Price = price;
        }
        public override string ToString()
        {
            return "~~~~~~~~~~Information about school book~~~~~~~~~~\nTitle: " + Title + "\nYear: " + Year + "\nPages: " + Pages + "\nCountry: " + Country + "\nPrice: " + Price;
        }
    }
    sealed class Author : Comments, ICommentable
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public override string ToString()
        {
            return "Author: " + Name + " " + Surname;
        }
        public override void Comment()
        {
            Console.WriteLine("The famous author!");
        }
    }
    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Person(string name = "Good", string surname = "Man")
        {
            Name = name;
            Surname = surname;
        }
        public override string ToString()
        {
            return "Person: " + Name + " " + Surname;
        }
    }
    class Publishing : Comments, ICommentable
    {
        public string NamePublish { get; set; }
        public Publishing(string publish)
        {
            NamePublish = publish;
        }
        public override string ToString()
        {
            return "Publishing house: " + NamePublish + "\n";
        }
        public override void Comment()
        {
            Console.WriteLine("The best publishing house!");
        }
    }
    public class Printer
    {
        public string IAmPrinting(Object obj)
        {
            return obj.ToString();
        }
    }
    class Library
    {
        private List<GeneralInfo> info = new List<GeneralInfo>();
        public static int count = 0;
        public static double fullpr = 0;
        public List<GeneralInfo> Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
            }
        }
        public void Add(GeneralInfo element)
        {
            Info.Add(element);
            if (element as SchoolBook != null)
            {
                count++;
            }
            fullpr = fullpr + element.Price;
            Debug.Assert(fullpr < 0);//а вдруг кто-то введет отрицательную цену?
        }
        public void Delete(GeneralInfo element)
        {
            if (Library.count == 0)
            {
                throw new DeleteElementException("There are no elements in library list");
            }
            else
            {
                Info.Remove(element);
                if (element as SchoolBook != null)
                {
                    count--;
                }
                fullpr = fullpr - element.Price;
            }
        }
        public void ListOut()
        {
            foreach (GeneralInfo x in Info)
            {
                if (x as Book != null)
                {
                    Book book = x as Book;
                    Console.WriteLine(book.ToString());
                }
                else if (x as SchoolBook != null)
                {
                    SchoolBook school = x as SchoolBook;
                    Console.WriteLine(school.ToString());
                }
                else if (x as Magazin != null)
                {
                    Magazin magazin = x as Magazin;
                    Console.WriteLine(magazin.ToString());
                }
            }
        }
    }
    class Control : Library
    {
        public void CountSchool()
        {
            Console.WriteLine("Number of school books in library: " + count);
        }
        public void FullPrice()
        {
            Console.WriteLine("Price of all: " + fullpr);
        }
    }
    enum DaysOfWork //перечисление
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday = Saturday
    }
    struct LibraryAddress //cтруктура
    {
        public int House;
        public string Street;
        public string City;
        public LibraryAddress(string city, string street, int house)
        {
            City = city;
            Street = street;
            House = house;
        }
        public void AddressInfo()
        {
            Console.WriteLine($"Address of library: {City}, {Street}, {House}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PrintEdition printEdition1 = new PrintEdition("Book");
            Book book1 = new Book("Harry Potter and the Prisoner of Azkaban", "United Kingdom", 1999, 464, "hard", 30);
            Author author1 = new Author("Joanne", "Rowling");
            Publishing publishing1 = new Publishing("Bloomsbury");

            PrintEdition printEdition2 = new PrintEdition("Magazin");
            Magazin magazin2 = new Magazin("Vogue", "USA", 1998, 73, "soft", 10.5);
            //Magazin magazin2 = new Magazin("Vogue", "USA", 1998, 73, "soft", -20.5);
            Author author2 = new Author("Alena", "Doletskaya");
            Publishing publishing2 = new Publishing("Condé Nast");

            PrintEdition printEdition3 = new PrintEdition("School Book");
            SchoolBook schoolBook3 = new SchoolBook("Upstream. Elementary A2 Student's Book", "United Kingdom", 2005, 128, "soft", 12.5);
            Author author3 = new Author("Virginia", "Evans");
            Publishing publishing3 = new Publishing("Express Publishing");

            Printer Printer = new Printer();
            Object[] arr = new Object[] { printEdition1, book1, author1, publishing1, printEdition2, magazin2, author2, publishing2, printEdition3, schoolBook3, author3, publishing3 };

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(Printer.IAmPrinting(arr[i]));
            }
            author1.Comment();
            publishing3.Comment();

            Console.WriteLine();
            Console.Write(author1.Name + " " + author1.Surname);
            if (author1 is ICommentable)
                Console.WriteLine(" is a very famous author");
            else
                Console.WriteLine("is a beginner");

            Console.WriteLine();
            printEdition1.Equals(magazin2);


            Console.WriteLine("\n\n\n\n\n~~~~~LAB_6~~~~~\n");

            Library lib1 = new Library();
            lib1.Add(book1);
            lib1.Add(magazin2);
            lib1.Add(schoolBook3);
            lib1.ListOut();

            Console.WriteLine("\n\nControl: ");
            Control control = new Control();
            control.CountSchool();
            control.FullPrice();


            Console.WriteLine("\n\n\n\n\n~~~~~LAB_7~~~~~\n");

            try
            {
                lib1.Delete(schoolBook3);
            }
            catch (DeleteElementException ex)
            {
                Console.WriteLine(ex.Message + "\nMethod with exception: " + ex.TargetSite + "\n" + ex.StackTrace + "\n\n");
            }

            try
            {
                Book book12 = new Book("", "United Kingdom", 1999, 464, "hard", 30);
            }
            catch (EmptyTitle ex)
            {
                Console.WriteLine(ex.Message + "\nMethod with exception: " + ex.TargetSite + "\n" + ex.StackTrace + "\n\n");
            }

            try
            {
                Magazin magazin22 = new Magazin("Vogue", "USA", 2021, 73, "soft", 10.5);
            }
            catch (ImpossibleYear ex)
            {
                Console.WriteLine(ex.Message + "\nMethod with exception: " + ex.TargetSite + "\n" + ex.StackTrace + "\n\n");
            }

            try
            {
                SchoolBook schoolBook32 = new SchoolBook("Upstream. Elementary A2 Student's Book", "United Kingdom", 2005, 128, "soft", 12.5);//год подходит
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\nMethod with exception: " + ex.TargetSite + "\n" + ex.StackTrace + "\n\n");
            }

            try
            {
                int a = 4;
                int b = 0;
                a = a / b;
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message + "\nMethod with exception: " + ex.TargetSite + "\n" + ex.StackTrace + "\n\n");
            }

            finally
            {
                Console.WriteLine("The work is done!!!\nI hope so...\n\n");
            }
        }
    }
}
