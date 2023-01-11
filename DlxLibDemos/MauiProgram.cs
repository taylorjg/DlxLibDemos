using MetroLog.MicrosoftExtensions;
using CommunityToolkit.Maui;
using DlxLibDemos.Demos.Sudoku;
using DlxLibDemos.Demos.Pentominoes;
using DlxLibDemos.Demos.NQueens;
using DlxLibDemos.Demos.DraughtboardPuzzle;
using DlxLibDemos.Demos.TetraSticks;
using DlxLibDemos.Demos.AztecDiamond;
using DlxLibDemos.Demos.RippleEffect;
using DlxLibDemos.Demos.FlowFree;
using DlxLibDemos.Demos.Kakuro;
using DlxLibDemos.Demos.Nonogram;
using DlxLibDemos.Demos.SelfVisualisation;

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

    builder.Services.AddTransient<ISolver, BackgroundSolver>();

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

    builder.Services.AddTransient<RippleEffectDemoPageView>();
    builder.Services.AddTransient<RippleEffectDemoPageViewModel>();

    builder.Services.AddTransient<FlowFreeDemoPageView>();
    builder.Services.AddTransient<FlowFreeDemoPageViewModel>();

    builder.Services.AddTransient<KakuroDemoPageView>();
    builder.Services.AddTransient<KakuroDemoPageViewModel>();

    builder.Services.AddTransient<NonogramDemoPageView>();
    builder.Services.AddTransient<NonogramDemoPageViewModel>();

    builder.Services.AddTransient<SelfVisualisationDemoPageView>();
    builder.Services.AddTransient<SelfVisualisationDemoPageViewModel>();

    builder.Services.AddTransient<SudokuThumbnailDrawable>();
    builder.Services.AddTransient<PentominoesThumbnailDrawable>();
    builder.Services.AddTransient<NQueensThumbnailDrawable>();
    builder.Services.AddTransient<DraughtboardPuzzleThumbnailDrawable>();
    builder.Services.AddTransient<TetraSticksThumbnailDrawable>();
    builder.Services.AddTransient<AztecDiamondThumbnailDrawable>();
    builder.Services.AddTransient<RippleEffectThumbnailDrawable>();
    builder.Services.AddTransient<FlowFreeThumbnailDrawable>();
    builder.Services.AddTransient<KakuroThumbnailDrawable>();
    builder.Services.AddTransient<NonogramThumbnailDrawable>();
    builder.Services.AddTransient<SelfVisualisationThumbnailDrawable>();

    builder.Services.AddTransient<SudokuDemo>();
    builder.Services.AddTransient<PentominoesDemo>();
    builder.Services.AddTransient<NQueensDemo>();
    builder.Services.AddTransient<DraughtboardPuzzleDemo>();
    builder.Services.AddTransient<TetraSticksDemo>();
    builder.Services.AddTransient<AztecDiamondDemo>();
    builder.Services.AddTransient<RippleEffectDemo>();
    builder.Services.AddTransient<FlowFreeDemo>();
    builder.Services.AddTransient<KakuroDemo>();
    builder.Services.AddTransient<NonogramDemo>();
    builder.Services.AddTransient<SelfVisualisationDemo>();

    Routing.RegisterRoute("SudokuDemoPage", typeof(SudokuDemoPageView));
    Routing.RegisterRoute("PentominoesDemoPage", typeof(PentominoesDemoPageView));
    Routing.RegisterRoute("NQueensDemoPage", typeof(NQueensDemoPageView));
    Routing.RegisterRoute("DraughtboardPuzzleDemoPage", typeof(DraughtboardPuzzleDemoPageView));
    Routing.RegisterRoute("TetraSticksDemoPage", typeof(TetraSticksDemoPageView));
    Routing.RegisterRoute("AztecDiamondDemoPage", typeof(AztecDiamondDemoPageView));
    Routing.RegisterRoute("RippleEffectDemoPage", typeof(RippleEffectDemoPageView));
    Routing.RegisterRoute("FlowFreeDemoPage", typeof(FlowFreeDemoPageView));
    Routing.RegisterRoute("KakuroDemoPage", typeof(KakuroDemoPageView));
    Routing.RegisterRoute("NonogramDemoPage", typeof(NonogramDemoPageView));
    Routing.RegisterRoute("SelfVisualisationDemoPage", typeof(SelfVisualisationDemoPageView));

    return builder.Build();
  }
}
