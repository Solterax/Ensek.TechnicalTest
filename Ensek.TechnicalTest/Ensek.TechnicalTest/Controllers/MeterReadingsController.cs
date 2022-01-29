using Ensek.TechnicalTest.Domain.Interfaces;
using Ensek.TechnicalTest.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ensek.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeterReadingsController : ControllerBase
    {
        private readonly IMeterReadingService _meterReadingService;
        public MeterReadingsController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }

        [HttpPost("meter-reading-uploads")]
        public async Task<MeterReadingUploadResponse> MeterReadingUpload(IFormFile uploadedFile)
        {
            return await _meterReadingService.ProcessUploadedFile(uploadedFile);
        }
    }
}
