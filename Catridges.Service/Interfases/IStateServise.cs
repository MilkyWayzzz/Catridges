using Catridges.Domain.Entity;
using Catridges.Domain.Response;

namespace Catridges.Service.Interfases;

public interface IStateServise
{
    Task<BaseResponse<bool>> Create(State? state);
    
    Task<BaseResponse<State>> Read(int id);
    
    Task<BaseResponse<bool>> Update(int id, State state);
    
    Task<BaseResponse<List<State>>> ReadAll();
    
    Task<BaseResponse<bool>> Delete(int id);
}