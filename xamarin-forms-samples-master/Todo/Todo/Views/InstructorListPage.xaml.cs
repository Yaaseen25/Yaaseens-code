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
    public partial class InstructorListPage : ContentPage
    {
        public InstructorListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            TodoItemDatabase database = await TodoItemDatabase.Instance;
            //change to Instructor
            listView.ItemsSource = await database.GetInstructorsAsync();
        }

        async void OnInstructorAdded(object sender, EventArgs e)
        {
            // change to instructorItemPage
            await Navigation.PushAsync(new InstructorPage
            {
                BindingContext = new Instructor()
            });
        }

        async void OnListItemSelected1(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new InstructorPage
                {
                    BindingContext = e.SelectedItem as Instructor
                });
            }
        }

    }
}