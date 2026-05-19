using Cwiczenia7.DTOss;

namespace Cwiczenia7.Servicess;

public interface IDbService
{
    Task<List<PcResponseDto>> GetAllPcsAsync();
    Task<List<PcComponentResponseDto>?> GetPcComponentsAsync(int id);
    Task<PcResponseDto> CreatePcAsync(CreatePcDto dto);
    Task<PcResponseDto?> UpdatePcAsync(int id, UpdatePcDto dto);
    Task<bool> DeletePcAsync(int id);
}