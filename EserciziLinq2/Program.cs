//1-Select all students, not duplicated  (starting from enrollments list).

using System.Diagnostics;
using System.Xml.Linq;

var courses = new List<Course>
{
    new Course { CourseId = 101, CourseName = "Mathematics", Credits = 3 },
    new Course { CourseId = 102, CourseName = "Physics", Credits = 4 },
    new Course { CourseId = 103, CourseName = "Chemistry", Credits = 3 },
    new Course { CourseId = 104, CourseName = "Biology", Credits = 4 },
    new Course { CourseId = 105, CourseName = "English Literature", Credits = 2 },
    new Course { CourseId = 106, CourseName = "Philosophy", Credits = 3 },
    new Course { CourseId = 107, CourseName = "Computer Science", Credits = 5 },
    new Course { CourseId = 108, CourseName = "Art History", Credits = 2 },
    new Course {CourseId = 109, CourseName = "Economy", Credits = 5},
    new Course {CourseId = 110, CourseName = "Law", Credits = 5}

};

var students = new List<Student>
{
    new Student { StudentId = 1, Name = "Alice" },
    new Student { StudentId = 2, Name = "Bob" },
    new Student { StudentId = 3, Name = "Charlie" },
    new Student { StudentId = 4, Name = "Diana" },
    new Student { StudentId = 5, Name = "Evan" },
    new Student { StudentId = 6, Name = "Fiona" },
    new Student { StudentId = 7, Name = "George" },
    new Student { StudentId = 8, Name = "Hannah" }
};

var enrollments = new List<Enrollment>
{
    // Alice's enrollments
    new Enrollment { EnrollmentId = 1, Student = students[0], Course = courses[0], Grade = 55 },
    new Enrollment { EnrollmentId = 2, Student = students[0], Course = courses[1], Grade = 90 },
    new Enrollment { EnrollmentId = 3, Student = students[0], Course = courses[2], Grade = null },
    new Enrollment { EnrollmentId = 25, Student = students[0], Course = courses[3], Grade = 44 },

    // Bob's enrollments
    new Enrollment { EnrollmentId = 4, Student = students[1], Course = courses[3], Grade = 88 },
    new Enrollment { EnrollmentId = 5, Student = students[1], Course = courses[4], Grade = 78 },
    new Enrollment { EnrollmentId = 6, Student = students[1], Course = courses[5], Grade = 85 },
    new Enrollment { EnrollmentId = 24, Student = students[1], Course = courses[0], Grade = 54 },

    // Charlie's enrollments
    new Enrollment { EnrollmentId = 7, Student = students[2], Course = courses[6], Grade = 92 },
    new Enrollment { EnrollmentId = 8, Student = students[2], Course = courses[7], Grade = 80 },
    new Enrollment { EnrollmentId = 9, Student = students[2], Course = courses[0], Grade = 60 },
    new Enrollment { EnrollmentId = 23, Student = students[2], Course = courses[7], Grade = 93 },
    new Enrollment { EnrollmentId = 9, Student = students[2], Course = courses[8], Grade = 90 },
    new Enrollment { EnrollmentId = 9, Student = students[2], Course = courses[9], Grade = 95 },

    // Diana
    new Enrollment { EnrollmentId = 10, Student = students[3], Course = courses[1], Grade = 82 },
    new Enrollment { EnrollmentId = 11, Student = students[3], Course = courses[2], Grade = 91 },
    new Enrollment { EnrollmentId = 22, Student = students[3], Course = courses[6], Grade = 88 },
    new Enrollment { EnrollmentId = 30, Student = students[3], Course = courses[7], Grade = null },
    // Evan
    new Enrollment { EnrollmentId = 12, Student = students[4], Course = courses[3], Grade = null },
    new Enrollment { EnrollmentId = 13, Student = students[4], Course = courses[4], Grade = 95 },
    new Enrollment { EnrollmentId = 21, Student = students[4], Course = courses[5], Grade = 72 },
    new Enrollment { EnrollmentId = 26, Student = students[4], Course = courses[2], Grade = 91 },
    // Fiona
    new Enrollment { EnrollmentId = 14, Student = students[5], Course = courses[5], Grade = 55 },
    new Enrollment { EnrollmentId = 15, Student = students[5], Course = courses[6], Grade = null },
    new Enrollment { EnrollmentId = 20, Student = students[5], Course = courses[3], Grade = 90 },
    new Enrollment { EnrollmentId = 27, Student = students[5], Course = courses[1], Grade = 78 },
    // George
    new Enrollment { EnrollmentId = 28, Student = students[6], Course = courses[4], Grade = 85 },
    new Enrollment { EnrollmentId = 16, Student = students[6], Course = courses[7], Grade = 89 },
    new Enrollment { EnrollmentId = 17, Student = students[6], Course = courses[0], Grade = 94 },
    // Hannah
    new Enrollment { EnrollmentId = 18, Student = students[7], Course = courses[1], Grade = null },
    new Enrollment { EnrollmentId = 19, Student = students[7], Course = courses[2], Grade = null },
    new Enrollment { EnrollmentId = 29, Student = students[7], Course = courses[5], Grade = null },
    // Additional enrollments to reach 30
};

