using HomeWork_Library;
using HomeWork_Library.models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static string? myuser;
    private static string? mypass;

    static void Main(string[] args)
    {
        GetUsersFromDB();
        WriteUsersInDbFromJson();
       
        //var _context = new ApplicationDBContext();
        //var a = _context.Database.EnsureCreated();
    }

    private static void MyMenu()
    {
        lable:
        Console.WriteLine("\n Select 1 To Show All Books...\n " +
           "Select 2 To Show a Book you want it...\n " +
           "Select 3 To Delete a Book...\n " +
           "Select 4 To Update The Title And The Price To a Book... \n ");
        int x = Convert.ToInt32(Console.ReadLine());

        switch (x)
        {
            case 1:
                GetBooksFromDB();
                break;
            case 2:
                GetOneBookFromDB();
                break;
            case 3:
                DeleteBookInDb();
                break;
            case 4:
                UpdateBookFromDB();
                break;
        }
        goto lable;
    }

    private static void ReadUsernameAndPass()
    {   
        var context = new ApplicationDBContext();
    lable:
        Console.Write("Enter UserName: ");
        string x = Console.ReadLine()!;
        Console.Write("Enter Password: ");
        string y = Console.ReadLine()!;

        var myUser = context.Users!.FirstOrDefault(u => u.UserName == x && u.Password ==y);

        if(myUser != null)
        {
            MyMenu();
        }
        else
        {
            Console.WriteLine("\nthe username or password is wrong.....\n");
            goto lable;
        }     
    }

    private static void GetUsersFromDB()
    {
        using (var context = new ApplicationDBContext())
        {
            var users = context.Users?.ToList();
            foreach(var user in users!)
            {
                Console.WriteLine(user.UserName + " " + user.Password);
            }
        }
    }
    private static void WriteUsersInDbFromJson()
    {
        try {
            string myJson = File.ReadAllText("userPassword.json");
            var userDefault = JsonSerializer.Deserialize<List<User>>(myJson!);

            var context = new ApplicationDBContext();
            if (context.Database.EnsureCreated())
            {
                using (var user = new ApplicationDBContext())
                {
                    user.Users?.AddRangeAsync(userDefault!);
                    Console.WriteLine(myJson);
                    user.SaveChanges();

                }
                Console.WriteLine("\nAdded UserName And Passwprd is Success.....\n");
            }
            else
            {
                Console.WriteLine("\nDatabase is Created at Before\n");
                ReadUsernameAndPass();
            }
        }
        catch (Exception e) 
        {
            //Console.WriteLine(e);
            Console.WriteLine("\nSome values are already in the table ... will not added...\n");
            ReadUsernameAndPass();
        }

    }

    private static void GetBooksFromDB()
    {
        using (var context = new ApplicationDBContext())
        {
            var users = context.Books?.ToList();
            foreach (var user in users!)
            {
                Console.WriteLine(
                    "idBook : " + user.Id + " " +
                    " / Title Book : " + user.TitleBook + " " +
                    " / Author Name : " + user.AuthorName + " " +
                    " / Number of Copies : " + user.NumberCopies + " " +
                    " / Price Book : " + user.price + " " +
                    " / Date of Publication : " + user.DatePublication + "\n"
                    );
            }
        }
    }

    private static void GetOneBookFromDB()
    {
        using (var context = new ApplicationDBContext())
        {
            var users = context.Books?.ToList();
            foreach (var user in users!)
            {
                Console.WriteLine($"Title Book : " + user.TitleBook + " / ID is : " + user.Id +"\n" );
            }

            Console.WriteLine("Enter ID the Book");
            int x = Convert.ToInt32(Console.ReadLine());

            var showBook = context
                .Books?
                .Where(e => e.Id.Equals(x))
                .TagWith("Hello every body.....")
                .ToList();

            foreach (var user in showBook!)
            { 
                Console.WriteLine(
                    "idBook : " + user.Id + " " +
                    " / Title Book : " + user.TitleBook + " " +
                    " / Author Name : " + user.AuthorName + " " +
                    " / Number of Copies : " + user.NumberCopies + " " +
                    " / Price Book : " + user.price + " " +
                    " / Date of Publication : " + user.DatePublication + "\n"
                    );
            }     
        }
    }

    private static void UpdateBookFromDB()
    {
        using (var context = new ApplicationDBContext())
        {
            var books = context.Books?.ToList();
            foreach (var book in books!)
            {
                Console.WriteLine(
                    $"Title Book : " + book.TitleBook + 
                    " / Price Book is : " + book.price +
                    " / ID is : " + book.Id + "\n");
            }

            Console.WriteLine("Enter ID The Book To Update The Title And The Price.... ");
            int x = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the New Title Book....");
            string y = Console.ReadLine()!;
            
            Console.WriteLine("Enter the New Price Book....");
            int z = Convert.ToInt32(Console.ReadLine());

            var UpdateBook = context
                .Books?
                .FirstOrDefault(e => e.Id.Equals(x));
            //.FirstOrDefault(e => e.TitleBook == y);

            UpdateBook!.TitleBook = y;
            UpdateBook!.price = z;

            context.SaveChanges();

            Console.WriteLine("Update Success......");
        }
    }

    private static void DeleteBookInDb()
    {
        using (var context = new ApplicationDBContext())
        {
            //// to show books title and books id
            var books = context.Books?.ToList();
            foreach (var book in books!)
            {
                Console.WriteLine($"Title Book : " + book.TitleBook + " / ID is : " + book.Id + "\n");
            }

            //// read id
            Console.WriteLine("Enter ID Book for Delete");
            int x = Convert.ToInt32(Console.ReadLine());

            var deleteBook = context
                .Books?
                .FirstOrDefault(e => e.Id.Equals(x));
          
            context.Books?.Remove(deleteBook!);
            
            context.SaveChanges();

            Console.WriteLine("Delete Success......");
        }
    }
}