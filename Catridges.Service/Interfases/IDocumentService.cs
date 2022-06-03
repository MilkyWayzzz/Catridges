using Catridges.Domain.Entity;
using Catridges.Domain.Response;

namespace Catridges.Service.Interfases;

public interface IDocumentService
{
    Task<BaseResponse<bool>> Create(Document? document);
    
    Task<BaseResponse<Document>> Read(int id);
    
    Task<BaseResponse<bool>> Update(int id, Document document);
    
    Task<BaseResponse<List<Document>>> ReadAll();
    
    Task<BaseResponse<bool>> Delete(int id);
}