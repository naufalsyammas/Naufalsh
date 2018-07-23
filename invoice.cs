using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace invoice
{
    public partial class invoice : Form
    {
        koneksi conn = new koneksi();
        DataTable dtinvoice = new DataTable();

        public invoice()
        {
            InitializeComponent();
            TampilInvoice();
            IsiCombo();
            ComboKayu();
            ComboJasa();
        }

        private void TampilInvoice()
        {
            DataTable dtampil = new DataTable();
            //string inv = "select * from invoice inner join detailinv on invoice.noinvoice=detailinv.noinvoice";
            string inv = "select a.noinvoice, a.tanggal, a.idcust, a.username, b.kdkayu, b.kdjasa, b.volume, b.tagihan from invoice a, detinvoice b where a.noinvoice=b.noinvoice";
            dtampil = conn.BukaTabel(inv);
            grid_invoice.DataSource = dtampil;
        }


        void IsiCombo()
        {
            MySqlConnection kon = new MySqlConnection(koneksi.strKoneksi);
            MySqlCommand cm = new MySqlCommand("select * from customer", kon);
            MySqlDataReader reader;

            try
            {
                kon.Open();
                reader = cm.ExecuteReader();

                while (reader.Read())
                {
                    string id = reader.GetString("idcust");
                    cb_idcust.Items.Add(id);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            kon.Close();
        }

        private void cb_idcust_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection kon = new MySqlConnection(koneksi.strKoneksi);
            MySqlCommand cm = new MySqlCommand("select * from customer where idcust='" + cb_idcust.Text + "'", kon);
            MySqlDataReader reader;

            try
            {
                kon.Open();
                reader = cm.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader.GetString("namacust");
                    txt_nacust.Text = name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             kon.Close();
        }

        void ComboKayu()
        {
            MySqlConnection kon = new MySqlConnection(koneksi.strKoneksi);
            MySqlCommand cn = new MySqlCommand("select * from kayu", kon);
            MySqlDataReader reader;

            try
            {
                kon.Open();
                reader = cn.ExecuteReader();

                while (reader.Read())
                {
                    string kode = reader.GetString("kdkayu");
                    cb_kayu.Items.Add(kode);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            kon.Close();
        }

        private void cb_kayu_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection kon = new MySqlConnection(koneksi.strKoneksi);
            MySqlCommand cn = new MySqlCommand("select * from kayu where kdkayu='" + cb_kayu.Text + "'", kon);
            MySqlDataReader reader;

            try
            {
                kon.Open();
                reader = cn.ExecuteReader();

                while (reader.Read())
                {
                    string jenis = reader.GetString("namakayu");
                    txt_nakayu.Text = jenis;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            kon.Close();
        }

        void ComboJasa()
        {
            MySqlConnection kon = new MySqlConnection(koneksi.strKoneksi);
            MySqlCommand ck = new MySqlCommand("select * from jasa", kon);
            MySqlDataReader reader;

            try
            {
                kon.Open();
                reader = ck.ExecuteReader();

                while (reader.Read())
                {
                    string nomer = reader.GetString("kdjasa");
                    cb_jasa.Items.Add(nomer);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            kon.Close();
        }

        private void cb_jasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection kon = new MySqlConnection(koneksi.strKoneksi);
            MySqlCommand ck = new MySqlCommand("select * from jasa where kdjasa='" + cb_jasa.Text + "'", kon);
            MySqlDataReader reader;

            try
            {
                kon.Open();
                reader = ck.ExecuteReader();

                while (reader.Read())
                {
                    string jenis = reader.GetString("jenisjasa");
                    string harga = reader.GetString("harga");
                    txt_jenis.Text = jenis;
                    txt_harga.Text = harga;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            kon.Close();
        }

        private void invoice_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            /*this.Text = "Invoice - Tambah Data";
            TampilDetail();

            DataTable dtdetail = new DataTable();
            string invo = "select noinvoice from detinvoice";
            dtdetail = conn.BukaTabel(invo);

            if (dtdetail.Rows.Count > 0)
            {
                string nin = dtdetail.Rows[dtdetail.Rows.Count - 1][0].ToString();
                int inv = int.Parse(nin.Substring(7, 3)) + 1;
                nin = "000" + inv.ToString();

                txt_noinv.Text = "INV" + DateTime.Now.Year + nin.Substring(nin.Length - 3, 3);
            }
            else
            {
                txt_noinv.Text = "INV2018001";
            }*/

            this.Text = "Invoice - Tambah Data";
            txt_noinv.Clear();
            cb_idcust.Text = null;
            txt_nacust.Clear();
            
            cb_kayu.Text = null; txt_nakayu.Clear();
            cb_jasa.Text = null; txt_jenis.Clear();
            txt_harga.Clear();
            txt_vol.Clear();
            txt_tag.Clear();

            dtinvoice = conn.BukaTabel("select noinvoice from invoice");

            if (dtinvoice.Rows.Count > 0)
            {
                string nin = dtinvoice.Rows[dtinvoice.Rows.Count - 1][0].ToString();
                int inv = int.Parse(nin.Substring(7, 3)) + 1;
                nin = "000" + inv.ToString();

                txt_noinv.Text = "INV" + DateTime.Now.Year + nin.Substring(nin.Length - 3, 3);
            }
            else {
                txt_noinv.Text = "INV2018001";
            }
        }

        private void btn_cha_Click(object sender, EventArgs e)
        {
            this.Text = "Invoice - Edit Data";
            txt_nacust.Focus();

            txt_noinv.Text = grid_invoice.CurrentRow.Cells[0].Value.ToString();
            pick_time.Text = grid_invoice.CurrentRow.Cells[1].Value.ToString();
            cb_idcust.Text = grid_invoice.CurrentRow.Cells[2].Value.ToString();
            cb_kayu.Text = grid_invoice.CurrentRow.Cells[4].Value.ToString();
            cb_jasa.Text = grid_invoice.CurrentRow.Cells[5].Value.ToString();
            txt_vol.Text = grid_invoice.CurrentRow.Cells[6].Value.ToString();
            txt_tag.Text = grid_invoice.CurrentRow.Cells[7].Value.ToString();
        }

        private void btn_sav_Click(object sender, EventArgs e)
        {
            /*menu mn = new menu();

            if (this.Text == "Invoice - Tambah data")
            {
                string transaksi1 = "insert into invoice(noinvoice, tanggal, idcust, username, totalvol, totaltag) " +
                        " values('" + txt_noinv.Text + "' , '" + pick_time.Text + "' , '" + cb_idcust.Text + "' , '" + koneksi.nmuser + "' , '" + txt_totalvol.Text + "' , '" + txt_totaltag.Text + "') ";
                conn.AksiQuery(transaksi1);
                string transaksi2 = "insert into detinvoice(noinvoice, kdkayu, kdjasa, volume, tagihan) " +
                        " values('" +txt_noinv.Text + "' , '" + cb_kayu.Text + "' , '" + cb_jasa.Text + "' , '" + txt_vol.Text + "' , '" + txt_tag.Text + "') ";
                conn.AksiQuery(transaksi2);
                MessageBox.Show("Simpan Transaksi Sukses.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Text = "Invoice";
                txt_noinv.Enabled = false;
                cb_idcust.Enabled = false;
                cb_kayu.Text = null;
                cb_jasa.Text = null;
                txt_vol.Clear(); txt_tag.Clear();
                TampilDetail();
                btn_add.Focus();
                this.Text = "Invoice - Tambah Data";

            }*/

            string SqlSimpan1;
            string SqlSimpan2;
            menu mn = new menu();


            if (txt_noinv.Text == "")
            {
                MessageBox.Show("No Invoice Tidak Boleh Kosong", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (this.Text == "Invoice - Tambah Data")
                {
                    SqlSimpan1 = "insert into invoice(noinvoice, tanggal, idcust, username) " +
                        " values('" + txt_noinv.Text + "' , '" + pick_time.Text + "' , '" + cb_idcust.Text + "' , '" + koneksi.nmuser + "') ";
                    conn.AksiQuery(SqlSimpan1);
                    SqlSimpan2 = "insert into detinvoice(noinvoice, kdkayu, kdjasa, volume, tagihan) " +
                        " values('" +txt_noinv.Text + "' , '" + cb_kayu.Text + "' , '" + cb_jasa.Text + "' , '" + txt_vol.Text + "' , '" + txt_tag.Text + "') ";
                    conn.AksiQuery(SqlSimpan2);
                    MessageBox.Show("Data Invoice Berhasil Disimpan");
                    TampilInvoice();
                    this.Text = "invoice";
                }
                else if (this.Text == "Invoice - Edit Jasa")
                {
                    string edit = "update invoice set tanggal='" + pick_time.Text + "', idcust='" + cb_idcust.Text + "' where noinvoice='" + txt_noinv.Text + "'";
                    conn.AksiQuery(edit);
                    string edit1 = "update detinvoice set kdkayu='" + cb_kayu.Text + "', kdjasa='" + cb_jasa.Text + "', volume='" + txt_vol.Text + "', tagihan='" + txt_tag.Text + "' where detinvoice='" + txt_noinv.Text + "'";
                    conn.AksiQuery(edit1);
                    MessageBox.Show("Edit Data Berhasil", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_noinv.Clear();
                    pick_time.Text = null;
                    cb_idcust.Text = null; txt_nacust.Clear();

                    cb_kayu.Text = null; txt_nakayu.Clear();
                    cb_jasa.Text = null; txt_jenis.Clear();
                    txt_harga.Clear();
                    txt_vol.Clear();
                    TampilInvoice();
                }
            }
        }

        private void txt_tag_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_tag_KeyDown(object sender, KeyEventArgs e)
        {
            txt_tag.Text = (Convert.ToInt32(txt_harga.Text) * Convert.ToDecimal(txt_vol.Text)).ToString();

/*            if (e.KeyCode == Keys.Enter)
            {
                int hrg;
                int vol;

                hrg = int.Parse(txt_harga.Text);
                vol = int.Parse(txt_vol.Text);

                txt_tag.Text = (hrg * vol).ToString();
            } */
        }
    }
}