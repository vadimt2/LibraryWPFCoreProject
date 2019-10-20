using System.Collections.Generic;
using WpfLibraryProj.Common;

namespace WpfLibraryProj.Logic.AbstractItemService
{
    public interface IAbsractItemService
    {
        bool AddItem(AbstractItem abstractItem);
        void BorrowItem(AbstractItem abstractItem, User user);
        void DeleteItem(int id);
        IEnumerable<AbstractItem> GetAllItems();
        AbstractItem GetItemById(int id);
        bool ReturnItem(AbstractItem abstractItem, User user);
        bool UpdateItem(AbstractItem abstractItem);
    }
}