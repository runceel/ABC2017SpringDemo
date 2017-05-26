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
        private INavigationService NavigationService { get; }
        private ITranslatorService TranslatorService { get; }

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

        public DelegateCommand<TagGroupViewModel> NavigateImageListPageCommand { get; }


        public MainPageViewModel(ITwitterService twitterService, 
            IVisionService visionService, 
            ITranslatorService translatorService, 
            INavigationService navigationService)
        {
            this.TwitterServhce = twitterService;
            this.VisionService = visionService;
            this.TranslatorService = translatorService;
            this.NavigationService = navigationService;

            this.AnalyzeCommand = new DelegateCommand(async () => await this.AnalyzeExecuteAsync());
            this.NavigateImageListPageCommand = new DelegateCommand<TagGroupViewModel>(async x => await this.NavigateImageListPageExecuteAsync(x));
        }

        private async Task NavigateImageListPageExecuteAsync(TagGroupViewModel x)
        {
            if (x == null)
            {
                return;
            }

            await this.NavigationService.NavigateAsync("ImageListPage", new NavigationParameters
            {
                { "data", x },
            });
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

            if (this.Tags != null)
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
                var tasks = (await this.VisionService.TaggingAsync(searchResults))
                    .GroupBy(x => x.Tag)
                    .Select(async x => new TagGroupViewModel
                    {
                        Tag = x.Key,
                        JpTag = await this.TranslatorService.TranslateToJapaneseAsync(x.Key.Replace("_", " ").Trim()),
                        Images = x,
                    })
                    .ToArray();
                await Task.WhenAll(tasks);
                this.Tags = tasks.Select(x => x.Result).ToArray();
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
