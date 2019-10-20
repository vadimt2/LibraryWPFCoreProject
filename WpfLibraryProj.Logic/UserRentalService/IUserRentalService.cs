using System.Collections.Generic;
using WpfLibraryProj.Common;

namespace WpfLibraryProj.Logic.UserRentalService
{
    public interface IUserRentalService
    {
        IEnumerable<CustomerRentalModel> GetAllRentals();
        IEnumerable<CustomerRentalModel> GetCurrentAllItemsUserById(int id);
        IEnumerable<CustomerRentalModel> GetCurrentBrrowingItemUsers();
        IEnumerable<CustomerRentalModel> GetCurrentReturendItemsUserById(int id);
        IEnumerable<CustomerRentalModel> GetCurrentReturnedItemUsers();
    }
}