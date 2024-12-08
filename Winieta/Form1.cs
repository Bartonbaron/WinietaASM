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
        private void BtnApplyVignette_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

                // Ustaw intensywno�� winiety z suwaka
                float intensity = IntensityTB.Value / (float)IntensityTB.Maximum; // Normalizacja do przedzia�u 0-1

                VignetteEffect vignetteEffect = new VignetteEffect();

                // Zastosuj efekt winiety
                Bitmap vignetteImage = vignetteEffect.ApplyVignette(originalImage, intensity);
                // Wy�wietl przetworzony obraz
                pictureBox2.Image = vignetteImage;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Za�aduj obraz przed zastosowaniem efektu!", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void IntensityTB_Scroll(object sender, EventArgs e)
        {
            // Wy�wietl bie��c� warto�� w etykiecie
            label4.Text = $"Intensity: {IntensityTB.Value}";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
