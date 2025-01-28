using System.IO;
using System.Drawing;
using ImageProcessing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Winieta
{
    public partial class Form1 : Form
    {
        private Bitmap? vignetteImage;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            checkBoxCSharp.CheckedChanged += checkBoxCSharp_CheckedChanged;
            checkBoxASM.CheckedChanged += checkBoxASM_CheckedChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Pobranie liczby procesorów logicznych
            int logicalProcessors = Environment.ProcessorCount;

            // Ustawienie wartoœci maksymalnej suwaka na 64
            threadTrackBar.Maximum = 64;

            // Ustawienie wartoœci bie¿¹cej na liczbê procesorów logicznych, jeœli jest mniejsza lub równa 64
            threadTrackBar.Value = Math.Min(logicalProcessors, 64);

            // Aktualizacja etykiety liczby w¹tków
            threadCountLabel.Text = $"Liczba w¹tków: {threadTrackBar.Value}";
        }

        private byte[] BitmapToByteArrayRGB(Bitmap bitmap, out int stride)
        {
            // Przekszta³cenie obrazu do formatu 24-bitowego RGB
            Bitmap rgbBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format24bppRgb);

            // Pobranie danych obrazu
            BitmapData bitmapData = rgbBitmap.LockBits(new Rectangle(0, 0, rgbBitmap.Width, rgbBitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int byteCount = bitmapData.Stride * bitmapData.Height;
            byte[] imageData = new byte[byteCount];
            stride = bitmapData.Stride;

            // Kopiowanie danych do tablicy
            Marshal.Copy(bitmapData.Scan0, imageData, 0, byteCount);

            rgbBitmap.UnlockBits(bitmapData);
            return imageData;
        }

        private Bitmap ByteArrayToBitmapRGB(byte[] imageData, int width, int height, int stride)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            // Kopiowanie danych z tablicy do obiektu Bitmap
            Marshal.Copy(imageData, 0, bitmapData.Scan0, imageData.Length);

            bitmap.UnlockBits(bitmapData);
            return bitmap;
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
            if (pictureBox1.Image is Bitmap originalImage)
            {
                try
                {
                    // Obliczenie intensywnoœci na podstawie suwaka
                    float intensity = IntensityTB.Value / (float)IntensityTB.Maximum;

                    // Pobranie liczby w¹tków z suwaka
                    int threadCount = threadTrackBar.Value;

                    // Konwersja obrazu do tablicy bajtów
                    int stride;
                    byte[] imageData = BitmapToByteArrayRGB(originalImage, out stride);

                    // Pomiar czasu przetwarzania
                    Stopwatch stopwatch = new Stopwatch();

                    // Wybór implementacji
                    stopwatch.Start();
                    if (checkBoxCSharp.Checked)
                    {
                        var vignetteEffect = new VignetteEffect();
                        vignetteEffect.ApplyVignette(imageData, originalImage.Width, originalImage.Height, stride, intensity, threadCount);
                    }
                    else if (checkBoxASM.Checked)
                    {
                        VignetteProcessorASM.ApplyVignette(imageData, originalImage.Width, originalImage.Height, stride, intensity, threadCount);
                    }
                    stopwatch.Stop();

                    // Konwersja przetworzonego obrazu z powrotem na Bitmap
                    vignetteImage = ByteArrayToBitmapRGB(imageData, originalImage.Width, originalImage.Height, stride);

                    // Wyœwietlenie przetworzonego obrazu w PictureBox
                    pictureBox2.Image = vignetteImage;
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                    // Wyœwietlenie czasu wykonania
                    long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
                    MessageBox.Show($"Czas wykonania z {threadCount} w¹tkami: {elapsedMilliseconds} ms",
                                    "Czas wykonania", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Zapisanie wyniku do pliku logów
                    SaveLog(threadCount, elapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    // Obs³uga b³êdów
                    MessageBox.Show($"Wyst¹pi³ b³¹d podczas przetwarzania obrazu: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Za³aduj obraz przed zastosowaniem efektu!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        /// <summary>
        /// Zapisuje logi o czasie wykonania do pliku.
        /// </summary>
        /// <param name="threadCount">Liczba w¹tków.</param>
        /// <param name="elapsedMilliseconds">Czas wykonania w milisekundach.</param>
        private void SaveLog(int threadCount, long elapsedMilliseconds)
        {
            try
            {
                // Okreœlenie œcie¿ki logów (katalog u¿ytkownika)
                string logFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ExecutionTimesLog.txt");

                // Tworzenie wpisu do logów
                string logEntry = $"{DateTime.Now}: Liczba w¹tków: {threadCount}, Czas wykonania: {elapsedMilliseconds} ms";

                // Dodanie wpisu do pliku logów
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Wyœwietlenie komunikatu w przypadku problemów z zapisem
                MessageBox.Show($"Nie uda³o siê zapisaæ logów: {ex.Message}", "B³¹d logowania", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Wyœwietl bie¿¹c¹ wartoœæ w etykiecie
            label4.Text = $"Intensity: {IntensityTB.Value}";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            // Tworzenie okna dialogowego do zapisu pliku
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png";
            saveFileDialog.Title = "Zapisz przetworzony obraz";

            // Wyœwietlenie okna dialogowego
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Zapisanie przetworzonego obrazu (jeœli istnieje)
                    if (vignetteImage != null)
                    {
                        vignetteImage.Save(saveFileDialog.FileName);
                        MessageBox.Show("Obraz zapisany pomyœlnie!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Brak obrazu do zapisania.", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Obs³uga b³êdów podczas zapisu pliku
                    MessageBox.Show($"Wyst¹pi³ b³¹d podczas zapisywania pliku: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBoxCSharp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCSharp.Checked)
            {
                checkBoxASM.Checked = false;
            }
        }

        private void checkBoxASM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxASM.Checked)
            {
                checkBoxCSharp.Checked = false;
            }
        }

        private void threadTrackBar_Scroll(object sender, EventArgs e)
        {
            threadCountLabel.Text = $"Liczba w¹tków: {threadTrackBar.Value}";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
