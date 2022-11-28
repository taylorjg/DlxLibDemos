using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.NQueens;

public partial class NQueensDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<NQueensDemoPageViewModel> _logger;
  private int _selectedGridSize;

  public NQueensDemoPageViewModel(
    ILogger<NQueensDemoPageViewModel> logger,
    NQueensDemo demo,
      DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedGridSize = AvailableGridSizes.Last();
  }

  public int[] AvailableGridSizes { get => new[] { 4, 5, 6, 7, 8 }; }

  public int SelectedGridSize
  {
    get => _selectedGridSize;
    set
    {
      if (value != _selectedGridSize)
      {
        _logger.LogInformation($"SelectedGridSize setter value: {value}");
        SetProperty(ref _selectedGridSize, value);
        DemoSettings = _selectedGridSize;
      }
    }
  }
}
