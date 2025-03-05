using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaToDo.ViewModels;
using System;
using System.Diagnostics;

namespace AvaloniaToDo.Views
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel ViewModel => (MainWindowViewModel)DataContext!;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        protected override void OnLoaded(RoutedEventArgs e) 
        {
            base.OnLoaded(e);
            ViewModel.OpenAddTaskWindowRequested += OpenAddTaskWindow;
        }

        private void OpenAddTaskWindow(object? sender, System.EventArgs e)
        {
            if (e is OpenAddTaskWindowEventArgs args)
            {
                var addTaskWindow = new AddTaskWindow
                {
                    DataContext = args.ViewModel
                };
                addTaskWindow.ShowDialog(this);
            }
            else
            {
                Debug.WriteLine("Ошибка: аргументы события не содержат вьюмодель");
            }
        }
    }
}