namespace RuleManager;
internal static class Utils
{
    internal static string FormatQueryText(string queryToRun)
    {
        if (!string.IsNullOrEmpty(queryToRun))
        {
            if (queryToRun.StartsWith("UD_RUNSQLD('"))
            {
                queryToRun = queryToRun.Replace("UD_RUNSQLD('", "");
                queryToRun = queryToRun.Substring(0, queryToRun.Length - 2).Trim();
            }
        }

        return queryToRun;
    }

    internal static SqlConnectionStringBuilder GetSqlServerConnection()
    {
        return new()
        {
            DataSource = "localhost",    //"USENKMSQL013D.global.ul.com",
            InitialCatalog = "GoldenStaging",
            IntegratedSecurity = true
        };
    }

    internal static OracleConnectionStringBuilder GetOracleConnection()
    {

        //Golden
        //return new()
        //{
        //	DataSource = "USENKLBUS010D.global.ul.com:1521/CELLSIGNAL",
        //	UserID = "WERCS",
        //	Password = "Thewercs1"    //CUST4 = "MPtotvEo#862" 
        //};

        //Golden
        return new()
        {

            DataSource = "STUDCONT-DB:1521/STUDCONT",
            UserID = "WERCS",
            Password = "TYtxtvEm#092"    //CUST4 = "MPtotvEo#862"  
        };

        //Ungerer
        //return new()
        //{
        //	DataSource = "USENKLBUS010D.global.ul.com:1521/ORA12BCUST4.global.ul.com",
        //	UserID = "WERCS",
        //	Password = "MPtotvEo#862"    //CUST4 = "MPtotvEo#862" , CUST6=	 "TYtxtvEm#092"
        //};
    }


    internal static OracleConnectionStringBuilder GetCSOracleConnection()
    {

        //Golden
        //return new()
        //{
        //	DataSource = "USENKLBUS010D.global.ul.com:1521/CELLSIGNAL",
        //	UserID = "WERCS",
        //	Password = "Thewercs1"    //CUST4 = "MPtotvEo#862" 
        //};


        return new()
        {
            DataSource = "STUDCS-db.global.ul.com:1521/STUDCS",
            UserID = "WERCS",
            Password = "TheWercs1"    //CUST4 = "MPtotvEo#862"  
        };

        ////Golden
        //return new()
        //{

        //    DataSource = "STUDCONT-DB:1521/STUDCONT",
        //    UserID = "WERCS",
        //    Password = "TYtxtvEm#092"    //CUST4 = "MPtotvEo#862"  
        //};

        //Ungerer
        //return new()
        //{
        //	DataSource = "USENKLBUS010D.global.ul.com:1521/ORA12BCUST4.global.ul.com",
        //	UserID = "WERCS",
        //	Password = "MPtotvEo#862"    //CUST4 = "MPtotvEo#862" , CUST6=	 "TYtxtvEm#092"
        //};
    }

    internal static string ModifyForRun(string queryToRun)
    {
        queryToRun = queryToRun.ToUpper();

        if (queryToRun.Contains("{FN NOW()}"))
        {
            queryToRun = queryToRun.Replace("{FN NOW()}", "sysdate");
        }

        if (queryToRun.Contains("{ FN NOW()}"))
        {
            queryToRun = queryToRun.Replace("{ FN NOW()}", "sysdate");
        }

        if (queryToRun.Contains("{FN NOW() }"))
        {
            queryToRun = queryToRun.Replace("{FN NOW() }", "sysdate");
        }

        return queryToRun;
    }

    public class RegionalStream
    {
        public RegionalStream() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Total { get; set; }
        public int Converted { get; set; }
        public int UnConverted { get; set; }
    }

    public static List<RegionalStream> GetRegionalRuleStreamList()
    {
        /*
         
         reference for this
         https://ul.sharepoint.com/:x:/r/sites/collab/622/GoldenGCS/_layouts/15/Doc.aspx?sourcedoc=%7BAB250C01-57F7-45F0-95CE-0288C9F0945C%7D&file=GCS%20Subformats%20&%20Rules%20Licensing.xlsx=&action=default&mobileredirect=true
          
         */

        return
        [
            new() { Id = 86, Name = "GHS Wizard Shared" },
            new() { Id = 74, Name = "Post-CA DB" },
            new() { Id = 104, Name = "Argentina" },
            new() { Id = 101, Name = "Australia" },
            new() { Id = 89, Name = "Brazil" },
            new() { Id = 93, Name = "Canada" },
            new() { Id = 108, Name = "Chile" },
            new() { Id = 90, Name = "China" },
            new() { Id = 107, Name = "Colombia" },
            new() { Id = 92, Name = "Europe" },
            new() { Id = 94, Name = "Indonesia" },
            new() { Id = 97, Name = "Israel" },
            new() { Id = 95, Name = "Japan" },
            new() { Id = 96, Name = "Korea" },
            new() { Id = 102, Name = "Malaysia" },
            new() { Id = 106, Name = "Mexico" },
            new() { Id = 103, Name = "New Zealand" },
            new() { Id = 98, Name = "North America" },
            new() { Id = 75, Name = "PCN A" },
            new() { Id = 83, Name = "PCN B" },
            new() { Id = 84, Name = "PCN C" },
            new() { Id = 99, Name = "Philipines" },
            new() { Id = 105, Name = "Russia" },
            new() { Id = 109, Name = "Singapore" },
            new() { Id = 121, Name = "South Africa" },
            new() { Id = 115, Name = "Taiwan" },
            new() { Id = 100, Name = "Thailand" },
            new() { Id = 111, Name = "Turkiye" },
            new() { Id = 87, Name = "United States" },
            new() { Id = 91, Name = "Vietnam" },
            new() { Id = 117, Name = "United Kingom" },
        ];
    }
}
