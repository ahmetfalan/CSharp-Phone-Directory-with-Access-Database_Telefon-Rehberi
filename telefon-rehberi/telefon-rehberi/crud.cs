using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// Summary description for Class1
/// </summary>
public class crud
{
	public crud()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static int mno;
    public OleDbConnection baglanti()
    {
        OleDbConnection baglan = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source= " + Application.StartupPath + "\\database.accdb");
        baglan.Open();
        return baglan;
    }
    //Baglanti metodu veritabanına bağlanılırken gerekli olan servis sağlayıcı ve veritabanı adını her seferinde yeniden yazma yükümlülüğünden bizi kurtarmıştır.

    public DataSet GetDataSet(string sqlCumle)
    {
        OleDbConnection baglanti = this.baglanti();
        OleDbDataAdapter adapter = new OleDbDataAdapter(sqlCumle, baglanti);
        DataSet ds = new DataSet();
        try
        {
            adapter.Fill(ds);
        }
        catch (OleDbException ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
        adapter.Dispose();
        baglanti.Close();
        return ds;
    }
    //GetDataSet metoduyla bağlanmış olduğumuz veritabanındaki bilgiler için geçici bir alan oluşturuyoruz ve ilgili dataset nesnesine gönderiyoruz.

    public DataTable GetDataTable(string sqlCumle)
    {
        OleDbConnection baglanti = this.baglanti();
        OleDbDataAdapter adapter = new OleDbDataAdapter(sqlCumle, baglanti);
        DataTable dt = new DataTable();
        try
        {
            adapter.Fill(dt);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }
        finally { }
        adapter.Dispose();
        baglanti.Close();
        return dt;
    }

    //GetDataTable metoduyla string sorgumuz sonucunda elde edilen verilerimizi Table nesnesine aktarıyoruz.

    public int cmd(string sqlCumle)
    {
        OleDbConnection baglanti = this.baglanti();
        OleDbCommand sorgu = new OleDbCommand(sqlCumle, baglanti);
        int sonuc = 0;
        try
        {
            sonuc = sorgu.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            sorgu.Connection.Close();
        }
        sorgu.Dispose();
        baglanti.Close();
        return (sonuc);
    }
    //Cmd metoduyla sorgu sonucunda etkilenen satır sayısını belirliyoruz .Bu metod  kullanıcı girişi yapılırken kullanıcı adi ve şifrenin kayıtlı olup olmadığını belirlerken kullanılmıştır.


    public String GetDataCell(string sqlCumle)
    {
        DataTable table = GetDataTable(sqlCumle);
        if (table.Rows.Count == 0)
            return null;
        return table.Rows[0][0].ToString();
    }
    //GetDataCell metoduyla nokta atışı hücre çekme işlemi yapıyoruz.

    public DataRow GetDataRow(string sqlCumle)
    {
        DataTable table = GetDataTable(sqlCumle);
        if (table.Rows.Count == 0) return null;
        return table.Rows[0];

    }
}