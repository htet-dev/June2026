using June2026.OOPPractice;

Student s1 = new Student(1, "Htet", 40);
Student s2 = new Student(2, "Mary", 30);
Student s3 = new Student(3, "John", 25);

StudentService t = new StudentService();
t.CelebrateBirthday(s1);
t.ChangeStudentName(s1, "Ko Htet");
StudentPrinter.PrintMe(s1);

t.CelebrateBirthday(s2);
t.ChangeStudentName(s2, "Ko Mary");
StudentPrinter.PrintMe(s2);

t.CelebrateBirthday(s3);
t.ChangeStudentName(s3, "Ko John");
StudentPrinter.PrintMe(s3);