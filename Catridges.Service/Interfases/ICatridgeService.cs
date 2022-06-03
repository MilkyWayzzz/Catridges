using Catridges.Domain.Entity;
using Catridges.Domain.Response;

namespace Catridges.Service.Interfases;

public interface ICatridgeService
{
    Task<BaseResponse<bool>> Create(Catridge? catridge);
    
    Task<BaseResponse<Catridge>> Read(int id);
    
    Task<BaseResponse<bool>> Update(int id, Catridge catridgenew);
    
    Task<BaseResponse<List<Catridge>>> ReadAll();
    
    Task<BaseResponse<bool>> Delete(int id);
}