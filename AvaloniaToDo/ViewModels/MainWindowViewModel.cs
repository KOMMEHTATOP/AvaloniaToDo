using Avalonia.Controls;
using AvaloniaToDo.Models;
using AvaloniaToDo.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AvaloniaToDo.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ToDoItem _selectedTask = new ToDoItem("Default Task");
        public ObservableCollection<ToDoItem> ItemsObservableCollectionVM { get; set; } = new();
        public IRelayCommand OpenAddTaskWindowCommand { get; }
        public event EventHandler? OpenAddTaskWindowRequested;
        public MainWindowViewModel()
        {
            ItemsObservableCollectionVM.Add(new ToDoItem("Task1"));
            ItemsObservableCollectionVM.Add(new ToDoItem("Task2"));
            ItemsObservableCollectionVM.Add(new ToDoItem("Task3"));

            OpenAddTaskWindowCommand = new RelayCommand(ExecuteOpenAddTaskWindowCommand);
        }

        private void ExecuteOpenAddTaskWindowCommand()
        {
            OpenAddTaskWindowRequested?.Invoke(this, EventArgs.Empty);
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
