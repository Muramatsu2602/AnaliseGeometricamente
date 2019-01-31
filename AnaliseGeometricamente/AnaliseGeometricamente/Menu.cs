using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnaliseGeometricamente
{
    public partial class Menu : Form
    {
        public Menu(Image imagem, string imgName)
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            picGabarito.Image = imagem;
            // MessageBox.Show(imgName);
            lblNomeImagem.Text = imgName;

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
