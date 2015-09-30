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
                });
            }
            set { SetProperty(ref _code, value); }
        }


        public void AddCodeNumber(int number)
        {
                var currentCode = Code.Text + number;

            Code = new Label()
            {
                Text = currentCode
            };

                Debug.WriteLine(Code.Text);
            }
        }
}
