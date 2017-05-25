using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABC2017SpringDemoApp.ViewModels
{
    public class ImageListPageViewModel : BindableBase, INavigationAware
    {
        private TagGroupViewModel data;

        public TagGroupViewModel Data
        {
            get { return this.data; }
            set { this.SetProperty(ref this.data, value); }
        }

        public ImageListPageViewModel()
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters == null)
            {
                return;
            }

            if (!parameters.ContainsKey("data"))
            {
                return;
            }

            this.Data = (TagGroupViewModel)parameters["data"];
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
