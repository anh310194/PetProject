using PetProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProject.Entities
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SaltPassword { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public long? CountryId { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public long? StateId { get; set; }
        public string? ZipCode { get; set; }
        public string? TimeZoneId { get; set; }
        public int? DateFormatId { get; set; }
        public int? TimeFormatId { get; set; }
        public int UserType { get; set; }
        public byte Status { get; set; }
        public string? IsLock { get; set; }
        public int? CountFailSignIn { get; set; }
        public string? PasswordExpiredDate { get; set; }
        public string? ImagePath { get; set; }

        public Country? Country { get; set; }
        public IEnumerable<UserRole>? UserRoles { get; set; }
    }
}
