using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CogsLite.Core
{
    public interface IImageStore
    {
		void Store(Guid id, Stream imageData);
		void Store(Guid id, byte[] imageData);
		byte[] Retrieve(Guid id);
    }
}
