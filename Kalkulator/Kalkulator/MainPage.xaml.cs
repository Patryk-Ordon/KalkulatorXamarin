﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kalkulator
{
    public partial class MainPage : ContentPage
    {
        string test = "123.95";
        CultureInfo culture = new CultureInfo("en-US");
        char operation = '0';
        decimal Number1;
        decimal Number2;
        string history;
        bool old = false;
        decimal limit = Decimal.MaxValue;

        public MainPage()
        {
            InitializeComponent();
        }

        private void FontCheck()
        {
            Console.WriteLine(lblResult.Text.Length);
            if(lblResult.Text.Length > 12)
            {
                double FontSize = 45-((lblResult.Text.Length - 12)*1.75);
                lblResult.FontSize = (int)FontSize;
            }
            else
            {
                lblResult.FontSize = 45;
            }
            Console.WriteLine(lblResult.FontSize);
        }

        private string DisplayNumber(decimal number)
        {
            var num = number.ToString();
            if (number >= 0) return num;
            else return "(" + num + ")";
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if(lblResult.Text.Contains('.'))
            {
                lblResult.Text += button.Text;
            }
            else if (lblResult.Text == "0" || Convert.ToDecimal(lblResult.Text, culture) == Number1)
            {
                lblResult.Text = button.Text;
            }
            else
            {
                lblResult.Text += button.Text;
            }
            old = false;
            FontCheck();
        }

        private void ButtonClear_Clicked(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblResult.Text = "0";
            Number1 = default(decimal);
            Number2 = default(decimal);
            history = "";
            lblHistory.Text = "";
            FontCheck();
        }

        private void ButtonBackspace_Clicked(object sender, EventArgs e)
        {
            var text = lblResult.Text;
            if (text.Length == 1)
            {
                lblResult.Text = "0";
            }
            else
            {
                lblResult.Text = text.Substring(0, text.Length - 1);
            }
            FontCheck();
        }

        private void ButtonFloat_Clicked(object sender, EventArgs e)
        {
            if (!lblResult.Text.Contains("."))
            {
                lblResult.Text += ".";
            }
        }

        private void ButtonSqrt_Clicked(object sender, EventArgs e)
        {
            double number = Convert.ToDouble(lblResult.Text);
            number = Math.Sqrt(number);
            lblResult.Text = number.ToString();
            FontCheck();
        }

        private void ButtonMinus_Clicked(object sender, EventArgs e)
        {
            decimal number = 0 - Convert.ToDecimal(lblResult.Text, culture);
            lblResult.Text = number.ToString();
            FontCheck();
        }

        private void ButtonOperator_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            operation = button.Text[0];
            Number1 = Convert.ToDecimal(lblResult.Text, culture);
            history = DisplayNumber(Number1) + " " + operation;
            lblHistory.Text = history;
        }

        private void ButtonResult_Clicked(object sender, EventArgs e)
        {
            decimal result = 0;
            if (Number1.ToString() == "")
            {
                return;
            }
            else
            {
                if (!old)
                    Number2 = Convert.ToDecimal(lblResult.Text, culture);
            }
            try
            {
                switch (operation)
                {
                    case '+':
                        result = Number1 + Number2;
                        break;
                    case '-':
                        result = Number1 - Number2;
                        break;
                    case 'x':
                        result = Number1 * Number2;
                        break;
                    case '÷':
                        result = Number1 / Number2;
                        break;
                    default:
                        return;
                }
                if (result > limit)
                    throw new ArgumentOutOfRangeException("Parameter index is out of range.");
            }
            catch (Exception ex)
            {
                //if(ex.Message == "Value was either too large or too small for a Decimal.")
                lblError.Text = "E";
                return;
            }
            lblResult.Text = result.ToString();
            history = DisplayNumber(Number1) + " " + operation + " " + DisplayNumber(Number2) + " =";
            lblHistory.Text = history;
            Number1 = result;
            old = true;
            FontCheck();
        }
    }
}