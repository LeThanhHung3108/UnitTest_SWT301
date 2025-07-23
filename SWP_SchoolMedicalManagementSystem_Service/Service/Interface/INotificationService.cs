using SWP_SchoolMedicalManagementSystem_BussinessOject.DTO.NotifyDto;

namespace SWP_SchoolMedicalManagementSystem_Service.Service.Interface
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetAllNotificationsAsync();
        Task<NotificationResponse> GetNotificationByIdAsync(Guid notificationId);
        Task<List<NotificationResponse>> GetNotificationsByUserIdAsync(Guid userId);
        Task CreateNotificationAsync(NotificationRequest notification);
        Task UpdateNotificationAsync(Guid notificationId, NotificationRequest notification);
        Task DeleteNotificationAsync(Guid notificationId);
    }
}
