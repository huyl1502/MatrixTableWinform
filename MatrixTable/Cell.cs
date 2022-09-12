using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MatrixTable
{
    public partial class Cell : UserControl, INotifyPropertyChanged
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public Color BackGroundColor
        {
            get { return label.BackColor; }
            set { label.BackColor = value; }
        }

        public Color TextColor
        {
            get { return label.ForeColor; }
            set { label.ForeColor = value; }
        }

        public Color BorderColor
        {
            get { return BackColor; }
            set { BackColor = value; }
        }

        bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, null);
                }
            }
        }

        //public event EventHandler Click;

        public Cell()
        {
            InitializeComponent();

            label.TextAlign = ContentAlignment.MiddleCenter;
            label.AutoSize = false;
            //label.Dock = DockStyle.Fill;

            label.Width = Width - 1;
            label.Height = Height - 1;

            BackGroundColor = Color.White;
            Text = "lbl";
            BorderColor = Color.FromArgb(15, Color.Black);

            label.MouseMove += MouseMoveAction;
            label.MouseLeave += MouseLeaveAction;

            //label.MouseClick += (s, e) =>
            //{
            //    Click?.Invoke(this, null);
            //};

            PropertyChanged += (s, e) =>
            {
                if(_isSelected == true)
                {
                    BackGroundColor = Color.Red;
                }
                else
                {
                    BackGroundColor = Color.White;
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void MouseMoveAction(object s, EventArgs e)
        {
            BackGroundColor = Color.FromArgb(15, Color.Black);
        }

        public void MouseLeaveAction(object s, EventArgs e)
        {
            BackGroundColor = Color.White;
        }
    }
}
