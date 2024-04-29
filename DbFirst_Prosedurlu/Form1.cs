using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbFirst_Prosedurlu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // temizleme için
        public void Temizle()
        {
            textBox1.Clear();
            // TextBox1.Text = string.Empty;
            // textBox1.Text = "";
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        Db_first_uygulama2Entities connection = new Db_first_uygulama2Entities(); //connection stringi burada tanımlanır. // database bağlantısı için

        private void button2_Click(object sender, EventArgs e) // Listeleme
        {
            // dataGridView1.DataSource = connection.Musterilers.ToList(); // prosedürsüz listeleme örneği
            dataGridView1.DataSource = connection.MListele().ToList();     // prosedürlü listeleme örneği
        }

        private void button1_Click(object sender, EventArgs e) // Kaydetme
        {

            Musteriler kaydet = new Musteriler(); // Musteriler tablosundan bir nesne oluşturulur.
            kaydet.AdSoyad = textBox1.Text; // textbox1'deki veriyi kaydet nesnesinin AdSoyad'ına atar.
            kaydet.Tc = textBox2.Text; // textbox2'deki veriyi kaydet nesnesinin Tc'sine atar.
            kaydet.Urun = textBox3.Text; // textbox3'deki veriyi kaydet nesnesinin Urun'üne atar.
            kaydet.Fiyat = Convert.ToDecimal(textBox4.Text); // textbox4'deki veriyi kaydet nesnesinin Fiyat'ına atar.
            //Prosedürsüz örnekte bu şekilde
            //connection.Musterilers.Add(kaydet); // Musterilers listesine kaydet nesnesini ekler. sonra savechanges ile database'e kaydeder.
            //connection.SaveChanges(); // Değişiklikleri database'e kaydeder ve çıkar.

            connection.MKaydet(kaydet.AdSoyad, kaydet.Tc, kaydet.Urun, kaydet.Fiyat); // prosedürlü kaydetme örneği
            dataGridView1.DataSource = connection.MListele().ToList(); // kaydettikten sonra listeler.
            Temizle(); // kaydettikten sonra textboxları temizler.
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // datagridview'de bir hücreye tıklandığında
        {
            DataGridViewRow satir = dataGridView1.CurrentRow; // seçilen satırı satir adlı değişkene atar.
            textBox1.Tag = satir.Cells["id"].Value.ToString(); // seçilen satırın id'sini textbox1'in tag'ine atar. 
            textBox1.Text = satir.Cells["AdSoyad"].Value.ToString(); // seçilen satırın AdSoyad'ını textbox1'e atar.
            textBox2.Text = satir.Cells["Tc"].Value.ToString(); // seçilen satırın Tc'sini textbox2'ye atar.
            textBox3.Text = satir.Cells["Urun"].Value.ToString(); // seçilen satırın Urun'ünü textbox3'e atar.
            textBox4.Text = satir.Cells["Fiyat"].Value.ToString(); // seçilen satırın Fiyat'ını textbox4'e atar.

        }

        private void button3_Click(object sender, EventArgs e) // Güncelleme
        {
            Musteriler guncelle = new Musteriler(); 
            guncelle.id = Convert.ToInt32(textBox1.Tag); // textbox1'in tag'inde tuttuğumuz veriyi id adlı değişkene atar.          
            guncelle.AdSoyad = textBox1.Text; // textbox1'deki veriyi guncelle nesnesinin AdSoyad'ına atar.
            guncelle.Tc = textBox2.Text; // textbox2'deki veriyi guncelle nesnesinin Tc'sine atar.
            guncelle.Urun = textBox3.Text; // textbox3'deki veriyi guncelle nesnesinin Urun'üne atar.
            guncelle.Fiyat = Convert.ToDecimal(textBox4.Text); // textbox4'deki veriyi guncelle nesnesinin Fiyat'ına atar.

            connection.MGuncelle(guncelle.id, guncelle.AdSoyad, guncelle.Tc, guncelle.Urun, guncelle.Fiyat); // prosedürlü güncelleme örneği    
            dataGridView1.DataSource = connection.MListele().ToList(); // güncelledikten sonra listeler.
            Temizle(); // güncelledikten sonra textboxları temizler.
        }

        private void button4_Click(object sender, EventArgs e) // Sil
        {
            Musteriler sil = new Musteriler();
            sil.id = Convert.ToInt32(textBox1.Tag); // textbox1'in tag'inde tuttuğumuz veriyi id adlı değişkene atar.
            connection.MSil(sil.id); // prosedürlü silme örneği
            dataGridView1.DataSource = connection.MListele().ToList(); // sildikten sonra listeler.
            Temizle(); // sildikten sonra textboxları temizler.
        }

        private void button5_Click(object sender, EventArgs e) // ad soyad'a göre arama
        {
            Musteriler ara = new Musteriler();
            ara.AdSoyad = textBox5.Text; // textbox5'deki veriyi ara nesnesinin AdSoyad'ına atar.
            dataGridView1.DataSource = connection.MAd_Ara(ara.AdSoyad).ToList(); // prosedürlü ad soyad'a göre arama örneği     
            // bu şekilde de yapılabilir fakat yukarıdaki gibi daha düzgün bir kullanım olur.
            //string ad = textBox5.Text; //dataGridView1.DataSource = connection.MAd_Ara(ad).ToList();
            //dataGridView1.DataSource = connection.MAd_Ara(textBox5.Text).ToList();

        }

        private void button6_Click(object sender, EventArgs e) // tc'ye göre arama
        {
            // SELECT * FROM Musteriler WHERE Tc = '1111111' // ADO.NET prosedürsüz parametresiz
            // SELECT * FROM Musteriler WHERE Tc = @Tc // ADO.NET prosedürlü parametreli

            var sonuc = connection.Musterilers.Where(x => x.Tc == textBox5.Text).ToList(); // linq sorgusu
            dataGridView1.DataSource = sonuc;
        }

        private void button7_Click(object sender, EventArgs e) // direk arama
        {
            dataGridView1.DataSource = connection.Musterilers.Where(x => x.AdSoyad== "Ali Kısa" && x.Tc == "44444" ).ToList(); // linq sorgusu
        }

        private void button8_Click(object sender, EventArgs e) // ara örnek
        {
            dataGridView1.DataSource = connection.Musterilers.Where(x => x.AdSoyad.Equals("Ali Kısa") && x.Tc.Equals("44444")).ToList(); // linq sorgusu    
        }
    }
}
