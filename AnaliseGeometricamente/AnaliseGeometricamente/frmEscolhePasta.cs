using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AnaliseGeometricamente
{
    public partial class frmEscolhePasta : Form
    {
        String[] dados = new string[100];
        public frmEscolhePasta()
        {
            InitializeComponent();

        }

        private void FrmEscolhePasta_Load(object sender, EventArgs e)
        {

            ExibeArquivosDaPastaSelecionada(TestaPendrive() + "//imagens");
            txtDiretorio.Text = TestaPendrive() + "\\imagens";
        }

        FolderBrowserDialog fbd1 = new FolderBrowserDialog();
        private void BtnSelecionarPasta_Click(object sender, EventArgs e)
        {
            try
            {
                //Define as propriedades do controle FolderBrowserDialog
                fbd1.Description = "Selecione uma pasta exibir as imagens";

                fbd1.SelectedPath = TestaPendrive();
                fbd1.ShowNewFolderButton = true;

                //Exibe a caixa de diálogo
                if (fbd1.ShowDialog() == DialogResult.OK)
                {
                    //Exibe a pasta selecionada
                    txtDiretorio.Text = fbd1.SelectedPath;
                    panel3.Controls.Clear();

                }
                ExibeArquivosDaPastaSelecionada(fbd1.SelectedPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ExibeArquivosDaPastaSelecionada(string pasta)
        {
            String pastaOrigem = pasta;
            var filtros = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
            var arquivos = GetArquivosDaPasta(pastaOrigem, filtros, false);
            int l = 0;
            int c = 0;
            for (int i = 0; i < arquivos.Length; i++)
            {
                FileInfo arquivo = new FileInfo(arquivos[i]);
                // quero enviar o nome da imagem no array dados, slot 3
                string caminhoImagem = pasta + "\\" + arquivo.Name;
                PictureBox picture = new PictureBox
                {
                    Name = "pictureBox" + i,
                    BackColor = Color.White,
                    Size = new Size(200, 200),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(c, l),
                    Image = Image.FromFile(caminhoImagem),
                    Tag = arquivo.Name,
                    Cursor = System.Windows.Forms.Cursors.Hand
                };
                panel3.Controls.Add(picture);
                picture.Click += new EventHandler(AbreForm);

                if (c > (panel3.Size.Width - 400))
                {
                    l = l + 210;
                    c = 0;
                }
                else
                {
                    c += 210;
                }
            }
        }
        public void AbreForm(object sender, EventArgs e)
        {
            var box = (PictureBox)sender;
            dados[10] = box.Tag.ToString();
            // FrmGravaDescricao f2 = new FrmGravaDescricao(box.Image, dados); // aqui o
            //f2.ShowDialog();
            Menu f2 = new Menu(box.Image, box.Tag.ToString()); // aqui o
            f2.ShowDialog();

        }

        public  string[] GetArquivosDaPasta(String pastaRaiz, String[] filtros, bool isRecursiva)
        {
            List<String> arquivosEncontrados = new List<String>();
            SearchOption opcaoDeBusca = isRecursiva ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            try
            {
                foreach (var filtro in filtros)
                {
                    arquivosEncontrados.AddRange(Directory.GetFiles(pastaRaiz, String.Format("*.{0}", filtro), opcaoDeBusca));
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                MessageBox.Show("A pasta Imagens não fora encontrada no local padrão! Mais detalhes \n" + ex.Message);
                btnSelecionarPasta.PerformClick();
            }
            return arquivosEncontrados.ToArray();
        }


        public string TestaPendrive()
        {
            return Environment.CurrentDirectory;
        }


        private void FrmEscolhePasta_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Deseja mesmo sair?", "GEOMETRICAMENTE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
