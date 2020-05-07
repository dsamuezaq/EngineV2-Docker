using Chariot.Engine.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business
{
    public abstract class ABusiness
    {
        public ChariotContext Context { get; }
        protected ABusiness(ChariotContext _chariotContext)
        {
            Context = _chariotContext;

        }

    }
}
