using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using QrWindow.Models;
using QrWindow.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using ZXing.Mobile;
using ZXing.QrCode;

namespace QrWindow.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public ObservableCollection<Schedule> Schedules { get; set; }

        private readonly IScheduleService _scheduleService;

        public MainViewModel()
        {
            _scheduleService = new ScheduleService();

            Schedules = new ObservableCollection<Schedule>();
        }

        private ImageSource _source;

        public ImageSource Source
        {
            get { return _source; }
            set {  Set(ref  _source,value); }
        }

        private Schedule _selectedSchedule;
        public Schedule SelectedSchedule
        {
            get { return _selectedSchedule; }
            set { Set(ref _selectedSchedule, value); }
        }

        private Windows.UI.Xaml.Visibility _isVisible;
        public Windows.UI.Xaml.Visibility IsVisible
        {
            get { return _isVisible; }
            set { Set(ref _isVisible, value); }
        }

        private RelayCommand _viewLoadedCommand;
        public RelayCommand ViewLoadedCommand =>
            _viewLoadedCommand ?? (_viewLoadedCommand = new RelayCommand(ExecuteViewLoadedCommand));

        async void ExecuteViewLoadedCommand()
        {
            await DoConnect();

            await FetchSchedules();
            
        }

        private async Task FetchSchedules()
        {
            var schedules = await _scheduleService.GetSchedules(_roomNumber);
            schedules.ForEach(schedule =>
            {
                Schedules.Add(schedule);
            });

        }

        internal string _roomNumber = "LAB 1";

        private async Task DoConnect()
        {
            try
            {

                var hubConn = new HubConnectionBuilder()
                    .WithUrl($"{Util.BASE_URL}/notifier", option =>
                    {
                        option.UseDefaultCredentials = true;
                    })
                    .Build();



                hubConn.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await hubConn.StartAsync();
                };

                hubConn.On("BroadcastMessage", async (string room, string students, int attendanceSessionId,int scheduleId, string guid) =>
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {


                        SelectedSchedule = Schedules.FirstOrDefault(x => x.ScheduleID == scheduleId);


                        if (SelectedSchedule == null || !IsIntendedReceiver(room))
                            return;

                        var write = new BarcodeWriter
                        {
                            Format = ZXing.BarcodeFormat.QR_CODE,
                            Options = new QrCodeEncodingOptions
                            {
                                Width = 400,
                                Height = 400
                            }
                        };
                        var subjJson = JsonConvert.SerializeObject(new SchedulePayload(SelectedSchedule.Time,SelectedSchedule.Subject));

                        var wb = write.Write($"{students}|{attendanceSessionId}|{guid}|{subjJson}");
                        Source = wb;
                        SetSelectedSchedule();
                        IsVisible = Windows.UI.Xaml.Visibility.Visible;
                    });
                });

                hubConn.On("StopBroadcasting", async (string room) =>
                {
                    if (!IsIntendedReceiver(room) || SelectedSchedule == null)
                        return;

                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {

                        SelectedSchedule.BackColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                        Source = null;
                        IsVisible = Windows.UI.Xaml.Visibility.Collapsed;
                    });
                });


                await hubConn.StartAsync();

            }
            catch (HttpRequestException ex)

            {
            }
        }

        void SetSelectedSchedule()
        {
            SelectedSchedule.BackColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 124));
        }

        bool IsIntendedReceiver(string room)
        {
            return room == _roomNumber;
        }
    }
}
