using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private string _display = "0";

        public string Display
        {
            get { return _display; }
            set 
            {
                _display = value;
                NotifyOfPropertyChange(() => Display);
            }
        }
        bool action = false;
        bool dot = false;
        double left = 0;
        double right = 0;
        char sign;
        // Следующий код по идее плохой, но я плохо понял как можно биндить разные кнопки одной функцией
        public void One()
        {
            if(Display == "0")
            {
                ClearAll();
            }            
            Display += "1";
        }
        public void Two()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "2";
        }
        public void Three()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "3";
        }
        public void Four()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "4";
        }
        public void Five()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "5";
        }
        public void Six()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "6";
        }
        public void Seven()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "7";
        }
        public void Eight()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "8";
        }
        public void Nine()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "9";
        }
        public void Zero()
        {
            if (Display == "0")
            {
                ClearAll();
            }
            Display += "0";
        }

        public void Dot()
        {
            if (Display == "" || dot == true)
            {
                return;
            }
            Display += ",";
            dot = true;
        }

        public void Addition()
        {
            if (Display == "" || Display[^1] == '+' || Display[^1] == '-' || Display[^1] == '*'
                || Display[^1] == '/' || Display[^1] == '%' )
                return;
            else if (action == true)
                Equal();
            Display += "+";
            action = true;
            dot = false;
        }

        public void Subtraction()
        {
           
            if (Display == "")
            {
                Display += "-";
                return;
            }
            else if (Display[^1] == '+' || Display[^1] == '-' || Display[^1] == '*'
                || Display[^1] == '/' || Display[^1] == '%')
                return;
            else if (action == true)
                Equal();
            Display += "-";
            action = true;
            dot = false;
        }

        public void Multiplication()
        {
            if (Display == "" || Display[^1] == '+' || Display[^1] == '-' || Display[^1] == '*'
               || Display[^1] == '/' || Display[^1] == '%')
                return;
            else if (action == true)
                Equal();
            Display += "*";
            action = true;
            dot = false;
        }

        public void Division()
        {
            if (Display == "" || Display[^1] == '+' || Display[^1] == '-' || Display[^1] == '*'
               || Display[^1] == '/' || Display[^1] == '%')
                return;
            else if (action == true)
                Equal();
            Display += "/";
            action = true;
            dot = false;
        }

        public void Percent()
        {
            if (Display == "" || Display[^1] == '+' || Display[^1] == '-' || Display[^1] == '*'
               || Display[^1] == '/' || Display[^1] == '%')
                return;
            else if (action == true)
                Equal();
            Display += "%";
            action = true;
            dot = false;
        }

        public void Square()
        {
            if (action == true)
                return;
            else if (Display == "")
                return;
            else
            Display = Math.Round(double.Parse(Display) * double.Parse(Display),4).ToString();
            dot = false;
        }

        public void Sqrt()
        {
            if (action == true)
                return;
            else if (Display == "")
                return;
            else
            {
                if (double.Parse(Display) < 0)
                { 
                    MessageBox.Show("For this operation, the number must be> 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ClearAll();
                    return;
                }
                else
                Display = Math.Round(Math.Sqrt(double.Parse(Display)),4).ToString();
            }
            dot = false;
        }

        //Парсер
        string l;
        string r;
        bool sig = false;
        public void Parser()
        {
            l = "";
            r = "";
            l += Display[0];
            for (int i = 1; i < Display.Length; i++)
            {
                if (Display[i] == '+' || Display[i] == '-' || Display[i] == '*' || Display[i] == '/' || Display[i] == '%')
                {
                    sig = true;
                    sign = Display[i];
                }
                else if (sig == false)
                    l += Display[i];
                else if (sig)
                    r += Display[i];
            }
            left = double.Parse(l);
            right = double.Parse(r);
            sig = false;
        }

        public void Equal()
        {
            Parser();
            switch (sign)
            {
                case '+' : Display = (left + right).ToString();
                    break;
                case '-':
                    Display = Math.Round(left - right, 4).ToString();
                    break;
                case '*':
                    Display = Math.Round(left * right, 4).ToString();
                    break;
                case '/':
                    if (right == 0)
                    {
                        MessageBox.Show("Нельзя делить на 0", "Error",MessageBoxButton.OK, MessageBoxImage.Error);
                        ClearAll();
                        break;
                    }
                    Display = Math.Round(left / right, 4).ToString();
                    break;
                case '%':
                    Display = Math.Round(left * right/100, 4).ToString();
                    break;

            }
            action = false;
            dot = false;
        }

        public void ClearAll() 
        {
            Display = "";
            action = false;
        }

        public void DeleteLast()
        {
            if (Display[Display.Length - 1] == '+' || Display[Display.Length - 1] == '-' || Display[Display.Length - 1] == '*'
                || Display[Display.Length - 1] == '/' || Display[Display.Length - 1] == '%')
                action = false;
            Display = Display.Remove(Display.Length - 1);
        }

        
    private readonly IWindowManager window = new WindowManager();
        public void Info()
        {
            InfoViewModel ivm = new InfoViewModel();
            ActivateItemAsync(ivm);
            window.ShowDialogAsync(ivm);
        }

        public void About()
        {
            AboutViewModel avm = new AboutViewModel();
            ActivateItemAsync(avm);
            window.ShowDialogAsync(avm);
        }



    }
}
