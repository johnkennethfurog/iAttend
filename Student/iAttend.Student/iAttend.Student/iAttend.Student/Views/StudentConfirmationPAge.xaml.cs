using Xamarin.Forms;

namespace iAttend.Student.Views
{
    public partial class StudentConfirmationPage : ContentPage
    {
        public StudentConfirmationPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            grid.IsVisible = false;
        }
    }
}
