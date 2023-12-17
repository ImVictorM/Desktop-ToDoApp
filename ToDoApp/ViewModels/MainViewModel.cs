using ReactiveUI;
using System;
using System.Reactive.Linq;
using ToDoApp.DataModels;
using ToDoApp.Services;

namespace ToDoApp.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel;

    public MainViewModel()
    {
        var service = new ToDoListService();
        ToDoList = new ToDoListViewModel(service.GetItems());
        _contentViewModel = ToDoList;
    }

    public ToDoListViewModel ToDoList { get; }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
    }

    public void AddItem()
    {
        AddItemViewModel addItemViewModel = new();

        Observable
            .Merge(
                addItemViewModel.OkCommand,
                addItemViewModel.CancelCommand.Select(_ => (ToDoItem?)null))
            .Take(1)
            .Subscribe((ToDoItem? newItem) =>
            {
                if (newItem != null)
                {
                    ToDoList.ListItems.Add(newItem);
                }

                ContentViewModel = ToDoList;
            });
               

        ContentViewModel = addItemViewModel;
    }
}
