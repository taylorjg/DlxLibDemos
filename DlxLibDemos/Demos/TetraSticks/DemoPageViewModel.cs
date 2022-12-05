using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.TetraSticks;

public partial class TetraSticksDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<TetraSticksDemoPageViewModel> _logger;
  private string _selectedMissingLetter;

  public TetraSticksDemoPageViewModel(
    ILogger<TetraSticksDemoPageViewModel> logger,
    TetraSticksDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedMissingLetter = TheMissingLetters.First();
  }

  public static string[] TheMissingLetters = new[] { "H", "J", "L", "N", "Y" };

  public string[] MissingLetters
  {
    get => TheMissingLetters;
  }

  public string SelectedMissingLetter
  {
    get => _selectedMissingLetter;
    set
    {
      _logger.LogInformation($"SelectedMissingLetter setter value: {value}");
      SetProperty(ref _selectedMissingLetter, value);
      DemoSettings = _selectedMissingLetter;
    }
  }
}
