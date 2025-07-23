using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccScheduleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto
{
    public class ScheduleCreateDto : ScheduleBaseRequest
    {
        public Guid CampaignId { get; set; }
    }
}
