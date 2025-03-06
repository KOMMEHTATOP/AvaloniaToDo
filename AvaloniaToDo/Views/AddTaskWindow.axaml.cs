using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaToDo.ViewModels;
using System;

namespace AvaloniaToDo.Views;

public partial class AddTaskWindow : Window
{
    public AddTaskWindow()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }
    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (DataContext is AddTaskViewModel viewModel)
        {
            viewModel.TaskAdded += OnTaskAdded;
        }
    }
    private void OnTaskAdded(object? sender, (string Title, string Description) e)
    {
        Close();
    }
}