namespace RuleDConversion;

internal static class ProductTesting
{
    //internal static string OracleConnString;
    //internal static string SqlConnString;

    internal static SqlConnection sqlConn = new();
    internal static OracleConnection oraConn = new();

    internal static SqlCommand sqlSource = new();
    internal static OracleCommand oraTarget = new();


    static ProductTesting()
    {
        SqlConnectionStringBuilder sscb = Utils.GetSqlServerConnection();

        OracleConnectionStringBuilder ocsb = Utils.GetOracleConnection();

        oraConn = new()
        {
            ConnectionString = ocsb.ConnectionString
        };

        sqlConn = new()
        {
            ConnectionString = sscb.ConnectionString
        };
    }

    internal static bool CopyProduct(string productId)
    {

        //just create the minimum data needed for RULE D Processing...
        var ok = AddProduct(productId);
        ok = AddProductAlias(productId);
        ok = AddFormulation(productId);
        ok = AddProductRelatedData(productId);

        return ok;
    }


    static bool AddProductRelatedData(string productid)
    {
        bool ok = AddProductText(productid);
        ok = AddProductData(productid);

        ok = AddFormulaText(productid);
        ok = AddFormulaData(productid);

        return ok;
    }

    static bool AddComponentRelatedData(string cas, string compid)
    {
        bool ok = AddComponentData(cas,compid);
       

        return ok;
    }

    static bool AddProductText(string productid)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PROD_TEXT WHERE F_PRODUCT = :PID";
        oraTarget.Parameters.Add("PID", productid);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        oraTarget.CommandText = @"INSERT INTO T_PROD_TEXT (F_PRODUCT, F_FORMAT, F_DATA_CODE, F_TEXT_CODE)
                            VALUES (:PID,:FMT,:DC,:TC)";
        oraTarget.Parameters.Add("PID");
        oraTarget.Parameters.Add("FMT");
        oraTarget.Parameters.Add("DC");
        oraTarget.Parameters.Add("TC");

        string sql = @"SELECT * FROM T_PROD_TEXT WHERE F_PRODUCT = @PID";

        SqlCommand sqlCmd = new(sql, sqlConn);
        sqlCmd.Parameters.Add("PID", SqlDbType.VarChar, 200).Value = productid;

        sqlConn.Open();
        SqlDataReader reader = sqlCmd.ExecuteReader();
        while (reader.Read())
        {
            string format = reader["F_FORMAT"] as string;
            string datacode = reader["F_DATA_CODE"] as string;
            string textcode = reader["F_TEXT_CODE"] as string;

            oraTarget.Parameters["PID"].Value = productid;
            oraTarget.Parameters["FMT"].Value = format;
            oraTarget.Parameters["DC"].Value = datacode;
            oraTarget.Parameters["TC"].Value = textcode;

            oraConn.Open();
            oraTarget.ExecuteNonQuery();
            oraConn.Close();
        }
        reader.Close();
        sqlConn.Close();

