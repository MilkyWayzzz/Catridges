using Catridges.Domain.Entity;
using Catridges.Domain.Response;

namespace Catridges.Service.Interfases;

public interface IPrinterService
{
    Task<BaseResponse<bool>> Create(Printer? printer);
    
    Task<BaseResponse<Printer>> Read(int id);
    
    Task<BaseResponse<bool>> Update(int id, Printer printer);
    
    Task<BaseResponse<List<Printer>>> ReadAll();
    
    Task<BaseResponse<bool>> Delete(int id);
}