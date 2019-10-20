using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WpfLibraryProj.Common
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        [Required]
        public int RankId
        {
            get => (int)this.UserRank;

            set
            {
                UserRank = (UserRank)value;
            }
        }
        [EnumDataType(typeof(UserRank))]
        public UserRank UserRank { get; set; }


        public string Email { get; set; }

        public User(string userName, string password, string email)
        {
            UserName = userName;

            Password = password;

            Email = email;
        }

        public User(string userName, string password)
        {
            UserName = userName;

            Password = password;
        }

        public User()
        {

        }
    }
}
