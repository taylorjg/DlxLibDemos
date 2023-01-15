using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Nonogram;

public partial class NonogramDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<NonogramDemoPageViewModel> _logger;
  private Puzzle _selectedPuzzle;
  private bool _showClues;

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
    ShowClues = true;
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

  public bool ShowClues
  {
    get => _showClues;
    set
    {
      _logger.LogInformation($"ShowClues setter value: {value}");
      SetProperty(ref _showClues, value);
      DemoOptionalSettings = _showClues;
    }
  }
}
