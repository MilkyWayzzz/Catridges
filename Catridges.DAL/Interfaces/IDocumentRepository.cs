using Catridges.Domain.Entity;
using Catridges.Domain.ViewModels;

namespace Catridges.DAL.Interfaces;

public interface IDocumentRepository : IBaseRepository<Document>
{
    Task<DocumentCreateViewModel> GetDocumentCreateViewModel();
    
}