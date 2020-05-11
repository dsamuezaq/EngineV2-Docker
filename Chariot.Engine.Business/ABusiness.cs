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
        protected  RedisCache _distributedCache;
        protected ABusiness(ChariotContext _chariotContext, RedisCache distributedCache)
        {
            Context = _chariotContext;
            _distributedCache = distributedCache;

        }

    }
}
