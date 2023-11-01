using System;

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
}
