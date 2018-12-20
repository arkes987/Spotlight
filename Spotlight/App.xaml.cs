using Spotlight.Models;
using Spotlight.ViewModels;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace Spotlight
{
    public partial class App : Application
    {
        public App()
        {
            SetupKeyboardHooks();
        }

        public MainViewModel MainViewModel;
        public MainWindow MainView;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainView = new MainWindow();
            MainViewModel = MainView.DataContext as MainViewModel;
            MainView.Show();
            MainView.TextBox1.Focus();
        }

        public GlobalKeyboardHook _globalKeyboardHook;

        public void SetupKeyboardHooks()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
        }

        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private void OnKeyPressed(object sender, KeyboardHook e)
        {
            if (e.KeyboardData.VirtualCode == 178)
            {
                Process currentProcess = Process.GetCurrentProcess();
                IntPtr hWnd = currentProcess.MainWindowHandle;

                if (hWnd != IntPtr.Zero)
                    SetForegroundWindow(hWnd);

                MainViewModel.IsVisible = Visibility.Visible;
                MainView.TextBox1.Focus();
            }
            else if (e.KeyboardData.VirtualCode == 27)
            {
                MainViewModel.IsVisible = Visibility.Hidden;
                MainViewModel.SearchInput = string.Empty;
            }

        }
        public void Dispose()
        {
            _globalKeyboardHook?.Dispose();
        }

    }
}
