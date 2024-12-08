using System.IO;
using System.Drawing;

namespace Winieta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLoadImg_Click(object sender, EventArgs e)
        {
            // Wy�wietl okno dialogowe do wyboru pliku
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Pliki graficzne|*.bmp;*.jpg;*.jpeg;*.png|Wszystkie pliki|*.*";

                Thread thread = new Thread(() =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Za�aduj obraz z wybranej �cie�ki
                            Image loadedImage = Image.FromFile(openFileDialog.FileName);

                            // Ustaw obraz w PictureBox
                            pictureBox1.Image = new Bitmap(loadedImage);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Dopasowanie obrazu do kontrolki
                        }
                        catch (Exception ex)
                        {
                            // Obs�uga wyj�tk�w w przypadku b��du podczas �adowania
                            MessageBox.Show($"B��d podczas �adowania obrazu: {ex.Message}", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

            }
          
        }
    }
}
