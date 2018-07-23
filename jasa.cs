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
    public partial class jasa : Form
    {
        koneksi conn = new koneksi();
        DataTable dtjasa = new DataTable();

        public jasa()
        {
            InitializeComponent();
            TampilJasa();
        }

        private void TampilJasa()
        {
            DataTable dtampil = new DataTable();
            string js = "select * from jasa";
            dtampil = conn.BukaTabel(js);
            grid_jasa.DataSource = dtampil;
        }

        private void jasa_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            this.Text = "Jasa - Tambah Data";
            txt_kd.Clear();
            txt_nm.Clear();
            txt_hg.Clear();

            dtjasa = conn.BukaTabel("select kdjasa from jasa");

            if (dtjasa.Rows.Count > 0)
            {
                string kdj = dtjasa.Rows[dtjasa.Rows.Count - 1][00].ToString();
                int js = int.Parse(kdj.Substring(4, 4)) + 1;
                kdj = "0000" + js.ToString();

                txt_kd.Text = "JASA" + kdj.Substring(kdj.Length - 4, 4);
                txt_kd.Enabled = false;
            }
            else {
                txt_kd.Text = "JASA0001";
            }
        }

        private void btn_cha_Click(object sender, EventArgs e)
        {
            this.Text = "Jasa - Edit Data";
            txt_nm.Focus();

            txt_kd.Text = grid_jasa.CurrentRow.Cells[0].Value.ToString();
            txt_nm.Text = grid_jasa.CurrentRow.Cells[1].Value.ToString();
            txt_hg.Text = grid_jasa.CurrentRow.Cells[2].Value.ToString();
        }

        private void btn_sav_Click(object sender, EventArgs e)
        {
            string SqlSimpan;

            if (txt_kd.Text == "")
            {
                MessageBox.Show("ID Jasa Tidak Boleh Kosong", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (this.Text == "Jasa - Tambah Data")
                {
                    SqlSimpan = "insert into jasa(kdjasa, jenisjasa, harga) " +
                        " values('" + txt_kd.Text + "' , '" + txt_nm.Text + "' , '" + txt_hg.Text + "') ";
                    conn.AksiQuery(SqlSimpan);
                    MessageBox.Show("Data Jasa Berhasil Disimpan");
                    TampilJasa();
                    this.Text = "jasa";
                }
                else if (this.Text == "Jasa - Edit Data")
                {
                    SqlSimpan = "update jasa set jenisjasa='" + txt_nm.Text + "', harga='" + txt_hg.Text + "' where kdjasa='" + txt_kd.Text + "'";
                    conn.AksiQuery(SqlSimpan);
                    MessageBox.Show("Edit Data Berhasil", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_kd.Clear();
                    txt_nm.Clear();
                    txt_hg.Clear();
                    TampilJasa();
                }
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grid_jasa.SelectedRows)
            {
                string kd = grid_jasa.Rows[grid_jasa.CurrentRow.Index].Cells[0].Value.ToString();
                string sqlHapus = "delete from jasa where kdjasa='" + kd + "'";

                conn.AksiQuery(sqlHapus);
                MessageBox.Show("Data berhasil dihapus.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            TampilJasa();
        }
    }
}
