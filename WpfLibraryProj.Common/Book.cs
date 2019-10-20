using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfLibraryProj.Common
{
    public class Book : AbstractItem
    {
        [Key]
        public override int Id { get; set; }

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

       

        [Required]
        public virtual int BookCategoryId
        {
            get => (int)this.BookCategory;

            set
            {
                BookCategory = (BookCategory)value;
            }
        }

        [EnumDataType(typeof(BookCategory))]
        public BookCategory BookCategory { get; set; }

        
        [Required]
        public Guid ISBN { get; set; }

        public override string Title { get; set; }

        public override string Description { get; set; }

        public override DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public override int Quantity { get; set; }

        public override int RentedQuantity { get; set; }

        public override decimal Price { get; set; }

        public override decimal Disscount { get; set; }

        public override decimal Total { get; set; }
    }
}
