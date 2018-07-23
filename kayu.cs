using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace invoice
{
    public partial class kayu : Form
    {
        koneksi conn = new koneksi();
        DataTable dtkayu = new DataTable();

        public kayu()
        {
            InitializeComponent();
            TampilKayu();
        }

        private void TampilKayu()
        {
            DataTable dtampil = new DataTable();
            string wood = "select * from kayu";
            dtampil = conn.BukaTabel(wood);
            grid_kayu.DataSource = dtampil;
        }

        private void kayu_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            this.Text = "Kayu - Tambah Data";
            txt_kd.Clear();
            txt_jenis.Clear();
            txt_ket.Clear();

            dtkayu = conn.BukaTabel("select kdkayu from kayu");

            if (dtkayu.Rows.Count > 0)
            {
                string kdk = dtkayu.Rows[dtkayu.Rows.Count - 1][00].ToString();
                int ky = int.Parse(kdk.Substring(4, 4)) + 1;
                kdk = "0000" + ky.ToString();

                txt_kd.Text = "WOOD" + kdk.Substring(kdk.Length - 4, 4);
                txt_kd.Enabled = false;
            }
            else {
                txt_kd.Text = "WOOD0001";
            }
        }

        private void btn_cha_Click(object sender, EventArgs e)
        {
            this.Text = "Kayu - Edit Data";
            txt_jenis.Focus();

            txt_kd.Text = grid_kayu.CurrentRow.Cells[0].Value.ToString();
            txt_jenis.Text = grid_kayu.CurrentRow.Cells[1].Value.ToString();
            txt_ket.Text = grid_kayu.CurrentRow.Cells[2].Value.ToString();
        }

        private void btn_sav_Click(object sender, EventArgs e)
        {
            string SqlSimpan;

            if (txt_kd.Text == "")
            {
                MessageBox.Show("ID Kayu Tidak Boleh Kosong.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (this.Text == "Kayu - Tambah Data")
                {
                    SqlSimpan = "insert into kayu(kdkayu, namakayu, keterangan) " +
                        " values('" + txt_kd.Text + "' , '" + txt_jenis.Text + "' , '" + txt_ket.Text + "') ";
                    conn.AksiQuery(SqlSimpan);
                    MessageBox.Show("Data Kayu Berhasil Disimpan.");
                    TampilKayu();
                    this.Text = "kayu";
                }
                else if (this.Text == "Kayu - Edit Data")
                {
                    SqlSimpan = "update kayu set namakayu='" + txt_jenis.Text + "', keterangan='" + txt_ket.Text + "' where kdkayu='" + txt_kd.Text + "'";
                    conn.AksiQuery(SqlSimpan);
                    MessageBox.Show("Edit Data Berhasil", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_kd.Clear();
                    txt_jenis.Clear();
                    txt_ket.Clear();
                    TampilKayu();
                }
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grid_kayu.SelectedRows)
            {
                string hap = grid_kayu.Rows[grid_kayu.CurrentRow.Index].Cells[0].Value.ToString();
                string sqlHapus = "delete from kayu where kdkayu='" + hap + "'";

                conn.AksiQuery(sqlHapus);
                MessageBox.Show("Data Berhasil Dihapus.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            TampilKayu();
        }


    }
}
