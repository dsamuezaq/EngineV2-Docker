using AutoMapper;
using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using System;
using System.Collections.Generic;
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

    }
}
