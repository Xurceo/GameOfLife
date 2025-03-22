using System.Windows;
using System.Windows.Controls;

namespace GameOfLife;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Grid gridContainer;
    private Button startButton, clearButton;
    private GameGrid gameGrid;

    public MainWindow()
    {
        InitializeComponent();

        gameGrid = new(157, 103, 5);

        gridContainer = new Grid();
        this.Content = gridContainer;

        gridContainer.Children.Add(gameGrid.Canvas);

        startButton = new Button
        {
            Content = "Start",
            Width = 100,
            Height = 50,
            Margin = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
        };

        clearButton = new Button
        {
            Content = "Clear",
            Width = 100,
            Height = 50,
            Margin = new Thickness(0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
        };
        StackPanel buttonPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Left
        };
        Slider speedSlider = new Slider
        {
            Minimum = 50,
            Maximum = 1000,
            Value = 500,
            Width = 150,
            Margin = new Thickness(10)
        };
        speedSlider.ValueChanged += (s, e) =>
        {
            if (gameGrid.Timer.Interval != TimeSpan.FromMilliseconds((int)speedSlider.Value))
            {
                gameGrid.Timer.Interval = TimeSpan.FromMilliseconds((int)speedSlider.Value);
            }
        };

        buttonPanel.Children.Add(startButton);
        buttonPanel.Children.Add(clearButton);
        buttonPanel.Children.Add(speedSlider);

        gridContainer.Children.Add(buttonPanel);
        Grid.SetRow(buttonPanel, 0);
        Grid.SetRow(gameGrid.Canvas, 1);

        startButton.Click += (s, e) => gameGrid.Run((int)speedSlider.Value);
        clearButton.Click += (s, e) => gameGrid.ClearGrid();
    }
}