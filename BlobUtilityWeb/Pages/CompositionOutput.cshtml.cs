using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobUtilityWeb.Constant;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlobUtilityWeb.Pages
{
    public class CompositionOutputModel : PageModel
    {
        private readonly ILogger<CompositionOutputModel> _logger;

        public CompositionOutputModel(ILogger<CompositionOutputModel> logger)
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
            var containerClient = blobServiceClient.GetBlobContainerClient(AppSetting.OutputContainerName);

            var items = containerClient.GetBlobs()
                .Where(x => x.Name.EndsWith(".mp4"))
                .OrderByDescending(x => x.Properties.LastModified)
                .Take(50).ToList();
            return items;
        }
    }
}