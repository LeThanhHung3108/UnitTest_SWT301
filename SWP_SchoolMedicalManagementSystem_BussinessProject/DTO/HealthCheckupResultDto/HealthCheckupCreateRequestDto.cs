using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto
{
    public class HealthCheckupCreateRequestDto : HealthCheckupUpdateRequestDto
    {
        public Guid? ScheduleDetailId { get; set; }
    }
}
