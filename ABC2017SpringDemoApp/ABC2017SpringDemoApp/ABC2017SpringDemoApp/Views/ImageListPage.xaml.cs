using Xamarin.Forms;

namespace ABC2017SpringDemoApp.Views
{
    public partial class ImageListPage : ContentPage
    {
        public ImageListPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            ((ListView)sender).SelectedItem = null;
        }
    }
}