// Assign Enrollments to Students
foreach (var student in students)
{
    student.Enrollments = enrollments.Where(e => e.Student.StudentId == student.StudentId).ToList();
}

//1-Select all students, not duplicated (starting from enrollments list).
IEnumerable<Student> students2 =
    enrollments
    .Select(e => e.Student)
    .DistinctBy(s => s.StudentId);

//foreach(var student in students2)
//{
//    Console.WriteLine(student.ToString());
//}


//2-Select students who have completed at least one course (grade not null).
IEnumerable<Student> studentsWhoHaveCompletedACourse =
    enrollments
    .Where(e => e.Grade != null)
    .Select(e => e.Student)
    .DistinctBy(s => s.StudentId)
    .ToList();

//foreach (var student in studentsWhoHaveCompletedACourse)
//{
//    Console.WriteLine(student.ToString());
//}

//3-Select courses with average grades (from 60 to 80).
IEnumerable<Course> courseWithAverageGrades = 
    enrollments
    .Where(e => e.Grade >= 60 && e.Grade <= 80)
    .Select(e => e.Course)
    .DistinctBy(s => s.CourseId)
    .ToList();

//4-Select students and their highest grade.
var studentsHighestGrade =
    enrollments
    .GroupBy(e => e.Student)
    .Select(g => new
    {
        Student = g.Key,
        HighestGrade = g.Max(e => e.Grade),
    })
    .ToList();

//foreach(var student in studentsHighestGrade)
//{
//    Console.WriteLine(student.Student.ToString() + " Highest grade: " + student.HighestGrade);
//}

//5-Group courses by credit count (starting from courses list, then from enrollment list, and finally from student list).
var groupCoursesByCredit =
    courses
    .GroupBy(c => c.Credits)
    .OrderBy(e => e.Key);

var groupCoursesByCredit2 =
    enrollments
    .Select(e => e.Course)
    .DistinctBy(e => e.CourseId)
    .GroupBy(e => e.Credits)
    .OrderBy(e => e.Key);

var groupCoursesByCredit3 = 
    students
    .SelectMany(s => s.Enrollments)
    .Select(e => e.Course)
    .DistinctBy(e => e.CourseId)
    .GroupBy(e => e.Credits)
    .OrderBy(e => e.Key);

//foreach (var group in groupCoursesByCredit3)
//{
//    Console.WriteLine("Corsi con " + group.Key + " crediti");
//    foreach(var course in group)
//    {
//        Console.WriteLine(course.ToString());
//    }
//}

//6-Find courses with no enrollments.
var courses1 = new List<Course>()
{
    new Course
    {
        CourseId = 15,
        CourseName = "Economy",
        Credits = 7,
    },
    new Course
    {
        CourseId = 16,
        CourseName = "Law",
        Credits = 7,
    },
    new Course
    {
        CourseId = 17,
        CourseName = "Philosophy",
        Credits = 3,
    },
    new Course
    {
        CourseId = 18,
        CourseName = "History",
        Credits = 3,
    },
};

