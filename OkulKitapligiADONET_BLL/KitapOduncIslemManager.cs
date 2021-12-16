using OkulKitapligiADONET_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkulKitapligiADONET_BLL
{
    public class KitapOduncIslemManager
    {
        MyPocketDAL myPocketDAL = new MyPocketDAL("DESKTOP-TUMHS1A","OKULKITAPLIGI","","");

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
                data = myPocketDAL.GetTheData("select i.IslemId,CONCAT(o.OgrAd,' ',o.OgrSoyad) as OgrenciAdSoyad,k.KitapAdi,i.OduncAldigiTarih,i.OduncBitisTarih from Islem i inner join kitaplar k on k.KitapId=i.KitapId inner join Ogrenciler o on o.OgrId=i.OgrId");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int KitabınStogunuGetir(int kitapId)
        {
            try
            {
                int stokAdeti = 0;
                object data = myPocketDAL.GetTheDataByExecuteScalar($"select Stok from Kitaplar where KitapId={kitapId}");
                if (data!=null)
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
    }
}
