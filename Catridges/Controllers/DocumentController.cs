using Catridges.Domain.Entity;
using Catridges.Domain.Response;
using Catridges.Domain.ViewModels;
using Catridges.Service.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace Catridges.Controllers;

public class DocumentController : Controller
{
    private readonly IDocumentService _documentService;

    public DocumentController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDocuments()
    {
        var response = await _documentService.ReadAll();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data.ToList());
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetDocument(int id)
    {
        var response = await _documentService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateDocument()
    {
        var response = new BaseResponse<DocumentCreateViewModel>();
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDocument(Catridge catridge, Printer printer, State state, Subdivision subdivision,
        DateTime dateTime)
    {
        var documents = await _documentService.ReadAll();
        int lastDocumentNumber = 0;
        foreach (var item in documents.Data)
        {
            if (item.Number > lastDocumentNumber)
            {
                lastDocumentNumber = item.Number;
            }
        }
        var document = new Document()
        {
            Catridge = catridge,
            Printer = printer,
            State = state,
            Subdivision = subdivision,
            DateTime = dateTime,
            Number = lastDocumentNumber
            
        };
        var response = await _documentService.Create(document);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateDocument(int id)
    {
        var response = await _documentService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDocument(int id, Catridge catridge, Printer printer, State state, Subdivision subdivision,
        DateTime dateTime)
    {
        var documentold = await _documentService.Read(id);
        var numberDocument = documentold.Data.Number;
        var document = new Document()
        {
            Catridge = catridge,
            Printer = printer,
            State = state,
            Subdivision = subdivision,
            DateTime = dateTime,
            Number = numberDocument
            
        };
        var response = await _documentService.Update(id, document);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllDocuments");
        }

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        var response = await _documentService.Delete(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }

        return View();
    }
}