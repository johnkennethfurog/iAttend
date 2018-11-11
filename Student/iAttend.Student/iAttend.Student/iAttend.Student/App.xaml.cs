using Prism;
using Prism.Ioc;
using iAttend.Student.ViewModels;
using iAttend.Student.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using DLToolkit.Forms.Controls;
using iAttend.Student.Interfaces;
using iAttend.Student.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace iAttend.Student
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
#if DEBUG
            LiveReload.Init();
#endif
            InitializeComponent();
            FlowListView.Init();
            await NavigationService.NavigateAsync("NavigationPage/LandingPage");
            //await NavigationService.NavigateAsync("ScannerPage");

            //var mainPage = new TabbedPage();
            //mainPage.Children.Add(new ScannerPage());

            //MainPage = new NavigationPage( new ScannerPage());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var apiAccess = new ApiAccess()
            {
                BaseUri = "https://10.40.1.197:5001/api"
            };


            containerRegistry.RegisterInstance<IApiAccess>(apiAccess);
            containerRegistry.Register<IRequestHandler, RequestHandler>();
            containerRegistry.Register<IStudentService, StudentService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LandingPage>();
            containerRegistry.RegisterForNavigation<SubjectAttendance>();


        }
    }
}
