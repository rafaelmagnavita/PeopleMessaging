﻿using System;

namespace RabbitProjectFiles.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Create new person
        /// </summary>
        /// <param name="name"></param>
        /// <param name="document"></param>
        /// <param name="birthDate"></param>
        public Person(string name, string document, DateTime birthDate)
        {
            Name = name;
            Document = document;
            BirthDate = birthDate;
        }
    }

    public class CustomerCreated
    {
        public CustomerCreated() { }

        public CustomerCreated(int id, string fullName, string email, string phoneNumber, DateTime birthDate)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class CustomerInputModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
