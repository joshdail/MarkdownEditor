open Avalonia
open Avalonia.Controls
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.FuncUI.Hosts
open Avalonia.FuncUI.Elmish
open Avalonia.Themes.Fluent
open Avalonia.Styling
open Elmish

type MainWindow() as this = 
    inherit HostWindow()
    do
        base.Title <- "Markdown Editor"
        base.Width <- 800.0
        base.Height <- 600.0

        Program.mkProgram Main.init Main.update Main.view
        |> Program.withHost this
        |> Program.run

type App() =
    inherit Application()

    override this.Initialize () =
        this.Styles.Add(FluentTheme())
    override this.OnFrameworkInitializationCompleted (): unit = 
            match this.ApplicationLifetime with
            | :? IClassicDesktopStyleApplicationLifetime as desktop ->
                desktop.MainWindow <- MainWindow()
            | _ -> ()

[<EntryPoint>]
    let main argv =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .StartWithClassicDesktopLifetime(argv)