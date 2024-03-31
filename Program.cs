using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CIT_195_Lesson_8_LINQ_Method
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public string Major { get; set; }
        public double Tuition { get; set; }
    }
    public class StudentClubs
    {
        public int StudentID { get; set; }
        public string ClubName { get; set; }
    }
    public class StudentGPA
    {
        public int StudentID { get; set; }
        public double GPA { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 2, StudentName = "Ivan Ulcer", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 3, StudentName = "Anita Walker",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 4, StudentName = "Avery Oldman",  Age = 58, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 5, StudentName = "Eileen Wright",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 7, StudentName = "Ima Flunkin" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
            };
            // Student GPA Collection
            IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
            // Club collection
            IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
            };

            var groupedResult = studentGPAList.GroupBy(s => s.GPA);

            var groupedByClub = studentClubList
                .OrderBy(sc => sc.ClubName)
                .GroupBy(sc => sc.ClubName);

            var count = studentGPAList
                .Where(s => s.GPA >= 2.5 && s.GPA <= 4.0)
                .Count();

            var averageTuition = studentList
                .Average(s => s.Tuition);

            var highestTuition = studentList.Max(s => s.Tuition);

            var joinedStudents = studentList.Join(
                studentGPAList,
                student => student.StudentID,
                gpa => gpa.StudentID,
                (student, gpa) => new
                {
                    student.StudentName,
                    student.Major,
                    gpa.GPA
                });

            var gameClubbers = studentList.Join(
                studentClubList,
                student => student.StudentID,
                club => club.StudentID,
                (student, club) => new 
                { 
                    student.StudentName, club.ClubName 
                })
                .Where(joinResult => joinResult.ClubName == "Game")
                .Select(joinResult => joinResult.StudentName);

            Console.WriteLine("group by GPA and display student IDs:");
            foreach (var student in groupedResult)
            {
                Console.WriteLine("Group GPA: " + student.Key);
                foreach (var studentGPA in student)
                {
                    Console.WriteLine("Student ID: " + studentGPA.StudentID);
                };
            }
            Console.WriteLine();
            Console.WriteLine("----------------------");

            Console.WriteLine("sort by club, group by club and display student IDs:");
            foreach (var group in groupedByClub)
            {
                Console.WriteLine($"Club: {group.Key}");
                Console.WriteLine("Student IDs:");
                foreach (var studentClub in group)
                {
                    Console.WriteLine(studentClub.StudentID);
                }
            }
            Console.WriteLine("----------------------");

            Console.WriteLine($"Number of students with GPA between 2.5 and 4.0: {count}");
            Console.WriteLine("----------------------");

            Console.WriteLine($"Average tuition: {averageTuition:C}");
            Console.WriteLine("----------------------");

            Console.WriteLine("Student paying the highest tuition:");
            foreach (var student in studentList)
            {
                if (student.Tuition == highestTuition)
                {
                    Console.WriteLine($"Student Name: {student.StudentName}");
                    Console.WriteLine($"Major: {student.Major}");
                    Console.WriteLine($"Tuition: {student.Tuition:C}");
                    Console.WriteLine($"Age: {student.Age}"); 
                    Console.WriteLine($"ED: {student.StudentID}");
                }
            }
            Console.WriteLine("----------------------");

            Console.WriteLine("Joined student lists:");
            foreach (var student in joinedStudents)
            {
                Console.WriteLine($"Student Name: {student.StudentName}");
                Console.WriteLine($"Major: {student.Major}");
                Console.WriteLine($"GPA: {student.GPA}" + (student.GPA == 1 ? " but she's trying super hard!" : ""));
                Console.WriteLine();
            }
            Console.WriteLine("----------------------");

            Console.WriteLine("Students in the Game Club:");
            foreach (var studentName in gameClubbers)
            {
                Console.WriteLine(studentName);
            }
            Console.WriteLine("----------------------");
        }
    }
}
