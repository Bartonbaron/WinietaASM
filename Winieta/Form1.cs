using System.IO;
using System.Drawing;
using ImageProcessing;


namespace Winieta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void BtnLoadImg_Click(object sender, EventArgs e)
        {
            // Wyœwietl okno dialogowe do wyboru pliku
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
                            // Za³aduj obraz z wybranej œcie¿ki
                            Image loadedImage = Image.FromFile(openFileDialog.FileName);

                            // Ustaw obraz w PictureBox
                            pictureBox1.Image = new Bitmap(loadedImage);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Dopasowanie obrazu do kontrolki
                        }
                        catch (Exception ex)
                        {
                            // Obs³uga wyj¹tków w przypadku b³êdu podczas ³adowania
                            MessageBox.Show($"B³¹d podczas ³adowania obrazu: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

            }

        }
        private void BtnApplyVignette_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

                // Ustaw intensywnoœæ winiety (np. z suwaka)
                float intensity = 2.0f; // Przyk³adowa wartoœæ

                // Zastosuj efekt winiety
                Bitmap processedImage = VignetteEffect.ApplyVignette(originalImage, intensity);

                // Wyœwietl przetworzony obraz
                pictureBox2.Image = processedImage;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Za³aduj obraz przed zastosowaniem efektu!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
