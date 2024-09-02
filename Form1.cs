using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Kısayol_Virüsü_Temizleme_Aracı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (driveListBox1.Drive == "c:")
            {
                MessageBox.Show("C:\\ diski üzerinde kısayol virüsü temizleme işlemi yapılamaz. Bunu yapmak bilgisayarınızdaki tüm kısayolların silinmesine sebep olacaktır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Clean();
            }
        }
        private void Clean()
        {
            if (MessageBox.Show("Seçtiğiniz sürücüdeki tüm kısayollar ve autorun.inf dosyası silinecektir. Aynı zamanda gizli olan tüm dosyalar ve klasörler tekrar görülebilir olacaktır. Devam etmek istiyor musunuz?" , "Onay", MessageBoxButtons.YesNo , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    //Kısayol dosyalarını silelim.
                    string drive = driveListBox1.Drive + "\\";
                    ProcessStartInfo process1 = new ProcessStartInfo("cmd", "/c del " + drive + "*.lnk /s");
                    process1.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(process1);
                    //Autorun dosyasını silelim.
                    if (File.Exists(driveListBox1.Drive + "\\autorun.inf") == true)
                    {
                        File.Delete(drive + "autorun.inf");
                    }
                    //Virüsün gizlediği dosyaları tekrar görünür yapalım.
                    process1.FileName = "attrib";
                    process1.Arguments = "-s -r -h " + drive + "*.* /s /d";
                    Process.Start(process1);
                    MessageBox.Show("Virüs sürücüden başarıyla kaldırıldı.", "Temizle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Virüs sürücüden kaldırılamadı. Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kısayol Virüsü Temizleme Aracı 1.0 - DarkEmir", "Hakkında", MessageBoxButtons.OK);
        }
    }
}
