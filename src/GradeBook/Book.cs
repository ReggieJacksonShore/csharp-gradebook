using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{

  public delegate void GradeAddedDelegate(object sender, EventArgs args);

  public class NamedObject
  {
    public NamedObject(string name)
    {
      Name = name;
    }

    public string Name
    {
      get;
      set;
    }

  }

  public interface IBook
  {
    void AddGrade(double grade);
    Statistics GetStatistics();
    string Name { get; }
    event GradeAddedDelegate GradeAdded;
  }

  public abstract class Book : NamedObject, IBook
  {
    public Book(string name) : base(name)
    {
    }

    public abstract event GradeAddedDelegate GradeAdded;

    public abstract void AddGrade(double grade);

    public abstract Statistics GetStatistics();

  }

  public class DiskBook : Book
  {
    public DiskBook(string name) : base(name)
    {
    }

    public override event GradeAddedDelegate GradeAdded;

    public override void AddGrade(double grade)
    {
      using (var writer = File.AppendText($"{Name}.txt"))
      {
        writer.WriteLine(grade);
        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }
      }
    }

    public override Statistics GetStatistics()
    {
      var result = new Statistics();
      using (var reader = File.OpenText($"{Name}.txt"))
      {
        var line = reader.ReadLine();
        while (line != null)
        {
          var number = double.Parse(line);
          result.Add(number);
          line = reader.ReadLine();
        }
      }
      return result;
    }
  }

  public class InMemoryBook : Book
  {

    public InMemoryBook(string name) : base(name)
    {
      grades = new List<double>();
      Name = name;
    }

    public void AddGrade(char letter)
    {
      switch (letter)
      {
        case 'A':
          AddGrade(90);
          break;

        case 'B':
          AddGrade(80);
          break;

        case 'C':
          AddGrade(70);
          break;

        default:
          AddGrade(0);
          break;


      }

    }

    public override void AddGrade(double grade)
    {
      if (grade <= 100 && grade >= 0)
      {

        grades.Add(grade);

        if (GradeAdded != null)
        {
          GradeAdded(this, new EventArgs());
        }


      }
      else
      {
        // Console.WriteLine("Invalid value!");
        throw new ArgumentException($"Invalid {nameof(grade)}");
      }

    }


    public override event GradeAddedDelegate GradeAdded;

    public override Statistics GetStatistics()
    {

      var result = new Statistics();

      // var numbers = new[] { 12.7, 10.3, 4.1, 5.8 }; 
      // List<double> grades = new List<double>();
      // var grades = new List<double>() { 12.7, 10.3, 4.1, 5.8 }; 
      // grades.Add(56.1);


      // foreach(var grade in grades){
      //     result.High = Math.Max(grade, result.High);
      //     result.Low = Math.Min(grade, result.Low);
      //     result.Average += grade;
      // }


      for (var index = 0; index < grades.Count; index += 1)
      {
        if (grades[index] == 42.1)
        {
          continue;

        }

        result.Add(grades[index]);
        // result.Average += grades[index];

      }



      return result;




    }


    private List<double> grades;

    public const string CATEGORY = "Science";

  }
}