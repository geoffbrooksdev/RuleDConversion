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

        return ok;
    }

    static bool AddProduct(string productId)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PRODUCTS WHERE F_PRODUCT = @PID";
        oraTarget.Parameters.Add(productId);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();
     
        oraTarget.CommandText = @"INSERT INTO T_PRODUCTS (F_PRODUCT, F_PRODUCT_NAME)
                            VALUES (@PID, @PN )";
        oraTarget.Parameters.Add(productId);
        oraTarget.Parameters.Add($"{productId} - Test Product");
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();


        return ok;
    }

    static bool AddProductAlias(string productId)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PRODUCT_ALIAS_NAMES WHERE F_PRODUCT = @PID";
        oraTarget.Parameters.Add(productId);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();
      
        oraTarget.CommandText = @"INSERT INTO T_PRODUCT_ALIAS_NAMES (F_PRODUCT, F_ALIAS, F_ALIAS_NAME )
                            VALUES (@PID, @PID, @PN )";
        oraTarget.Parameters.Add(productId);
        oraTarget.Parameters.Add(productId);
        oraTarget.Parameters.Add($"{productId} - Test Product Alias");
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }


    static bool AddFormulation(string productId)
    {
        bool ok = false;

        oraTarget.CommandText = "DELETE FROM T_PROD_COMP WHERE F_PRODUCT = @PID";
        oraTarget.Parameters.Add(productId);
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();


        string sql = @"SELECT * FROM T_PROD_COMP WHERE F_PRODUCT = @PID";

        SqlCommand sqlCmd = new (sql, sqlConn);
        sqlCmd.Parameters.Add(productId);

        oraTarget.CommandText = @"INSERT INTO T_PROD_COMP (F_PRODUCT, F_CAS_NUMBER, F_COMPONENT_ID , 
                                    F_CHEM_NAME, F_TRADE_SECRET_CHEM_NAME, F_HAZ_FLAG, F_PERCENT, F_PERCENT_RANGE,
                                    F_UNITS, F_TRADE_SECRET_FLAG)
                                  VALUES (@PID, @CAS, @COMP, @CN, @TSN,@HAZ,@PCT,@PCTR, @UNI,@TSF )";

        SqlDataReader reader = sqlCmd.ExecuteReader();
        while (reader.Read())
        {



        }



        oraTarget.Parameters.Add(productId);
        oraTarget.Parameters.Add($"{productId} - Test Product Alias");
        oraConn.Open();
        oraTarget.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }





}
