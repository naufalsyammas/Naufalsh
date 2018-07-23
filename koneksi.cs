using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace invoice
{
    class koneksi
    {
        public static string strKoneksi = ConfigurationManager.AppSettings["Database"];
        MySqlConnection sambung = null;
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        public static string nmuser = "";
       
        public DataTable BukaTabel(string isiSql)
        {
            sambung = new MySqlConnection(strKoneksi);
            try
            {
                sambung.Open();
                cmd = new MySqlCommand(isiSql, sambung);
                //cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
            }
            catch
            {
                PesanKesalahan("Koneksi database gagal..");
            }
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            sambung.Close();
            return dt;
        }

        public bool AksiQuery(string strSql)
        {
            sambung = new MySqlConnection(strKoneksi);
            sambung.Open();
            cmd = new MySqlCommand(strSql, sambung);
            cmd.ExecuteNonQuery();
            sambung.Close();

            return true;
        }

        public string ImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //convert image menjadi byte
                image.Save(ms, image.RawFormat);
                byte[] imageByte = ms.ToArray();

                //convert byte menjadi base64string
                string base64String = Convert.ToBase64String(imageByte);
                return base64String;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
            imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public void PesanKesalahan(string judul)
        {
            MessageBox.Show(judul, "Error 101", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PesanBenar(string judul)
        {
            MessageBox.Show(judul, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
