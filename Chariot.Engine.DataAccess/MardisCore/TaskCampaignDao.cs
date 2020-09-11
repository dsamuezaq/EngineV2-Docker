using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Framework.Resources;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataAccess.MardisCore
{
    public class TaskCampaignDao : ADao
    {
        public TaskCampaignDao(ChariotContext context)
      : base(context)
        {

        }

        /// <summary>
        /// Save data mobil from tracking
        /// </summary>
        /// <param name="_table">Data table Tracking</param>
        /// <returns></returns>
        public List<Branch> GetBranchList(int idAccount, string Imeid)
        {

            List<Branch> consulta = new List<Branch>();
            if (Imeid == "")
            {
                consulta = Context.Branches.Include(b => b.PersonOwner).Where(tb => tb.StatusRegister == CStatusRegister.Active && tb.IdAccount == idAccount && tb.ESTADOAGGREGATE.Equals("") == false && tb.ESTADOAGGREGATE != null).ToList();
            }
            else
            {
                consulta = Context.Branches
                    .Include(b => b.PersonOwner)
                    .Include(t => t.Province)
                    .Include(t => t.District)
                    .Where(tb => tb.StatusRegister == CStatusRegister.Active
                              && tb.IdAccount == idAccount
                              && tb.ESTADOAGGREGATE.Equals("") == false
                              && tb.ESTADOAGGREGATE != null
                              && tb.IMEI_ID.Contains(Imeid)).ToList();

            }
            return consulta;

        }
        public void d() {
       var     consulta = Context.Branches.Include(b => b.PersonOwner).Where(tb => tb.StatusRegister == CStatusRegister.Active && tb.IdAccount == 4 && tb.ESTADOAGGREGATE.Equals("") == false && tb.ESTADOAGGREGATE != null).ToList();

            var d = 2;
        }
    }
}
