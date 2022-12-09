using DetailingApi.Models;
using DetailingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DetailingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeaturesController: ControllerBase
{
  private readonly FeaturesService _featuresService;
  public FeaturesController(FeaturesService featuresService) => 
    _featuresService = featuresService;

  [HttpGet]
  public async Task<List<Feature>>Get() =>
    await _featuresService.GetAsync();


  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Feature>> Get(string id)
  {
    var feature = await _featuresService.GetAsync(id);

    if (feature is null)
    {
      return NotFound();
    }
    return feature;
  }


  [HttpPost]
  public async Task<IActionResult> Post(Feature newFeature)
  {
    await _featuresService.CreateAsync(newFeature);
    return CreatedAtAction(nameof(Get), new { id = newFeature.Id }, newFeature);
  }


  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Feature updatedFeature)
  {
    var feature = await _featuresService.GetAsync(id);

    if (feature is null)
    {
      return NotFound();
    }

    updatedFeature.Id = feature.Id;

    await _featuresService.UpdateAsync(id, updatedFeature);

    return NoContent();
  }


  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var feature = await _featuresService.GetAsync(id);

    if (feature is null){
      return NotFound();
    }

    await _featuresService.RemoveAsync(id);

    return NoContent();
  }
}