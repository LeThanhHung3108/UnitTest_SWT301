using Microsoft.AspNetCore.Mvc;
using SchoolMedicalManagementSystem.Enum;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Service;
using SWP_SchoolMedicalManagementSystem_Service.Service;
using ClosedXML.Excel;
using System.Data;
using System.Net.Mime;

namespace SWP_SchoolMedicalManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly StudentExcelReader _excelReader;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
            _excelReader = new StudentExcelReader();
        }

        //1. Get all students
        [HttpGet("get-all-students")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        //2. Get student by ID
        [HttpGet("get-student-by-id/{studentId}")]
        public async Task<IActionResult> GetStudentById(Guid studentId)
        {
            var student = await _studentService.GetStudentByIdAsync(studentId);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        //3. Get student by student code
        [HttpGet("get-student-by-student-code/{studentCode}")]
        public async Task<IActionResult> GetStudentByCode(string studentCode)
        {
            var student = await _studentService.GetStudentByStudentCodeAsync(studentCode);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        //4. Get students by parent ID
        [HttpGet("get-student-by-parent-id/{parentId}")]
        public async Task<IActionResult> GetStudentsByParent(Guid parentId)
        {
            var students = await _studentService.GetStudentsByParentIdAsync(parentId);
            return Ok(students);
        }

        //5. Get students by class
        [HttpGet("get-students-by-class-name/{className}")]
        public async Task<IActionResult> GetStudentsByClass(string className)
        {
            var students = await _studentService.GetStudentsByClassAsync(className);
            return Ok(students);
        }

        //6. Get students by school year
        [HttpGet("get-students-by-school-year/{schoolYear}")]
        public async Task<IActionResult> GetStudentsBySchoolYear(string schoolYear)
        {
            var students = await _studentService.GetStudentsBySchoolYearAsync(schoolYear);
            return Ok(students);
        }

        //7. Create a new student
        [HttpPost("create-student")]
        public async Task<IActionResult> CreateStudent([FromBody] StudentRequest request)
        {
            await _studentService.CreateStudentAsync(request);
            return Ok("Student created successfully.");

        }

        //7.1 Import students from Excel file
        [HttpPost("import-student-from-excel")]
        public async Task<IActionResult> ImportStudentsFromExcel(IFormFile file)
        {
            {
                using (var stream = file.OpenReadStream())
                {
                    var students = await _excelReader.ReadStudentsFromExcelAsync(stream);
                    foreach (var student in students)
                    {
                        await _studentService.CreateStudentAsync(student);
                    }
                    return Ok(new { message = $"Successfully imported {students.Count} students" });
                }
            }

        }

        //8. Update an existing student
        [HttpPut("update-student/{studentId}")]
        public async Task<IActionResult> UpdateStudent(Guid studentId, [FromBody] StudentRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request data");
            }

            await _studentService.UpdateStudentAsync(studentId, request);
            return Ok("Student updated successfully.");
        }

        //9. Delete a student
        [HttpDelete("delete-student/{studentId}")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            await _studentService.DeleteStudentAsync(studentId);
            return Ok("Student deleted successfully.");
        }
    }
}
