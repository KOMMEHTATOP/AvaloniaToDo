using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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

        public IRelayCommand AddTaskCommand { get; }

        public AddTaskViewModel()
        {
            AddTaskCommand = new RelayCommand(ExecuteAddTaskCommand, CanExecuteAddTaskCommand);
            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Title))
                {
                    AddTaskCommand.NotifyCanExecuteChanged();
                }
            };
        }
        private void ExecuteAddTaskCommand()
        {
            // Логика добавления задачи (пока пустая, добавим позже)
            Debug.WriteLine("Задача добавлена: " + Title);
        }

        private bool CanExecuteAddTaskCommand()
        {
            Debug.WriteLine("Проверка CanExecute: " + Title);
            return !string.IsNullOrEmpty(Title); 
        }
    }
}
