using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class Blog : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid? AuthorId {  get; set; }
        public User? Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public BlogStatus Status { get; set; }
        public List<string>? Images { get; set; }
    }
}
