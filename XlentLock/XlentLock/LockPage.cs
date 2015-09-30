using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace XlentLock
{
    internal class LockPage : BaseView
    {
        private Button[] _buttons;

        public Button[] Buttons
        {
            get
            {
                return _buttons ?? (_buttons = new[]
                {
                    new Button
                    {
                        Text = "1",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        WidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 1
                    },
                    new Button
                    {
                        Text = "2"
                        ,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        WidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 2
                    },
                    new Button
                    {
                        Text = "3"
                        ,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        WidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 3
                    },
                    new Button
                    {
                        Text = "4"
                        ,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        WidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 4
                    },
                    new Button
                    {
                        Text = "5",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        WidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 5
                    },
                    new Button
                    {
                        Text = "6",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        WidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 6
                    },
                    new Button
                    {
                        Text = "7",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        WidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 7
                    },
                    new Button
                    {
                        Text = "8",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        MinimumWidthRequest = ButtonWidth,
                        MinimumHeightRequest = 30,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 8
                    },
                    new Button
                    {
                        Text = "9",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        MinimumWidthRequest = ButtonWidth,
                        MinimumHeightRequest = 30,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 9
                    },
                    new Button
                    {
                        Text = "",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        MinimumWidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = "*"
                    },
                    new Button
                    {
                        Text = "0",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand
                        ,
                        MinimumWidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = 0
                    },
                    new Button
                    {
                        Text = "OK",
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        MinimumWidthRequest = ButtonWidth,
                        BackgroundColor = Color.Gray,
                        Command = ButtonClickedCommand,
                        CommandParameter = "OK"
                    }
                });
            }
            set { _buttons = value; }
        }

        public int ButtonWidth => CalculateWidth();

        private int CalculateWidth()
        {
            var device = Resolver.Resolve<IDevice>();
            var displayWidth = device.Display.Width;
            return (displayWidth/3) - 10;
        }

        public StackLayout MainStackLayout { get; set; }

        public Label CodeLabel
        {
            get
            {
                return new Label
                {
                    Text = "1",
                    FontSize = 20,
                    TextColor = Color.Black
                };
            }
            set { }
        }

        public LockPage()
        {
            BindingContext = new LockViewModel();

            CodeLabel.SetBinding(Label.TextProperty, "Code.Text");


            NumberGrid = new Grid();

            NumberGrid.Children.Add(Buttons[0], 0, 1);

            NumberGrid.Children.Add(Buttons[1], 1, 1);

            NumberGrid.Children.Add(Buttons[2], 2, 1);


            NumberGrid.Children.Add(Buttons[3], 0, 2);


            NumberGrid.Children.Add(Buttons[4], 1, 2);


            NumberGrid.Children.Add(Buttons[5], 2, 2);

            NumberGrid.Children.Add(Buttons[6], 0, 3);


            NumberGrid.Children.Add(Buttons[7], 1, 3);

            NumberGrid.Children.Add(Buttons[8], 2, 3);

            NumberGrid.Children.Add(Buttons[9], 0, 4);

            NumberGrid.Children.Add(Buttons[10], 1, 4);
            NumberGrid.Children.Add(Buttons[9], 2, 4);

            // Accomodate iPhone status bar.
            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            MainStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {NumberGrid}
            };

            // Build the page.
            Content = NumberGrid;
        }

        public Grid NumberGrid { get; set; }

        public Command ButtonClickedCommand
        {
            get
            {
                return new Command(parameter =>
                {
                    var number = (int) parameter;
                    var vm = (LockViewModel) BindingContext;
                    vm.AddCodeNumber(number);
                });
            }
            set { }
        }
    }
}
