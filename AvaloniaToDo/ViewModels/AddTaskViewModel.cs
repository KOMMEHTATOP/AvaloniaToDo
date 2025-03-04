using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToDo.ViewModels
{
    public partial class AddTaskViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = string.Empty;

        public IRelayCommand AddTaskCommand { get; }

        public AddTaskViewModel()
        {
            AddTaskCommand = new RelayCommand(ExecuteAddTaskCommand, CanExecuteAddTaskCommand);
        }
        private void ExecuteAddTaskCommand()
        {
            // Логика добавления задачи (пока пустая, добавим позже)
        }

        private bool CanExecuteAddTaskCommand()
        {
            return !string.IsNullOrEmpty(Title); 
        }
    }
}
