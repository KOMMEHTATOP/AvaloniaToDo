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
            ItemsObservableCollectionVm.Add(new ToDoItem("ОЧЕНЬ ДЛИННОЕ НАЗВАНИЕ ЗАГОЛОВКА, СПЕЦИАЛЬНО ЧТОБЫ НЕ ВМЕЩАЛОСЬ В ИНТЕРФЕЙС ПОЛЬЗОВАТЕЛЯ") {Description = "ASDF", IsCompleted = false});
            ItemsObservableCollectionVm.Add(new ToDoItem("Огромное описание задачи") {Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec sollicitudin ligula nec erat sodales commodo. Vivamus et nunc non leo convallis fringilla in ac est. Duis egestas eu purus id malesuada. Maecenas pulvinar aliquam efficitur. Phasellus viverra dignissim dui a dignissim. Proin porttitor urna ante, nec dignissim sapien accumsan et. Ut ac lectus nec purus vestibulum accumsan in eu erat. Maecenas condimentum aliquet lacus, ut aliquet lectus. Mauris pharetra quam nec enim congue dapibus. Donec ac tincidunt leo, sed lacinia dolor. Suspendisse scelerisque erat ac malesuada lacinia. Cras non vulputate mi. Integer vulputate sodales justo nec euismod. Maecenas sollicitudin augue metus, et varius ex rhoncus id. Nulla euismod pellentesque massa, fringilla aliquam tellus facilisis nec.\n\nDonec ullamcorper mauris sed lectus posuere aliquam. Suspendisse eros nisi, dignissim at neque et, hendrerit luctus orci. Donec sit amet neque nec orci venenatis viverra. Nam porta ac tortor id molestie. Phasellus non est erat. Fusce faucibus felis at magna tristique cursus. Quisque non varius nisi. Cras ut lorem nec mi posuere aliquet in a lectus. Ut bibendum justo neque, vitae placerat velit rutrum ac. Nullam eu aliquam massa, id hendrerit odio. Integer pharetra eget velit nec rutrum. In venenatis mattis nibh vel fermentum. Integer tincidunt ex mauris, fringilla ultrices sapien porttitor a. Aliquam ut tortor ut tortor molestie cursus.\n\nMaecenas eleifend scelerisque dolor sed tincidunt. Praesent mattis varius diam sed ornare. Praesent sagittis velit vitae porta dapibus. Mauris a malesuada metus. In maximus ex augue, sed imperdiet quam hendrerit in. Phasellus porttitor risus sit amet dui bibendum, vitae blandit nisl mattis. Vestibulum ornare egestas bibendum. Phasellus tincidunt ante vitae aliquet tincidunt. Phasellus sit amet augue at nulla pharetra lobortis. Nam a ultricies libero.\n\nNam quam justo, feugiat at tortor ac, fermentum euismod metus. Proin elementum diam non placerat tempor. Maecenas scelerisque orci in dolor dictum lacinia. Sed vehicula justo id pretium dictum. Cras convallis justo nec libero elementum, ac ullamcorper lectus blandit. Maecenas neque risus, sollicitudin eu arcu fermentum, tincidunt interdum nulla. Etiam non porta massa. Duis ullamcorper placerat consequat. Maecenas sodales iaculis feugiat. Vivamus in scelerisque velit, ut vehicula est.\n\nSuspendisse bibendum enim ligula, id cursus sem lacinia vitae. Donec imperdiet non lacus vel gravida. Fusce porttitor, sapien ut blandit egestas, neque eros ultricies ante, eu rutrum elit lacus ac diam. In ullamcorper, felis eu gravida consectetur, arcu ante viverra felis, nec vehicula purus enim a diam. Curabitur dignissim sodales risus. Nam eu pellentesque purus, vitae vulputate diam. Duis malesuada mi ante, eleifend tempor nunc efficitur quis. Aliquam erat volutpat. Nulla scelerisque tellus at elit gravida volutpat id vitae libero. Donec sit amet enim et lectus eleifend scelerisque. Nam consequat porta purus eu malesuada. Aliquam sollicitudin commodo quam in placerat.\n\n", IsCompleted = false});
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
