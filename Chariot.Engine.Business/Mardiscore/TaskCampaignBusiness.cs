using Chariot.Engine.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.Business.Mardiscore
{
    public class TaskCampaignBusiness:ABusiness
    {
        public TaskCampaignBusiness(ChariotContext _chariotContext) : base(_chariotContext) { 
        
        
        }
        public List<int> Get() {


        var r=    Context.TaskCampaigns.Select(x=>x.Id).ToList();
        return r;
        }
    }
}
