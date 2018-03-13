using System;
using System.Collections.Generic;
using System.Text;

namespace EGrower.Core.Domain {
    class User {
        // 
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Country { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime LastActivity { get; private set; }
        public bool Deleted { get; private set; }
        public bool Activated { get; private set; }
        public User () { }
        //password is created by AAuthRepository
        public User (string email, int companyId) {
            Email = email;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            LastActivity = DateTime.UtcNow;
            Deleted = false;
            Activated = false;
        }

        public void SetPasswords (byte[] passwordHash, byte[] passwordSalt) {
            PasswordHash = passwordHash;
            PasswordSalt = PasswordSalt;
        }
        public void Update (byte[] passwordHash, byte[] passwordSalt) {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            UpdatedAt = DateTime.UtcNow;
        }
        public void Activate () {
            Activated = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete () {
            Deleted = true;
        }
        public void Restore () {
            if (Deleted == true) {
                Deleted = false;
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}