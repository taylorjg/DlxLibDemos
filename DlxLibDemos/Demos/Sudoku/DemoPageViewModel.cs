using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.Sudoku;

public partial class SudokuDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<SudokuDemoPageViewModel> _logger;
  private Puzzle _selectedPuzzle;

  public SudokuDemoPageViewModel(
    ILogger<SudokuDemoPageViewModel> logger,
    SudokuDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedPuzzle = Puzzles.First();
  }

  public Puzzle[] Puzzles { get => Sudoku.Puzzles.ThePuzzles; }

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
