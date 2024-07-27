using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Application.Services;

namespace PlaceRentalApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlacesController : ControllerBase
{
    private readonly IPlaceService _placeService;

    #region Constructor
    public PlacesController(IPlaceService placeService)
    => _placeService = placeService; 
    #endregion

    //GET api/places?search=casa
    [HttpGet]
    public IActionResult Get(string search, DateTime startDate, DateTime endDate)
    {
        var availablePlaces = _placeService.GetAllAvailable(search, startDate, endDate);

        return Ok(availablePlaces);
    }


    //GET api/places/123
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var place = _placeService.GetById(id);

        return Ok(place);
    }

    //POST api/places
    [HttpPost]
    public IActionResult Post(CreatePlaceInputModel model)
    {
        var id = _placeService.Insert(model);

        return CreatedAtAction(nameof(GetById), new { id }, model);
    }

    //PUT api/places
    [HttpPut]
    public IActionResult Put(int id, UpdatePlaceInputModel model)
    {
        _placeService.Update(id, model);

        return NoContent();
    }


    //POST api/places/1234/amenities
    [HttpPost("{id}/amenities")]
    public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
    {
        _placeService.InsertAmenity(id, model);
        return NoContent();
    }


    //DELETE api/places/123
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _placeService.Delete(id);

        return NoContent();
    }

    //POST api/places/1234/books
    [HttpPost("{id}/books")]
    public IActionResult PostBook(int id, CreateBookInputModel model)
    {
        _placeService.Book(id, model);

        return NoContent();
    }

    //POST api/places/1234/comments
    [HttpPost("{id}/comments")]
    public IActionResult PostComment(int id, CreateCommentInputModel model)
    {
        //var place = _context.Places.SingleOrDefault(p => p.Id == id);
        //if (place is null)
        //    return NotFound();

        return NoContent();
    }

    //PUT api/places/1234/photos
    [HttpPost("{id}/photos")]
    public IActionResult PostPhotos(int id, IFormFile file)
    {
        var description = $"File: {file.Name}, Size: {file.Length}";
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        var fileBytes = ms.ToArray();
        var base64 = Convert.ToBase64String(fileBytes);

        return Ok(new { description, base64 });
    }
}
