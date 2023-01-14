using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Nonogram;

public partial class NonogramDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<NonogramDemoPageViewModel> _logger;
  private Puzzle _selectedPuzzle;

  public NonogramDemoPageViewModel(
    ILogger<NonogramDemoPageViewModel> logger,
    NonogramDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedPuzzle = Puzzles.First();
  }

  public Puzzle[] Puzzles { get => Nonogram.Puzzles.ThePuzzles; }

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
