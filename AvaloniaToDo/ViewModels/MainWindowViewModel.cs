using Avalonia.Controls;
using AvaloniaToDo.Models;
using AvaloniaToDo.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace AvaloniaToDo.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ToDoItem _selectedTask = new ToDoItem("Default Task");
        public ObservableCollection<ToDoItem> ItemsObservableCollectionVM { get; set; } = new();
        public IRelayCommand OpenAddTaskWindowCommand { get; }
        public IRelayCommand DeleteTask { get; }
        public event EventHandler<OpenAddTaskWindowEventArgs>? OpenAddTaskWindowRequested;
        public MainWindowViewModel()
        {
            OpenAddTaskWindowCommand = new RelayCommand(ExecuteOpenAddTaskWindowCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTaskCommand, CanExecuteDeleteTaskCommand);
            ItemsObservableCollectionVM.CollectionChanged += OnCollectionChanged;
        }
        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            DeleteTask.NotifyCanExecuteChanged();
        }
        private bool CanExecuteDeleteTaskCommand()
        {
            return ItemsObservableCollectionVM.Count > 0;
        }
        private void ExecuteDeleteTaskCommand()
        {
            ItemsObservableCollectionVM.Remove(SelectedTask);
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
            SelectedTask = ItemsObservableCollectionVM.Last();
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
