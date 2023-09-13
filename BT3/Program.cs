using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main()
    {
        List<Student> students = new List<Student>();

        Console.Write("Enter the number of students: ");
        int numberOfStudents = int.Parse(Console.ReadLine());

        for (int i = 0; i < numberOfStudents; i++)
        {
            Console.WriteLine($"Enter information for student {i + 1}:");

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());

            students.Add(new Student { ID = id, Name = name, Age = age });
        }

        students = students.OrderBy(s => s.Name).ToList();

        Console.WriteLine("\nSorted list of students:");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}");
        }
    }
}
