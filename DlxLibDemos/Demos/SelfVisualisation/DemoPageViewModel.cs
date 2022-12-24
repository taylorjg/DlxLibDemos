using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.SelfVisualisation;

public partial class SelfVisualisationDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<SelfVisualisationDemoPageViewModel> _logger;
  private SampleMatrix _selectedSampleMatrix;

  public SelfVisualisationDemoPageViewModel(
    ILogger<SelfVisualisationDemoPageViewModel> logger,
    SelfVisualisationDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    SelectedSampleMatrix = SampleMatrices.First();
  }

  public SampleMatrix[] SampleMatrices { get => SelfVisualisation.SampleMatrices.TheSampleMatrices; }

  public SampleMatrix SelectedSampleMatrix
  {
    get => _selectedSampleMatrix;
    set
    {
      if (value != _selectedSampleMatrix)
      {
        _logger.LogInformation($"SelectedSampleMatrix setter value: {value}");
        SetProperty(ref _selectedSampleMatrix, value);
        DemoSettings = _selectedSampleMatrix;
      }
    }
  }
}
