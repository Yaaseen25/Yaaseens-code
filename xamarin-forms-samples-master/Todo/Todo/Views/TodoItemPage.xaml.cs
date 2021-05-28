using Foundation;
using System;
using Todo.Views;
using Xamarin.Forms;
using System.Linq;

namespace Todo
{
    public partial class TodoItemPage : ContentPage
    {
        INotificationManager notificationManager;
        
        public TodoItemPage()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
            
        }
        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }
        

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var Length = duration.Text;
            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(duration.Text))
            {
                await DisplayAlert("Error", "One or more field is empty", "OK");
            }
            else if(Convert.ToInt32(Length) > 150 || Convert.ToInt32(Length) < 30 
                || name.Text.ToString().Any(char.IsDigit))
            {
                await DisplayAlert("Error", "One or more field is incorrect", "OK");
            }       
            else
            {
                var todoItem = (TodoItem)BindingContext;
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.SaveItemAsync(todoItem);
                await Navigation.PopAsync();
                
                string title = $"Driving Lesson";
                string message = $"Its almost time for your lesson " + name.Text;
                double dateDiff = (onDateSelected.Date - DateTime.Now).TotalHours;
                notificationManager.SendNotification(title, message, DateTime.Now.AddHours(dateDiff - 1));
            }
            
        }
        
        async void OnDeleteClicked(object sender, EventArgs e)
        {
            

            var todoItem = (TodoItem)BindingContext;
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            //await database.ConvertCountAsync();
            await database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
            
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
