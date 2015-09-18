using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs;
using XLabs.Forms.Behaviors;
using XLabs.Forms.Mvvm;

namespace XlentLock
{
    class LockPage : BaseView
    {
        private Button[] _buttons;

        public Button[] Buttons
        {

            get
            {
                return _buttons ?? (_buttons = new Button[]
                {
                    new Button()
                    {
                        Text = "1"
                    },
                    new Button()
                    {
                        Text = "2"
                    },
                    new Button()
                    {
                        Text = "3"
                    },
                    new Button()
                    {
                        Text = "4"
                    },
                    new Button()
                    {
                        Text = "5"
                    },
                    new Button()
                    {
                        Text = "6"
                    },
                    new Button()
                    {
                        Text = "7"
                    },
                    new Button()
                    {
                        Text = "8"
                    },
                    new Button()
                    {
                        Text = "9"
                    }
                });
            }
            set {  _buttons = value; }
        }


        public Grid ButtonGridLayout;

        public StackLayout MainStackLayout;

		


        public LockPage()
        {
            ButtonGridLayout = new Grid()
            {
                RowDefinitions = new RowDefinitionCollection() { }

            }
            
            
         


        }

    }
}
