using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Context;
using SWP_SchoolMedicalManagementSystem_BussinessOject.Entity;
using SWP_SchoolMedicalManagementSystem_Service.Repository.Interface;

namespace SWP_SchoolMedicalManagementSystem_Service.Repository
{
    public class MedicalSuplierRepository : IMedicalSupplierRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public MedicalSuplierRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateSupplierAsync(MedicalSupply supplier)
        {
            await _context.MedicalSupplies.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(Guid id)
        {
            var oldSupplier = await _context.MedicalSupplies.FirstOrDefaultAsync(s => s.Id.Equals(id));
            _context.MedicalSupplies.Remove(oldSupplier);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MedicalSupply>> GetAllSuppliersAsync()
        {
            var listSupplier = await _context.MedicalSupplies.AsNoTracking().ToListAsync();
            return listSupplier;
        }

        public async Task<MedicalSupply?> GetSupplierByIdAsync(Guid id)
        {
            var supplier = await _context.MedicalSupplies.FirstOrDefaultAsync(s => s.Id.Equals(id));
            return supplier;
        }

        public async Task UpdateSupplierAsync(MedicalSupply supplier)
        {
            _context.Entry(supplier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
