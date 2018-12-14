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
using System.Threading.Tasks;

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


            //await GotoStudentApp();

            await NavigationService.NavigateAsync("LoginPage");
        }

        async Task GotoStudentApp()
        {
            var preferences = Container.Resolve<IPreferences>();

            Helpers.Student.CurrentStudent = preferences.Get<Models.StudentInfo>(Helpers.Student.STUDENT_KEY);

            if (Helpers.Student.CurrentStudent == null)
                await NavigationService.NavigateAsync(nameof(StudentConfirmationPage));
            else
                await NavigationService.NavigateAsync(nameof(StudentLandingPage));

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

            var apiAccess = new ApiAccess()
            {
                //BaseUri = "http://192.168.137.1:10778"
                BaseUri = "http://192.168.1.50:10778"
            };

            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterInstance<IApiAccess>(apiAccess);
            containerRegistry.Register<IRequestHandler, RequestHandler>();
            containerRegistry.Register<IStudentService, StudentService>();
            containerRegistry.Register<ITeacherService, TeacherService>();
            containerRegistry.Register<IPreferences, Preference>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<StudentLandingPage>();
            containerRegistry.RegisterForNavigation<SubjectAttendance>();


            containerRegistry.RegisterForNavigation<TeacherLandingPage>();
            containerRegistry.RegisterForNavigation<SubjectStudentsPage>();
            containerRegistry.RegisterForNavigation<SwitchPage>();

            containerRegistry.RegisterForNavigation<StudentsAttendance>();
            containerRegistry.RegisterForNavigation<StudentConfirmationPage, StudentConfirmationPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportFilterPage, ReportFilterPageViewModel>();
            containerRegistry.RegisterForNavigation<AbsentStatPage, AbsentStatPageViewModel>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            AppCenter.Start("android=f5a31609-d5dc-4641-88ac-720453f1143d;",
                  typeof(Analytics), typeof(Crashes));
        }
    }
}
