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
    public partial class FormKitaplar : Form
    {
        public FormKitaplar()
        {
            InitializeComponent();
        }

        //Global alan

        string SQLBaglantiCumlesi = @"Server=DESKTOP-TUMHS1A;Database=OKULKITAPLIGI; Trusted_Connection=True";
        SqlConnection baglanti = new SqlConnection();

        private void FormKitaplar_Load(object sender, EventArgs e)
        {
            //grid ile ilgili ayarlamalar
            dataGridViewKitaplar.MultiSelect = false;
            dataGridViewKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            TumKitaplariViewileGrideGetir();

            UC_MyButton_Guncelle.myButton.Text = "GÜNCELLE";
            UC_MyButton_Guncelle.myButton.Click += new EventHandler(btn_KitapGuncelle);

            GuncelleSayfasindakiGroupBoxIciniTemizle();

            //Düzenle'deki combo dolsun
            TumKitaplariComboBoxaGetir();

            //Yazarları combo'ya getir
            TumYazarlariComboBoxaGetir();

            //Turleri combo'ya getir
            TumTurleriComboBoxaGetir();

            tabControl1.Click += new EventHandler(tabControl1_Click);

            UC_MyButton_Ekle.myButton.Text = "KİTAP EKLE";
            UC_MyButton_Ekle.myButton.Click += new EventHandler(btn_KitapEkle);

            UC_MyButton_Sil.myButton.Text = "KİTAP SİL";
            UC_MyButton_Sil.myButton.Click += new EventHandler(btn_KitapSil);

        }
        private void btn_KitapSil(object sender, EventArgs e)
        {
            try
            {
                //silme işlemi
                DialogResult cevap = MessageBox.Show("Seçtiğiniz kitabı silmek yerine listeden kaldırmak ister misiniz?", "UYARI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    //Pasifleştirmek
                    SqlCommand komut = new SqlCommand($"update Kitaplar set SilindiMi=1 where KitapId={cmbBox_Sil_Kitap.SelectedValue}", baglanti);
                    BaglantiyiAc();
                    int sonuc = komut.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Kitap pasifleştirme işlemiyle listeden kaldırıldı...");
                    }
                    else
                    {
                        MessageBox.Show("HATA: Beklenmedik bir hata oluştu !");
                    }
                }
                else if (cevap == DialogResult.No)
                {
                    //Gerçekten silmek
                    SqlCommand komut = new SqlCommand($"select * from Kitaplar k inner join Islem i on i.KitapId = k.KitapId where k.KitapId={cmbBox_Sil_Kitap.SelectedValue.ToString()}", baglanti);
                    BaglantiyiAc();
                    SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                    //İçinde birden fazla datatable barındırabilen nesnedir.
                    DataSet dataset = new DataSet();
                    adaptor.Fill(dataset);
                    if ((int)dataset.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("Bu kitaba ait ödünç alma işlemleri yapılmıi.Silmek için öncelikle ödünç işlemler sayfasına gidip oradan bu kitabın bilgilerini silmelisiniz !!");
                    }
                    else
                    {
                        // silecek
                        komut = new SqlCommand($"delete from Kitaplar where KitapId=@kitapid", baglanti);
                        komut.Parameters.Clear();

                        komut.Parameters.AddWithValue("@kitapid", cmbBox_Sil_Kitap.SelectedValue.ToString());

                        BaglantiyiAc();
                        int sonuc = komut.ExecuteNonQuery();
                        if (sonuc > 0)
                        {
                            MessageBox.Show("Silindi");
                            SilmeSayfasiTumKontrolleriTemizle();
                        }
                        else
                        {
                            MessageBox.Show("HATA:Silinemedi!");
                        }
                        BaglantiyiKapat();
                    }

                    BaglantiyiKapat();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: Silme işleminde hata oluştu !" + ex.Message);
            }
        }

        private void btn_KitapEkle(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_KitapEkle.Text.Trim()))
                {
                    MessageBox.Show("Kitap adı girmeniz gerekmektedir", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (numericUpDown_Ekle_SayfaSayisi.Value < 0)
                {
                    MessageBox.Show("Kitabın sayfa sayısı sıfırdan büyük olmalıdır", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (numericUpDown_Ekle_Stok.Value < 0)
                {
                    MessageBox.Show("Kitabın stok adeti sıfırdan büyük olmalıdır", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (cmbBox_Ekle_Tur.SelectedIndex < 0)
                {
                    MessageBox.Show("Kitabın türünü seçmelisiniz! ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else if (cmbBox_Ekle_Yazar.SelectedIndex < 0)
                {
                    MessageBox.Show("Kitabın yazarını seçmelisiniz! ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EklemeSayfasiTumKontrolleriTemizle();
                }
                else
                {
                    using (baglanti)
                    {
                        // ekleme işlemini prosedürle yapacağız
                        SqlCommand komut = new SqlCommand($"SP_KitapEkle", baglanti);
                        komut.CommandType = CommandType.StoredProcedure;
                        //parametreleri ekle
                        //1. yol
                        //SqlParameter KitapAdParameter = new SqlParameter();
                        //KitapAdParameter.Value = "@kitapAdi";
                        //KitapAdParameter.Value = txt_KitapEkle.Text.Trim();

                        //2.yol
                        komut.Parameters.AddWithValue("kayitTarihi", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        komut.Parameters.AddWithValue("@kitapAdi", txt_KitapEkle.Text.Trim());
                        komut.Parameters.AddWithValue("@sayfaSayisi", numericUpDown_Ekle_SayfaSayisi.Value);
                        komut.Parameters.AddWithValue("@turId", (int)cmbBox_Ekle_Tur.SelectedValue);
                        komut.Parameters.AddWithValue("@yazarId", (int)cmbBox_Ekle_Yazar.SelectedValue);
                        komut.Parameters.AddWithValue("@stok", numericUpDown_Ekle_Stok.Value);
                        BaglantiyiAc();

                        //ExecuteScalar , genellikle sorgunuz tek bir değer verdiğinde kullanılır.Daha fazla döndürürse, sonuç ilk satırın ilk sütunu olur. Bir örnek SELECT @@IDENTITY AS 'Identity' olabilir.ExecuteReader , birden çok satır / sütuna sahip herhangi bir sonuç için kullanılır(örneğin, SELECT col1, col2 from sometable ).

                        int sonEklenenKitapId = Convert.ToInt32(komut.ExecuteScalar());
                        if (sonEklenenKitapId > 0)
                        {
                            MessageBox.Show($"Yeni bir kitap eklendi... \n kitapId: {sonEklenenKitapId}");
                            EklemeSayfasiTumKontrolleriTemizle();
                        }
                        else
                        {
                            MessageBox.Show("HATA: Kitap eklenemedi !");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: Ekleme işleminde hata oluştu " + ex.Message);
            }
        }

        private void SilmeSayfasiTumKontrolleriTemizle()
        {
            cmbBox_Sil_Kitap.SelectedIndex = -1;
            cmbBox_Sil_Kitap.Text = "Kitap seçiniz...";
            richTextBoxKitap.Clear();

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            TumKitaplariViewileGrideGetir();
            TumKitaplariComboBoxaGetir();
            GuncelleSayfasiPasifTemizlikIslemiYap();
            EklemeSayfasiTumKontrolleriTemizle();
            SilmeSayfasiTumKontrolleriTemizle();
        }

        private void EklemeSayfasiTumKontrolleriTemizle()
        {
            txt_KitapEkle.Clear();
            cmbBox_Ekle_Tur.SelectedIndex = -1;
            cmbBox_Ekle_Tur.Text = "Tür seçiniz...";
            cmbBox_Ekle_Yazar.SelectedIndex = -1;
            cmbBox_Ekle_Yazar.Text = "Yazar seçiniz";
            numericUpDown_Ekle_SayfaSayisi.Value = 0;
            numericUpDown_Ekle_Stok.Value = 0;
        }

        private void GuncelleSayfasiPasifTemizlikIslemiYap()
        {
            comboBoxKitapGuncelle.SelectedIndex = -1;
            GuncelleSayfasindakiGroupBoxIciniTemizle();
            groupBoxKitapGuncelle.Enabled = false;
        }

        private void TumTurleriComboBoxaGetir()
        {
            try
            {
                using (baglanti)
                {
                    SqlCommand komut = new SqlCommand("select * from Turler order by TurAdi", baglanti);
                    BaglantiyiAc();
                    SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                    DataTable sanalTablo = new DataTable();
                    adaptor.Fill(sanalTablo);
                    cmbBox_Guncelle_Tur.DisplayMember = "TurAdi";
                    cmbBox_Guncelle_Tur.ValueMember = "TurId";
                    cmbBox_Guncelle_Tur.DataSource = sanalTablo;
                    cmbBox_Guncelle_Tur.SelectedIndex = -1;

                    cmbBox_Ekle_Tur.DisplayMember = "TurAdi";
                    cmbBox_Ekle_Tur.ValueMember = "TurId";
                    cmbBox_Ekle_Tur.DataSource = sanalTablo;
                    cmbBox_Ekle_Tur.SelectedIndex = -1;
                    cmbBox_Ekle_Tur.Text = "Tür seçiniz...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata ! " + ex.Message);
            }
        }

        private void TumYazarlariComboBoxaGetir()
        {
            try
            {
                using (baglanti)
                {
                    SqlCommand komut = new SqlCommand("select * from Yazarlar order by YazarAdSoyad", baglanti);
                    BaglantiyiAc();
                    SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                    DataTable sanalTablo = new DataTable();
                    adaptor.Fill(sanalTablo);
                    cmbBox_Guncelle_Yazar.DisplayMember = "YazarAdSoyad";
                    cmbBox_Guncelle_Yazar.ValueMember = "YazarId";
                    cmbBox_Guncelle_Yazar.DataSource = sanalTablo;
                    cmbBox_Guncelle_Yazar.SelectedIndex = -1;

                    cmbBox_Ekle_Yazar.DisplayMember = "YazarAdSoyad";
                    cmbBox_Ekle_Yazar.ValueMember = "YazarId";
                    cmbBox_Ekle_Yazar.DataSource = sanalTablo;
                    cmbBox_Ekle_Yazar.SelectedIndex = -1;
                    cmbBox_Ekle_Yazar.Text = "Yazar seçiniz...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yazarlar getirilirken bir hata oluştu ! " + ex.Message);
            }
        }

        private void TumKitaplariComboBoxaGetir()
        {
            try
            {
                using (baglanti)
                {
                    SqlCommand komut = new SqlCommand("select * from Kitaplar where SilindiMi=0 order by KitapAdi", baglanti);
                    SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                    DataTable sanalTablo = new DataTable();
                    adaptor.Fill(sanalTablo);
                    comboBoxKitapGuncelle.DisplayMember = "KitapAdi";
                    comboBoxKitapGuncelle.ValueMember = "KitapId";
                    comboBoxKitapGuncelle.DataSource = sanalTablo;
                    comboBoxKitapGuncelle.SelectedIndex = -1;

                    cmbBox_Sil_Kitap.DisplayMember = "KitapAdi";
                    cmbBox_Sil_Kitap.ValueMember = "KitapId";
                    cmbBox_Sil_Kitap.DataSource = sanalTablo;
                    cmbBox_Sil_Kitap.SelectedIndex = -1;
                    cmbBox_Sil_Kitap.Text = "Kitap seçiniz...";

                    BaglantiyiKapat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu! " + ex.Message);
            }
        }

        private void CombodanSecilenKitabinTumBilgileriniDoldur(int kitapId)
        {
            try
            {
                using (baglanti)
                {
                    SqlCommand komut = new SqlCommand($"select * from Kitaplar k left join Turler t on k.TurId=t.TurId left join Yazarlar y on y.YazarId=k.YazarId where k.KitapId={kitapId}", baglanti);
                    BaglantiyiAc();
                    SqlDataReader okuyucu = komut.ExecuteReader();
                    while (okuyucu.Read())
                    {
                        txt_GuncelleKitapAdi.Text = okuyucu["KitapAdi"].ToString();
                        numericUpDown_Guncelle_SayfaSayisi.Value = (int)okuyucu["SayfaSayisi"];
                        numericUpDown_Guncelle_Stok.Value = (int)okuyucu["Stok"];
                        if (string.IsNullOrEmpty(okuyucu["TurId"].ToString()))
                        {
                            //kitabın türü yok
                            cmbBox_Guncelle_Tur.SelectedIndex = -1;
                            cmbBox_Guncelle_Tur.Text = "Türü yok...";
                        }
                        else
                        {
                            cmbBox_Guncelle_Tur.SelectedValue = (int)okuyucu["TurId"];
                        }
                        cmbBox_Guncelle_Yazar.SelectedValue = (int)okuyucu["YazarId"];
                    }
                    BaglantiyiKapat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu!" + ex.Message);
            }
        }

        private void btn_KitapGuncelle(Object sender, EventArgs e)
        {
            //Eğer yazar seçilmediyse uyarsın
            //Eğer kitap adı yazılmamışsa uyarsın
            //Stok ve sayfa sayısı > 0 olmalı

            if (string.IsNullOrEmpty(txt_GuncelleKitapAdi.Text.Trim()))
            {
                MessageBox.Show("Lütfen kitap adı giriniz! ", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasifTemizlikIslemiYap();
            }
            else if (numericUpDown_Guncelle_SayfaSayisi.Value <= 0)
            {
                MessageBox.Show("Sayfa sayısı sıfırdan büyük olmalıdır!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasifTemizlikIslemiYap();
            }
            else if (numericUpDown_Guncelle_Stok.Value <= 0)
            {
                MessageBox.Show("Stok adedi sıfırdan büyük olmalıdır!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasifTemizlikIslemiYap();
            }
            else if (cmbBox_Guncelle_Yazar.SelectedIndex < 0)
            {
                MessageBox.Show("Kitabın yazarını seçmelisiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleSayfasiPasifTemizlikIslemiYap();
            }
            else
            {
                object turidDurumu = (int)cmbBox_Guncelle_Tur.SelectedValue < 0 ? null : cmbBox_Guncelle_Tur.SelectedValue;

                //güncelleyeceğiz...
                string updateSQLQuery = $"update Kitaplar set KitapAdi='{txt_GuncelleKitapAdi.Text.Trim()}',TurId={turidDurumu.ToString()},YazarId={cmbBox_Guncelle_Yazar.SelectedValue},Stok={numericUpDown_Guncelle_Stok.Value},SayfaSayisi={numericUpDown_Guncelle_SayfaSayisi.Value} where KitapId={comboBoxKitapGuncelle.SelectedValue}";
                SqlCommand komut = new SqlCommand(updateSQLQuery, baglanti);
                BaglantiyiAc();
                int sonuc = komut.ExecuteNonQuery();
                if (sonuc > 0)
                {
                    DataRowView data = comboBoxKitapGuncelle.SelectedItem as DataRowView;
                    string eskiKitapAdi = data.Row.ItemArray[2].ToString();

                    MessageBox.Show($"{eskiKitapAdi} isimli kitaba ait bilgiler güncellendi...");
                    GuncelleSayfasindakiGroupBoxIciniTemizle();
                    comboBoxKitapGuncelle.SelectedIndex = -1;
                    TumKitaplariComboBoxaGetir();
                    TumKitaplariViewileGrideGetir();
                }
                else
                {
                    MessageBox.Show($"HATA: {comboBoxKitapGuncelle.SelectedItem}'a ait bilgiler güncellenemedi !");
                }
            }
        }

        private void TumKitaplariViewileGrideGetir()
        {
            try
            {
                SqlCommand komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                komut.CommandText = "select * from View_KitapYazarTur where SilindiMi=0";
                BaglantiyiAc();
                SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                DataTable sanalTablo = new DataTable();
                adaptor.Fill(sanalTablo);
                dataGridViewKitaplar.DataSource = sanalTablo;
                dataGridViewKitaplar.RowHeadersWidth = 150;
                dataGridViewKitaplar.Columns["SilindiMi"].Visible = false;
                BaglantiyiKapat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitaplar listelenirken beklenmedik bir hata oluştu" + ex.Message);
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

        private void comboBoxKitapGuncelle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Combodan neyi seçtiğimizi almamız gerekli.
            if (comboBoxKitapGuncelle.DataSource != null && comboBoxKitapGuncelle.SelectedIndex >= 0)
            {
                GuncelleSayfasiAktifTemizlikIslemiYap();
                int secilenYazarId = (int)comboBoxKitapGuncelle.SelectedValue;
                GuncelleSayfasindakiGroupBoxIciniTemizle();
                CombodanSecilenKitabinTumBilgileriniDoldur(secilenYazarId);
            }
            else
            {
                GuncelleSayfasiPasifTemizlikIslemiYap();
            }

        }

        private void GuncelleSayfasiAktifTemizlikIslemiYap()
        {
            groupBoxKitapGuncelle.Enabled = true;
            GuncelleSayfasindakiGroupBoxIciniTemizle();
        }

        private void GuncelleSayfasindakiGroupBoxIciniTemizle()
        {
            txt_GuncelleKitapAdi.Clear();
            numericUpDown_Guncelle_SayfaSayisi.Value = 0;
            numericUpDown_Guncelle_Stok.Value = 0;
            cmbBox_Guncelle_Yazar.SelectedIndex = -1;
            cmbBox_Guncelle_Yazar.Text = "Yazar seçiniz...";
            cmbBox_Guncelle_Tur.SelectedIndex = -1;
            cmbBox_Guncelle_Tur.Text = "Tür seçiniz...";
        }

        private void cmbBox_Sil_Kitap_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Seçili olan kitaba ait bilgileri richtext'e yazacağız 
            //dataset ile yapalım

            try
            {
                if (cmbBox_Sil_Kitap.SelectedIndex >= 0)
                {
                    using (baglanti)
                    {
                        SqlCommand komut = new SqlCommand($"select * from Kitaplar k left join Turler t on k.TurId=t.TurId left join Yazarlar y on k.YazarId=y.YazarId where KitapId={cmbBox_Sil_Kitap.SelectedValue}", baglanti);
                        SqlDataAdapter adaptor = new SqlDataAdapter(komut);
                        BaglantiyiAc();
                        DataSet dataSet = new DataSet();
                        adaptor.Fill(dataSet, "Kitaplar");
                        string kitapBilgileri = dataSet.Tables["Kitaplar"].Rows[0]["KitapAdi"].ToString()
                            + "\n" +
                             dataSet.Tables["Kitaplar"].Rows[0]["TurAdi"].ToString()
                             + "\n" + dataSet.Tables["Kitaplar"].Rows[0]["YazarAdSoyad"].ToString();
                        komut = new SqlCommand($"select * from View_KitabinIslemAdedi where KitapId={cmbBox_Sil_Kitap.SelectedValue}", baglanti);
                        object islemAdetBilgisi = komut.ExecuteScalar();
                        islemAdetBilgisi = islemAdetBilgisi == null ? 0 : islemAdetBilgisi;
                        kitapBilgileri += $" \n Bu kitabın {islemAdetBilgisi.ToString()} adet işlemi vardır";
                        richTextBoxKitap.Text = kitapBilgileri;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: Beklenmedik hata oluştu !" + ex.Message);
            }
        }
    }
}
