using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DbFirst_prosedursuz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // connection için
        Db_first_uygulama1Entities connection = new Db_first_uygulama1Entities(); //connection stringi burada tanımlanır. // database bağlantısı için

        private void button4_Click(object sender, EventArgs e) // listeleme
        {
            dataGridView1.DataSource = connection.Araclars.ToList(); // Araclars tablosundaki verileri listeler. // araclarS dbcontextteki oluşturulan liste adı arraylist
        }

        private void button1_Click(object sender, EventArgs e) // Kaydetme
        {
            Araclar aracKaydet = new Araclar(); // Araclar tablosundan bir nesne oluşturulur.
            aracKaydet.AracSeriNo = Convert.ToInt32(textBox1.Text); // textbox1'deki veriyi aracKaydet nesnesinin AracSeriNo'suna atar.
            aracKaydet.Marka = textBox2.Text; // textbox2'deki veriyi aracKaydet nesnesinin Marka'sına atar.
            aracKaydet.Model = textBox3.Text; // textbox3'deki veriyi aracKaydet nesnesinin Model'sine atar.
            aracKaydet.Renk = textBox4.Text; // textbox4'deki veriyi aracKaydet nesnesinin Renk'sine atar.
            connection.Araclars.Add(aracKaydet); // Araclars listesine aracKaydet nesnesini ekler.
            connection.SaveChanges(); // Değişiklikleri database'e kaydeder ve çıkar.
            dataGridView1.DataSource = connection.Araclars.ToList(); // kaydettikten sonra listeler.
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) // datagridview'de bir hücreye tıklandığında
        {
            DataGridViewRow satir = dataGridView1.CurrentRow; // seçilen satırı satir adlı değişkene atar.
            textBox1.Tag = satir.Cells["id"].Value.ToString(); // seçilen satırın id'sini textbox1'in tag'ine atar.
            textBox1.Text = satir.Cells["AracSeriNo"].Value.ToString(); // seçilen satırın AracSeriNo'sunu textbox1'e atar.
            textBox2.Text = satir.Cells["Marka"].Value.ToString(); // seçilen satırın Marka'sını textbox2'ye atar.
            textBox3.Text = satir.Cells["Model"].Value.ToString(); // seçilen satırın Model'ini textbox3'e atar.
            textBox4.Text = satir.Cells["Renk"].Value.ToString(); // seçilen satırın Renk'ini textbox4'e atar.
        }

        private void button2_Click(object sender, EventArgs e) // Güncelleme
        {
            int id = Convert.ToInt32(textBox1.Tag); // textbox1'in tag'inde tuttuğumuz veriyi id adlı değişkene atar.
            var guncelle = connection.Araclars.Where(x => x.id == id).FirstOrDefault(); // Araclars listesinde/tablosunda(x) id'si id olan veriyi guncelle adlı değişkene atar. Linq sorgusu.
            // FirstOrDefault() metodu, sorgu sonucunda dönen verilerin ilkini alır.

            guncelle.AracSeriNo = Convert.ToInt32(textBox1.Text); // textbox1'deki veriyi guncelle nesnesinin AracSeriNo'suna atar.
            guncelle.Marka = textBox2.Text; // textbox2'deki veriyi guncelle nesnesinin Marka'sına atar.
            guncelle.Model = textBox3.Text; // textbox3'deki veriyi guncelle nesnesinin Model'sine atar.
            guncelle.Renk = textBox4.Text; // textbox4'deki veriyi guncelle nesnesinin Renk'sine atar.
            connection.SaveChanges(); // Değişiklikleri database'e kaydeder ve çıkar.
            dataGridView1.DataSource = connection.Araclars.ToList(); // güncelledikten sonra listeler.

        }

        private void button3_Click(object sender, EventArgs e) // Sil
        {
            int id = Convert.ToInt32(textBox1.Tag); // textbox1'in tag'inde tuttuğumuz veriyi id adlı değişkene atar.
            var sil = connection.Araclars.Where(x => x.id == id).FirstOrDefault(); // Araclars listesinde/tablosunda(x) id'si id olan veriyi sil adlı değişkene atar. Linq sorgusu.
            connection.Araclars.Remove(sil); // Araclars listesinden sil nesnesini siler. 
            connection.SaveChanges(); // Değişiklikleri database'e kaydeder ve çıkar.
            dataGridView1.DataSource = connection.Araclars.ToList(); // sildikten sonra listeler.

            
        }
    }
}
