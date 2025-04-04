using Microsoft.AspNetCore.Mvc;
using  StudentManagmentSystem.WebApp.Models;

namespace StudentManagmentSystem.WebApp.Controllers
{
    public class StudentRecordsController : Controller
    {
        private static List<StudentRecordModel> Students = new List<StudentRecordModel>()
        {
            new StudentRecordModel(){ RollNo = 1, Name = "Rishabh" , DateOfBirth = new DateTime(2002,07,04), Email ="rishabh@gmail.com"},
            new StudentRecordModel(){ RollNo = 2, Name = "Isha" , DateOfBirth = new DateTime(2002,04,07), Email ="isha@gmail.com"}
        };
        public IActionResult Index()
        {
           
            return View(Students);
        }
        public IActionResult Details(int id)
        {
            
            var Student = Students.Find(s => s.RollNo == id);
            return View(Student);
        }
        public IActionResult CreateUpdate(int id)
        {
            
            StudentRecordModel student = Students.Find(s => s.RollNo == id);
            if (student == null)
            {
                student = new StudentRecordModel();
            }
            return View(student);
        }
        public IActionResult CreateUpdateRecord(StudentRecordModel newStudent)
            
        {
            
            StudentRecordModel oldStudent = Students.Find(s => s.RollNo == newStudent.RollNo);
            if (oldStudent == null)
            {
                int rollno = Students.Count() + 1;
                newStudent.RollNo = rollno;
                Students.Add(newStudent);
            }
            else
            {  
                oldStudent.Name = newStudent.Name;
                oldStudent.DateOfBirth = newStudent.DateOfBirth;
                oldStudent.Email = newStudent.Email;
            }
           return RedirectToAction("Index");
        }

       
           
           
        

        public IActionResult Delete(int id)
        {

            StudentRecordModel student = Students.Find(s => s.RollNo == id);
            Students.Remove(student);
            return RedirectToAction("Index");
        }
    }
}
