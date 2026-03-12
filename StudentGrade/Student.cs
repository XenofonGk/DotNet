
class Student
{
    public string Name { get; private set; }
    public string Major { get; private set; }

    public int[] Grades { get; private set; }
    public double GPA { get; private set; }

    public Student(string aName, string aMajor,int[] aGrades)
    {
        Name = aName;
        Major = aMajor;
        Grades = aGrades;
        GPA = CalculateGPA();
    }

    public double CalculateGPA()
    {
        double gpa;
        double avg = Grades.Average();
        if (avg >= 90)
        {
            gpa = 4.0;
        } else if (avg >= 80)
        {
            gpa = 3.0;
        } else if (avg >= 70)
        {
            gpa = 2.0;
        } else if (avg >= 60)
        {
            gpa = 1.0;
        } else
        {
            Console.WriteLine("See you next year");
            gpa = 0.0;
        }

        return gpa;
    }
    public bool Passes()
    {
        if (CalculateGPA() > 1.0)
        {
            Console.WriteLine("Pass");
            return true;
        } else
        {
        Console.WriteLine("Fail");
        return false;
        }
    }
}