var enrollments1 = new List<Enrollment>()
{
    new Enrollment
    {
        EnrollmentId = 1,
        Student = students[0],
        Course = courses1[0],
        Grade = 55,
    },
    new Enrollment
    {
        EnrollmentId = 2,
        Student = students[1],
        Course = courses1[1],
        Grade = 95,
    }
};
var coursesWithNoEnrollements =
    courses1
    .Except(enrollments1.Select(e => e.Course));

//foreach (var course in coursesWithNoEnrollements)
//{
//    Console.WriteLine(course.ToString());
//}


//7-Select enrollments with grades above 90 (starting from enrollment list and then from student list)
var enrollementsWithGradesAbove90 =
    enrollments1
    .Where(e => e.Grade > 90)
    .OrderBy(e => e.EnrollmentId);

var enrollementsWithGradesAbove902 = 
    students
    .SelectMany(s => s.Enrollments)
    .Where(e => e.Grade > 90)
    .OrderBy (e => e.EnrollmentId);

//foreach (var enrollment in enrollementsWithGradesAbove90)
//{
//    Console.WriteLine(enrollment.ToString());
//}


//8-Select students enrolled in a specific course (e.g., Mathematics).
var studentsEnrolledInACourse =
    enrollments1
    .GroupBy(e => e.Course.CourseId)
    .OrderBy(e => e.Key);

var studentsEnrolledInACourse2 =
    enrollments
    .GroupBy(e => e.Course)
    .Select(g => new
    {
       Course = g.Key,
       Student = g.Select(e => e.Student).ToList(),
    });


//foreach (var group in studentsEnrolledInACourse)
//{
//    Console.WriteLine("\nStudenti iscritti al corso id: " + group.Key + "\n");
//    foreach (var student in group)
//    {
//        Console.WriteLine(student.ToString());
//    }
//}

//foreach(var course in studentsEnrolledInACourse2)
//{
//    Console.WriteLine("\n" + course.Course.ToString() + "\nStudents:\n");
//    foreach (var student in course.Student)
//    {
//        Console.WriteLine("Id: " + student.StudentId + " Name: " + student.Name);

//    }
//}


//9-Group students by the number of courses they've completed.
var groupStudentsByNumberCoursesCompleted =
    students
    .GroupBy(s => s.Enrollments.Count(e => e.Grade != null))
    .OrderBy(s => s.Key);

//foreach (var group in groupStudentsByNumberCoursesCompleted)
//{
//    Console.WriteLine("\nStudenti che hanno " + group.Key + " corsi:\n");
//    foreach (var student in group)
//    {
//        Console.WriteLine(student.ToString());
//    }
//}


//10-Find students who have completed all their enrolled courses.
var studentsCompletedAllTheirCourses =
    students
    .Where(s => s.Enrollments.Count == s.Enrollments.Count(e => e.Grade != null));

//foreach (var student in studentsCompletedAllTheirCourses)
//{
//    Console.WriteLine(student.ToString());
//}


//11-Select courses along with the count of students enrolled.
var groupCoursesByStudentEnrolled =
    enrollments
    .GroupBy(e => e.Course)
    .Select(g => new
    {
        Course = g.Key,
        StudentCount = g.Select(e => e.Student).Distinct().Count(),
    });

//foreach (var courseCount in groupCoursesByStudentEnrolled)
//{
//    Console.WriteLine(courseCount.Course.ToString() + " Student enrolled: " + courseCount.StudentCount);
//}


//12-Find courses with the highest average grade.
var coursesWithHighestAverageGrade =
    enrollments
    .GroupBy(e => e.Course.CourseId)
    .Select(g => new
    {
        Course = g.Key,
        AverageGrade = g.Sum(e => e.Grade) / g.Count(),
    })
    .OrderByDescending(e => e.AverageGrade);

foreach (var average in coursesWithHighestAverageGrade)
{
    Console.WriteLine(average.Course.ToString() + " Average grade: " + average.AverageGrade);
}


//13-List students and their average grades in descending order.
var studentsAverageGrades =
    enrollments
    .GroupBy(e => e.Student)
    .Select(g => new
    {
        Student = g.Key,
        AverageGrade = g.Sum(e => e.Grade) / g.Count(),
    })
    .OrderByDescending(g => g.AverageGrade);


//foreach (var student in studentsAverageGrades)
//{
//    Console.WriteLine(student.Student.ToString() + " Average grades: " + student.AverageGrade);
//}


