using Microsoft.AspNetCore.Http;

namespace Ensek.TechnicalTest.Domain.Interfaces
{
    public interface ICsvParser
    {
        List<T> ParseFile<T>(IFormFile file);

        List<T> ParseFromPath<T>(string filePath);
    }
}