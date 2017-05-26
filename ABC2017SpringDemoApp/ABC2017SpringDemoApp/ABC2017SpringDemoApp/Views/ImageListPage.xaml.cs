using ABC2017SpringDemoApp.BusinessObjects;
using System;
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

            this.imagePreview.Source = ImageSource.FromUri(new Uri(((TaggedImage)e.SelectedItem).Image));
            this.frameImageHost.IsVisible = true;
            this.frameImageHostBackground.IsVisible = true;

            ((ListView)sender).SelectedItem = null;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            this.frameImageHostBackground.IsVisible = false;
            this.frameImageHost.IsVisible = false;
            this.imagePreview.Source = null;
        }
    }
}
