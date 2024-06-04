using ReasnAPI.Models.DTOs;
using ReasnAPI.Validators;

namespace ReasnAPI.Tests.UnitTests.Validators;

[TestClass]
public class AddressValidatorTests
{
    private AddressValidator _validator = null!;
    
    [TestInitialize]
    public void Setup()
    {
        _validator = new AddressValidator();
    }
    
    [TestMethod]
    public void Validate_WhenValidRequest_ShouldReturnTrue()
    {
        var request = new AddressDto
        {
            Street = "The Wall",
            City = "Castle Black",
            Country = "Westeros",
            State = "The North",
            ZipCode = "12345"
        };
        var result = _validator.Validate(request);
        
        Assert.IsTrue(result.IsValid);
    }
    
    [TestMethod]
    public void Validate_WhenEmptyStreet_ShouldReturnFalse()
    {
        var request = new AddressDto { Street = "" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Street' must not be empty."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenStreetTooLong_ShouldReturnFalse()
    {
        var request = new AddressDto { Street = new string('a', 65) };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == 
                 "The length of 'Street' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidStreet_ShouldReturnFalse()
    {
        var request = new AddressDto { Street = "The Wall!" };
        var result = _validator.Validate(request);
        
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Street' is not in the correct format."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenEmptyCity_ShouldReturnFalse()
    {
        var request = new AddressDto { City = "" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'City' must not be empty."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenCityTooLong_ShouldReturnFalse()
    {
        var request = new AddressDto { City = new string('a', 65) };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == 
                 "The length of 'City' must be 64 characters or fewer. You entered 65 characters."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenInvalidCity_ShouldReturnFalse()
    {
        var request = new AddressDto { City = "Castle Black!" };
        var result = _validator.Validate(request);
        
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'City' is not in the correct format."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenEmptyCountry_ShouldReturnFalse()
    {
        var request = new AddressDto { Country = "" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Country' must not be empty."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenCountryTooLong_ShouldReturnFalse()
    {
        var request = new AddressDto { Country = new string('a', 65) };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "" +
                "The length of 'Country' must be 64 characters or fewer. You entered 65 characters."
        ));
    }

    [TestMethod]
    public void Validate_WhenInvalidCountry_ShouldReturnFalse()
    {
        var request = new AddressDto { Country = "Westeros!" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Country' is not in the correct format."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenEmptyState_ShouldReturnFalse()
    {
        var request = new AddressDto { State = "" };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'State' must not be empty."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenStateTooLong_ShouldReturnFalse()
    {
        var request = new AddressDto { State = new string('a', 65) };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "" +
                "The length of 'State' must be 64 characters or fewer. You entered 65 characters."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenInvalidState_ShouldReturnFalse()
    {
        var request = new AddressDto { State = "The North!" };
        var result = _validator.Validate(request);
        
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'State' is not in the correct format."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenZipCodeTooLong_ShouldReturnFalse()
    {
        var request = new AddressDto { ZipCode = new string('a', 9) };
        var result = _validator.Validate(request);

        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == 
                 "The length of 'Zip Code' must be 8 characters or fewer. You entered 9 characters."
        ));
    }
    
    [TestMethod]
    public void Validate_WhenInvalidZipCode_ShouldReturnFalse()
    {
        var request = new AddressDto { ZipCode = "12345!" };
        var result = _validator.Validate(request);
        
        Assert.IsFalse(result.IsValid);
        Assert.IsTrue(result.Errors.Exists(
            e => e.ErrorMessage == "'Zip Code' is not in the correct format."
        ));
    }
}