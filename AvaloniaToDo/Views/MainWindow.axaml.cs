using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaToDo.ViewModels;
using System;

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
            var addTaskWindow = new AddTaskWindow
            {
                DataContext = new AddTaskViewModel()
            };
            addTaskWindow.ShowDialog(this); // Открываем модально относительно MainWindow
        }
    }
}