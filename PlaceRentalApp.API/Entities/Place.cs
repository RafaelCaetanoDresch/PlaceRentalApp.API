using PlaceRentalApp.API.Enums;
using PlaceRentalApp.API.ValueObjects;

namespace PlaceRentalApp.API.Entities;

public class Place  : BaseEntity
{
    protected Place() {}
    public Place(string title, string description, decimal dailyPrice, Address address, int allowedNumberPerson, bool allowedPets, int createdBy)
        :base()
    {
        Title = title;
        Description = description;
        DailyPrice = dailyPrice;
        Address = address;
        AllowedNumberPerson = allowedNumberPerson;
        AllowedPets = allowedPets;
        CreatedBy = createdBy;

        Status = PlaceStatus.Active;

        Books = [];
        Ameneties = [];
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal DailyPrice { get; private set; }
    public Address Address { get; private set; }
    public int AllowedNumberPerson { get; private set; }
    public bool AllowedPets { get; private set; }

    public int CreatedBy { get; private set; }
    public PlaceStatus Status { get; private set; }
    public User User { get; private set; } = null!;

    public List<PlaceBook> Books { get; private set; }
    public List<PlaceAmenity> Ameneties { get; private set; }

    public void Update(string title, string description, decimal dailyPrice)
    {
        Title = title;
        Description = description;
        DailyPrice= dailyPrice;
    }
}
