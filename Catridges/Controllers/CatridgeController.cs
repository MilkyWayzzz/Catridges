using Catridges.Domain.Entity;
using Catridges.Domain.Response;
using Catridges.Service.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace Catridges.Controllers;

public class CatridgeController : Controller
{
    private readonly ICatridgeService _catridgeService;

    public CatridgeController(ICatridgeService catridgeService)
    {
        _catridgeService = catridgeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCatridges()
    {
        var response = await _catridgeService.ReadAll();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data.ToList());
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetCatridge(int id)
    {
        var response = await _catridgeService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateCatridge()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCatridge(string model)
    {
        var catridge = new Catridge() { Model = model };
        var response = await _catridgeService.Create(catridge);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllCatridges");
        }
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> UpdateCatridge(int id)
    {
        var response = await _catridgeService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateCatridge(int id, string model)
    {
        var catridge = new Catridge() { Model = model};
        var response = await _catridgeService.Update(id, catridge);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllCatridges");
        }
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteCatridge(int id)
    {
        var response = await _catridgeService.Delete(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllCatridges");
        }
        return View();
    }
}