using ReactiveUI;
using System.Reactive;
using ToDoApp.DataModels;

namespace ToDoApp.ViewModels;

public class AddItemViewModel: ViewModelBase
{
    private string _description = string.Empty;

    public ReactiveCommand<Unit, ToDoItem> OkCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public AddItemViewModel()
    {
        var isValidObservable = this.WhenAnyValue(
            x => x.Description,
            x => !string.IsNullOrEmpty(x));

        OkCommand = ReactiveCommand.Create(
            () => new ToDoItem { Description = Description }, 
            isValidObservable);

        CancelCommand = ReactiveCommand.Create(() => { });
    }

    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
}
