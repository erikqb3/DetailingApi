using DetailingApi.Models;
using DetailingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DetailingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DealsController: ControllerBase
{
  private readonly DealsService _dealsService;
  public DealsController(DealsService dealsService) => 
    _dealsService = dealsService;

  [HttpGet]
  public async Task<List<Deal>>Get() =>
    await _dealsService.GetAsync();


  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Deal>> Get(string id)
  {
    var deal = await _dealsService.GetAsync(id);

    if (deal is null)
    {
      return NotFound();
    }
    return deal;
  }


  [HttpPost]
  public async Task<IActionResult> Post(Deal newDeal)
  {
    await _dealsService.CreateAsync(newDeal);
    return CreatedAtAction(nameof(Get), new { id = newDeal.Id }, newDeal);
  }


  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Deal updatedDeal)
  {
    var deal = await _dealsService.GetAsync(id);

    if (deal is null)
    {
      return NotFound();
    }

    updatedDeal.Id = deal.Id;

    await _dealsService.UpdateAsync(id, updatedDeal);

    return NoContent();
  }


  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var deal = await _dealsService.GetAsync(id);

    if (deal is null){
      return NotFound();
    }

    await _dealsService.RemoveAsync(id);

    return NoContent();
  }
}