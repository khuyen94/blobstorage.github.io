using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobUtilityWeb.Constant;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlobUtilityWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IList<BlobItem> BlobItems { get; set; }
        public void OnGet()
        {
            BlobItems = GetListBlobInfo();
        }

        private IList<BlobItem> GetListBlobInfo()
        {
            if (AppSetting.BlobConnectionString == string.Empty)
            {
                return new List<BlobItem>();
            }

            var blobServiceClient = new BlobServiceClient(AppSetting.BlobConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(AppSetting.ContainerName);

            var items = containerClient.GetBlobs()
                .DistinctBy(x => x.Name.Split('/').First())
                .OrderByDescending(x => x.Properties.LastModified)
                .Take(50).ToList();
            return items;
        }
    }
}