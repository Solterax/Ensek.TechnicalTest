using Ensek.TechnicalTest.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Ensek.TechnicalTest.Domain.Interfaces
{
    public interface IMeterReadingService
    {
        Task<MeterReadingUploadResponse> ProcessUploadedFile(IFormFile uploadedFile);
    }
}