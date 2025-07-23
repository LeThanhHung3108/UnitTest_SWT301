using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMedicalManagementSystem.Enum;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.Entity
{
    public class MedicalSupply : BaseEntity
    {
        public string? SupplyName { get; set; }
        public SupplyType SupplyType { get; set; }
        public string? Unit {  get; set; }
        public int? Quantity {  get; set; }   
        public string? Supplier {  get; set; }
        public List<string>? Image {  get; set; }
        public ICollection<MedicalSupplyUsage>? MedicalSupplyUsages { get; set; }
    }
}
