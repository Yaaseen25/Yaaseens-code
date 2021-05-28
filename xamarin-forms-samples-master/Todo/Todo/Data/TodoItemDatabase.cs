using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Todo.Models;

namespace Todo
{
    public class TodoItemDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<TodoItemDatabase> Instance = new AsyncLazy<TodoItemDatabase>(async () =>
        {
            var instance = new TodoItemDatabase();
            CreateTableResult result = await Database.CreateTableAsync<TodoItem>();
            CreateTableResult result1 = await Database.CreateTableAsync<Instructor>();
            CreateTableResult result2 = await Database.CreateTableAsync<Student>();
            return instance;
        });

        public TodoItemDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }



        public Task<List<TodoItem>> GetItemsAsync()
        {            
            return Database.Table<TodoItem>().ToListAsync();
        }

        public Task<List<Instructor>> GetInstructorsAsync()
        {
            return Database.Table<Instructor>().ToListAsync();
        }

        public Task<List<Student>> GetStudentsAsync()
        {
            return Database.Table<Student>().ToListAsync();
        }

        public Task<List<TodoItem>> GetItemNameAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT string(Name as Text) FROM [TodoItem]");
        }

        public Task<TodoItem> GetItemAsync(int id)
        {
            return Database.Table<TodoItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<Instructor> GetInstructorAsync(int id)
        {
            return Database.Table<Instructor>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<Student> GetStudentAsync(int id)
        {
            return Database.Table<Student>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }



        public Task<int> SaveItemAsync(TodoItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> SaveInstructorAsync(Instructor item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> SaveStudentAsync(Student item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }



        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<int> DeleteInstructorAsync(Instructor item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<int> DeleteStudentAsync(Student item)
        {
            return Database.DeleteAsync(item);
        }

        public async Task<List<TodoItem>> GetName()
        {
            return await Database.QueryAsync<TodoItem>("SELECT Name FROM TodoItem");
        }

        public async Task<int> GetCountAsync()
        {
            return await Database.Table<Instructor>().CountAsync();

        }
        public async Task<int> ConvertCountAsync()
        {
            var task = GetCountAsync();
            int result = await task;
            System.Console.WriteLine("TestCOUNT " + result);
            return result;
        }

        public async Task<int> LoginValidate1(string telephone, string password)
        {
            var d3 = Database.Table<Student>().Where(x => x.Telephone == telephone && x.Password == password);
            var result = await d3.ToListAsync();
            int i = 0;
            foreach (var x in result)
            {
                i++;
            }
            System.Console.WriteLine("TestCount " + i);
            return i;
            
        }
        public async Task<int> LoginValidate(string telephone, string password)
        {
            var d3 = Database.Table<Instructor>().Where(x => x.Telephone == telephone && x.Password == password);
            var result = await d3.ToListAsync();
            int i = 0;
            foreach(var x in result)
            {
                i++;
            }
            System.Console.WriteLine("TestCount " + i);
            return i;
  
        }
    }
}

