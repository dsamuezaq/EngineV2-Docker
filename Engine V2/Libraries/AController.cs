using Chariot.Engine.DataObject;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engine_V2.Libraries
{
    public class AController<T> : ControllerBase
    {
        #region variable Class
        protected string TableName;
        protected string ControllerName;
        #endregion
        protected ChariotContext Context { get; }
        public AController(ChariotContext _chariotContext)
        {
            Context = _chariotContext;

        }



    }
}
