using Catridges.Domain.Entity;
using Catridges.Domain.Response;
using Catridges.Service.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace Catridges.Controllers;

public class StateController : Controller
{
   private readonly IStateServise _stateServise;

   public StateController(IStateServise stateServise)
   {
      _stateServise = stateServise;
   }
   
   [HttpGet]
   public async Task<IActionResult> GetAllStates()
   {
      var response = await _stateServise.ReadAll();
      if (response.StatusCode == Domain.Enum.StatusCode.OK)
      {
         return View(response.Data.ToList());
      }

      return View();
   }
   
   [HttpGet]
   public async Task<IActionResult> GetState(int id)
   {
      var response = await _stateServise.Read(id);
      if (response.StatusCode == Domain.Enum.StatusCode.OK)
      {
         return View(response.Data);
      }
      return View();
   }
   
   [HttpGet]
   public IActionResult CreateState()
   {
      return View();
   }

   [HttpPost]
   public async Task<IActionResult> CreateState(string model)
   {
      var state = new State() { Name = model };
      var response = await _stateServise.Create(state);
      if (response.StatusCode == Domain.Enum.StatusCode.OK)
      {
         return View(response.Data);
      }
      return View();
   }
   
   [HttpGet]
   public async Task<IActionResult> UpdateState(int id)
   {
      var response = await _stateServise.Read(id);
      if (response.StatusCode == Domain.Enum.StatusCode.OK)
      {
         return View(response.Data);
      }
      return View();
   }
    
   [HttpPost]
   public async Task<IActionResult> UpdateState(int id, string model)
   {
      var state = new State() { Name = model};
      var response = await _stateServise.Update(id, state);
      if (response.StatusCode == Domain.Enum.StatusCode.OK)
      {
         return RedirectToAction("GetAllStates");
      }
      return View();
   }
    
   [HttpGet]
   public async Task<IActionResult> DeleteState(int id)
   {
      var response = await _stateServise.Delete(id);
      if (response.StatusCode == Domain.Enum.StatusCode.OK)
      {
         return View(response.Data);
      }
      return View();
   }
   
}