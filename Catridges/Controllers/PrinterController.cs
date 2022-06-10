using Catridges.Domain.Entity;
using Catridges.Service.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace Catridges.Controllers;

public class PrinterController : Controller
{
   private readonly IPrinterService _printerService;

    public PrinterController(IPrinterService printerService)
    {
        _printerService = printerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPrinters()
    {
        var response = await _printerService.ReadAll();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data.ToList());
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetPrinter(int id)
    {
        var response = await _printerService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreatePrinter()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrinter(string model)
    {
        var printer = new Printer() { Model = model };
        var response = await _printerService.Create(printer);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllPrinters");
        }
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdatePrinter(int id)
    {
        var response = await _printerService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdatePrinter(int id, string model)
    {
        var printer = new Printer() { Model = model};
        var response = await _printerService.Update(id, printer);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllPrinters");
        }
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> DeletePrinter(int id)
    {
        var response = await _printerService.Delete(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllPrinters");
        }
        return View();
    }
}