// See https://aka.ms/new-console-template for more information



List<Student> students = new List<Student>
{
    new Student("Jim", "CPP",new int[] {80,80,80,80}),
    new Student("Julia", "Buisness",new int[] {100,80,67,30}),
    new Student("Jammie", "CPA",new int[] {84,68,50,90}),
    new Student("John", "Firefighter",new int[] {0,0,100,100})
};
for (int i = 0; i < students.Count; i++)
{
       Console.WriteLine($"{i+1}. Student Name: {students[i].Name} Major: {students[i].Major} Grades: {string.Join(", ",students[i].Grades)} GPA: {students[i].CalculateGPA()} | {students[i].Passes()}");

}


  var ranked = students.OrderByDescending(s => s.GPA);
    Console.WriteLine("\n --- Ranking ---");
    foreach (var student in ranked)
{
        Console.WriteLine($"{student.Name} - GPA: {student.GPA}");
}


    

