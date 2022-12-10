using MetroLog.MicrosoftExtensions;
using CommunityToolkit.Maui;
using DlxLibDemos.Demos.Sudoku;
using DlxLibDemos.Demos.Pentominoes;
using DlxLibDemos.Demos.NQueens;
using DlxLibDemos.Demos.DraughtboardPuzzle;
using DlxLibDemos.Demos.TetraSticks;
using DlxLibDemos.Demos.AztecDiamond;

namespace DlxLibDemos;

public static class MauiProgram
{
  public static MauiApp CreateMauiApp()
  {
    var builder = MauiApp.CreateBuilder();

    builder
      .UseMauiApp<App>()
      .UseMauiCommunityToolkit()
      .ConfigureFonts(fonts =>
      {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
      });

    builder.Logging.AddStreamingFileLogger(options =>
      {
        options.RetainDays = 2;
        options.FolderPath = Path.Combine(FileSystem.CacheDirectory, "MetroLogs");
      });

    builder.Services.AddSingleton<INavigationService, NavigationService>();

    builder.Services.AddTransient<HomePageView>();
    builder.Services.AddTransient<HomePageViewModel>();

    builder.Services.AddTransient<DemoPageBaseViewModel.Dependencies>();

    builder.Services.AddTransient<SudokuDemoPageView>();
    builder.Services.AddTransient<SudokuDemoPageViewModel>();

    builder.Services.AddTransient<PentominoesDemoPageView>();
    builder.Services.AddTransient<PentominoesDemoPageViewModel>();

    builder.Services.AddTransient<NQueensDemoPageView>();
    builder.Services.AddTransient<NQueensDemoPageViewModel>();

    builder.Services.AddTransient<DraughtboardPuzzleDemoPageView>();
    builder.Services.AddTransient<DraughtboardPuzzleDemoPageViewModel>();

    builder.Services.AddTransient<TetraSticksDemoPageView>();
    builder.Services.AddTransient<TetraSticksDemoPageViewModel>();

    builder.Services.AddTransient<AztecDiamondDemoPageView>();
    builder.Services.AddTransient<AztecDiamondDemoPageViewModel>();

    builder.Services.AddTransient<SudokuThumbnailDrawable>();
    builder.Services.AddTransient<PentominoesThumbnailDrawable>();
    builder.Services.AddTransient<NQueensThumbnailDrawable>();
    builder.Services.AddTransient<DraughtboardPuzzleThumbnailDrawable>();
    builder.Services.AddTransient<TetraSticksThumbnailDrawable>();
    builder.Services.AddTransient<AztecDiamondThumbnailDrawable>();

    builder.Services.AddTransient<SudokuDemo>();
    builder.Services.AddTransient<PentominoesDemo>();
    builder.Services.AddTransient<NQueensDemo>();
    builder.Services.AddTransient<DraughtboardPuzzleDemo>();
    builder.Services.AddTransient<TetraSticksDemo>();
    builder.Services.AddTransient<AztecDiamondDemo>();

    Routing.RegisterRoute("SudokuDemoPage", typeof(SudokuDemoPageView));
    Routing.RegisterRoute("PentominoesDemoPage", typeof(PentominoesDemoPageView));
    Routing.RegisterRoute("NQueensDemoPage", typeof(NQueensDemoPageView));
    Routing.RegisterRoute("DraughtboardPuzzleDemoPage", typeof(DraughtboardPuzzleDemoPageView));
    Routing.RegisterRoute("TetraSticksDemoPage", typeof(TetraSticksDemoPageView));
    Routing.RegisterRoute("AztecDiamondDemoPage", typeof(AztecDiamondDemoPageView));

    return builder.Build();
  }
}
