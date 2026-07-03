using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace June2026.OOPPractice
{
    public class StudentService
    {
        public void CelebrateBirthday(Student student)
        {
            ++student.Age;
        }

        public void ChangeStudentName(Student student, string newName)
        {
            student.Name = newName;
        }

        public bool IsAdult(Student student)
        {
            return student.Age >= 18 ? true : false;
        }
    }
    

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }


        public Student(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }

    public static class StudentPrinter
    {
        public static void PrintMe(Student stu)
        {
            Console.WriteLine($"Id: {stu.Id} , Name: {stu.Name} , Age: {stu.Age}");
        }
    }
}
