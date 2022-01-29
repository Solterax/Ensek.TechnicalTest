using CsvHelper;
using Ensek.TechnicalTest.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace Ensek.TechnicalTest.Domain
{
    public class CsvParser : ICsvParser
    {
        public List<T> ParseFile<T>(IFormFile file)
        {
            var parsedObjects = new List<T>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                parsedObjects = csv.GetRecords<T>().ToList();
            }

            return parsedObjects;
        }
        public List<T> ParseFromPath<T>(string filePath)
        {
            var parsedObjects = new List<T>();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                parsedObjects = csv.GetRecords<T>().ToList();
            }

            return parsedObjects;
        }
    }
}
