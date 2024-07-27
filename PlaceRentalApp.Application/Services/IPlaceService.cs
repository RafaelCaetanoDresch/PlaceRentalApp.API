using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;

namespace PlaceRentalApp.Application.Services;

public interface IPlaceService
{
    void Book(int id, CreateBookInputModel model);
    void Delete(int id);
    List<Place> GetAllAvailable(string search, DateTime startDate, DateTime EndDate);
    Place? GetById(int id);
    int Insert(CreatePlaceInputModel model);
    void InsertAmenity(int id, CreatePlaceAmenityInputModel model);
    void Update(int id, UpdatePlaceInputModel model);
}
