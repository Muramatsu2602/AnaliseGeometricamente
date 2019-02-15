using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace AnaliseGeometricamente
{
    public partial class Menu : Form
    {
        public string imgNameCorrigida;
        public string pastao;
        public Menu(Image imagem, string imgName)
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            picGabarito.Image = imagem;
            // MessageBox.Show(imgName);
            lblNomeImagem.Text = imgName;
            string[] split = imgName.Split('.');
            this.imgNameCorrigida = split[0];
            btnAbreVideos.Enabled = false;

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                string[] files = Directory.GetFiles(fbd.SelectedPath);

                foreach (string file in files)
                {


                    if (Path.GetFileName(file).Contains(".mp4"))
                    {
                        listBox1.Items.Add(Path.GetFileName(file));
                    }
                }
                pastao = fbd.SelectedPath;
            }

            #region Alternativa do file browser, mas so exibe um por vez
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.RestoreDirectory = true;
            //ofd.InitialDirectory = "C:\\";
            //ofd.FilterIndex = 1;
            //ofd.Filter = "mp4 files (*.mp4)|*.mp4|All files (*.*)|*.*";
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    listBox1.Items.Clear();
            //    string[] files = (ofd.FileNames);
            //    foreach (string file in files)
            //    {
            //        // Como incluir somente videos relacionados a tal desenho?????
            //        //if (Path.GetFileName(file).Contains(imgNameCorrigida) && Path.GetFileName(file).Contains(".mp4"))
            //        //{
            //        //    listBox1.Items.Add(Path.GetFileName(file));
            //        //}
            //        listBox1.Items.Add(Path.GetFileName(file));

            //    }
            //    string fullPath = ofd.FileName;
            //    string fileName = ofd.SafeFileName;
            //    string path = fullPath.Replace(fileName, "");

            //    pastao = path;

            //}
            #endregion
        }



        private void BtnNovaAnalise_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Deseja Inciar uma nova análise? \n Isso resultará no fechamento da análise atual", "Nova Análise", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)

            {
                //frmEscolhePasta f = new frmEscolhePasta();
                //f.ShowDialog();
                this.Close();
            }
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Deseja Sair?", "Sair...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            int index = this.listBox1.IndexFromPoint(e.Location);
            string nome = this.listBox1.GetItemText(listBox1.Items[index]);
            if (index != ListBox.NoMatches)
            {

                playerVideo.URL = pastao + "\\" + nome;
                lblNomeVideo.Text = nome;
                //axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        #region Global controls
        bool pp;
        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (ControleSimultaneo())
            {
                if (pp)
                {
                    playerVideo.Ctlcontrols.play();
                    playerAudio.Ctlcontrols.play();
                    pp = false;
                    btnPlay.Image = Image.FromFile("C:\\AnaliseGeometricamente\\AnaliseGeometricamente\\imagens\\Pause.png");
                    btnPlay.BorderStyle = BorderStyle.Fixed3D;

                }
                else if (!pp)
                {
                    playerVideo.Ctlcontrols.pause();
                    playerAudio.Ctlcontrols.pause();
                    btnPlay.Image = Image.FromFile("C:\\AnaliseGeometricamente\\AnaliseGeometricamente\\imagens\\Play.png");
                    pp = true;
                    btnPlay.BorderStyle = BorderStyle.None;

                }

                //axWindowsMediaPlayer1.Ctlcontrols.play();
                //axWindowsMediaPlayer2.Ctlcontrols.play();
            }

        }

        private bool ControleSimultaneo()
        {
            try { if (playerAudio.currentMedia.sourceURL == null || playerVideo.currentMedia.sourceURL == null) { } }
            catch (Exception)
            {
                MessageBox.Show("Função Inválida!\n Arquivo de áudio (.mp3) ou vídeo (.mp4) não selecionado.");
                return false;
            }
            return true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            ControleSimultaneo();
            playerVideo.Ctlcontrols.pause();
            playerAudio.Ctlcontrols.pause();
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            ControleSimultaneo();
            playerVideo.Ctlcontrols.stop();
            playerAudio.Ctlcontrols.stop();
        }

        private void btnFastFoward_Click(object sender, EventArgs e)
        {
            ControleSimultaneo();
            playerVideo.Ctlcontrols.fastForward();
            playerAudio.Ctlcontrols.fastForward();
        }

        private void btnReverse_Click(object sender, EventArgs e)
        {
            ControleSimultaneo();
            playerVideo.Ctlcontrols.fastReverse();
            playerAudio.Ctlcontrols.fastReverse();
        }
        #endregion

        private void btnAbreAudio_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = "C:\\";
            ofd.FilterIndex = 1;
            ofd.Filter = "mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                playerAudio.URL = ofd.FileName;
                string[] split = ofd.FileName.Split('_');
                lblNomeAudio.Text = split[3];
                //playerAudio.Ctlcontrols.stop();
            }
            btnAbreVideos.Enabled = true;
        }

        private void picGabarito_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Deseja escolher uma imagem? \n Isso resultará no fechamento da análise atual", "Nova Análise", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)

            {
                //frmEscolhePasta f = new frmEscolhePasta();
                //f.ShowDialog();
                this.Close();
            }
        }

        private void picGabarito_MouseHover(object sender, EventArgs e)
        {
            picGabarito.BorderStyle = BorderStyle.Fixed3D;
            picGabarito.BackColor = Color.LightGray;
        }

        private void picGabarito_MouseLeave(object sender, EventArgs e)
        {
            picGabarito.BackColor = Color.Transparent;
            picGabarito.BorderStyle = BorderStyle.None;
        }

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.ShowDialog();
        }
    }
}
