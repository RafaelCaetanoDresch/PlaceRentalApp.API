﻿using PlaceRentalApp.Application.Exceptions;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Core.Entities;
using PlaceRentalApp.Core.ValueObjects;
using PlaceRentalApp.Infrastructure.Persistence;

namespace PlaceRentalApp.Application.Services;

public class PlaceService : IPlaceService
{
    private readonly PlaceRentalDbContext _context;

    public PlaceService(PlaceRentalDbContext context)
    {
        _context = context;
    }
    public void Book(int id, CreateBookInputModel model)
    {
        var exists = _context.Places.Any(p => p.Id == id);
        if (!exists)
            throw new NotFoundException();

        var book = new PlaceBook(model.IdUser, model.IdPlace, model.StartDate, model.EndDate, model.Comments);

        _context.PlaceBooks.Add(book);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id);
        if (place is null)
            throw new NotFoundException();

        place.SetAsDeleted();

        _context.Places.Update(place);
        _context.SaveChanges();
    }

    public List<Place> GetAllAvailable(string search, DateTime startDate, DateTime endDate)
    {
        var availablePlaces = _context.Places.Where(p => p.Title.Contains(search) &&
               !p.Books.Any(b =>
               (b.StartDate >= b.StartDate && startDate <= b.EndDate) ||
               (b.EndDate >= b.StartDate && endDate <= b.EndDate) ||
               (b.StartDate <= b.StartDate && endDate >= b.EndDate)
               )
               && !p.IsDeleted);

        return availablePlaces.ToList();
    }

    public Place? GetById(int id)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id);
        if (place is null)
            throw new NotFoundException();

        return place;
    }

    public int Insert(CreatePlaceInputModel model)
    {
        var address = new Address(
             model.Address.Street,
             model.Address.Number,
             model.Address.ZipCode,
             model.Address.District,
             model.Address.City,
             model.Address.State,
             model.Address.Country
             );

        var place = new Place(
            model.Title,
            model.Description,
            model.DailyPrice,
            address,
            model.AllowedNumberPerson,
            model.AllowedPets,
            model.CreatedBy
            );

        _context.Places.Add(place);
        _context.SaveChanges();

        return place.Id;
    }

    public void InsertAmenity(int id, CreatePlaceAmenityInputModel model)
    {
        var exists = _context.Places.Any(p => p.Id == id);
        if (!exists)
            throw new NotFoundException();

        var amenity = new PlaceAmenity(model.Description, id);
        _context.PlaceAmenities.Add(amenity);
        _context.SaveChanges();
    }

    public void Update(int id, UpdatePlaceInputModel model)
    {
        var place = _context.Places.SingleOrDefault(p => p.Id == id);
        if (place is null)
            throw new NotFoundException();

        place.Update(model.Title, model.Description, model.DailyPrice);

        _context.Places.Update(place);
        _context.SaveChanges();
    }
}
