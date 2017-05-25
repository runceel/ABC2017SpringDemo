using ABC2017SpringDemoApp.BusinessObjects;
using ABC2017SpringDemoApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ABC2017SpringDemoApp.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private ITwitterService TwitterServhce { get; }
        private IVisionService VisionService { get; }

        private IEnumerable<TagGroupViewModel> tags;

        public IEnumerable<TagGroupViewModel> Tags
        {
            get { return this.tags; }
            set { this.SetProperty(ref this.tags, value); }
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.SetProperty(ref this.isBusy, value); }
        }

        public DelegateCommand AnalyzeCommand { get; }


        public MainPageViewModel(ITwitterService twitterService, IVisionService visionService)
        {
            this.TwitterServhce = twitterService;
            this.VisionService = visionService;
            this.AnalyzeCommand = new DelegateCommand(async () => await this.AnalyzeExecuteAsync());
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

            this.AnalyzeCommand.Execute();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        private async Task AnalyzeExecuteAsync()
        {
            this.IsBusy = true;
            try
            {
                var searchResults = await this.TwitterServhce.SearchImageAsync(Consts.SearchKeyword);
                this.Tags = (await this.VisionService.TaggingAsync(searchResults))
                    .GroupBy(x => x.Tag)
                    .Select(x => new TagGroupViewModel
                    {
                        Tag = x.Key,
                        Images = x,
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
