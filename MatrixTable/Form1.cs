using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var uc = new MatrixTable(10, 10);

            //uc.Color = Color.Red;
            //uc.TextHeaderColor = Color.White;

            uc.CellClick += (s, e) =>
            {
                //var v = (((Cell)s).Tag) as Sample;
                //((Cell)s).Text = v.text;
            };

            List<Sample> samples = new List<Sample>
            {
                new Sample { row = 2, col = 1, text = "2, 1" },
                new Sample { row = 3, col = 2, text = "3, 2" },
                new Sample { row = 4, col = 3, text = "4, 3" },
                new Sample { row = 7, col = 5, text = "7, 5" },
                new Sample { row = 9, col = 2, text = "9, 2" }
            };

            uc.DataSource = samples;

            var btn = new Button
            {
                Location = new Point(1500, 800),
            };
            btn.Click += (s, e) =>
            {
                uc.DataSource =
                new List<Sample>
                {
                    new Sample { row = 2, col = 2, text = "2, 2" },
                    new Sample { row = 3, col = 2, text = "3, 2" },
                    new Sample { row = 4, col = 3, text = "4, 3" },
                    new Sample { row = 7, col = 5, text = "7, 5" },
                    new Sample { row = 9, col = 2, text = "9, 2" }
                };
            };

            Controls.Add(uc);
            Controls.Add(btn);
        }
    }
}
