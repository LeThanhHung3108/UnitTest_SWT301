using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class HealthCheckupResultRepository : IHealthCheckupResultRepository
    {
        private readonly ApplicationDBContext _context;

        public HealthCheckupResultRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task Create(HealthCheckupResult healthCheckupResult)
        {
            await _context.HealthCheckupResults.AddAsync(healthCheckupResult); // viêt câu lệnh SQL Insert
            await _context.SaveChangesAsync(); // lưu thay đổi vào cơ sở dữ liệu
        }

        public async Task Delete(Guid id)
        {
            var healthCheckupResult = await _context.HealthCheckupResults.FirstOrDefaultAsync(x => x.Id.Equals(id));

            _context.HealthCheckupResults.Remove(healthCheckupResult!);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HealthCheckupResult>> GetAll()
        {
            var healthCheckupResults = await _context.HealthCheckupResults.ToListAsync();
            return healthCheckupResults; // trả về danh sách kết quả khám sức khỏe
        }

        public async Task<HealthCheckupResult> GetById(Guid id)
        {
            var healthCheckupResult = await _context.HealthCheckupResults.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return healthCheckupResult;
        }

        public async Task Update(HealthCheckupResult healthCheckupResult)
        {
            _context.Entry(healthCheckupResult).State = EntityState.Modified; // đánh dấu thực thể là đã sửa đổi
            await _context.SaveChangesAsync(); // lưu thay đổi vào cơ sở dữ liệu
        }
    }
}
