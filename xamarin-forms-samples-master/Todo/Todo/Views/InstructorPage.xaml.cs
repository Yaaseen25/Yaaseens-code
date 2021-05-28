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
    public partial class InstructorPage : ContentPage
    {
        public InstructorPage()
        {
            InitializeComponent();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (Instructor)BindingContext;
            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(Telephone.Text) 
                || string.IsNullOrWhiteSpace(password.Text))
            {
                await DisplayAlert("Error", "One or more field is empty", "OK");
            }
            else if(Telephone.Text.Contains("( / ) N , , * ; # + . ") || name.Text.ToString().Any(char.IsDigit) 
                || Telephone.Text.Length < 10  || password.Text.Length < 8)
            {
                await DisplayAlert("Error", "One or more field is incorrect", "OK");
            }
            else
            {
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.SaveInstructorAsync(todoItem);
                await Navigation.PopAsync();
            }
            
            
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (Instructor)BindingContext;
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            await database.DeleteInstructorAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }

}