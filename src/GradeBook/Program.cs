using System;
using System.Collections.Generic;

namespace GradeBook
{


  class Program
  {
    static void Main(string[] args)
    {

      IBook book = new DiskBook("Reggie Grade Book");
      book.GradeAdded += OnGradeAdded;
      // book.GradeAdded += OnGradeAdded;
      // book.GradeAdded -= OnGradeAdded;
      // book.GradeAdded += OnGradeAdded;

      EnterGrades(book);
      // book.AddGrade(90.1);
      // book.AddGrade(89.1);
      // book.AddGrade(33.1);

      var stats = book.GetStatistics();

      // Console.WriteLine($"Category is {InMemoryBook.CATEGORY}");
      Console.WriteLine($"For the book named {book.Name}");
      Console.WriteLine($"The lowest grade is {stats.Low}");
      Console.WriteLine($"The highest grade is {stats.High}");
      Console.WriteLine($"The average grades is {stats.Average:N3}");
      Console.WriteLine($"The letter grade is {stats.Letter}");

      // if (args.Length > 0)
      // {
      //     Console.WriteLine($"Hello {args[0]}!");
      // } else {
      //     Console.WriteLine("Hello! " + result);
      // }

      static void OnGradeAdded(object sender, EventArgs e)
      {

        Console.WriteLine("A grade was added");

      }
    }

    private static void EnterGrades(IBook book)
    {
      while (true)
      {
        Console.WriteLine("Enter a grade or 'q' to quit");
        var input = Console.ReadLine();

        if (input == "q")
        {
          break;
        }

        try
        {

          var grade = double.Parse(input);
          book.AddGrade(grade);

        }
        catch (ArgumentException ex)
        {
          Console.WriteLine(ex.Message);
          throw;
        }
        catch (FormatException ex)
        {
          Console.WriteLine(ex.Message);
          throw;
        }
        finally
        {
          Console.WriteLine("**");
        }

      }
    }
  }
}
