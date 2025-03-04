using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaToDo.ViewModels;

namespace AvaloniaToDo.Views;

public partial class AddTaskWindow : Window
{
    public AddTaskWindow()
    {
        InitializeComponent();
        DataContext = new AddTaskViewModel();
    }
}