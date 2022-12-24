using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.RippleEffect;

public partial class RippleEffectDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<RippleEffectDemoPageViewModel> _logger;
  private Puzzle _selectedPuzzle;

  public RippleEffectDemoPageViewModel(
    ILogger<RippleEffectDemoPageViewModel> logger,
    RippleEffectDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedPuzzle = Puzzles.First();
  }

  public Puzzle[] Puzzles { get => RippleEffect.Puzzles.ThePuzzles; }

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
