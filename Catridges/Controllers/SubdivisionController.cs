using Catridges.Domain.Entity;
using Catridges.Service.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace Catridges.Controllers;

public class SubdivisionController : Controller
{
    private readonly ISubdivisionService _subdivisionService;

    public SubdivisionController(ISubdivisionService subdivisionService)
    {
        _subdivisionService = subdivisionService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllSubdivisions()
    {
        var response = await _subdivisionService.ReadAll();
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data.ToList());
        }
    
        return View();
    }
       
    [HttpGet]
    public async Task<IActionResult> GetSubdivision(int id)
    {
        var response = await _subdivisionService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }
       
    [HttpGet]
    public IActionResult CreateSubdivision()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSubdivision(string model)
    {
        var subdivision = new Subdivision() { Name = model };
        var response = await _subdivisionService.Create(subdivision);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllSubdivisions");
        }
        return View();
    }
       
    [HttpGet]
    public async Task<IActionResult> UpdateSubdivision(int id)
    {
        var response = await _subdivisionService.Read(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return View(response.Data);
        }
        return View();
    }
        
    [HttpPost]
    public async Task<IActionResult> UpdateSubdivision(int id, string model)
    {
        var subdivision = new Subdivision() { Name = model};
        var response = await _subdivisionService.Update(id, subdivision);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllSubdivisions");
        }
        return View();
    }
        
    [HttpGet]
    public async Task<IActionResult> DeleteSubdivision(int id)
    {
        var response = await _subdivisionService.Delete(id);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return RedirectToAction("GetAllSubdivisions");
        }
        return View();
    }
}