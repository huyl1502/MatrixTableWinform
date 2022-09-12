using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MatrixTable
{
    public partial class MatrixTable : UserControl, INotifyPropertyChanged
    {
        public int NoRows { get; set; }
        public int NoCols { get; set; }

        

        public Cell[,] Cells = new Cell[21, 21];

        Cell _selectedItem;
        public Cell SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = (Cell)value;
                _selectedItem.IsSelected = true;
                Cells[_selectedItem.Row, _selectedItem.Col].IsSelected = true;
            }
        }

        List<Sample> _dataSource = new List<Sample>();
        public List<Sample> DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;

                foreach (var v in _dataSource)
                {
                    int i = v.row;
                    int j = v.col;

                    Cells[i, j].Tag = v;
                }

                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, null);
                }
            }
        }

        public Color Color
        {
            set
            {
                BackColor = value;
            }
        }

        public Color AlternatingColor
        {
            set
            {
                for (int i = 1; i <= NoRows - 1; i++)
                {
                    for (int j = 0; j <= NoCols - 1; j++)
                    {
                        if (i % 2 != 0)
                        {
                            Cells[i, j].BackGroundColor = value;
                        }
                    }
                }
            }
        }

        public Color TextCellColor
        {
            set
            {
                for (int i = 1; i <= NoRows - 1; i++)
                {
                    for (int j = 1; j <= NoCols - 1; j++)
                    {
                        Cells[i, j].TextColor = value;
                    }
                }
            }
        }

        public Color TextHeaderColor
        {
            set
            {
                Cells[0, 0].TextColor = value;

                for (int i = 1; i < NoCols; i++)
                {
                    Cells[0, i].TextColor = value;
                }

                for (int i = 1; i < NoCols; i++)
                {
                    Cells[i, 0].TextColor = value;
                }
            }
        }

        public event EventHandler CellClick;

        public event PropertyChangedEventHandler PropertyChanged;

        private void CreateColumnHeader()
        {
            for (int i = 1; i < NoCols; i++)
            {
                var cell = new Cell
                {
                    Col = i,
                    Row = 0
                };
                cell.Location = new Point(cell.Width * i, 0);
                cell.Text = "Cột " + (char)(i + 64);

                cell.BackGroundColor = Color.FromArgb(50, Color.Black);
                cell.label.MouseMove -= cell.MouseMoveAction;
                cell.label.MouseLeave -= cell.MouseLeaveAction;

                Cells[0, i] = cell;

                Controls.Add(cell);
            }
        }

        private void CreateRowHeader()
        {
            for (int i = 1; i < NoRows; i++)
            {
                var cell = new Cell
                {
                    Row = i,
                    Col = 0
                };
                cell.Location = new Point(0, cell.Height * i);
                cell.Text = "Hàng " + i;

                cell.BackGroundColor = Color.FromArgb(50, Color.Black);
                cell.label.MouseMove -= cell.MouseMoveAction;
                cell.label.MouseLeave -= cell.MouseLeaveAction;

                Cells[i, 0] = cell;

                Controls.Add(cell);
            }
        }

        private void CreateCornerCell()
        {
            var cell = new Cell
            {
                Location = new Point(0, 0),
                Text = "Hàng Cột",
                Row = 0, 
                Col = 0
            };
            cell.BackGroundColor = Color.FromArgb(75, Color.Black);
            cell.label.MouseMove -= cell.MouseMoveAction;
            cell.label.MouseLeave -= cell.MouseLeaveAction;

            Cells[0, 0] = cell;
            Controls.Add(cell);
        }

        private void SetWidthAndHeight()
        {
            var cellWidth = Cells[1, 1].Width;
            var cellHeight = Cells[1, 1].Height;

            Width = cellWidth * NoCols;
            Height = cellHeight * NoRows;
        }

        public T GetItemByRowCol<T>(int row, int col)
        {
            return (T)Cells[row, col].Tag;
        }

        public MatrixTable(int rows = 10, int cols = 5)
        {
            InitializeComponent();

            NoRows = rows + 1;
            NoCols = cols + 1;

            CreateCornerCell();
            CreateColumnHeader();
            CreateRowHeader();

            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    var cell = new Cell
                    {
                        Row = i,
                        Col = j
                    };
                    cell.Location = new Point(cell.Width * j, cell.Height * i);

                    cell.label.Click += (s, e) =>
                    {
                        CellClick?.Invoke(cell, null);
                    };

                    cell.Cursor = System.Windows.Forms.Cursors.Hand;

                    Cells[i, j] = cell;

                    Controls.Add(cell);
                }
            }

            CellClick += (s, e) =>
            {
                SelectedItem = (Cell)s;
            };

            PropertyChanged += (s, e) => {
                for (int i = 1; i <= rows; i++)
                {
                    for (int j = 1; j <= cols; j++)
                    {
                        if (Cells[i, j].Tag != null)
                        {
                            Cells[i, j].Tag = null;
                        }
                    }
                }

                foreach (var v in DataSource)
                {
                    int i = v.row;
                    int j = v.col;

                    Cells[i, j].Tag = v;
                }
            };
        }
    }
}
