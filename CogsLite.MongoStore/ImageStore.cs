using System;
using System.IO;
using CogsLite.Core;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.GridFS;

namespace CogsLite.MongoStore
{
	public class ImageStore : BaseMongoStore, IImageStore
	{
		public ImageStore(IConfiguration configuration) : base(configuration)
		{
		}

		public byte[] Retrieve(Guid id)
		{
			var database = GetDatabase();
			var fileStorage = new GridFSBucket(database, new GridFSBucketOptions
			{
				BucketName = "images"
			});
			return fileStorage.DownloadAsBytesByName(id.ToString());
		}

		public void Store(Guid id, Stream imageData)
		{
			var database = GetDatabase();
			
			var fileStorage = new GridFSBucket(database, new GridFSBucketOptions
			{
				BucketName = "images"
			});

			fileStorage.UploadFromStream(id.ToString(), imageData);
		}
	}
}
