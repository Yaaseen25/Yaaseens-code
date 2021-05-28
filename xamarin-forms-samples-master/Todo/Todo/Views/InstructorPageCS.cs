using System;
using System.Collections.Generic;
using System.Text;
using Todo.Models;
using Xamarin.Forms;

namespace Todo.Views
{
    public class InstructorPageCS : ContentPage
    {
        public InstructorPageCS()
        {
            Title = "Todo Item";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var telephoneEntry = new Entry();
            telephoneEntry.SetBinding(Entry.TextProperty, "Telephone");

            var passwordEntry = new Entry();
            passwordEntry.SetBinding(Entry.TextProperty, "Password");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                if (nameEntry.Equals(""))
                {
                    await DisplayAlert("Error", "One or more field is empty", "OK");
                }
                else
                {
                    var todoItem = (Instructor)BindingContext;
                    TodoItemDatabase database = await TodoItemDatabase.Instance;
                    await database.SaveInstructorAsync(todoItem);
                    await Navigation.PopAsync();
                }
                
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var todoItem = (Instructor)BindingContext;
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.DeleteInstructorAsync(todoItem);
                await Navigation.PopAsync();
            };

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    new Label { Text = "Name" },
                    nameEntry,
                    new Label { Text = "Telephone" },
                    telephoneEntry,
                    new Label { Text = "password" },
                    passwordEntry,
                    saveButton,
                    deleteButton,
                    cancelButton
                }
            };
        }
    }
}
