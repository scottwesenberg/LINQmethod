namespace assignment8ex5
{
    class Program {
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



        static void Main(string[] args)
        {
            // Student collection
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 1, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 2, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 3, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 5, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
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
            // Group by gpa and display
            var studentGroupedByGPA = studentGPAList.Join(studentList,
                                    sGPA => sGPA.StudentID,
                                    s => s.StudentID,
                                    (sGPA, s) => new { s.StudentID, s.StudentName, sGPA.GPA })
                                .GroupBy(s => s.GPA)
                                .Select(g => new { GPA = g.Key, StudentIDs = g.Select(s => s.StudentID) });

            foreach (var gpaGroup in studentGroupedByGPA)
            {
                Console.WriteLine("Students with GPA {0}:", gpaGroup.GPA);
                foreach (int studentID in gpaGroup.StudentIDs)
                {
                    Console.WriteLine("\tStudent ID: {0}", studentID);
                }
            }

            // sort by club, group and display
            Console.WriteLine();
            var studentGroupedByClub = studentClubList.Join(studentList,
                                    sClub => sClub.StudentID,
                                    s => s.StudentID,
                                    (sClub, s) => new { s.StudentID, s.StudentName, sClub.ClubName })
                                .OrderBy(s => s.ClubName)
                                .GroupBy(s => s.ClubName)
                                .Select(g => new { Club = g.Key, StudentIDs = g.Select(s => s.StudentID) });

            foreach (var clubGroup in studentGroupedByClub)
            {
                Console.WriteLine("Students in {0} club:", clubGroup.Club);
                foreach (int studentID in clubGroup.StudentIDs)
                {
                    Console.WriteLine("\tStudent ID: {0}", studentID);
                }
            }
            // count students with gpa between 2.5 & 4
            Console.WriteLine();
            int studentCount = studentGPAList.Join(studentList,
                            sGPA => sGPA.StudentID,
                            s => s.StudentID,
                            (sGPA, s) => new { s.StudentID, sGPA.GPA })
                        .Where(s => s.GPA >= 2.5 && s.GPA <= 4.0)
                        .Count();

            Console.WriteLine("Number of students with GPA between 2.5 and 4.0: {0}", studentCount);

            // average tuition
            Console.WriteLine();
            double averageTuition = studentList.Average(s => s.Tuition);

            Console.WriteLine("Average tuition: {0}", averageTuition);

            //highest tuition
            Console.WriteLine();
            double highestTuition = 0;
            Student highestTuitionStudent = null;

            foreach (Student student in studentList)
            {
                if (student.Tuition > highestTuition)
                {
                    highestTuition = student.Tuition;
                    highestTuitionStudent = student;
                }
            }

            Console.WriteLine("Student with highest tuition:");
            Console.WriteLine("Name: " + highestTuitionStudent.StudentName);
            Console.WriteLine("Major: " + highestTuitionStudent.Major);
            Console.WriteLine("Tuition: $" + highestTuitionStudent.Tuition);

            // linq inner-join display
            Console.WriteLine();
            Console.WriteLine("Student information:");
            var joinedList = from student in studentList
                             join gpa in studentGPAList on student.StudentID equals gpa.StudentID
                             select new { student.StudentName, student.Major, gpa.GPA };

            foreach (var item in joinedList)
            {
                Console.WriteLine("Name: " + item.StudentName);
                Console.WriteLine("Major: " + item.Major);
                Console.WriteLine("GPA: " + item.GPA);
                Console.WriteLine();
            }

            // join and display 2
            Console.WriteLine();
            Console.WriteLine("Game Club members:");
            var joinedList2 = from student in studentList
                             join club in studentClubList on student.StudentID equals club.StudentID
                             where club.ClubName == "Game"
                             select student.StudentName;

            foreach (string name in joinedList2)
            {
                Console.WriteLine(name);
            }

        }
    }
}