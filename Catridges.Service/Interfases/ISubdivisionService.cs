using Catridges.Domain.Entity;
using Catridges.Domain.Response;

namespace Catridges.Service.Interfases;

public interface ISubdivisionService
{
    Task<BaseResponse<bool>> Create(Subdivision? subdivision);
    
    Task<BaseResponse<Subdivision>> Read(int id);
    
    Task<BaseResponse<bool>> Update(int id, Subdivision subdivision);
    
    Task<BaseResponse<List<Subdivision>>> ReadAll();
    
    Task<BaseResponse<bool>> Delete(int id);
}