using Azure.Storage.Blobs;
using BlobUtilityWeb.Constant;
using Microsoft.AspNetCore.Mvc;

namespace BlobUtilityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        [HttpPost]
        public IActionResult UpdateConnection([FromBody] string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    var blobServiceClient = new BlobServiceClient(connectionString);
                    var containerClient = blobServiceClient.GetBlobContainerClient(AppSetting.ContainerName);
                    AppSetting.BlobConnectionString = connectionString;
                }
                catch
                {

                }
            }

            return Ok();
        }
    }
}
