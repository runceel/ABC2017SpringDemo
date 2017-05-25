using Autofac;
using Prism.Autofac;
using Prism.Autofac.Forms;
using ABC2017SpringDemoApp.Views;
using Xamarin.Forms;
using ABC2017SpringDemoApp.Services;

namespace ABC2017SpringDemoApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override async void OnInitialized()
        {
            this.InitializeComponent();

            await this.NavigationService.NavigateAsync("SplashScreenPage");
            await this.Container.Resolve<ITwitterService>().InitializeAsync();
            await this.NavigationService.NavigateAsync("/NavigationPage/MainPage");
        }

        protected override void RegisterTypes()
        {
            var b = new ContainerBuilder();
            b.RegisterType<TwitterService>().As<ITwitterService>().SingleInstance();
            b.RegisterType<VisionService>().As<IVisionService>().SingleInstance();
            // 新しいAutofacではUpdateは非推奨らしいが今のところ回避策が見つからないので使う…
            b.Update(this.Container);

            this.Container.RegisterTypeForNavigation<NavigationPage>();
            this.Container.RegisterTypeForNavigation<MainPage>();
            this.Container.RegisterTypeForNavigation<SplashScreenPage>();
        }
    }
}
