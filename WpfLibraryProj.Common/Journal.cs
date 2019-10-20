using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfLibraryProj.Common
{
    public class Journal : AbstractItem
    {
        [Key]
        public override int Id { get; set; }

        [Required]
        public override int AbstractItemCategoryId
        {
            get => (int)this.AbstractItemCategory;

            set
            {
                AbstractItemCategory = (AbstractItemCategory)value;
            }
        }

        [EnumDataType(typeof(AbstractItemCategory))]
        public override AbstractItemCategory AbstractItemCategory { get; set; }

        public virtual int JournalCategoryId
        {
            get => (int)this.JournalCategory;

            set
            {
                JournalCategory = (JournalCategory)value;
            }
        }

        [EnumDataType(typeof(JournalCategory))]
        public JournalCategory JournalCategory { get; set; }

        public override string Title { get; set; }

        public override string Description { get; set; }

        public string Publisher { get; set; }

        public override DateTime PublishDate { get; set; }

        public override int Quantity { get; set; }

        public override int RentedQuantity { get; set; }

        public override decimal Price { get; set; }

        public override decimal Disscount { get; set; }

        public override decimal Total { get; set; }
    }
}
