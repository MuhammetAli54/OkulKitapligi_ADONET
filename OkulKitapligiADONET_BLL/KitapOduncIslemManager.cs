using OkulKitapligiADONET_BLL.ViewModels;
using OkulKitapligiADONET_DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulKitapligiADONET_BLL
{
    public class KitapOduncIslemManager
    {
        MyPocketDAL myPocketDAL = new MyPocketDAL("DESKTOP-TUMHS1A", "OKULKITAPLIGI", "", "");

        public DataTable TumKitaplariGetir()
        {
            try
            {
                DataTable data = new DataTable();
                data = myPocketDAL.GetTheData("Kitaplar", "*", "SilindiMi=0");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable TumOgrencileriGetir()
        {
            try
            {
                DataTable data = new DataTable();
                data = myPocketDAL.GetTheData("Ogrenciler", "OgrId, CONCAT(OgrAd, ' ' , OgrSoyad) as OgrenciAdSoyad, Cinsiyet, Sinif, DogumTarihi, SilindiMi", "SilindiMi=0");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GrideVerileriGetir()
        {
            try
            {
                DataTable data = new DataTable();
                data = myPocketDAL.GetTheData("select i.IslemId,i.KitapId,CONCAT(o.OgrAd,' ',o.OgrSoyad) as OgrenciAdSoyad,k.KitapAdi,i.OduncAldigiTarih,i.OduncBitisTarih from Islem i inner join kitaplar k on k.KitapId=i.KitapId inner join Ogrenciler o on o.OgrId=i.OgrId");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IslemViewModel> GrideVerileriViewModelleGetir()
        {
            List<IslemViewModel> data = new List<IslemViewModel>();
            try
            {
                DataTable theData = new DataTable();
                theData = myPocketDAL.GetTheData("select i.IslemId, k.KitapId, o.OgrId, CONCAT(o.OgrAd, ' ', o.OgrSoyad) as OgrenciAdSoyad, k.KitapAdi, i.OduncAldigiTarih, i.OduncBitisTarih, i.TeslimEdildiMi from  Islem i inner join Kitaplar k on k.KitapId = i.KitapId inner join Ogrenciler o on o.OgrId = i.OgrId");

                //veriler datatable ile geldi.
                //ama ben o verileri tek tek döngü ile dönerken içindeki verileri viewmodelime aktaracağım.
                for (int i = 0; i < theData.Rows.Count; i++)
                {
                    DataRow satir = theData.Rows[i];

                    IslemViewModel veri = new IslemViewModel()
                    {
                        IslemId = (int)theData.Rows[i].ItemArray[0],
                        KitapId = (int)theData.Rows[i].ItemArray[1],
                        OgrId = (int)theData.Rows[i].ItemArray[2],
                        OgrenciAdSoyad = theData.Rows[i].ItemArray[3].ToString(),
                        KitapAdi = theData.Rows[i].ItemArray[4].ToString(),
                        OduncAldigiTarih = Convert.ToDateTime(theData.Rows[i].ItemArray[5]),
                        OduncBitisTarih = Convert.ToDateTime(theData.Rows[i].ItemArray[6]),
                        TeslimEdildiMi = (bool)theData.Rows[i].ItemArray[7]
                    };
                    //IslemViewModel tipindeki veri isimli nesne IslemViewModel tipine sahip listeye eklenecek
                    data.Add(veri);  //ekledik.
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return data;
        }

        public int KitabınStogunuGetir(int kitapId)
        {
            try
            {
                int stokAdeti = 0;
                object data = myPocketDAL.GetTheDataByExecuteScalar($"select Stok from Kitaplar where KitapId={kitapId}");
                if (data != null)
                {
                    stokAdeti = Convert.ToInt32(data);
                }

                return stokAdeti;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool OduncKitapKaydiniYap(string tableName, Hashtable htVeri)
        {
            bool sonuc = false;
            try
            {
                //stok adet
                object stokAdeti = myPocketDAL.GetTheDataByExecuteScalar("select Stok from Kitaplar where KitapId=" + htVeri["KitapId"].ToString());
                if (stokAdeti != null)
                {
                    //Stoğu azaltacağız
                    stokAdeti = (int)stokAdeti - 1;
                    string updateCumlesi = "Update Kitaplar set Stok= " + stokAdeti + " where KitapId=" + htVeri["KitapId"];

                    //ödünç 
                    string insertCumlesi = myPocketDAL.CreateInsertQueryAsString(tableName, htVeri);
                    sonuc = myPocketDAL.ExecuteTheQueriesWithTransaction(insertCumlesi, updateCumlesi);
                }
                else
                {
                    throw new Exception("HATA: Stok adet bilgisi alınamadığı için hata oluştu !");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sonuc;
        }

        public bool OduncKitapteslimEt(string tableName, int islemId, int kitapId)
        {
            //

            try
            {
                bool sonuc = false;
                //stok
                object stokAdet = myPocketDAL.GetTheDataByExecuteScalar("select Stok from Kitaplar where KitapId=" + kitapId);
                if (stokAdet!=null)
                {
                    stokAdet = (int)stokAdet + 1;
                    string[] komutCumleleri = new string[2];
                    komutCumleleri[0] = "update Kitaplar Set Stok=" + stokAdet + " where KitapId=" + kitapId;

                    //createUpdateAsString'i kullanmak amacıyla hashtable yapacağız
                    //kullanmak istemezseniz 2. komut cümlesini yukarıdaki gibi ekleyebilirsiniz
                    komutCumleleri[1] = "update Islem Set TeslimEdildiMi=1,OduncBitisTarih='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where IslemId=" + islemId;

                    //Hashtable htVeri = new Hashtable();
                    //string bitisTarih = "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";

                    //htVeri.Add("TeslimEdildiMi", "1");
                    //htVeri.Add("OduncBitisTarih", bitisTarih);
                    //string kosulum = "IslemId=" + islemId;
                    //komutCumleleri[1] = myPocketDAL.CreateUpdateQueryAsString("Islem", htVeri,kosulum);
                    sonuc = myPocketDAL.ExecuteTheQueriesWithTransaction(komutCumleleri);
                }
                else
                {
                    throw new Exception("HATA: Stok adeti çekilemediği için hata oluştu!");
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
