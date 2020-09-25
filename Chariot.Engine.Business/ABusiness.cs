using AutoMapper;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using Chariot.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business
{
    public abstract class ABusiness
    {
        protected ChariotContext Context { get; }
        protected  RedisCache _RedisCache;
        protected IMapper _mapper;
        protected ABusiness(ChariotContext _chariotContext, RedisCache distributedCache, IMapper mapper)
        {
            Context = _chariotContext;
            _RedisCache = distributedCache;
            _mapper = mapper;

        }
        public string GetUrlAzureContainerbyStrem(MemoryStream File, string namefile, string extension)
        {



            DateTime localDate = DateTime.Now;

            MemoryStream stream = File;
            AzureStorageUtil.UploadFromStream(stream, "evidencias", namefile + extension).Wait();
            var uri = AzureStorageUtil.GetUriFromBlob("evidencias", namefile + extension);
            // loading bytes from a file is very easy in C#. The built in System.IO.File.ReadAll* methods take care of making sure every byte is read properly.

            return uri;
        }

    }
}
