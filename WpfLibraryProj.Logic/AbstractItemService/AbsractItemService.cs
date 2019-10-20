using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfLibraryProj.Common;
using WpfLibraryProj.DAL;

namespace WpfLibraryProj.Logic.AbstractItemService
{
    public class AbsractItemService : IAbsractItemService
    {

        private readonly IRepository<AbstractItem> _abstractItemRepository;

        private readonly IRepository<CustomerRentalModel> _custRentModelRepository;

        public AbsractItemService(IRepository<AbstractItem> abstractItemRepository, IRepository<CustomerRentalModel> custRentModelRepository)
        {
            _custRentModelRepository = custRentModelRepository;

            _abstractItemRepository = abstractItemRepository;
        }


        public IEnumerable<AbstractItem> GetAllItems()
        {
            return _abstractItemRepository.GetAll();
        }

        public bool AddItem(AbstractItem abstractItem)
        {
            _abstractItemRepository.Insert(abstractItem);

            var result = _abstractItemRepository.Save();

            return result;
        }

        public bool UpdateItem(AbstractItem abstractItem)
        {
            var getItem = _abstractItemRepository.GetById(abstractItem.Id);

            _abstractItemRepository.Update(getItem);

            var result = _abstractItemRepository.Save();

            return result;
        }

        public AbstractItem GetItemById(int id)
        {
            var getabstractItem = _abstractItemRepository.GetById(id);

            return getabstractItem ?? null;
        }

        public void DeleteItem(int id)
        {
            var getabstractItem = _abstractItemRepository.GetById(id);

            if (getabstractItem == null) return;

            _abstractItemRepository.Delete(getabstractItem.Id);

            _abstractItemRepository.Save();
        }

        public void BorrowItem(AbstractItem abstractItem, User user)
        {

            var getItem = _abstractItemRepository.GetById(abstractItem.Id);

            if (getItem == null) return;

            if (getItem.RentedQuantity >= getItem.Quantity) return;

            var getAllRentalItems = _custRentModelRepository.GetAll();

            var getBorrowingUser = getAllRentalItems.FirstOrDefault(current => current.AbstractItemId == abstractItem.Id && current.UserId == user.Id);

            if (getBorrowingUser != null)
            {
                if (!getBorrowingUser.IsItemReturned) return;

                getItem.RentedQuantity++;

                getBorrowingUser.IsItemReturned = false;

                getBorrowingUser.DateItemReturned = null;

                _abstractItemRepository.Update(getItem);

                _abstractItemRepository.Save();

                _custRentModelRepository.Update(getBorrowingUser);

                _custRentModelRepository.Save();

                return;
            }

            getItem.RentedQuantity++;

            _abstractItemRepository.Update(getItem);

            _abstractItemRepository.Save();

            CustomerRentalModel customerRental = new CustomerRentalModel
            { AbstractItemId = abstractItem.Id, UserId = user.Id, IsItemReturned = false, DateItemRented = DateTime.Now };
            _custRentModelRepository.Insert(customerRental);
            _custRentModelRepository.Save();
        }


        public bool ReturnItem(AbstractItem abstractItem, User user)
        {

            var getItem = _abstractItemRepository.GetById(abstractItem.Id);

            if (getItem.RentedQuantity <= 0) return false;

            if (getItem == null) return false;

            var getCustomerRentals = _custRentModelRepository.GetAll();

            var currentUser = getCustomerRentals.FirstOrDefault(current => current.UserId == user.Id && current.AbstractItemId == abstractItem.Id);

            if (currentUser != null)
            {
                if (currentUser.IsItemReturned) return false;

                currentUser.DateItemReturned = DateTime.Now;

                getItem.RentedQuantity--;

                currentUser.IsItemReturned = true;

                _custRentModelRepository.Update(currentUser);

                _custRentModelRepository.Save();

                _abstractItemRepository.Update(getItem);

                _abstractItemRepository.Save();
                return true;
            }
            return false;
        }



    }
}