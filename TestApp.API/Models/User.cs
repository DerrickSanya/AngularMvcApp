namespace TestApp.API.Models {
    using System.ComponentModel.DataAnnotations;
    public class User {
        public int Id { get; set; }

        [MaxLength (50)]
        public string Firstname { get; set; }

        [MaxLength (50)]
        public string Lastname { get; set; }

        [MaxLength (50)]
        public string EmailAddress { get; set; }

        [MaxLength (50)]
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}