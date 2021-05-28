using System;
using Xamarin.Forms;

namespace Todo
{
    public class TodoItemPageCS : ContentPage
    {
        public TodoItemPageCS()
        {
            Title = "Todo Item";

            var nameEntry = new Entry();
            nameEntry.SetBinding(Entry.TextProperty, "Name");

            var notesEntry = new Entry();
            notesEntry.SetBinding(Entry.TextProperty, "Notes");

            var dateEntry = new DatePicker();
            dateEntry.SetBinding(DatePicker.DateProperty, "DateOfLesson");
            

            var timeEntry = new TimePicker();
            timeEntry.SetBinding(TimePicker.TimeProperty, "TimeOfLesson");

            var durationEntry = new Entry();
            durationEntry.SetBinding(Entry.TextProperty, "DurationOfLesson");

            var doneSwitch = new Switch();
            doneSwitch.SetBinding(Switch.IsToggledProperty, "Done");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                if(nameEntry.Equals(""))
                {
                    await DisplayAlert("Error", "One or more field is empty", "OK");
                }
                else
                {
                    var todoItem = (TodoItem)BindingContext;
                    TodoItemDatabase database = await TodoItemDatabase.Instance;
                    await database.SaveItemAsync(todoItem);
                    await Navigation.PopAsync();
                }
                
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.DeleteItemAsync(todoItem);
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
                    new Label { Text = "Notes" },
                    notesEntry,
                    new Label { Text = "Date of Lesson" },
                    dateEntry,
                    new Label { Text = "Time of Lesson" },
                    timeEntry,
                    new Label { Text = "Duration of Lesson" },
                    durationEntry,
                    new Label { Text = "Done" },
                    doneSwitch,
                    saveButton,
                    deleteButton,
                    cancelButton
                }
            };
        }
    }
}
