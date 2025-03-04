using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaToDo.Models
{
    public partial class ToDoItem : ObservableObject
    {
        private string _title;
        private string _description;
        private bool _isCompleted;

        public ToDoItem(string title)
        {
            _title = title;
            _description = string.Empty;
            _isCompleted = false;
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }
    }
}