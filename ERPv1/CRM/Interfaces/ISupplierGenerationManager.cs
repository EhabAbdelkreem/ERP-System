using ERPv1.CRM.Model;
using ERPv1.CRM.ViewModel;
using System.Collections.Generic;

namespace ERPv1.CRM.Interfaces
{
    public interface ISupplierGenerationManager
    {
        IEnumerable<Contacts> GetAllSuppliers();
        void AddNewSupplier(ContactCreatingViewModel supplier);
    }
}