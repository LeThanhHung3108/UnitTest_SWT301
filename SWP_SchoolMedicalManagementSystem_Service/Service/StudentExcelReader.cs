using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_Service.Service
{
    public class StudentExcelReader
    {
        static StudentExcelReader()
        {
            ExcelPackage.License.SetNonCommercialOrganization("My Organization");
        }

        public async Task<List<StudentRequest>> ReadStudentsFromExcelAsync(Stream fileStream)
        {
            var students = new List<StudentRequest>();
            if (fileStream == null || fileStream.Length == 0)
            {
                throw new FileNotFoundException("No file uploaded");
            }

            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        var student = new StudentRequest
                        {
                            StudentCode = worksheet.Cells[row, 1].Value?.ToString(),
                            FullName = worksheet.Cells[row, 2].Value?.ToString(),
                            DateOfBirth = DateTime.Parse(worksheet.Cells[row, 3].Value?.ToString() ?? DateTime.Now.ToString()),
                            Gender = ParseGender(worksheet.Cells[row, 4].Value?.ToString()),
                            Class = worksheet.Cells[row, 5].Value?.ToString(),
                            SchoolYear = worksheet.Cells[row, 6].Value?.ToString()
                        };

                        students.Add(student);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing row {row}: {ex.Message}");
                    }
                }
            }
            if (!students.Any())
            {
                throw new Exception("No valid student data found in the file");
            }
            return students;
        }

        private Gender ParseGender(string? genderStr)
        {
            if (string.IsNullOrWhiteSpace(genderStr))
                return Gender.Other;

            return genderStr.ToLower() switch
            {
                "male" or "m" => Gender.Male,
                "female" or "f" => Gender.Female,
                _ => Gender.Other
            };
        }
    }
}