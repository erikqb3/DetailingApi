using DetailingApi.Models;
using DetailingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DetailingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController: ControllerBase
{
  private readonly PhotosService _photosService;
  public PhotosController(PhotosService photosService) => 
    _photosService = photosService;

  [HttpGet]
  public async Task<List<Photo>>Get() =>
    await _photosService.GetAsync();


  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Photo>> Get(string id)
  {
    var photo = await _photosService.GetAsync(id);

    if (photo is null)
    {
      return NotFound();
    }
    return photo;
  }


  [HttpPost]
  public async Task<IActionResult> Post(Photo newPhoto)
  {
    await _photosService.CreateAsync(newPhoto);
    return CreatedAtAction(nameof(Get), new { id = newPhoto.Id }, newPhoto);
  }


  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Photo updatedPhoto)
  {
    var photo = await _photosService.GetAsync(id);

    if (photo is null)
    {
      return NotFound();
    }

    updatedPhoto.Id = photo.Id;

    await _photosService.UpdateAsync(id, updatedPhoto);

    return NoContent();
  }


  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var photo = await _photosService.GetAsync(id);

    if (photo is null){
      return NotFound();
    }

    await _photosService.RemoveAsync(id);

    return NoContent();
  }
}