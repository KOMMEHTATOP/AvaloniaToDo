using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToDo.ViewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;
        
        public IRelayCommand AddTaskCommand { get; }
        public event EventHandler<(string Title, string Description)>? TaskAdded;
        public AddTaskViewModel()
        {
            AddTaskCommand = new RelayCommand(ExecuteAddTaskCommand, CanExecuteAddTaskCommand);
            PropertyChanged += OnPropertyChanged;
        }
        
        private void ExecuteAddTaskCommand()
        {
            TaskAdded?.Invoke(this, (Title, Description));
        }

        private bool CanExecuteAddTaskCommand()
        {
            return !string.IsNullOrEmpty(Title); 
        }
        
        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Title))
            {
                AddTaskCommand.NotifyCanExecuteChanged();
            }
        }
        
    }
}
