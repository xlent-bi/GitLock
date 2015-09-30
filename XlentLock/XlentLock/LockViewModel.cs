using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;

namespace XlentLock
{
    class LockViewModel : ViewModel
    {
        private Label _code;

        public Label Code
        {

            get
            {
                return _code ?? (_code = new Label()
                {
                    Text = ""
                });
            }
            set { SetProperty(ref _code, value); }
        }


        public void AddCodeNumber(int number)
        {
            if (Code.Text.Length < 5)
            {
                Code.Text = Code.Text + number;
               

                Debug.WriteLine(Code);
            }
        }
    }
}
