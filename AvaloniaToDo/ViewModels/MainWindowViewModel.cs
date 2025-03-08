using Avalonia.Collections;
using AvaloniaToDo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace AvaloniaToDo.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private ToDoItem _selectedTask = new ToDoItem("Default Task");
        private bool _showOnlyCompletedTasks;
        public bool ShowOnlyCompletedTasks
        {
            get => _showOnlyCompletedTasks;
            set
            {
                this.SetProperty(ref _showOnlyCompletedTasks, value);
                FilteredTasksView.Refresh();
            }
        }        
        public DataGridCollectionView FilteredTasksView { get; }
        public ObservableCollection<ToDoItem> ItemsObservableCollectionVm { get; set; } = new();
        public IRelayCommand OpenAddTaskWindowCommand { get; }
        public IRelayCommand DeleteTask { get; }
        public event EventHandler<OpenAddTaskWindowEventArgs>? OpenAddTaskWindowRequested;
        public MainWindowViewModel()
        {
            OpenAddTaskWindowCommand = new RelayCommand(ExecuteOpenAddTaskWindowCommand);
            DeleteTask = new RelayCommand(ExecuteDeleteTaskCommand, CanExecuteDeleteTaskCommand);
            ItemsObservableCollectionVm.CollectionChanged += OnCollectionChanged;
            FilteredTasksView = new DataGridCollectionView(ItemsObservableCollectionVm)
            {
                Filter = item =>!ShowOnlyCompletedTasks || !((ToDoItem)item).IsCompleted
            };
            
            ItemsObservableCollectionVm.Add(new ToDoItem("Убрать мусор") {Description = "ASDF", IsCompleted = true});
            ItemsObservableCollectionVm.Add(new ToDoItem("Убрать мусор") {Description = "ASDF", IsCompleted = false});
            ItemsObservableCollectionVm.Add(new ToDoItem("Убрать мусор") {Description = "ASDF", IsCompleted = true});
            ItemsObservableCollectionVm.Add(new ToDoItem("Убрать мусор") {Description = "ASDF", IsCompleted = false});
            ItemsObservableCollectionVm.Add(new ToDoItem("Убрать мусор") {Description = "ASDF", IsCompleted = true});
            ItemsObservableCollectionVm.Add(new ToDoItem("Убрать мусор") {Description = "ASDF", IsCompleted = false});
            ItemsObservableCollectionVm.Add(new ToDoItem("Убрать мусор") {Description = "ASDF", IsCompleted = true});
        }
        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            DeleteTask.NotifyCanExecuteChanged();
        }
        private bool CanExecuteDeleteTaskCommand()
        {
            return ItemsObservableCollectionVm.Count > 0;
        }
        private void ExecuteDeleteTaskCommand()
        {
            ItemsObservableCollectionVm.Remove(SelectedTask);
        }

        private void ExecuteOpenAddTaskWindowCommand()
        {
            var addTaskVm = new AddTaskViewModel();
            addTaskVm.TaskAdded += OnTaskAdded; 
            OpenAddTaskWindowRequested?.Invoke(this, new OpenAddTaskWindowEventArgs(addTaskVm));
        }
        private void OnTaskAdded(object? sender, (string Title, string Description) e)
        {
            AddItem(e.Title, e.Description);
            SelectedTask = ItemsObservableCollectionVm.Last();
        }

        private void AddItem(string titleTask, string description)
        {
            if (!string.IsNullOrWhiteSpace(titleTask))
            {
                ItemsObservableCollectionVm.Add(new ToDoItem(titleTask) { Description = description });
            }
        }
    }
}
