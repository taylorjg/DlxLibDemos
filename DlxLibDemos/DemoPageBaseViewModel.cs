using System.Collections.Concurrent;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using DlxLib;

namespace DlxLibDemos;

public partial class DemoPageBaseViewModel : ObservableObject, IWhatToDraw
{
  private ILogger<DemoPageBaseViewModel> _logger;
  private IBackgroundSolver _backgroundSolver;
  private IDemo _demo;
  private object _demoSettings;
  private object _demoOptionalSettings;
  private IDrawable _drawable;
  private object[] _solutionInternalRows;
  private bool _solutionAvailable;
  private IDispatcherTimer _dispatcherTimer;
  private bool _animationEnabled;
  private int _animationInterval;
  private ConcurrentQueue<BaseMessage> _messages = new ConcurrentQueue<BaseMessage>();
  private bool _isSolving;
  private CancellationTokenSource _cancellationTokenSource;

  public DemoPageBaseViewModel(Dependencies dependencies)
  {
    _logger = dependencies.Logger;
    _logger.LogInformation("constructor");
    _backgroundSolver = dependencies.BackgroundSolver;
    var dispatcherProvider = dependencies.DispatcherProvider;
    var dispatcher = dispatcherProvider.GetForCurrentThread();
    _dispatcherTimer = dispatcher.CreateTimer();
    _dispatcherTimer.Tick += (_, __) => OnTick();
    SolutionInternalRows = new object[0];
    AnimationEnabled = false;
    AnimationInterval = 10;
  }

  // https://stackoverflow.com/questions/52982560/asp-net-core-constructor-injection-with-inheritance
  public sealed class Dependencies
  {
    internal ILogger<DemoPageBaseViewModel> Logger { get; private set; }
    internal IBackgroundSolver BackgroundSolver { get; private set; }
    internal IDispatcherProvider DispatcherProvider { get; private set; }

    public Dependencies(
      ILogger<DemoPageBaseViewModel> logger,
      IBackgroundSolver backgroundSolver,
      IDispatcherProvider dispatcherProvider
    )
    {
      Logger = logger;
      BackgroundSolver = backgroundSolver;
      DispatcherProvider = dispatcherProvider;
    }
  }

  public event EventHandler NeedRedraw;

  public IDrawable Drawable
  {
    get => _drawable;
    set
    {
      _logger.LogInformation($"Drawable setter value: {value}");
      SetProperty(ref _drawable, value);
    }
  }

  public IDemo Demo
  {
    get => _demo;
    set
    {
      _logger.LogInformation($"Demo setter value: {value}");
      SetProperty(ref _demo, value);
      Drawable = _demo.CreateDrawable(this);
    }
  }
  public object DemoSettings
  {
    get => _demoSettings;
    set
    {
      _logger.LogInformation($"DemoSettings setter value: {value}");
      SetProperty(ref _demoSettings, value);
      RaiseNeedRedraw();
      SolutionInternalRows = new object[0];
    }
  }

  public object DemoOptionalSettings
  {
    get => _demoOptionalSettings;
    set
    {
      _logger.LogInformation($"DemoOptionalSettings setter value: {value}");
      SetProperty(ref _demoOptionalSettings, value);
      RaiseNeedRedraw();
    }
  }

  public object[] SolutionInternalRows
  {
    get => _solutionInternalRows;
    set
    {
      SetProperty(ref _solutionInternalRows, value);
      SolutionAvailable = _solutionInternalRows.Any();
      RaiseNeedRedraw();
    }
  }

  public bool SolutionAvailable
  {
    get => _solutionAvailable;
    set
    {
      SetProperty(ref _solutionAvailable, value);
      UpdateButtonCommands();
    }
  }

  public bool AnimationEnabled
  {
    get => _animationEnabled;
    set
    {
      _logger.LogInformation($"AnimationEnabled setter value: {value}");
      SetProperty(ref _animationEnabled, value);
    }
  }

  public int AnimationInterval
  {
    get => _animationInterval;
    set
    {
      if (value != _animationInterval)
      {
        _logger.LogInformation($"AnimationInterval setter value: {value}");
        SetProperty(ref _animationInterval, value);
        _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(_animationInterval);
      }
    }
  }

  public bool IsSolving
  {
    get => _isSolving;
    set
    {
      _logger.LogInformation($"IsSolving setter value: {value}");
      SetProperty(ref _isSolving, value);
      UpdateButtonCommands();
    }
  }

  private void UpdateButtonCommands()
  {
    SolveCommand.NotifyCanExecuteChanged();
    CancelCommand.NotifyCanExecuteChanged();
    ResetCommand.NotifyCanExecuteChanged();
  }

  private void OnTick()
  {
    if (_messages.TryDequeue(out BaseMessage message))
    {
      OnMessage(message as dynamic);
    }
  }

  private void OnMessage(SearchStepMessage message)
  {
    SolutionInternalRows = message.SolutionInternalRows;
  }

  private void OnMessage(SolutionFoundMessage message)
  {
    SolutionInternalRows = message.SolutionInternalRows;
    StopTimer();
  }

  private void OnMessage(NoSolutionFoundMessage message)
  {
    SolutionInternalRows = new object[0];
    StopTimer();
  }

  [RelayCommand(CanExecute = nameof(CanSolve))]
  private void Solve()
  {
    _cancellationTokenSource = new CancellationTokenSource();

    _backgroundSolver.Solve(
      AnimationEnabled,
      _messages.Enqueue,
      _cancellationTokenSource.Token,
      _demo,
      _demoSettings
    );

    StartTimer();
  }

  private bool CanSolve()
  {
    return !IsSolving && !SolutionAvailable;
  }

  [RelayCommand(CanExecute = nameof(CanReset))]
  private void Reset()
  {
    SolutionInternalRows = new object[0];
  }

  private bool CanReset()
  {
    return !IsSolving && SolutionAvailable;
  }

  [RelayCommand(CanExecute = nameof(CanCancel))]
  private void Cancel()
  {
    _cancellationTokenSource.Cancel();
    StopTimer();
  }

  private bool CanCancel()
  {
    return IsSolving;
  }

  private void StartTimer()
  {
    _logger.LogInformation("starting the dispatch timer");
    _dispatcherTimer.Start();
    IsSolving = true;
  }

  private void StopTimer()
  {
    _logger.LogInformation("stopping the dispatch timer");
    _dispatcherTimer.Stop();
    _messages.Clear();
    IsSolving = false;
  }

  private void RaiseNeedRedraw()
  {
    var handler = NeedRedraw;
    handler?.Invoke(this, EventArgs.Empty);
  }
}
