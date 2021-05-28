using SQLite;
using System;

namespace Todo
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime DateOfLesson { get; set; }
        public TimeSpan TimeOfLesson { get; set; }
        public int DurationOfLesson { get; set; }
        public bool Done { get; set; }
    }
}

