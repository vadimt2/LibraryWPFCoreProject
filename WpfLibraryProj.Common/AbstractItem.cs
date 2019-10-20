using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfLibraryProj.Common
{
    public abstract class AbstractItem
    {
        public abstract int Id { get; set; }

        public abstract string Title { get; set; }

        public abstract string Description { get; set; }

        public abstract DateTime PublishDate { get; set; }

        public abstract int Quantity { get; set; }

        public abstract int RentedQuantity { get; set; }

        public abstract decimal Price { get; set; }

        public abstract decimal Disscount { get; set; }

        public abstract decimal Total { get; set; }

        public virtual int AbstractItemCategoryId
        {
            get => (int)this.AbstractItemCategory;

            set
            {
                AbstractItemCategory = (AbstractItemCategory)value;
            }
        }

        [EnumDataType(typeof(AbstractItemCategory))]
        public virtual AbstractItemCategory AbstractItemCategory { get; set; }
     
        public virtual void Discount(int discount)
        {
            if (discount == 0 && Price > 0)
            {
                Total = Price;
                return;
            }

            if (Price <= 0)
                return;

            var result = (Price * (decimal)discount) / 100;
            Total = Price - result;
        }
    }
}
