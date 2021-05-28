using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Todo.Models
{
    public class Instructor
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }

    }
}
