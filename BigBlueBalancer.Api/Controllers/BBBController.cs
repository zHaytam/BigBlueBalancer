using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client.Helpers;
using BigBlueButton.Client.Parameters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Controllers
{
    [ApiController]
    [Produces("application/xml")]
    public abstract class BBBController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public BBBController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        protected Task<Server> GetAvailableServer() => _appDbContext.Servers
            .Where(s => s.Up)
            .OrderBy(s => s.Load)
            .FirstOrDefaultAsync();

        protected bool IsChecksumValid<T>(string callName, T request, string checksum) where T : class
        {
            var query = ParametersExtractor.GenerateQueryString(request);
            var realChecksum = ChecksumGenerator.Generate(callName, _configuration["Secret"], query);
            return realChecksum == checksum;
        }
    }
}
