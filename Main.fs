module Main

open Avalonia.FuncUI.DSL
open Avalonia.Controls
open Avalonia.Controls.Primitives
open Avalonia.Layout
open Avalonia.FuncUI.Types
open Avalonia.Media
open Markdig
open Elmish

type Model =
    { Text: string }

type Msg =
    | TextChanged of string

let init () : Model * Cmd<Msg> =
    { Text = "" }, Cmd.none

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | TextChanged newText -> {model with Text = newText}, Cmd.none

let view (model: Model) (dispatch: Msg -> unit) =
    let html = Markdown.ToHtml(model.Text)
    Grid.create [
        Grid.columnDefinitions"*,*"
        Grid.children [
            // Editing pane
            TextBox.create [
                Grid.column 0
                TextBox.text model.Text
                TextBox.acceptsReturn true
                TextBox.verticalScrollBarVisibility ScrollBarVisibility.Auto
                TextBox.horizontalScrollBarVisibility ScrollBarVisibility.Auto
                TextBox.onTextChanged (TextChanged >> dispatch)
                TextBox.fontFamily "sans serif"
                TextBox.fontSize 14.0
                TextBox.padding 10.0
                TextBox.horizontalAlignment HorizontalAlignment.Stretch
                TextBox.verticalAlignment VerticalAlignment.Stretch
            ]
            // Preview pane
            ScrollViewer.create [
                Grid.column 1
                ScrollViewer.content (
                    TextBlock.create [
                        TextBlock.text html
                        TextBlock.textWrapping TextWrapping.Wrap
                        TextBlock.padding 10.0
                        TextBlock.fontFamily "sans serif"
                    ]
                )
            ]
        ]
    ]