//14-Select all courses a student is enrolled in but hasn't completed (grade is null).
var coursesThatAStudentNotCompleted = 
    enrollments
    .Where(e => e.Grade == null)
    .Select(e => e.Course)
    .DistinctBy(e => e.CourseId)
    .ToList();

//foreach(var course in coursesThatAStudentNotCompleted)
//{
//    Console.WriteLine(course.ToString());
//}

//15-Group enrollments by course and calculate average grade per course.
var AverageGradePerCourse =
    enrollments
    .GroupBy(e => e.Course)
    .Select(g => new
    {
        Course = g.Key,
        AverageGrade = g.Sum(e => e.Grade) / g.Count()
    })
    .OrderBy(e => e.AverageGrade);

//foreach(var course in AverageGradePerCourse)
//{
//    Console.WriteLine(course.Course.toString() + " Average grade: " + course.AverageGrade);
//}


//16-Find the student with the most courses completed.
var studentWithMostCoursesCompleted =
    students
    .OrderByDescending(s => s.Enrollments.Count(e => e.Grade.HasValue))
    .First();

//Console.WriteLine(studentWithMostCoursesCompleted.ToString() + " Courses completed: " + studentWithMostCoursesCompleted.Enrollments.Count(e => e.Grade.HasValue));

//17-Select students who have enrolled in but not completed any courses (all grades null).
var studentsNoHaveCompletedAnyCourses =
    students
    .Where(s => s.Enrollments.Count(e => e.Grade != null) == 0);

//foreach (var student in studentsNoHaveCompletedAnyCourses)
//{
//    Console.WriteLine(student.ToString());
//}



//18-Group courses by the number of completed enrollments (grade not null).
var groupCoursesByNumberOfCompletedEnrollements =
    enrollments
    .Where(e => e.Grade != null)
    .GroupBy(e => e.Course )
    .Select(g => new
    {
        EnrollemntsCompleted = g.Count(),
        Course = g.Key
    })
    .GroupBy(e => e.EnrollemntsCompleted);

//foreach (var group in groupCoursesByNumberOfCompletedEnrollements)
//{
//    Console.WriteLine("\nEnrollements " + group.Key + ":\n");
//    foreach (var course in group)
//    {
//        Console.WriteLine(course.Course.ToString() + "\n Enrollements: " + course.EnrollemntsCompleted);
//    }
//}


//19-Select courses with at least one failing grade (<60).
var coursesWithAtLeastOneFailingGrade =
    enrollments
    .Where(e => e.Grade < 60)
    .Select(e => e.Course)
    .ToList();

//foreach(var course in coursesWithAtLeastOneFailingGrade)
//{
//    Console.WriteLine(course.ToString());
//}

//20-Find the course with the most enrollments, completed or not.
var courseWithTheMostEnrollements =
    enrollments
    .GroupBy(e => e.Course)
    .Select(g => new
    {
        Course = g.Key,
        Enrollements = g.Count()
    })
    .OrderByDescending(e => e.Enrollements)
    .First();

//Console.WriteLine(courseWithTheMostEnrollements.Course.ToString() + " Enrollements: " + courseWithTheMostEnrollements.Enrollements);



public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set;}
    public List<Enrollment> Enrollments { get; set; }

    public string ToString()
    {
        string s = "";
        foreach (var enrollement in Enrollments)
        {
            s += enrollement.Course.ToString() + " Grade: " + (enrollement.Grade.HasValue ? enrollement.Grade : "Failed") + "\n";
        }
        return "Id: " + StudentId + " Name: " + Name + "\nCourses:\n" + s;
    }
}

public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int Credits { get; set; }

    public string ToString()
    {
        return "Id: " + CourseId + " Name: " + CourseName + " Credits: " + Credits;
    }
}

public class Enrollment
{
    public int EnrollmentId { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }
    public int? Grade { get; set; }

    public string ToString()
    {
        return "Enrollement Id: " + EnrollmentId + "\nStudent Id: " + Student.StudentId + " Name: " + Student.Name + "\nCourse" + Course.ToString() + "\nGrade: " + (Grade.HasValue ? Grade : "Failed");
    }
}