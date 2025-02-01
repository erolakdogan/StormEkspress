using StormEkspress.Models.Entities;

namespace StormEkspress.Services.Interfaces
{
    public interface IFormService
    {
        Task SubmitApplicationAsync(object application, string formType);
    }
}
