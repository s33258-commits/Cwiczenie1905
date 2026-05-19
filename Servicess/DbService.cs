using Cwiczenia7.Dataa;
using Cwiczenia7.DTOss;
using Cwiczenia7.Modelss;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia7.Servicess;

public class DbService : IDbService
{
    private readonly AppDbContext _context;

    public DbService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PcResponseDto>> GetAllPcsAsync()
    {
        return await _context.PCs
            .Select(p => new PcResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            })
            .ToListAsync();
    }

    public async Task<List<PcComponentResponseDto>?> GetPcComponentsAsync(int id)
    {
        var pcExists = await _context.PCs.AnyAsync(p => p.Id == id);

        if (!pcExists)
            return null;

        return await _context.PCComponents
            .Where(pc => pc.PcId == id)
            .Select(pc => new PcComponentResponseDto
            {
                ComponentCode = pc.Component.Code,
                Name = pc.Component.Name,
                Description = pc.Component.Description,
                Type = pc.Component.ComponentType.Name,
                Manufacturer = pc.Component.ComponentManufacturer.FullName,
                Amount = pc.Amount
            })
            .ToListAsync();
    }

    public async Task<PcResponseDto> CreatePcAsync(CreatePcDto dto)
    {
        var pc = new Pc
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<PcResponseDto?> UpdatePcAsync(int id, UpdatePcDto dto)
    {
        var pc = await _context.PCs.FindAsync(id);

        if (pc == null)
            return null;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return new PcResponseDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> DeletePcAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PcComponents)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc == null)
            return false;

        _context.PCComponents.RemoveRange(pc.PcComponents);
        _context.PCs.Remove(pc);

        await _context.SaveChangesAsync();

        return true;
    }
}