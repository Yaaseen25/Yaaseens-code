using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
          
            
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            //int num = await database.ConvertCountAsync(telephoneEntry.Text.ToString(), passwordEntry.Text.ToString());
            var result = database.LoginValidate(telephoneEntry.Text, passwordEntry.Text);
            var result1 = database.LoginValidate1(telephoneEntry.Text, passwordEntry.Text);
            int num = await result;
            int num1 = await result1;
            if (num == 0)
            {
                if(num1 == 0)
                {
                    await DisplayAlert("Information", "Login details incorrect or not found", "OK");
                }
                else
                {
                    await DisplayAlert("Information", "You have successfully logged in as a Student", "OK");
                    await Navigation.PushAsync(new TodoListPage());
                }
            }
            else 
            {
                await DisplayAlert("Information", "You have successfully logged in as an Instructor", "OK");
                await Navigation.PushAsync(new TodoListPage());
            }


            
        }

        async void NavigateButton_OnClicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodoPage());
        }
    }
}