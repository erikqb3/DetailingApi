using DetailingApi.Models;
using DetailingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DetailingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController: ControllerBase
{
  private readonly ContactService _contactService;
  public ContactController(ContactService contactService) => 
    _contactService = contactService;

  [HttpGet]
  public async Task<List<Contact>>Get() =>
    await _contactService.GetAsync();


  [HttpGet("{id:length(24)}")]
  public async Task<ActionResult<Contact>> Get(string id)
  {
    var contact = await _contactService.GetAsync(id);

    if (contact is null)
    {
      return NotFound();
    }
    return contact;
  }


  [HttpPost]
  public async Task<IActionResult> Post(Contact newContact)
  {
    await _contactService.CreateAsync(newContact);
    return CreatedAtAction(nameof(Get), new { id = newContact.Id }, newContact);
  }


  [HttpPut("{id:length(24)}")]
  public async Task<IActionResult> Update(string id, Contact updatedContact)
  {
    var contact = await _contactService.GetAsync(id);

    if (contact is null)
    {
      return NotFound();
    }

    updatedContact.Id = contact.Id;

    await _contactService.UpdateAsync(id, updatedContact);

    return NoContent();
  }


  [HttpDelete("{id:length(24)}")]
  public async Task<IActionResult> Delete(string id)
  {
    var contact = await _contactService.GetAsync(id);

    if (contact is null){
      return NotFound();
    }

    await _contactService.RemoveAsync(id);

    return NoContent();
  }
}