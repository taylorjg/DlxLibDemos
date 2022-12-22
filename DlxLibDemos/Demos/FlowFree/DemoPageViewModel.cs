using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.FlowFree;

public partial class FlowFreeDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<FlowFreeDemoPageViewModel> _logger;
  private Puzzle _selectedPuzzle;

  public FlowFreeDemoPageViewModel(
    ILogger<FlowFreeDemoPageViewModel> logger,
    FlowFreeDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedPuzzle = FlowFree.Puzzles.ThePuzzles.First();
  }

  public Puzzle[] Puzzles { get => FlowFree.Puzzles.ThePuzzles; }

  public Puzzle SelectedPuzzle
  {
    get => _selectedPuzzle;
    set
    {
      if (value != _selectedPuzzle)
      {
        _logger.LogInformation($"SelectedPuzzle setter value: {value}");
        SetProperty(ref _selectedPuzzle, value);
        DemoSettings = _selectedPuzzle;
      }
    }
  }
}