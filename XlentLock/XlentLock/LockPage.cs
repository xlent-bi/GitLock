using System;
using System.Diagnostics;
using System.Windows.Input;
using AdvancedTimer.Forms.Plugin.Abstractions;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace XlentLock
{
    internal class LockPage : BaseView
    {
        private readonly IAdvancedTimer _advancedTimer;

        private Button[] _buttons;
        private Label _codeLabel;
        private Label _decLabel;

        private string _latestGuess;
        private readonly LockService _lockService;
        private Label _responsLabel;
        private Label _secLabel;
        private Label _timerLabel;

        public bool hasNotStarted = true;

        public bool HasWon;
        private readonly int milliSec;
        private int rest;
        private int sec;
        private int _fontSize;
        private Color _xlentOrange;
        private Color _xlentGray;

        public LockPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new LockViewModel();
            sec = 30;
            rest = 00;
            milliSec = 30000;

            _xlentGray = Color.FromRgb(100, 101, 105);
            _xlentOrange = Color.FromRgb(255, 170, 25);
            _advancedTimer = Resolver.Resolve<IAdvancedTimer>();
            _advancedTimer.initTimer(1000, (sender, args) =>
            {
                sec--;
                Device.BeginInvokeOnMainThread(() => { SecLabel.Text = sec.ToString(); });
                if (sec == 5)
                    Device.BeginInvokeOnMainThread(() => { SecLabel.TextColor = Color.Red; });


                Debug.WriteLine(milliSec);
                if (sec == 0)
                {
                    _advancedTimer.stopTimer();
                    GameCompleted();
                }
            }, true);


            NumberGrid = new Grid
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                RowSpacing = 10,
                ColumnSpacing = 10
            };


            _lockService = new LockService();

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
            NumberGrid.Children.Add(Buttons[11], 2, 4);

            // Accomodate iPhone status bar.
            Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            ClockStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    SecLabel
                }
            };

            if (Device.Idiom == TargetIdiom.Phone)
                MainStackLayout = new StackLayout
                {
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children =
                    {
                        ClockStackLayout,
                        ResponsLabel,
                        LabelAndEraseLayout,
                        NumberGrid
                    },
                    Padding = new Thickness(0, 0, 0, 10)
                };
            if (Device.Idiom == TargetIdiom.Tablet)
            {
                var rightStack = new StackLayout
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Children =
                    {
                        ClockStackLayout,
                        ResponsLabel,
                        LabelAndEraseLayout,
                        NumberGrid
                    },
                    Padding = new Thickness(0, 0, 0, 10)
                };

                var width = Resolver.Resolve<IDevice>().Display.Width;


                var leftImage = new Image
                {
                    Source = "iconlogo1024x500.png",
                    Aspect = Aspect.AspectFill,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    WidthRequest = width/2
                };

                var leftContentView = new ContentView
                {
                    Content = leftImage,
                    MinimumWidthRequest = Width/2,
                    BackgroundColor = Color.White
                };
                var leftStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Children =
                    {
                        leftContentView,
                        new Button
                        {
                            Text = "Reset",
                            BorderColor = Color.Green,
                            BorderRadius = 5,
                            Command = new Command(() => { Reset(); }),
                            VerticalOptions = LayoutOptions.End
                        }
                    },
                    VerticalOptions = LayoutOptions.FillAndExpand
                };


                MainStackLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Children =
                    {
                        // leftStackLayout,
                        rightStack
                    }
                };
            }


            Content = MainStackLayout;
            //_advancedTimer.startTimer();
        }

        public Label TimerLabel { get; set; }

        public int FontSize
        {
            get
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    return 20;
                }

                return 50;
            }
            set { _fontSize = value; }
        }

        public Button[] Buttons
        {
        get
            {
                return _buttons ?? (_buttons = new[]
                       {
                           new Button
                           {
                               Text = "1",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               HeightRequest = ButtonWidth,
                               WidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 1
                           },
                           new Button
                           {
                               Text = "2",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               WidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 2
                           },
                           new Button
                           {
                               Text = "3",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               WidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 3
                           },
                           new Button
                           {
                               Text = "4",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand
                               ,
                               WidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 4
                           },
                           new Button
                           {
                               Text = "5",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               WidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 5
                           },
                           new Button
                           {
                               Text = "6",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               WidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 6
                           },
                           new Button
                           {
                               Text = "7",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               WidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 7
                           },
                           new Button
                           {
                               Text = "8",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               MinimumWidthRequest = ButtonWidth,
                               MinimumHeightRequest = 30,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 8
                           },
                           new Button
                           {
                               Text = "9",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               MinimumWidthRequest = ButtonWidth,
                               MinimumHeightRequest = 30,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 9
                           },
                           new Button
                           {
                               Text = "CE",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               MinimumWidthRequest = ButtonWidth,
                               BackgroundColor = _xlentOrange,
                               Command = EraseCommand,
                               CommandParameter = 12
                           },
                           new Button
                           {
                               Text = "0",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               MinimumWidthRequest = ButtonWidth,
                               BackgroundColor = _xlentGray,
                               Command = ButtonClickedCommand,
                               CommandParameter = 0
                           },
                           new Button
                           {
                               Text = "OK",
                               FontSize = FontSize,
                               TextColor = Color.White,
                               VerticalOptions = LayoutOptions.FillAndExpand,
                               HorizontalOptions = LayoutOptions.FillAndExpand,
                               MinimumWidthRequest = ButtonWidth,
                               BackgroundColor = _xlentOrange,
                               Command = OkClickedCommand
                           }
                       });
            }
            set { _buttons = value;
    }
        }

        public ICommand OkClickedCommand
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if ((CodeLabel.Text != null) && (CodeLabel.Text != "") && (CodeLabel.Text != " "))
                        {
                            var respons = _lockService.Guess(int.Parse(CodeLabel.Text)).ToString();
                            LatestGuess = CodeLabel.Text;
                            if (respons == LockService.LockResponse.GuessHigher.ToString())
                            {
                                ResponsLabel.Text = "GUESS HIGHER";
                                ResponsLabel.FontSize = FontSize;
                                ResponsLabel.FontAttributes = FontAttributes.Bold;
                            }
                            else if (respons == LockService.LockResponse.GuessLower.ToString())
                            {
                                ResponsLabel.Text = "GUESS LOWER";
                                ResponsLabel.FontSize = FontSize;
                                ResponsLabel.FontAttributes = FontAttributes.Bold;
                            }
                            else if (respons == LockService.LockResponse.Unlocked.ToString())
                            {
                                HasWon = true;
                                GameCompleted();
                            }

                            Debug.WriteLine(LatestGuess);

                            CodeLabel.Text = "";
                        }
                        else
                        {
                            ResponsLabel.Text = "Invalid Input";
                        }
                    }
                    catch (Exception ee)
                    {
                        ResponsLabel.Text = "Not a valid input try again";
                        CodeLabel.Text = "";
                        Debug.WriteLine(ee.StackTrace);
                    }
                });
            }
            set { }
        }

        public string LatestGuess
        {
            get { return _latestGuess ?? (_latestGuess = "-1"); }
            set { }
        }

        public Label SecLabel
        {
            get
            {
                return _secLabel ?? (_secLabel = new Label
                       {
                           Text = "30",
                           FontSize = 100,
                           FontAttributes = FontAttributes.Bold,
                           HorizontalOptions = LayoutOptions.CenterAndExpand,
                           VerticalOptions = LayoutOptions.CenterAndExpand
                       });
            }
            set { }
        }

        public StackLayout ClockStackLayout { get; set; }


        public int ButtonWidth => CalculateWidth();

        public StackLayout MainStackLayout { get; set; }

        public Label CodeLabel
        {
            get
            {
                return _codeLabel ?? (_codeLabel = new Label
                       {
                           Text = "",
                           TextColor = Color.Black,
                           BackgroundColor = Color.White,
                           HeightRequest = 100,
                           FontSize = FontSize,
                           HorizontalOptions = LayoutOptions.CenterAndExpand,
                           VerticalOptions = LayoutOptions.CenterAndExpand
                       });
            }
            set { }
        }

        public Label ResponsLabel
        {
            get
            {
                return _responsLabel ?? (_responsLabel = new Label
                       {
                           Text = "GUESS THE CORRECT NUMBER BETWEEN 0-1000",
                           HorizontalOptions = LayoutOptions.Center,
                           FontSize = 15
                       });
            }
            set { }
        }

        public Image SafeImage
        {
            get
            {
                return new Image
                {
                    Source = "safe-icon1.png",
                    Aspect = Aspect.AspectFill,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
            }
            set { }
        }

        public Grid NumberGrid { get; set; }

        public Button EraseButton
        {
            get
            {
                return new Button
                {
                    Text = "<",
                    Command = EraseCommand,
                    HorizontalOptions = LayoutOptions.End,
                    HeightRequest = 40,
                    WidthRequest = 40,
                    BackgroundColor = Color.White,
                    TextColor = Color.Black
                };
            }
            set { }
        }

        public ICommand EraseCommand
        {
            get { return new Command(() => { CodeLabel.Text = ""; }); }
            set { }
        }

        public StackLayout LabelAndEraseLayout
        {
            get
            {
                return new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new ContentView
                        {
                            BackgroundColor = Color.White,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Content = CodeLabel,
                            MinimumHeightRequest = 20
                        },
                        EraseButton
                    }
                };
            }
            set { }
        }

        public Command ButtonClickedCommand
        {
            get
            {
                return new Command(parameter =>
                {
                    var number = (int) parameter;
                    var vm = (LockViewModel) BindingContext;
                    vm.AddCodeNumber(number);
                    var oldCode = CodeLabel.Text;
                    var newCode = oldCode + number.ToString();
                    CodeLabel.Text = newCode;

                    if (hasNotStarted)
                    {
                        _advancedTimer.startTimer();
                        hasNotStarted = false;
                    }
                });
            }
            set { }
        }

        private async void GameCompleted()
        {
            var time = 30 - sec;

            if (HasWon)
            {
                ResponsLabel.Text = "YOU GUESSED CORRECT";
                _advancedTimer.stopTimer();
                await DisplayAlert("You guessed the correct number in " + time + " seconds", "Please fill in the competition form on one of our computers", "OK");
                Reset();
            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var wantToPlayAgian = DisplayAlert("You Loose", "Try Again?", "Yes", "No");
                    Reset();
                });
            }
        }

        private void Reset()
        {
            sec = 30;
            Device.BeginInvokeOnMainThread(() => { Application.Current.MainPage = new LockPage(); });
        }

        private int CalculateWidth()
        {
            var device = Resolver.Resolve<IDevice>();
            var displayWidth = device.Display.Width;
            return displayWidth/3 - 10;
        }

        public static void timerElapsed(object o, EventArgs e)
        {
        }
    }
}