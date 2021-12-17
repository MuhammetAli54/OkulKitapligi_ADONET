using OkulKitapligiADONET_BLL;
using OkulKitapligiADONET_BLL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulKitapligi_ADONET
{
    public partial class FormKitapOduncIslemleri : Form
    {
        public FormKitapOduncIslemleri()
        {
            InitializeComponent();
        }
        //Global alan
        KitapOduncIslemManager kitapOduncIslemManager = new KitapOduncIslemManager();

        private void FormKitapOduncIslemleri_Load(object sender, EventArgs e)
        {
            TumOgrencileriComboyaGetir();
            OgrenciGroupBoxTemizle();
            KitapGroupBoxPasifYap();
            OduncTarihGroupBoxPasifYap();
            //datetimepicker ayarlarını burada yapalım
            dateTimePickerBaslangic.Format = DateTimePickerFormat.Custom;
            dateTimePickerBaslangic.CustomFormat = "dd.MM.yyyy ";
            dateTimePickerBaslangic.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dateTimePickerBitis.Format = DateTimePickerFormat.Custom;
            dateTimePickerBitis.CustomFormat = "dd.MM.yyyy ";
            dateTimePickerBitis.MinDate = dateTimePickerBaslangic.Value.AddDays(1);
            dateTimePickerBitis.MaxDate = dateTimePickerBaslangic.Value.AddMonths(3);

            //datagridi dolduralım
            GridViewiAyarlaveDoldur();

            UC_MyButtonOduncAl.myButton.Text = "ÖDÜNÇ AL";
            UC_MyButtonOduncAl.myButton.Click += new EventHandler(btn_OduncKitapAl);

        }

        private void btn_OduncKitapAl(object sender, EventArgs e)
        {
            try
            {
                bool kontrol = false;
                //tarihleri kontrol et
                if (dateTimePickerBitis.Value < dateTimePickerBaslangic.Value)
                {
                    MessageBox.Show("HATA: Tarih bilgilerinde yanlış giriş yaptınız!");
                }
                else
                {
                    if (comboBoxOgrenci.SelectedIndex > -1)
                    {
                        if (comboBoxKitap.SelectedIndex > -1)
                        {
                            //kitap stoğu var mı?
                            kontrol = kitapOduncIslemManager.KitabınStogunuGetir((int)comboBoxKitap.SelectedValue) == 0 ? false : true;
                        }
                        else
                        {
                            MessageBox.Show("HATA: Kitap seçmeden işlem yapılamaz!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("HATA: Öğrenci seçmeden işlem yapılamaz!");
                    }
                }

                if (kontrol)
                {
                    string baslangicTarihi = "'" + dateTimePickerBaslangic.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    string bitisTarihi = "'" + dateTimePickerBitis.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    Hashtable htVeri = new Hashtable();
                    htVeri.Add("OgrId", (int)comboBoxOgrenci.SelectedValue);
                    htVeri.Add("KitapId", (int)comboBoxKitap.SelectedValue);
                    htVeri.Add("OduncAldigiTarih", baslangicTarihi);
                    htVeri.Add("OduncBitisTarih", bitisTarihi);
                    if (kitapOduncIslemManager.OduncKitapKaydiniYap("Islem",htVeri))
                    {
                        MessageBox.Show("Ödünç alma işleminiz başarıyla kaydedilmiştir...");
                        //temizlik
                        GridViewiAyarlaveDoldur();
                        OgrenciGroupBoxTemizle();
                        KitapGroupBoxPasifYap();
                        OduncTarihGroupBoxPasifYap();
                    }
                    else
                    {
                        MessageBox.Show("HATA: Kayıt yenilenirken beklenmedik bir hata oluştu !");
                    }
                }
                else
                {
                    MessageBox.Show("HATA: Kitap stokta olmadığı için ödünç alamazsınız...");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Beklenmedik hata oluştu!" + ex.Message);
            }
        }

        private void GridViewiAyarlaveDoldur()
        {
            dataGridViewOduncKitaplar.MultiSelect = false;
            dataGridViewOduncKitaplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridViewOduncKitaplar.DataSource = kitapOduncIslemManager.GrideVerileriGetir();
            //dataGridViewOduncKitaplar.Columns["IslemId"].Visible = false;
            //dataGridViewOduncKitaplar.Columns["KitapId"].Visible = false;

            //datagridview'in datasource'una veriler viewmodel ile geldi.
            List<IslemViewModel> list = kitapOduncIslemManager.GrideVerileriViewModelleGetir();
            dataGridViewOduncKitaplar.DataSource = list;
            dataGridViewOduncKitaplar.Columns["IslemId"].Visible = false;
            dataGridViewOduncKitaplar.Columns["KitapId"].Visible = false;
            dataGridViewOduncKitaplar.Columns["OgrId"].Visible = false;
            dataGridViewOduncKitaplar.Columns["TeslimEdildiMi"].Visible = false;
            dataGridViewOduncKitaplar.Columns["TeslimEdildiMiString"].HeaderText = "Teslim Durumu";
            dataGridViewOduncKitaplar.Columns["OduncAldigiTarih"].HeaderText = "Ödünç Baş. Tarih";
            dataGridViewOduncKitaplar.Columns["OduncBitisTarih"].HeaderText = "Ödünç Bitiş Tarih";
            dataGridViewOduncKitaplar.Columns["OgrenciAdSoyad"].HeaderText = "Öğrenci Ad Soyad";


            for (int i = 0; i < dataGridViewOduncKitaplar.Columns.Count; i++)
            {
                dataGridViewOduncKitaplar.Columns[i].Width = 130;
            }
            dataGridViewOduncKitaplar.ContextMenuStrip = contextMenuStrip1;
        }

        private void TumKitaplariComboyaGetir()
        {
            comboBoxKitap.DisplayMember = "KitapAdi";
            comboBoxKitap.ValueMember = "KitapId";
            comboBoxKitap.DataSource = kitapOduncIslemManager.TumKitaplariGetir();
        }

        private void TumOgrencileriComboyaGetir()
        {
            comboBoxOgrenci.DisplayMember = "OgrenciAdSoyad";
            comboBoxOgrenci.ValueMember = "OgrId";
            comboBoxOgrenci.DataSource = kitapOduncIslemManager.TumOgrencileriGetir();
        }

        private void KitapGroupBoxPasifYap()
        {
            comboBoxKitap.SelectedIndex = -1;
            groupBoxKitap.Enabled = false;
        }

        private void OduncTarihGroupBoxPasifYap()
        {
            dateTimePickerBaslangic.Value = DateTime.Now;
            dateTimePickerBitis.Value = DateTime.Now.AddDays(1);
            groupBoxOduncTarihler.Enabled = false;
        }

        private void OgrenciGroupBoxTemizle()
        {
            comboBoxOgrenci.SelectedIndex = -1;
        }

        private void comboBoxOgrenci_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOgrenci.SelectedIndex > -1)
            {
                //aktif 
                KitapGroupBoxAktifYap();
                TumKitaplariComboyaGetir();
                comboBoxKitap.SelectedIndex = -1;
            }
        }

        private void KitapGroupBoxAktifYap()
        {
            groupBoxKitap.Enabled = true;
        }

        private void OduncTarihGroupBoxAktifYap()
        {
            dateTimePickerBaslangic.Value = DateTime.Now;
            dateTimePickerBitis.Value = DateTime.Now.AddDays(1);
            groupBoxOduncTarihler.Enabled = true;
        }

        private void comboBoxKitap_SelectedIndexChanged(object sender, EventArgs e)
        {
            //groupbox tarih alanı aktifleşecek.
            if (comboBoxKitap.SelectedIndex>-1)
            {
                OduncTarihGroupBoxAktifYap();
            }
            else
            {
                OduncTarihGroupBoxPasifYap();
            }
       
        }

        private void dateTimePickerBaslangic_ValueChanged(object sender, EventArgs e)
        {
            //burada seçilecek tarih bitişi etkileyecek
            dateTimePickerBitis.MinDate = dateTimePickerBaslangic.Value.AddDays(1);
            dateTimePickerBitis.Value = dateTimePickerBaslangic.Value.AddDays(1);
            dateTimePickerBitis.MaxDate = dateTimePickerBaslangic.Value.AddMonths(3);
        }

        private void kitabiTeslimEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //veri tabanına aşağıdaki sorgu ile yeni alan eklendi
            //alter table Islem add TesliEdildiMi bit not null default 0
            try
            {
                //datagridview'de seçilen satır alınacak
                //o satırdaki islemId ve kitapId alınacak
                DataGridViewRow secilenSatir = dataGridViewOduncKitaplar.SelectedRows[0];
                //islemId
                int islemId = (int)secilenSatir.Cells["IslemId"].Value;
                //kitapId
                int kitapId = (int)secilenSatir.Cells["KitapId"].Value;

                bool teslimSonuc = false;
                teslimSonuc = kitapOduncIslemManager.OduncKitapteslimEt("Islem", islemId, kitapId);
                if (teslimSonuc)
                {
                    MessageBox.Show("Teşekkürler... Kitap teslim alındı...");
                    //temizlik
                    GridViewiAyarlaveDoldur();
                    OgrenciGroupBoxTemizle();
                    KitapGroupBoxPasifYap();
                    OduncTarihGroupBoxPasifYap();
                }
                else
                {
                    MessageBox.Show("HATA: Kitap teslim edilemedi!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: Bir hata oluştu!" + ex.Message + " " + ex.ToString());
            }

        }
    }
}
