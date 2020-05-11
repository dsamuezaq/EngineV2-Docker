using Chariot.Engine.DataObject;
using Chariot.Framework.Complement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.Mardiscore
{
    public class TaskCampaignBusiness:ABusiness
    {
        public TaskCampaignBusiness(ChariotContext _chariotContext,
                                     RedisCache distributedCache) : base(_chariotContext, distributedCache)
        { 
        
        
        }
        public List<int> Get() {


        var r=    Context.TaskCampaigns.Select(x=>x.Id).ToList();
        return r;
        }
    }
}
