namespace DlxLibDemos.Tests;

public class MockDispatcherTimer : IDispatcherTimer
{
  private bool _isRunning = false;

  public TimeSpan Interval { get; set; }
  public bool IsRepeating { get; set; }

  public bool IsRunning => _isRunning;

  public event EventHandler Tick;

  public void Start()
  {
    _isRunning = true;
  }

  public void Stop()
  {
    _isRunning = false;
  }
}
