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
  private ISolver _solver;
  private IDemo _demo;
  private object _demoSettings;
  private object _demoOptionalSettings;
  private IDrawable _drawable;
  private object[] _solutionInternalRows;
  private bool _isSolution;
  private bool _solutionAvailable;
  private IDispatcherTimer _animationTimer;
  private bool _animationEnabled;
  private int _animationInterval;
  private ConcurrentQueue<BaseMessage> _messageQueue = new();
  private List<BaseMessage> _messageList = new();
  private bool _isBackgroundSolving;
  private bool _isTimerPaused;
  private CancellationTokenSource _cancellationTokenSource;
  private int _searchStepCount;
  private int _currentSearchStepNumber;
  private int _solutionCount;
  private int _currentSolutionNumber;

  public DemoPageBaseViewModel(Dependencies dependencies)
  {
    _logger = dependencies.Logger;
    _logger.LogInformation("constructor");
    _solver = dependencies.Solver;
    var dispatcherProvider = dependencies.DispatcherProvider;
    var dispatcher = dispatcherProvider.GetForCurrentThread();
    _animationTimer = dispatcher.CreateTimer();
    _animationTimer.Tick += (_, __) => OnAnimationTick();
    SolutionInternalRows = new object[0];
    AnimationEnabled = false;
    AnimationInterval = 10;
    SearchStepCount = 0;
    CurrentSearchStepNumber = 0;
    SolutionCount = 0;
    CurrentSolutionNumber = 0;
  }

  // https://stackoverflow.com/questions/52982560/asp-net-core-constructor-injection-with-inheritance
  public sealed class Dependencies
  {
    internal ILogger<DemoPageBaseViewModel> Logger { get; private set; }
    internal ISolver Solver { get; private set; }
    internal IDispatcherProvider DispatcherProvider { get; private set; }

    public Dependencies(
      ILogger<DemoPageBaseViewModel> logger,
      ISolver solver,
      IDispatcherProvider dispatcherProvider
    )
    {
      Logger = logger;
      Solver = solver;
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
      Reset();
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
      OnPropertyChanged(nameof(AnimationDisabled));
    }
  }

  public bool AnimationDisabled
  {
    get => !_animationEnabled;
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
        _animationTimer.Interval = TimeSpan.FromMilliseconds(_animationInterval);
      }
    }
  }

  public bool IsBackgroundSolving
  {
    get => _isBackgroundSolving;
    set
    {
      _logger.LogInformation($"IsBackgroundSolving setter value: {value}");
      SetProperty(ref _isBackgroundSolving, value);
      UpdateButtonCommands();
    }
  }

  public bool IsAnimating
  {
    get { return false; }
  }

  public bool IsBusy
  {
    get { return false; }
  }

  public bool IsTimerPaused
  {
    get => _isTimerPaused;
    set
    {
      _logger.LogInformation($"IsTimerPaused setter value: {value}");
      SetProperty(ref _isTimerPaused, value);
      UpdateButtonCommands();
    }
  }

  public int SearchStepCount
  {
    get => _searchStepCount;
    set
    {
      SetProperty(ref _searchStepCount, value);
      OnPropertyChanged(nameof(SearchStepSummary));
    }
  }

  public int CurrentSearchStepNumber
  {
    get => _currentSearchStepNumber;
    set
    {
      SetProperty(ref _currentSearchStepNumber, value);
      OnPropertyChanged(nameof(SearchStepSummary));
    }
  }

  public string SearchStepSummary
  {
    get => (CurrentSearchStepNumber, SearchStepCount) switch
    {
      (0, 0) => string.Empty,
      (0, _) => $"-/{SearchStepCount}",
      _ => $"{CurrentSearchStepNumber}/{SearchStepCount}"
    };
  }

  public int SolutionCount
  {
    get => _solutionCount;
    set
    {
      SetProperty(ref _solutionCount, value);
      OnPropertyChanged(nameof(SolutionSummary));
    }
  }

  public int CurrentSolutionNumber
  {
    get => _currentSolutionNumber;
    set
    {
      SetProperty(ref _currentSolutionNumber, value);
      OnPropertyChanged(nameof(SolutionSummary));
    }
  }

  public string SolutionSummary
  {
    get => (CurrentSolutionNumber, SolutionCount) switch
    {
      (0, 0) => string.Empty,
      (0, _) => $"-/{SolutionCount}",
      _ => $"{CurrentSolutionNumber}/{SolutionCount}"
    };
  }

  private void UpdateButtonCommands()
  {
    GoToFirstMessageCommand.NotifyCanExecuteChanged();
    GoToPreviousMessageCommand.NotifyCanExecuteChanged();
    GoToNextMessageCommand.NotifyCanExecuteChanged();
    GoToLastMessageCommand.NotifyCanExecuteChanged();

    PauseCommand.NotifyCanExecuteChanged();
    ResumeCommand.NotifyCanExecuteChanged();

    SolveCommand.NotifyCanExecuteChanged();
    CancelCommand.NotifyCanExecuteChanged();
    ResetCommand.NotifyCanExecuteChanged();
  }

  private void OnAnimationTick()
  {
    if (_messageQueue.TryDequeue(out BaseMessage message))
    {
      OnMessage(message as dynamic);
    }
  }

  private void OnMessage(SearchStepMessage message)
  {
    SolutionInternalRows = message.SolutionInternalRows;
    CurrentSearchStepNumber = message.SearchStepCount;
    _isSolution = false;
  }

  private void OnMessage(SolutionFoundMessage message)
  {
    SolutionInternalRows = message.SolutionInternalRows;
    CurrentSearchStepNumber = message.SearchStepCount;
    CurrentSolutionNumber = CurrentSolutionNumber + 1;
    _isSolution = true;
    PauseTimer();
  }

  private void OnMessage(FinishedMessage message)
  {
    SearchStepCount = message.SearchStepCount;

    if (!_isSolution)
    {
      SolutionInternalRows = new object[0];
    }

    StopTimer();
  }

  [RelayCommand(CanExecute = nameof(CanSolve))]
  private void Solve()
  {
    _cancellationTokenSource = new CancellationTokenSource();

    var onMessage = (BaseMessage message) =>
    {
      if (_cancellationTokenSource.IsCancellationRequested) return;

      var progressMessage = message as ProgressMessage;
      if (progressMessage != null)
      {
        SearchStepCount = progressMessage.SearchStepCount;
        return;
      }

      var solutionFoundMessage = message as SolutionFoundMessage;
      if (solutionFoundMessage != null)
      {
        SearchStepCount = solutionFoundMessage.SearchStepCount;
        SolutionCount = solutionFoundMessage.SolutionCount;
      }

      var finishedMessage = message as FinishedMessage;
      if (finishedMessage != null)
      {
        SearchStepCount = finishedMessage.SearchStepCount;
        SolutionCount = finishedMessage.SolutionCount;
        IsBackgroundSolving = false;
      }

      if (AnimationEnabled)
      {
        _messageQueue.Enqueue(message);
      }
      else
      {
        _messageList.Add(message);
        if (SolutionCount == 1) GoToFirstMessage();
      }

      UpdateButtonCommands();
    };

    var options = new SolverOptions(AnimationEnabled, _demo.ProgressFrequency);

    _solver.Solve(
      options,
      onMessage,
      _cancellationTokenSource.Token,
      _demo,
      _demoSettings
    );

    StartTimer();
  }

  private bool CanSolve()
  {
    return !IsBackgroundSolving && !SolutionAvailable;
  }

  [RelayCommand(CanExecute = nameof(CanReset))]
  private void Reset()
  {
    _messageList.Clear();
    _messageQueue.Clear();
    StopTimer();
    SolutionInternalRows = new object[0];
    _isSolution = false;
    SearchStepCount = 0;
    CurrentSearchStepNumber = 0;
    SolutionCount = 0;
    CurrentSolutionNumber = 0;
  }

  private bool CanReset()
  {
    return !IsBackgroundSolving && SolutionAvailable;
  }

  [RelayCommand(CanExecute = nameof(CanCancel))]
  private void Cancel()
  {
    _cancellationTokenSource.Cancel();
    StopTimer();
  }

  private bool CanCancel()
  {
    return IsBackgroundSolving || _animationTimer.IsRunning;
  }

  [RelayCommand(CanExecute = nameof(CanPause))]
  private void Pause()
  {
    PauseTimer();
  }

  private bool CanPause()
  {
    return _animationTimer.IsRunning;
  }

  [RelayCommand(CanExecute = nameof(CanResume))]
  private void Resume()
  {
    ResumeTimer();
  }

  private bool CanResume()
  {
    return !_animationTimer.IsRunning;
  }

  [RelayCommand(CanExecute = nameof(CanGoToFirstMessage))]
  private void GoToFirstMessage()
  {
    var message = _messageList[0] as SolutionFoundMessage;
    SolutionInternalRows = message.SolutionInternalRows;
    CurrentSearchStepNumber = message.SearchStepCount;
    CurrentSolutionNumber = 1;
    UpdateButtonCommands();
  }

  private bool CanGoToFirstMessage()
  {
    return SolutionCount > 0 && CurrentSolutionNumber > 1;
  }

  [RelayCommand(CanExecute = nameof(CanGoToPreviousMessage))]
  private void GoToPreviousMessage()
  {
    var currentSolutionIndex = CurrentSolutionNumber - 1;
    var message = _messageList[currentSolutionIndex - 1] as SolutionFoundMessage;
    SolutionInternalRows = message.SolutionInternalRows;
    CurrentSearchStepNumber = message.SearchStepCount;
    CurrentSolutionNumber = CurrentSolutionNumber - 1;
    UpdateButtonCommands();
  }

  private bool CanGoToPreviousMessage()
  {
    return SolutionCount > 0 && CurrentSolutionNumber > 1;
  }

  [RelayCommand(CanExecute = nameof(CanGoToNextMessage))]
  private void GoToNextMessage()
  {
    var currentSolutionIndex = CurrentSolutionNumber - 1;
    var message = _messageList[currentSolutionIndex + 1] as SolutionFoundMessage;
    SolutionInternalRows = message.SolutionInternalRows;
    CurrentSearchStepNumber = message.SearchStepCount;
    CurrentSolutionNumber = CurrentSolutionNumber + 1;
    UpdateButtonCommands();
  }

  private bool CanGoToNextMessage()
  {
    return SolutionCount > 0 && CurrentSolutionNumber < SolutionCount;
  }

  [RelayCommand(CanExecute = nameof(CanGoToLastMessage))]
  private void GoToLastMessage()
  {
    var message = _messageList[SolutionCount - 1] as SolutionFoundMessage;
    SolutionInternalRows = message.SolutionInternalRows;
    CurrentSearchStepNumber = message.SearchStepCount;
    CurrentSolutionNumber = SolutionCount;
    UpdateButtonCommands();
  }

  private bool CanGoToLastMessage()
  {
    return SolutionCount > 0 && CurrentSolutionNumber < SolutionCount;
  }

  private void StartTimer()
  {
    _logger.LogInformation("starting the animation timer");
    _animationTimer.Start();
    IsBackgroundSolving = true;
  }

  private void StopTimer()
  {
    _logger.LogInformation("stopping the animation timer");
    _animationTimer.Stop();
    _messageQueue.Clear();
    IsBackgroundSolving = false;
  }

  private void PauseTimer()
  {
    _logger.LogInformation("pausing the animation timer");
    _animationTimer.Stop();
    IsTimerPaused = true;
  }

  private void ResumeTimer()
  {
    _logger.LogInformation("resuming the animation timer");
    _animationTimer.Start();
    IsTimerPaused = false;
  }

  private void RaiseNeedRedraw()
  {
    var handler = NeedRedraw;
    handler?.Invoke(this, EventArgs.Empty);
  }
}
