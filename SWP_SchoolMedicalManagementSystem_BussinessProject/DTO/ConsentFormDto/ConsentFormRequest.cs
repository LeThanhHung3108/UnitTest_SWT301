using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto
{
    public class ConsentFormRequest
    {
        public Guid CampaignId { get; set; }
        public Guid StudentId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ConsentDate { get; set; }
        public string? ReasonForDecline { get; set; }
    }
}
