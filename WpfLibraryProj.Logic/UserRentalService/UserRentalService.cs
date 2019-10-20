using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfLibraryProj.Common;
using WpfLibraryProj.DAL;

namespace WpfLibraryProj.Logic.UserRentalService
{
    public class UserRentalService : IUserRentalService
    {
        private readonly IRepository<CustomerRentalModel> _userRentalRpository;

        public UserRentalService(IRepository<CustomerRentalModel> userRentalRpository)
        {
            _userRentalRpository = userRentalRpository;
        }

        public IEnumerable<CustomerRentalModel> GetAllRentals()
        {
            return _userRentalRpository.GetAll();
        }

        public IEnumerable<CustomerRentalModel> GetCurrentBrrowingItemUsers()
        {
            return _userRentalRpository.GetAll().Where(current => !current.IsItemReturned);
        }

        public IEnumerable<CustomerRentalModel> GetCurrentReturnedItemUsers()
        {
            return _userRentalRpository.GetAll().Where(current => current.IsItemReturned);
        }

        public IEnumerable<CustomerRentalModel> GetCurrentAllItemsUserById(int id)
        {
            _userRentalRpository.Reload();

            var getBrrowingUser = _userRentalRpository.GetAll();

            var getById = getBrrowingUser.Where(current => current.UserId == id);

            var whereItemNoReturned = getById.Where(current => !current.IsItemReturned).ToList();

            return whereItemNoReturned;
        }

        public IEnumerable<CustomerRentalModel> GetCurrentReturendItemsUserById(int id)
        {
            var getBrrowingUser = _userRentalRpository.GetAll();

            var getById = getBrrowingUser.Where(current => current.UserId == id);

            var whereItemReturned = getById.Where(current => current.IsItemReturned).ToList();

            return whereItemReturned;
        }

    }
}
