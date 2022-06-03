using Catridges.DAL.Interfaces;
using Catridges.Domain.Entity;
using Catridges.Domain.Enum;
using Catridges.Domain.Response;
using Catridges.Domain.ViewModels;
using Catridges.Service.Interfases;

namespace Catridges.Service.Implementations;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        _documentRepository = documentRepository;
    }

    public async Task<BaseResponse<bool>> Create(Document? document)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            if (document != null)
            {
                await _documentRepository.Create(document);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Create] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<Document>> Read(int id)
    {
        var baseResponse = new BaseResponse<Document>();
        try
        {
            var document = await _documentRepository.Read(id);
            if (document != null)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = document;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<Document>()
            {
                Description = $"[Read] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> Update(int id, Document documentnew)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var document = await _documentRepository.Read(id);
            if (document != null)
            {
                document.Catridge = documentnew.Catridge;
                document.Printer = documentnew.Printer;
                document.State = documentnew.State;
                document.Subdivision = documentnew.Subdivision;
                document.DateTime = documentnew.DateTime;
                document.Number = documentnew.Number;
                await _documentRepository.Update(document.Id,document);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[ReadAll] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<List<Document>>> ReadAll()
    {
        var baseResponse = new BaseResponse<List<Document>>();
        try
        {
            var documents = await _documentRepository.ReadAll();
            if (documents.Count > 0)
            {
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = documents;
                return baseResponse;
            }
            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<List<Document>>()
            {
                Description = $"[ReadAll] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<bool>> Delete(int id)
    {
        var baseResponse = new BaseResponse<bool>();
        try
        {
            var document = await _documentRepository.Read(id);
            if (document != null)
            {
                await _documentRepository.Delete(document);
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[Delete] : {e.Message}"
            };
        }
    }

    public async Task<BaseResponse<DocumentCreateViewModel>> GetDocumentCreateViewModel()
    {
        var baseResponse = new BaseResponse<DocumentCreateViewModel>();
        try
        {
            var documentCreateViewModel = await _documentRepository.GetDocumentCreateViewModel();
            if (documentCreateViewModel != null)
            {
                baseResponse.Data = documentCreateViewModel;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }

            baseResponse.StatusCode = StatusCode.NoOk;
            return baseResponse;
        }
        catch (Exception e)
        {
            return new BaseResponse<DocumentCreateViewModel>()
            {
                Description = $"[Delete] : {e.Message}"
            };
        }
    }
}