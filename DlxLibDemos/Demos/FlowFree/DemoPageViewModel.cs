using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.FlowFree;

public partial class FlowFreeDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<FlowFreeDemoPageViewModel> _logger;
  private PuzzleSizeEntry _selectedPuzzleSize;
  private Puzzle[] _puzzlesOfSelectedSize;
  private Puzzle _selectedPuzzle;
  private bool _showLabels;

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
    SelectedPuzzleSize = PuzzleSizes.First();
    ShowLabels = true;
  }

  public PuzzleSizeEntry[] PuzzleSizes { get => FlowFree.Puzzles.ThePuzzleSizes; }

  public PuzzleSizeEntry SelectedPuzzleSize
  {
    get => _selectedPuzzleSize;
    set
    {
      if (value != _selectedPuzzleSize)
      {
        SetProperty(ref _selectedPuzzleSize, value);
        PuzzlesOfSelectedSize = FlowFree.Puzzles.ThePuzzles
          .Where(puzzle => puzzle.Size == _selectedPuzzleSize.Size)
          .ToArray();
      }
    }
  }

  public Puzzle[] PuzzlesOfSelectedSize
  {
    get => _puzzlesOfSelectedSize;
    set
    {
      SetProperty(ref _puzzlesOfSelectedSize, value);
      SelectedPuzzle = _puzzlesOfSelectedSize.First();
    }
  }

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

  public bool ShowLabels
  {
    get => _showLabels;
    set
    {
      _logger.LogInformation($"ShowLabels setter value: {value}");
      SetProperty(ref _showLabels, value);
      DemoOptionalSettings = _showLabels;
    }
  }
}
