using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOKO_ABC
{
    public class storedUser
    {
        public static string nama;
    }
    public class storedSupplier
    {
        public static int id;
        public static string nama;
        public static string alamat;
        public static string noTelp;
    }

    public class storedPembeli
    {
        public static int id;
        public static string nama;
        public static string alamat;
        public static string noTelp;
    }

    public class storedBarang
    {
        public static int id;
        public static string nama;
        public static int harga;
        public static int supplierID;
        public static string jenisBarang;
        public static int jumlah;
        public static string keterangan;
        public static string noTelp;
    }

    public class storedPenjualan
    {
        public static int subTotalBarang;
        public static int quantity;
    }
}