        return ok;
    }

    static bool AddProductData(string productid)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PROD_DATA WHERE F_PRODUCT = :PID";
        oraTarget.Parameters.Add("PID", productid);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        oraTarget.CommandText = @"INSERT INTO T_PROD_DATA (F_PRODUCT, F_DATA_CODE, F_DATA)
                            VALUES (:PID,:DC,:DAT)";
        oraTarget.Parameters.Add("PID");
        oraTarget.Parameters.Add("DC");
        oraTarget.Parameters.Add("DAT");

        string sql = @"SELECT * FROM T_PROD_DATA WHERE F_PRODUCT = @PID";

        SqlCommand sqlCmd = new(sql, sqlConn);
        sqlCmd.Parameters.Add("PID", SqlDbType.VarChar, 200).Value = productid;

        sqlConn.Open();
        SqlDataReader reader = sqlCmd.ExecuteReader();
        while (reader.Read())
        {
            string datacode = reader["F_DATA_CODE"] as string;
            string data = reader["F_DATA"] as string;

            oraTarget.Parameters["PID"].Value = productid;
            oraTarget.Parameters["DC"].Value = datacode;
            oraTarget.Parameters["DAT"].Value = data;

            oraConn.Open();
            oraTarget.ExecuteNonQuery();
            oraConn.Close();
        }
        reader.Close();
        sqlConn.Close();

        return ok;
    }

    static bool AddFormulaText(string productid)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_FORM_TEXT WHERE F_PRODUCT = :PID";
        oraTarget.Parameters.Add("PID", productid);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        oraTarget.CommandText = @"INSERT INTO T_FORM_TEXT (F_PRODUCT, F_CAS_NUMBER, F_COMPONENT_ID, F_FORMAT, F_DATA_CODE, F_TEXT_CODE)
                            VALUES (:PID, :CAS,:COMP,:FMT,:DC,:TC)";
        oraTarget.Parameters.Add("PID");
        oraTarget.Parameters.Add("CAS");
        oraTarget.Parameters.Add("COMP");
        oraTarget.Parameters.Add("FMT");
        oraTarget.Parameters.Add("DC");
        oraTarget.Parameters.Add("TC");

        string sql = @"SELECT * FROM T_FORM_TEXT WHERE F_PRODUCT = @PID";

        SqlCommand sqlCmd = new(sql, sqlConn);
        sqlCmd.Parameters.Add("PID", SqlDbType.VarChar, 200).Value = productid;

        sqlConn.Open();
        SqlDataReader reader = sqlCmd.ExecuteReader();
        while (reader.Read())
        {
            string cas = reader["F_CAS_NUMBER"] as string;
            string compid = reader["F_COMPONENT_ID"] as string;
            string format = reader["F_FORMAT"] as string;
            string datacode = reader["F_DATA_CODE"] as string;
            string textcode = reader["F_TEXT_CODE"] as string;

            oraTarget.Parameters["PID"].Value = productid;
            oraTarget.Parameters["CAS"].Value = cas;
            oraTarget.Parameters["COMP"].Value = compid;
            oraTarget.Parameters["FMT"].Value = format;
            oraTarget.Parameters["DC"].Value = datacode;
            oraTarget.Parameters["TC"].Value = textcode;

            oraConn.Open();
            oraTarget.ExecuteNonQuery();
            oraConn.Close();
        }
        reader.Close();
        sqlConn.Close();

        return ok;
    }

    static bool AddFormulaData(string productid)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_FORM_DATA WHERE F_PRODUCT = :PID";
        oraTarget.Parameters.Add("PID", productid);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        oraTarget.CommandText = @"INSERT INTO T_FORM_DATA (F_PRODUCT, F_CAS_NUMBER, F_COMPONENT_ID, F_DATA_CODE, F_DATA)
                            VALUES (:PID,:CAS,:COMP,:DC,:DAT)";
        oraTarget.Parameters.Add("PID");
        oraTarget.Parameters.Add("CAS");
        oraTarget.Parameters.Add("COMP");
        oraTarget.Parameters.Add("DC");
        oraTarget.Parameters.Add("DAT");

        string sql = @"SELECT * FROM T_FORM_DATA WHERE F_PRODUCT = @PID";

        SqlCommand sqlCmd = new(sql, sqlConn);
        sqlCmd.Parameters.Add("PID", SqlDbType.VarChar, 200).Value = productid;

        sqlConn.Open();
        SqlDataReader reader = sqlCmd.ExecuteReader();
        while (reader.Read())
        {
            string cas = reader["F_CAS_NUMBER"] as string;
            string compid = reader["F_COMPONENT_ID"] as string;
            string datacode = reader["F_DATA_CODE"] as string;
            string data = reader["F_DATA"] as string;

            oraTarget.Parameters["PID"].Value = productid;
            oraTarget.Parameters["CAS"].Value = cas;
            oraTarget.Parameters["COMP"].Value = compid;
            oraTarget.Parameters["DC"].Value = datacode;
            oraTarget.Parameters["DAT"].Value = data;

            oraConn.Open();
            oraTarget.ExecuteNonQuery();
            oraConn.Close();
        }
        reader.Close();
        sqlConn.Close();

        return ok;
    }

    static bool AddComponentData(string cas, string compid)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_COMP_DATA WHERE F_CAS_NUMBER = :CAS AND F_COMPONENT_ID = :COMP";
        oraTarget.Parameters.Add("CAS", cas);
        oraTarget.Parameters.Add("COMP", compid);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        oraTarget.CommandText = @"INSERT INTO T_COMP_DATA (F_CAS_NUMBER, F_COMPONENT_ID, F_DATA_CODE, F_DATA)
                            VALUES (:CAS,:COMP,:DC,:DAT)";

        oraTarget.Parameters.Add("CAS");
        oraTarget.Parameters.Add("COMP");
        oraTarget.Parameters.Add("DC");
        oraTarget.Parameters.Add("DAT");

        string sql = @"SELECT * FROM T_COMP_DATA WHERE F_CAS_NUMBER = :CAS AND F_COMPONENT_ID = :COMP";

        SqlCommand sqlCmd = new(sql, sqlConn);
        sqlCmd.Parameters.Add("CAS", SqlDbType.VarChar, 15).Value = cas;
        sqlCmd.Parameters.Add("COMP", SqlDbType.VarChar, 35).Value = compid;

        sqlConn.Open();
        SqlDataReader reader = sqlCmd.ExecuteReader();
        while (reader.Read())
        {           
            string datacode = reader["F_DATA_CODE"] as string;
            string data = reader["F_DATA"] as string;

            oraTarget.Parameters["CAS"].Value = cas;
            oraTarget.Parameters["COMP"].Value = compid;
            oraTarget.Parameters["DC"].Value = datacode;
            oraTarget.Parameters["DAT"].Value = data;

            oraConn.Open();
            oraTarget.ExecuteNonQuery();
            oraConn.Close();
        }
        reader.Close();
        sqlConn.Close();

        return ok;
    }

    static bool AddProduct(string productId)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PRODUCTS WHERE F_PRODUCT = @PID";
        oraTarget.Parameters.Add("PID", productId);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();
     
        oraTarget.CommandText = @"INSERT INTO T_PRODUCTS (F_PRODUCT, F_PRODUCT_NAME)
                            VALUES (:PID,:PN )";
        oraTarget.Parameters.Add("PID",productId);
        oraTarget.Parameters.Add("PN", $"{productId} - Test Product");
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }

    static bool AddProductAlias(string productId)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PRODUCT_ALIAS_NAMES WHERE F_PRODUCT = @PID";
        oraTarget.Parameters.Add("PID",productId);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();
      
        oraTarget.CommandText = @"INSERT INTO T_PRODUCT_ALIAS_NAMES (F_PRODUCT, F_ALIAS, F_ALIAS_NAME )
                            VALUES (:PID,:AID,:PN )";
        oraTarget.Parameters.Add("PID",productId);
        oraTarget.Parameters.Add("AID", productId);
        oraTarget.Parameters.Add("PN", $"{productId} - Test Product Alias");
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }

    static bool AddComponent(string cas, string compid, string chemname, string tsname)
    {
        bool ok = false;

        oraTarget.CommandText = @"INSERT INTO T_COMPONENTS (F_CAS_NUMBER, F_COMPONENT_ID, F_CHEM_NAME, F_TRADE_SECRET_NAME )
                            VALUES (:CAS,:COMP,:CN,:TSN )";
        oraTarget.Parameters.Add("CAS",cas);
        oraTarget.Parameters.Add("COMPID",compid);
        oraTarget.Parameters.Add("CN",chemname);
        oraTarget.Parameters.Add("TSN",tsname);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        AddComponentAlias(cas, compid, chemname, tsname);

        return ok;
    }

    static bool AddComponentAlias(string cas, string compid, string chemname, string tsname)
    {
        bool ok = false;

        oraTarget.CommandText = @"INSERT INTO T_COMPONENTS_ALIAS (F_LANGUAGE, F_CAS_NUMBER, F_COMPONENT_ID, F_CHEM_NAME, F_TRADE_SECRET_NAME )
                            VALUES ('EN',:CAS,:COMP,:CN,:TSN )";
        oraTarget.Parameters.Add("CAS", cas);
        oraTarget.Parameters.Add("COMPID", compid);
        oraTarget.Parameters.Add("CN", chemname);
        oraTarget.Parameters.Add("TSN", tsname);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }

    static bool AddComponentToFormula(string productid, string cas, string compid, string chemname, string tsname, double percent, string percentrange, string units, int hazflag, int tsflag)
    {
        bool ok = false;

        oraTarget.CommandText = @"INSERT INTO T_PROD_COMP (F_PRODUCT, F_CAS_NUMBER, F_COMPONENT_ID , 
                                    F_CHEM_NAME, F_TRADE_SECRET_CHEM_NAME, F_HAZ_FLAG, F_PERCENT, F_PERCENT_RANGE,
                                    F_UNITS, F_TRADE_SECRET_FLAG)
                                  VALUES (:PID,:CAS,:COMP,:CN,:TSN,:HAZ,:PCT,:PCTR,:UNI,:TSF)";

        oraTarget.Parameters.Add("PID", productid);
        oraTarget.Parameters.Add("CAS", cas);
        oraTarget.Parameters.Add("COMP", compid);
        oraTarget.Parameters.Add("CN", chemname);
        oraTarget.Parameters.Add("TSN", tsname);
        oraTarget.Parameters.Add("HAZ", hazflag);
        oraTarget.Parameters.Add("PCT", percent);
        oraTarget.Parameters.Add("PCTR", percentrange);
        oraTarget.Parameters.Add("UNI", units);
        oraTarget.Parameters.Add("TSF", tsflag);

        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        AddComponentRelatedData(cas, compid);

        return ok;
    }

    static bool DoesComponentExist(string cas, string compid)
    {
        string sql = @"SELECT 1 FROM T_COMPONENTS WHERE F_CAS_NUMBER = :CAS AND F_COMPONENT_ID = :COMP";
        OracleCommand oraCmd = new(sql, oraConn);
        oraCmd.Parameters.Add("CAS",cas);
        oraCmd.Parameters.Add("COMP",compid);

        oraConn.Open();
        int x = Convert.ToInt16(oraCmd.ExecuteScalar());
        oraConn.Close();

        return (x > 0);
    }


    static bool AddFormulation(string productid)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PROD_COMP WHERE F_PRODUCT = :PID";
        oraTarget.Parameters.Add("PID",productid);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        string sql = @"SELECT * FROM T_PROD_COMP WHERE F_PRODUCT = @PID";

        SqlCommand sqlCmd = new (sql, sqlConn);
        sqlCmd.Parameters.Add("PID",SqlDbType.VarChar,200).Value = productid;

        sqlConn.Open();
        SqlDataReader reader = sqlCmd.ExecuteReader();
        while (reader.Read())
        {
            string cas = reader["F_CAS_NUMBER"] as string;
            string compid = reader["F_COMPONENT_ID"] as string;
            string chemname = reader["F_CHEM_NAME"] as string;
            string tsname = reader["F_TRADE_SECRET_NAME"] as string;
            double percent = reader["F_PERCENT"] as double? ?? default;
            string percentrange = reader["F_PERCENT_RANGE"] as string;
            string units = reader["F_UNITS"] as string;
            int hazflag = reader["F_HAZ_FLAG"] as int?  ?? default;
            int tsflag = reader["F_TRADE_SECRET_FLAG"] as int? ?? default;

            if (!DoesComponentExist(cas, compid))
            {
                AddComponent(cas, compid, chemname, tsname);
            }

            AddComponentToFormula(productid, cas, compid, chemname, tsname, percent, percentrange, units, hazflag, tsflag);    
        }
        reader.Close();
        sqlConn.Close();

        oraTarget.Parameters.Add(productid);
        oraTarget.Parameters.Add($"{productid} - Test Product Alias");
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }





}
