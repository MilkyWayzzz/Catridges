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
    public async Task<IActionResult> CreateDocument()
    {
        var response = await _documentService.GetDocumentCreateViewModel();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDocument(int catridgeId, int printerId, int stateId, int subdivisionId,
        DateTime dateTime)
    {
        var documents = await _documentService.ReadAll();
        var documentview = await _documentService.GetDocumentCreateViewModel();
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
            Catridge = documentview.Data.Catridges.FirstOrDefault(x=>x.Id == catridgeId) ,
            Printer = documentview.Data.Printers.FirstOrDefault(x=>x.Id == printerId),
            State = documentview.Data.States.FirstOrDefault(x=>x.Id == stateId),
            Subdivision = documentview.Data.Subdivisions.FirstOrDefault(x=>x.Id == subdivisionId),
            DateTime = dateTime,
            Number = lastDocumentNumber
            
        };
        var response = await _documentService.Create(document);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllDocuments");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateDocument(int id)
    {
        var document = await _documentService.Read(id);
        var response = await _documentService.GetDocumentCreateViewModel();
        if (document.StatusCode == Domain.Enum.StatusCode.OK)
        {
            ViewBag.Date = document.Data.DateTime;
            return View(response.Data);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDocument(int id, int catridgeId, int printerId, int stateId, int subdivisionId,
        DateTime dateTime)
    {
        var documentview = await _documentService.GetDocumentCreateViewModel();
        var documentold = await _documentService.Read(id);
        var numberDocument = documentold.Data.Number;
        var document = new Document()
        {
            Catridge = documentview.Data.Catridges.FirstOrDefault(x=>x.Id == catridgeId),
            Printer = documentview.Data.Printers.FirstOrDefault(x=>x.Id == printerId),
            State = documentview.Data.States.FirstOrDefault(x=>x.Id == stateId),
            Subdivision = documentview.Data.Subdivisions.FirstOrDefault(x=>x.Id == subdivisionId),
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