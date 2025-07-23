using AutoMapper;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.HealthCheckupResultDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalConsultationDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalDiaryDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalIncidentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplierDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.MedicalSupplyUsageDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Dto.UserDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.HealthRecordDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.MedicationRequestsDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.NotifyDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDetailDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.ScheduleDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.StudentDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccCampaignDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccFormDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccResultDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.VaccScheduleDto;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;

namespace SWP_SchoolMedicalManagementSystem_BussinessOject.MapperProfile
{
    public class MapperEntities : Profile
    {
        public MapperEntities()
        {
            //User Mapper
            CreateMap<User, UserRegisterRequestDto>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();
            CreateMap<User, UserCreateRequestDto>().ReverseMap();
            CreateMap<User, UserUpdateRequestDto>().ReverseMap();

            //Student Mapper
            CreateMap<Student, StudentRequest>().ReverseMap();
            CreateMap<Student, StudentResponse>().ReverseMap();

            //HealthRecord Mapper
            CreateMap<HealthRecord, HealthRecordRequest>().ReverseMap();
            CreateMap<HealthRecord, HealthRecordResponse>().ReverseMap();

            //Medical Request Mapper
            CreateMap<MedicalRequest, MedicationReqRequest>().ReverseMap();
            CreateMap<MedicalRequest, MedicationReqResponse>()
                .ForMember(dest => dest.StudentCode, opt => opt.MapFrom(x => x.Student!.StudentCode))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student!.FullName))
                .ForMember(dest => dest.MedicalStaffId, opt => opt.MapFrom(src => src.MedicalStaffId))
                .ForMember(dest => dest.MedicalStaffName, opt => opt.MapFrom(src => src.MedicalStaff!.FullName))
                .ReverseMap();

            //Campaign Mapper
            CreateMap<Campaign, CampaignRequest>().ReverseMap();
            CreateMap<Campaign, CampaignResponse>().ReverseMap();

            //Schedule Mapper
            CreateMap<Schedule, ScheduleBaseRequest>().ReverseMap();
            CreateMap<Schedule, ScheduleGetByIdResponse>()
                .ForMember(dest => dest.ScheduleDetail, opt => opt.MapFrom(src => src.ScheduleDetails))
                .ReverseMap();
            CreateMap<Schedule, ScheduleResponse>().ReverseMap();
            CreateMap<Schedule, ScheduleConsentForm>()
                .ForMember(dest => dest.ScheduledDate, opt => opt.MapFrom(src => src.ScheduledDate))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
            CreateMap<ScheduleCreateDto, Schedule>().ReverseMap();

            //Consent Form Mapper
            CreateMap<ConsentForm, ConsentFormRequest>().ReverseMap();
            CreateMap<ConsentForm, ConsentFormResponse>()
                .ForMember(dest => dest.CampaignName, opt => opt.MapFrom(src => src.Campaign!.Name))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student!.FullName))
                .ForMember(dest => dest.ScheduleConsentForms, opt => opt.MapFrom(src => src.Campaign!.Schedules))
                .ReverseMap();

            //Schedule Detail Mapper
            CreateMap<ScheduleDetail, ScheduleDetailRequest>().ReverseMap();
            CreateMap<ScheduleDetail, ScheduleDetailResponse>().ReverseMap();

            //Vaccination Result Mapper
            CreateMap<VaccinationResult, VaccResultRequest>().ReverseMap();
            CreateMap<VaccinationResult, VaccResultResponse>().ReverseMap();

            //Incident Mapper
            CreateMap<MedicalIncident, IncidentResponseDto>()
                .ForMember(dest => dest.MedicalStaffId, opt => opt.MapFrom(src => src.MedicalStaff!.Id))
                .ReverseMap();
            CreateMap<MedicalIncident, IncidentCreateRequestDto>().ReverseMap();
            CreateMap<MedicalIncident, IncidentUpdateRequestDto>().ReverseMap();

            //Medcal Supply Mapper
            CreateMap<MedicalSupply, SupplierRequestDto>().ReverseMap();
            CreateMap<MedicalSupply, SupplierResponseDto>().ReverseMap();

            // Medical Supply Usage Mapper
            CreateMap<MedicalSupplyUsage, MedicalSupplyUsageCreateDto>().ReverseMap();
            CreateMap<MedicalSupplyUsage, MedicalSupplyUsageResponseDto>().ReverseMap();

            // Medical Diary Mapper
            CreateMap<MedicalDiary, MedicalDiaryRequestDto>().ReverseMap();
            CreateMap<MedicalDiary, MedicalDiaryResponseDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.MedicationReq!.Student!.FullName))
                .ReverseMap();

            // HealthCheckup Result Mapper
            CreateMap<HealthCheckupResult, HealthCheckupCreateRequestDto>().ReverseMap();
            CreateMap<HealthCheckupResult, HealthCheckupUpdateRequestDto>().ReverseMap();
            CreateMap<HealthCheckupResult, HealthCheckupResponseDto>().ReverseMap();

            //Notification Mapper
            CreateMap<Notification, NotificationRequest>().ReverseMap();
            CreateMap<Notification, NotificationResponse>().ReverseMap();

            // Medical Consultation Mapper
            CreateMap<MedicalConsultation, MedicalConsultationCreateRequestDto>().ReverseMap();
            CreateMap<MedicalConsultation, MedicalConsultationUpdateRequesteDto>().ReverseMap();
            CreateMap<MedicalConsultation, MedicalConsultationResponeDto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student!.FullName))
                .ForMember(dest => dest.MedicalStaffName, opt => opt.MapFrom(src => src.MedicalStaff!.FullName))
                .ReverseMap();
        }
    }
}
