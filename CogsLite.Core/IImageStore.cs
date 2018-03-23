using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CogsLite.Core
{
    public interface IImageStore
    {
		void Store(Guid id, Stream imageData);
		byte[] Retrieve(Guid id);
    }
}
