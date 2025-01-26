using StormEkspress.Models.Entities;

namespace StormEkspress.Services.Interfaces
{
    public interface IFormService
    {
        Task SubmitCourierApplication(CourierApplication application);
        Task SubmitRestaurantApplication(RestaurantApplication application);
    }
}
