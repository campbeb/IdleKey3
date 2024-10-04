using H.NotifyIcon;
using Microsoft.UI.Xaml;
using Spectrum.CUE.SDK;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.ApplicationModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace IdleKey3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Window
    {
        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private Thread backgroundThread = null;
        private bool idleSet = false;
        private CancellationTokenSource cts = null;
        private double configuredIdleTimeMinutes = 30.0;
        private bool loading = true;
        public bool FirstRun {  get; set; }
        private bool cueConnected = false;
        private bool exiting = false;

        private const string SETTING_PROGRAM = "IdleKey3";
        private const string SETTING_IDLE_TIME = "idleTime";
        private const string SETTING_AUTOSTART = "autostart";

        public MainView()
        {
            FirstRun = true;

            this.InitializeComponent();
            this.AppWindow.Resize(new Windows.Graphics.SizeInt32(470, 330));
            this.Title = SETTING_PROGRAM;

            CUEConnect();
            LoadSettings();
            UpdateAutostartUI();
            NumberBoxIldeTime.Value = configuredIdleTimeMinutes;
            loading = false;
        }

        private void LoadSettings()
        {
            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                // Create a setting in a container
                Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer(SETTING_PROGRAM, Windows.Storage.ApplicationDataCreateDisposition.Always);
                if (localSettings.Containers.ContainsKey(SETTING_PROGRAM))
                {
                    if (localSettings.Containers[SETTING_PROGRAM].Values.TryGetValue(SETTING_IDLE_TIME, out object idleTime))
                    {
                        // existing install
                        FirstRun = false;
                        configuredIdleTimeMinutes = (double)idleTime;
                        WriteDebug($"Existing install: {configuredIdleTimeMinutes}");
                    }
                    else
                    {
                        // new install
                        WriteDebug($"new install");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteDebug($"Failed to load settings {ex.Message}");
            }
        }

        private void SaveSettings()
        {
            configuredIdleTimeMinutes = NumberBoxIldeTime.Value;

            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                // Create a setting in a container
                Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer(SETTING_PROGRAM, Windows.Storage.ApplicationDataCreateDisposition.Always);
                if (localSettings.Containers.ContainsKey(SETTING_PROGRAM))
                {
                    localSettings.Containers[SETTING_PROGRAM].Values[SETTING_IDLE_TIME] = configuredIdleTimeMinutes;
                }
            }
            catch (Exception ex)
            {
                WriteDebug($"Failed to save settings {ex.Message}");
            }

        }

        private void Window_VisibilityChanged(object sender, WindowVisibilityChangedEventArgs args)
        {
            if (args.Visible)
            {
                WriteDebug("newly visible");
                // close out background thread
                cts?.Cancel();
                backgroundThread?.Join();
                cts?.Dispose();
                cts = null;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            WriteDebug("OK Button click");
            HideWindowAndRun();
        }

        private void HideWindowAndRun()
        {
            WriteDebug("HideWindowAndRun");
            SaveSettings();
            StartBackgroundThread();

            this.Hide();
        }

        public void StartBackgroundThread()
        {
            cts = new CancellationTokenSource();

            backgroundThread = new Thread(new ParameterizedThreadStart(Idle_Check));
            backgroundThread.IsBackground = true;
            backgroundThread.Start(cts.Token);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            WriteDebug("Exit Button click");
            exiting = true;
            SaveSettings();

            // let the App close out
            App.HandleClosedEvents = false;
            cts?.Cancel();
            backgroundThread?.Join(100);
            cts?.Dispose();
            CUEDisconnect();
            App.Current.Exit();
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            WriteDebug("closed");
            if (!exiting)
            {
                // if user didn't click the Exit button, don't close the window
                args.Handled = true;
                HideWindowAndRun();
            }
        }

        async private void AutostartCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (loading)
                return;

            bool autostart = (bool)autostartCheckBox.IsChecked;

            StartupTask startupTask = await StartupTask.GetAsync(SETTING_PROGRAM);
            WriteDebug($"startupTask {startupTask.State.ToString()}");

            if (autostart)
            {
                StartupTaskState newState = await startupTask.RequestEnableAsync();
                WriteDebug($"newState {newState.ToString()}");
            }
            else
            {
                startupTask.Disable();
            }
        }

        private void UpdateAutostartUI()
        {
            StartupTask startupTask = StartupTask.GetAsync(SETTING_PROGRAM).AsTask().Result;
            switch (startupTask.State)
            {
                case StartupTaskState.Disabled:
                    WriteDebug("Startup is Disabled.");
                    autostartCheckBox.IsChecked = false;
                    autostartCheckBox.IsEnabled = true;
                    break;
                case StartupTaskState.DisabledByUser:
                    WriteDebug("Startup is DisabledByUser.");
                    autostartCheckBox.IsChecked = false;
                    autostartCheckBox.IsEnabled = true;
                    break;
                case StartupTaskState.DisabledByPolicy:
                    WriteDebug(
                        "Startup disabled by group policy, or not supported on this device");
                    autostartCheckBox.IsChecked = false;
                    autostartCheckBox.IsEnabled = false;
                    break;
                case StartupTaskState.Enabled:
                    WriteDebug("Startup is enabled.");
                    autostartCheckBox.IsChecked = true;
                    autostartCheckBox.IsEnabled = true;
                    break;
            }

        }

        private void Idle_Check(object obj)
        {
            WriteDebug("thread start");
            CancellationToken ct = (CancellationToken)obj;

            while (!ct.IsCancellationRequested)
            {
                double idle_ticks = 0.0;
                LASTINPUTINFO plii = default(LASTINPUTINFO);
                plii.cbSize = (uint)Marshal.SizeOf((object)plii);
                plii.dwTime = 0;
                if (GetLastInputInfo(ref plii))
                {
                    idle_ticks = Environment.TickCount - plii.dwTime;
                }

                double idle_time = idle_ticks / 1000.0 / 60.0;
                //WriteDebug($"Idle ticks {idle_ticks} Idle time {idle_time}");

                if (idle_time >= configuredIdleTimeMinutes)
                {
                    if (!idleSet)
                    {
                        TakeControl();
                        idleSet = true;
                    }
                }
                else
                {
                    if (idleSet)
                    {
                        ReleaseControl();
                        idleSet = false;
                    }
                }

                Thread.Sleep(100);
            }
        }

        private void CUEConnect()
        {
            if (cueConnected)
                return;

            CorsairError res = CUESDKNative.CorsairConnect(CUESDKNative.CorsairSessionStateChangedCallback, 0);
            if (res == CorsairError.CE_Success)
            {
                cueConnected = true;
                CUESDKNative.SessionStateChanged += CUESessionStateChanged;
            }
            else
            {
                WriteDebug($"Failed to connect: {res}");
            }
        }

        private void CUEDisconnect()
        {
            if (!cueConnected)
                return;

            CorsairError res = CUESDKNative.CorsairDisconnect();
            if (res != CorsairError.CE_Success)
            {
                // Can get here if we weren't connected
                WriteDebug("Disconnect failed");
            }
            cueConnected = false;
        }

        private void CUESessionStateChanged(object sender, CorsairSessionState e)
        {
            WriteDebug($"CorsairSessionState {e}");
        }

        private void TakeControl()
        {
            CUEConnect();

            CorsairError res = CUESDKNative.CorsairRequestControl((IntPtr)null, CorsairAccessLevel.CAL_ExclusiveLightingControl);
            if (res != CorsairError.CE_Success)
            {
                WriteDebug("Exclusive Control Failed");
            }
            else
            {
                WriteDebug("Exclusive control success");
            }
        }

        private void ReleaseControl()
        {
            CorsairError res = CUESDKNative.CorsairReleaseControl((IntPtr)null);
            if (res != CorsairError.CE_Success)
            {
                WriteDebug("Exclusive Release Failed");
            }
            else
            {
                WriteDebug("Exclusive release success");
            }
        }

        private void WriteDebug(string message)
        {
            
            Debug.WriteLine($"{DateTime.Now.ToString()} {message}");
        }

    }
}
