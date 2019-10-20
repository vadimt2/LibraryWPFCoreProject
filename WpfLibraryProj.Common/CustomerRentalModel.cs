using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfLibraryProj.Common
{
    public class CustomerRentalModel
    {
        [Key]
        public int Id { get; set; }

        public int AbstractItemId { get; set; }

        [ForeignKey("AbstractItemId")]
        public AbstractItem AbstractItem { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool IsItemReturned { get; set; }

        public DateTime? DateItemRented { get; set; }

        public DateTime? DateItemReturned { get; set; }
    }
}
