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
using Prism.Plugin.Popups;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;

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
            await NavigationService.NavigateAsync("NavigationPage/SwitchPage");
            //await NavigationService.NavigateAsync("NavigationPage/StudentLandingPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            var apiAccess = new ApiAccess()
            {
                //BaseUri = "http://192.168.100.4:10778"
                BaseUri = "http://192.168.0.24:10778"
            };

            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterInstance<IApiAccess>(apiAccess);
            containerRegistry.Register<IRequestHandler, RequestHandler>();
            containerRegistry.Register<IStudentService, StudentService>();
            containerRegistry.Register<ITeacherService, TeacherService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<StudentLandingPage>();
            containerRegistry.RegisterForNavigation<SubjectAttendance>();


            containerRegistry.RegisterForNavigation<TeacherLandingPage>();
            containerRegistry.RegisterForNavigation<SubjectStudentsPage>();
            containerRegistry.RegisterForNavigation<SwitchPage>();

            containerRegistry.RegisterForNavigation<StudentsAttendance>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            AppCenter.Start("android=f5a31609-d5dc-4641-88ac-720453f1143d;",
                  typeof(Analytics), typeof(Crashes));
        }
    }
}
