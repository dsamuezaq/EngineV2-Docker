using Chariot.Engine.DataObject;
using Chariot.Engine.DataObject.MardisCommon;
using Chariot.Engine.DataObject.MardisCore;
using Chariot.Engine.DataObject.MardisOrders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chariot.Engine.DataAccess.MardisCore
{
        public class BranchMigrateDao : ADao
        {
            public BranchMigrateDao(ChariotContext chariotcontext) :
                base(chariotcontext)
            {

            }

            #region Nutri
            public int ExistProductNutri(string barcode)
            {
                try
                {
                    var itemReturn = Context.ProductOrders.Where(x => x.IdArticulo == barcode).Select(t => t.Id);

                    if (itemReturn.Count() > 0)
                    {
                        return itemReturn.FirstOrDefault();

                    }
                    return 0;
                }

                catch (Exception e)

                {
                    throw new Exception("Error al consultar Sectores");
                }
            }

        public bool ExistinvoiceNutri(string numFact)
        {
            try
            {
                var itemReturn = Context.Invoices.Where(x => x.NUMBER == numFact).Select(t => t.IDINVOICE);

                if (itemReturn.Count() > 0)
                {
                    return true;
                }
                return false;
            }

            catch (Exception e)

            {
                throw new Exception("Error al consultar Facturas");
            }
        }

        public List<Guid> UsersRuc(string ruc)
            {
                var _ruc = Context.Users.Include(x => x.Persons).Where(x => x.StatusRegister == "A" && x.Persons.Document == ruc).Select(s => s.Id);
                if (_ruc.Count() > 0)
                {
                    return _ruc.ToList();
                }

                return null;
            }
            public string SaveProductNutriMigrate(Invoice _insertData, int idAccount, int iduser, string option, string campaign, string statusStask)
            {
                bool status = false;
                Branch branch = null;
                Person person = null;
                Central_Warenhouse centralw = new Central_Warenhouse();
                Invoice invoice = new Invoice();
                IQueryable<Invoice> invoices = Enumerable.Empty<Invoice>().AsQueryable();
                TaskCampaign task = null;

            try
            {

                invoices = Context.Invoices.Where(i => i.NUMBER == _insertData.NUMBER);

                if (invoices.Count() > 0) {
                    invoice = invoices.First();
                }

                if (invoice.IDINVOICE != 0)
                {
                    invoice.Invoice_details = new List<Invoice_detail>();
                    invoice.Invoice_details.Add(_insertData.Invoice_details.First());
                }
                else {
                    invoice = _insertData;
                    Context.Invoices.Add(invoice);
                }

                if (Context.Central_Warenhouses.Where(c => c.IDPRODUCTO == _insertData.Invoice_details.First().IDPRODUCTO).Count()>0) {
                    centralw = Context.Central_Warenhouses.Where(c => c.IDPRODUCTO == _insertData.Invoice_details.First().IDPRODUCTO).First();
                }

                if (centralw.ID_CENTRALW != 0)
                {
                    centralw.BALANCE = centralw.BALANCE + (decimal)_insertData.Invoice_details.First().AMOUNT;
                }
                else
                {
                    centralw.IDPRODUCTO = _insertData.Invoice_details.First().IDPRODUCTO;
                    //centralw.ID = _insertData.Invoice_details.First().IDPRODUCTO;
                    centralw.IDDISTRIBUTOR = 1;
                    centralw.BALANCE = (decimal)_insertData.Invoice_details.First().AMOUNT;
                    centralw.DESCRIPTION = _insertData.Invoice_details.First().DESCRIPTION;
                    Context.Central_Warenhouses.Add(centralw);
                }
                
                Context.SaveChanges();

                return "";

                }
                catch (Exception e)
                {
                    return "No puedo actualizar la información en la base de datos. Revise la Información";
                    throw;
                }
                finally
                {
                    status = true;
                }

            }

            #endregion
    }
}
