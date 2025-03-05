using System;

namespace AvaloniaToDo.ViewModels;

public class OpenAddTaskWindowEventArgs :EventArgs
{
    // класс прослойка для того чтобы передавать его между viewmodels.
    public AddTaskViewModel ViewModel { get; }
    public OpenAddTaskWindowEventArgs(AddTaskViewModel viewModel)
    {
        ViewModel = viewModel;
    }
}
