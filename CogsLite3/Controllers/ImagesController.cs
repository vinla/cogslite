using System;
using CogsLite.Core;
using Microsoft.AspNetCore.Mvc;

namespace CogsLite3.Controllers
{
    public class ImagesController : Controller
    {
		private readonly IImageStore _imageStore;

		public ImagesController(IImageStore imageStore)
		{
			_imageStore = imageStore ?? throw new ArgumentNullException(nameof(imageStore));
		}
        public IActionResult Get(Guid id)
        {
			return new FileContentResult(_imageStore.Retrieve(id), "application/octet-stream");
        }
    }
}