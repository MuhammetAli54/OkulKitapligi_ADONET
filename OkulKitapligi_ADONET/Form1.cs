using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulKitapligi_ADONET
{
    public partial class FormYazarlar : Form
    {
        public FormYazarlar()
        {
            InitializeComponent();
        }

        //GLOBAL ALAN
        //SQLCOONECTION Nesnesi: SQL Veri Tabanına bağlantı kurmak için kullanacağımız classtır. System.Data.Client namespace'i içerisinde yer alır.

        SqlConnection baglanti = new SqlConnection();
        string SQLBaglantiCumlesi = @"Server=DESKTOP-TUMHS1A;Database=OKULKITAPLIGI; Trusted_Connection=True";
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewYazarlar.MultiSelect = false; // çoklu satır seçimi engellendi
            dataGridViewYazarlar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // fare ile datagrid üzerinde bir hücreye tıklandığında  bulunduğu satırı tamemen seçecek

            //grid'in içine bilgileri getireceğiz
            //SQLConnection'a bağlanacağı adresi verdik.

            //dataGridView'e sağ tıklanınca gelecek olan contextmenustrip ataması yapıldı
            dataGridViewYazarlar.ContextMenuStrip = contextMenuStrip1;

            TumYazarlariGetir();
        }

        private void TumYazarlariGetir()
        {
            try
            {
                //SQLCOMMAND Nesmesi : Sorgularımızı ve prosedürlerimize ait komutları alan nesnedir.
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                string sorgu = "Select * from Yazarlar where SilindiMi=0 order by YazarId desc";
                komut.CommandText = sorgu;
                BaglantiyiAc();

                //DataSQLADAPTER nesnesi, sorgu çalışınca oluşan dataların katarılması işlemini yapar.Adaptöre hangi komutu işleyeceğini ctor'da verebiliriz yada daha sonra verebiliriz.
                SqlDataAdapter adaptor = new SqlDataAdapter(komut);  //ctor'da komutunu verdik.
                                                                     // adaptor.SelectCommand = komut;   2. SEÇENEK ---> Komutu sonradan verdik

                //Adaptorun içindeki verileri sanalTabloya alıyoruz
                DataTable sanalTablo = new DataTable();
                adaptor.Fill(sanalTablo);

                dataGridViewYazarlar.DataSource = sanalTablo;
                dataGridViewYazarlar.Columns["SilindiMi"].Visible = false;
                dataGridViewYazarlar.Columns["YazarAdSoyad"].Width = 220;
                dataGridViewYazarlar.Columns["KayitTarihi"].Width = 100;

                BaglantiyiKapat();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Beklenmedik bir hata oluştu. HATA : {ex.Message}", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            switch (btnEkle.Text)
            {
                case "EKLE":
                    try
                    {
                        if (string.IsNullOrEmpty(txtYazar.Text))
                        {
                            MessageBox.Show("Lütfen yazar bilgisini giriniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //Ekleme yapacağız.
                            string insertCumlesi = $"insert into Yazarlar (KayitTarihi,YazarAdSoyad,SilindiMi) values('{TarihiDuzenle(DateTime.Now)}','{txtYazar.Text.Trim()}',0)";
                            SqlCommand insertkomut = new SqlCommand(insertCumlesi, baglanti);
                            //baglantı açılacak metot çağıralım.
                            BaglantiyiAc();
                            int sonuc = insertkomut.ExecuteNonQuery();
                            if (sonuc > 0) // affected rows var
                            {
                                MessageBox.Show("Yeni yazar sisteme eklendi.");
                                TumYazarlariGetir();
                            }
                            else
                            {
                                MessageBox.Show("Bir hata oluştu. Yeni yazar eklenemedi!");
                            }
                            //baglantı kapanacak metot çağıralım
                            BaglantiyiKapat();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ekleme işleminde beklenmedik hata oluştu!" + ex.Message);
                    }
                    Temizle();
                    break;

                case "GÜNCELLE":
                    try
                    {
                        if (!string.IsNullOrEmpty(txtYazar.Text))
                        {
                            using (baglanti)
                            {
                                DataGridViewRow satir = dataGridViewYazarlar.SelectedRows[0];
                                int YazarId = Convert.ToInt32(satir.Cells["YazarId"].Value);
                                string updateSorgucumlesi = $"Update Yazarlar Set YazarAdSoyad= '{txtYazar.Text.Trim()}' where YazarId={YazarId}";
                                SqlCommand updateCommand = new SqlCommand(updateSorgucumlesi, baglanti);
                                BaglantiyiAc();

                                int sonuc = updateCommand.ExecuteNonQuery();
                                if (sonuc > 0)
                                {
                                    MessageBox.Show($"Yazar güncellendi");
                                    TumYazarlariGetir();
                                }
                                else
                                {
                                    MessageBox.Show("Yazar güncellenmedi !");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Yazar adı olmadan güncelleştirme yapılamaz!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Güncelleme işleminde beklenmedik hata oluştu !" + ex.Message);
                    }
                    Temizle();
                    break;

                default:
                    break;
            }
        }

        private void Temizle()
        {
            btnEkle.Text = "EKLE";
            txtYazar.Clear();
        }

        private void BaglantiyiKapat()
        {
            try
            {
                if (baglanti.State != ConnectionState.Closed)
                {
                    baglanti.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantıyı kapatırken bir hata oluştu !" + ex.Message);
            }
        }

        private void BaglantiyiAc()
        {
            try
            {
                //baglantı açık degilse acalım
                if (baglanti.State != ConnectionState.Open)
                {
                    baglanti.ConnectionString = SQLBaglantiCumlesi;
                    baglanti.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı açılırken bir hata oluştu !" + ex.Message);
            }
        }

        private string TarihiDuzenle(DateTime tarih)
        {
            string tarihString = string.Empty;
            if (tarih != null)
            {
                tarihString = tarih.Year + "-" + tarih.Month + "-" + tarih.Day + " " + tarih.Hour + " : " + tarih.Minute + " : " + tarih.Second;
            }


            return tarihString;
        }

        private void guncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewYazarlar.SelectedRows.Count > 0)
            {
                DataGridViewRow satir = dataGridViewYazarlar.SelectedRows[0];
                string yazarAdSoyad = Convert.ToString(satir.Cells["YazarAdSoyad"].Value);
                btnEkle.Text = "GÜNCELLE";
                txtYazar.Text = yazarAdSoyad;

            }
            else
            {
                MessageBox.Show("Güncelleme işlemi için tablodan bir yazar seçmeniz gerekiyor.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow secilenSatir = dataGridViewYazarlar.SelectedRows[0];
            int yazarId = (int)secilenSatir.Cells["YazarId"].Value;
            string yazar = Convert.ToString(secilenSatir.Cells["YazarAdSoyad"].Value);

            //Yazarın kitapları varsa Kitaplar tablosunda YazarId foreign key vardır.
            //Bu durumda  silme işlemi yaılmamalıdır.

            //Önce bir select sorgusu ile kitaplar tablosunda o yazara ait kayıt var mı diye bakmalıyız.
            //Varsa silmesine izin vermeyeceğiz.
            //Yoksa silmek ister misin diye son bir kez sorup evet derse sileceğiz.

            SqlCommand komut = new SqlCommand($"select * from Kitaplar where YazarId= {yazarId}", baglanti); //Sql command nesnesine çalıştıracağı sorguyu ve hangi bağlantı üzerinde çalıştıracağını ctor'da verdik.

            komut.Connection = baglanti;
            SqlDataAdapter adaptor = new SqlDataAdapter(komut); // adaptöre işleyeceği komutu adaptorun ctor'unda verdik.
            DataTable sanalTablo = new DataTable();
            BaglantiyiAc();
            adaptor.Fill(sanalTablo);
            if (sanalTablo.Rows.Count > 0)
            {
                MessageBox.Show($"{yazar} isimli yazarın kitaplar tablosunda {sanalTablo.Rows.Count.ToString()} adet kitabı bulunmaktadır. Bu yazarı silmek için önceliklesistemdeki ona tanımlı kitaplar silmeniz gerekmektedir. Lütfen Kitap işlemleri sayfasına gidiniz !");
            }
            else
            {
                //Kitabı yok. Foreign key patlaması olmaz. Silebilirsiniz
                DialogResult cevap = MessageBox.Show($"{yazar} adlı yazarı silmek istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    //silecek
                    // komut.CommandText = $"Delete from Yazarlar where YazarId= {yazarId}"; ---> 2.YOL
                    komut.CommandText = $"Delete from Yazarlar where YazarId=@yzrid";
                    komut.Parameters.Clear();
                    //AddWithValue metodu @yzrid yerine yazarId değerini sqlcommand nesnesinin commendText'inde bulunan sorgu cümlesine entegre eder.
                    komut.Parameters.AddWithValue("@yzrid", yazarId);

                    BaglantiyiAc();
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc>0)
                    {
                        MessageBox.Show("Silindi");
                        TumYazarlariGetir();
                    }
                    else
                    {
                        MessageBox.Show("HATA: Silinemedi!");
                    }BaglantiyiKapat();
                }
            }

        }

        private void silPasifeCekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Kullanıcı silindi zannedecek ama biz aslında pasif yapacağız.
            try
            {
                using (baglanti)
                {
                    DataGridViewRow satir = dataGridViewYazarlar.SelectedRows[0];
                    int YazarId = Convert.ToInt32(satir.Cells["YazarId"].Value);
                    string updateSorgucumlesi = "Update Yazarlar Set SilindiMi=1 where YazarId=@yzrid";
                    SqlCommand updateCommand = new SqlCommand(updateSorgucumlesi, baglanti);
                    updateCommand.Parameters.AddWithValue("@yzrid",YazarId);

                    BaglantiyiAc();

                    int sonuc = updateCommand.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show($"Yazar silindi");
                        TumYazarlariGetir();
                    }
                    else
                    {
                        MessageBox.Show("DİKKAT : Yazar silinmedi !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pasife çek silme işleminde hata: " + ex.Message);
            }
        }

        private void silBaskaBirYontemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Bu yöntem yukarıdakiler gibi kullanışlı değildir.
            try
            {
                DataGridViewRow secilenSatir = dataGridViewYazarlar.SelectedRows[0];
                int yazarId = (int)secilenSatir.Cells["YazarId"].Value;
                string yazar = Convert.ToString(secilenSatir.Cells["YazarAdSoyad"].Value);
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;

                DialogResult cevap = MessageBox.Show($"{yazar} adlı yazarı silmek istediğinize emin misiniz?", "ONAY", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    //silecek
                    //komut.CommandText = $"Delete from Yazarlar where YazarId={yazarId}";
                    //@yzrid diyerek bir parametre oluşturmuş olduk.
                    komut.CommandText = $"Delete from Yazarlar where YazarId=@yzrid";
                    komut.Parameters.Clear();
                    //AddWithValue metodu @yzrid yerine yazarId değerini sqlcommand nesnesinin commendText'inde bulunan sorgu cümlesine entegre eder.
                    komut.Parameters.AddWithValue("@yzrid", yazarId);

                    BaglantiyiAc();
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Silindi");
                        TumYazarlariGetir();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: " + ex.Message);
            }
        }
    }
}

