using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator.ViewModels
{
    public class ErrorOperationViewModel : Screen
    {
        public bool IsOk{ get; set; }
        //public bool CanAcceptButton
        //{
        //    get { return true; /* add logic here */ }
        //}

        public void AcceptButton()
        {
            TryCloseAsync(true);
            IsOk = true;
        }

        //public bool CanCancelButton
        //{
        //    get { return true; }
        //}

        public void CancelButton()
        {
            TryCloseAsync(false);
            IsOk = false;
        }
    }
}
