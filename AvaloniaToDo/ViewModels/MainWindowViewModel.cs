using Avalonia.Controls;
using AvaloniaToDo.Models;
using AvaloniaToDo.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace AvaloniaToDo.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ToDoItem _selectedTask = new ToDoItem("Default Task");
        public ObservableCollection<ToDoItem> ItemsObservableCollectionVM { get; set; } = new();
        public IRelayCommand OpenAddTaskWindowCommand { get; }
        public event EventHandler<OpenAddTaskWindowEventArgs>? OpenAddTaskWindowRequested;
        public MainWindowViewModel()
        {
            OpenAddTaskWindowCommand = new RelayCommand(ExecuteOpenAddTaskWindowCommand);
        }

        private void ExecuteOpenAddTaskWindowCommand()
        {
            var addTaskVM = new AddTaskViewModel();
            addTaskVM.TaskAdded += OnTaskAdded; 
            OpenAddTaskWindowRequested?.Invoke(this, new OpenAddTaskWindowEventArgs(addTaskVM));
        }
        private void OnTaskAdded(object? sender, (string Title, string Description) e)
        {
            AddItem(e.Title, e.Description);
            Debug.WriteLine($"Задача добавлена: Title={e.Title} Description={e.Description}");
        }

        public void AddItem(string titleTask, string description)
        {
            if (!string.IsNullOrWhiteSpace(titleTask))
            {
                ItemsObservableCollectionVM.Add(new ToDoItem(titleTask) { Description = description });
            }
        }
    }
}
