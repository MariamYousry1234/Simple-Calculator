using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      
        enum enOperationtype
        {
            Add,
            Subtract,
            Division,
            Multiple,
            None
        }

       struct stCalculator
        {
            public string Number1;
            public string Number2;
            public enOperationtype Operationtype;
        }

        stCalculator Calculator = new stCalculator();

        void Reset()
        {
            txtResults.Tag = "0";
            Calculator.Number1 = null;
            Calculator.Number2 = null;
            Calculator.Operationtype = enOperationtype.None;
        }

        void Clear()
        {
            txtResults.Text = "0";
            Reset();
        }

        void RemovefromText()
        {
            if (!string.IsNullOrEmpty(txtResults.Text))
            {
                txtResults.Text = txtResults.Text.Remove(txtResults.Text.Length - 1);
                if (string.IsNullOrEmpty(txtResults.Text))
                    Clear();
            }
        }
        void Delete()
        {
            if (Calculator.Number2 != null)
            {
                Calculator.Number2= Calculator.Number2.Remove(Calculator.Number2.Length - 1);

                if (string.IsNullOrEmpty(Calculator.Number2))
                    Calculator.Number2 = null;
            }

            if (Calculator.Operationtype != enOperationtype.None && Calculator.Number2 == null)
            {
                 Calculator.Operationtype = enOperationtype.None;
            }

            if (Calculator.Number1 != null && Calculator.Operationtype == enOperationtype.None)
            {
                Calculator.Number1 = Calculator.Number1.Remove(Calculator.Number1.Length - 1);

                if (string.IsNullOrEmpty(Calculator.Number1))
                    Calculator.Number1 = null;
            }

            RemovefromText();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
        }

        void SetTwoNumbers(Button button)
        {
            if (Calculator.Operationtype == enOperationtype.None) 
                Calculator.Number1 += button.Tag;
                
            else
                Calculator.Number2 += button.Tag;

            if (txtResults.Tag.ToString() == "0")
            {
                txtResults.Text = "";
                txtResults.Tag = " ";
            }

            txtResults.Text += button.Tag.ToString();
        }

        void SetOperationType(Button button)
        {
            if (Calculator.Number1 == null ||
                (Calculator.Number1 != null && Calculator.Number2!= null))
                return;

            switch (button.Tag.ToString())
            {
                case "+":
                    Calculator.Operationtype = enOperationtype.Add;
                    break;

                case "-":
                    Calculator.Operationtype = enOperationtype.Subtract;
                    break;

                case "x":
                    Calculator.Operationtype = enOperationtype.Multiple;
                    break;

                case "/":
                    Calculator.Operationtype = enOperationtype.Division;
                    break;
            }

            txtResults.Text += button.Tag.ToString();
        }

        double GetCalculatorResult(double Number1 , double Number2,enOperationtype OperationType)
        {
           switch(OperationType)
            {
                case enOperationtype.Add:
                    return Number1 + Number2;

                case enOperationtype.Subtract:
                    return Number1 - Number2;

                case enOperationtype.Multiple:
                    return Number1*Number2;

                case enOperationtype.Division:
                    {
                        if (Number2 == 0)
                        {
                            MessageBox.Show("Cannot divide by zero!");
                            return 0;
                        }
                        return Number1 / Number2;

                    }

                default:
                    return 0;
            }
        }

        void ShowFinalResult()
        {
            try
            {
                Calculator.Number1 = GetCalculatorResult(Convert.ToDouble(Calculator.Number1),
                        Convert.ToDouble(Calculator.Number2), Calculator.Operationtype).ToString();

                txtResults.Text = Calculator.Number1.ToString();

            }

            catch (Exception E)
            {
                MessageBox.Show("Enter a Valid Numbers");
               
            }

        }

        private void btn_Click(object sender, EventArgs e)
        {
            SetTwoNumbers((Button)sender);
        }

        private void btnOperationType_Click(object sender, EventArgs e)
        {
            SetOperationType((Button)sender);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (Calculator.Number1 == null ||
                Calculator.Number2 == null ||
                Calculator.Operationtype == enOperationtype.None) return;

            ShowFinalResult();

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}
