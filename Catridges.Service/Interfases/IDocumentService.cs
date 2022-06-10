using Catridges.Domain.Entity;
using Catridges.Domain.Response;
using Catridges.Domain.ViewModels;

namespace Catridges.Service.Interfases;

public interface IDocumentService
{
    Task<BaseResponse<bool>> Create(Document? document);
    
    Task<BaseResponse<Document>> Read(int id);
    
    Task<BaseResponse<bool>> Update(int id, Document document);
    
    Task<BaseResponse<List<Document>>> ReadAll(string? searchString, string? sortOrder);
    
    Task<BaseResponse<bool>> Delete(int id);

    Task<BaseResponse<DocumentCreateViewModel>> GetDocumentCreateViewModel();
    
}