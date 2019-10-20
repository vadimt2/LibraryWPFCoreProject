using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfLibraryProj.Common
{
    public class ItemCollection
    {
        [Key]
        public int Id { get; set; }

        // Book Juornal Must hava it as fk
        public AbstractItemCategory AbstractItemCategory { get; set; }
    }
}
