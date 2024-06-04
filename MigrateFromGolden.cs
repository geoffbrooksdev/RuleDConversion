using Oracle.ManagedDataAccess.Types;
using static RuleDConversion.Utils;

namespace RuleDConversion;

internal static class MigrateFromGolden
{
    internal static string OracleConnString;
    internal static string SqlConnString;

    internal static SqlConnection sqlConn = new();
    internal static OracleConnection oraConn = new();

    internal static OracleCommand streamCmd;
    internal static OracleCommand streamMemberCmd;
    internal static OracleCommand ruleCmd;
    internal static OracleCommand ruleDCmd;
    internal static OracleCommand groupCmd;
    internal static OracleCommand groupMemberCmd;

    internal static List<RegionalStream> GetStreamCounts()
    {
        List<RegionalStream> regions = Utils.GetRegionalRuleStreamList();
        List<RegionCount> convertedRules = [];
        List<RegionCount> unconvertedRules = [];
        List<RegionCount> totalRules = [];
        List<RegionalStream> retRegions = [];

        OracleCommand oraCmd = new()
        {
            Connection = oraConn,
            CommandText = @"SELECT s.F_STREAM_ID, COUNT(*) AS CONVERTED
                                FROM T_D_RULES_GOLDEN d, T_GROUP_MEMBERS_GOLDEN m, T_RULE_STREAM_MEMBERS_GOLDEN s,T_D_RULES d1
                                WHERE d.F_RULE_NUMBER = m.F_RULE_NUMBER
                                AND d.F_RULE_NUMBER = d1.F_RULE_NUMBER
                                AND NVL(d1.F_CONVERTED,0) = 1
                                AND LENGTH(d1.F_CALC_ORA) > 0
                                AND m.F_GROUP_ID = s.F_GROUP_ID
                                AND s.F_STREAM_ID IN(86,74,104,101,89,93,108,90,107,92,94,97,95,96,102,106,103,98,75,83,84,99,105,109,121,115,100,111,87,91,117)
                                GROUP BY s.F_STREAM_ID
                                ORDER BY 1"
        };
        oraConn.Open();
        OracleDataReader oraRdr = oraCmd.ExecuteReader();

        while (oraRdr.Read())
        {
            int streamId = Convert.ToInt32(oraRdr["F_STREAM_ID"] as decimal? ?? 0);
            int converted = Convert.ToInt32(oraRdr["CONVERTED"] as decimal? ?? 0);
            convertedRules.Add((new() { StreamId = streamId, RuleCount = converted }));
        }
        oraConn.Close();

        oraCmd.CommandText = @"SELECT s.F_STREAM_ID, COUNT(*) AS UNCONVERTED
                                FROM T_D_RULES_GOLDEN d, T_GROUP_MEMBERS_GOLDEN m, T_RULE_STREAM_MEMBERS_GOLDEN s,T_D_RULES d1
                                WHERE d.F_RULE_NUMBER = m.F_RULE_NUMBER
                                AND d.F_RULE_NUMBER = d1.F_RULE_NUMBER
                                AND NVL(d1.F_CONVERTED,0) = 0
                                AND m.F_GROUP_ID = s.F_GROUP_ID
                                AND s.F_STREAM_ID IN(86,74,104,101,89,93,108,90,107,92,94,97,95,96,102,106,103,98,75,83,84,99,105,109,121,115,100,111,87,91,117)
                                GROUP BY s.F_STREAM_ID
                                ORDER BY 1";
        oraConn.Open();
        oraRdr = oraCmd.ExecuteReader();

        while (oraRdr.Read())
        {
            int streamId = Convert.ToInt32(oraRdr["F_STREAM_ID"] as decimal? ?? 0);
            int unconverted = Convert.ToInt32(oraRdr["UNCONVERTED"] as decimal? ?? 0);
            unconvertedRules.Add((new() { StreamId = streamId, RuleCount = unconverted }));
        }
        oraConn.Close();

        oraCmd.CommandText = @"SELECT s.F_STREAM_ID, COUNT(*) AS TOTAL
                                FROM T_D_RULES_GOLDEN d, T_GROUP_MEMBERS_GOLDEN m, T_RULE_STREAM_MEMBERS_GOLDEN s,T_D_RULES d1
                                WHERE d.F_RULE_NUMBER = m.F_RULE_NUMBER
                                AND d.F_RULE_NUMBER = d1.F_RULE_NUMBER                               
                                AND m.F_GROUP_ID = s.F_GROUP_ID
                                AND s.F_STREAM_ID IN(86,74,104,101,89,93,108,90,107,92,94,97,95,96,102,106,103,98,75,83,84,99,105,109,121,115,100,111,87,91,117)
                                GROUP BY s.F_STREAM_ID
                                ORDER BY 1";
        oraConn.Open();
        oraRdr = oraCmd.ExecuteReader();

        while (oraRdr.Read())
        {
            int streamId = Convert.ToInt32(oraRdr["F_STREAM_ID"] as decimal? ?? 0);
            int total = Convert.ToInt32(oraRdr["TOTAL"] as decimal? ?? 0);
            totalRules.Add((new() { StreamId = streamId, RuleCount = total }));
        }
        oraConn.Close();

        foreach (var region in regions)
        {
            retRegions.Add(new()
            {
                Id = region.Id,
                Name = region.Name,
                Total = totalRules.FirstOrDefault(c => c.StreamId == region.Id) == null ? 0 : totalRules.FirstOrDefault(c => c.StreamId == region.Id).RuleCount,
                Converted = convertedRules.FirstOrDefault(c => c.StreamId == region.Id) == null ? 0 : convertedRules.FirstOrDefault(c => c.StreamId == region.Id).RuleCount,
                UnConverted = unconvertedRules.FirstOrDefault(c => c.StreamId == region.Id) == null ? 0 : unconvertedRules.FirstOrDefault(c => c.StreamId == region.Id).RuleCount
            });
        }

        return retRegions;
    }

    internal class RegionCount
    {
        internal RegionCount() { }
        internal int StreamId { get; set; }
        internal int RuleCount { get; set; } = 0;
    }

    static MigrateFromGolden()
    {
        SqlConnectionStringBuilder sscb = Utils.GetSqlServerConnection();
        SqlConnString = sscb.ConnectionString;

        OracleConnectionStringBuilder ocsb = Utils.GetOracleConnection();
        OracleConnString = ocsb.ConnectionString;

        oraConn = new()
        {
            ConnectionString = OracleConnString
        };

        sqlConn = new()
        {
            ConnectionString = SqlConnString
        };

        //----------------------------------------------------------------------------------------------------------------------------------------

        streamCmd = new(@"INSERT INTO T_RULE_STREAMS_GOLDEN (F_STREAM_ID, F_NAME,F_ACTIVE,F_DATE_STAMP,F_USER_UPDATED,F_GUID,F_COMMENTS) 
                            VALUES (:SID,:NAME,:ACTIVE,sysdate,:USR,:GUID,:COMM)", oraConn);
        streamCmd.Parameters.Add("SID", OracleDbType.Int32);
        streamCmd.Parameters.Add("NAME", OracleDbType.Varchar2);
        streamCmd.Parameters.Add("ACTIVE", OracleDbType.Int16);
        streamCmd.Parameters.Add("USR", OracleDbType.Varchar2);
        streamCmd.Parameters.Add("GUID", OracleDbType.Varchar2);
        streamCmd.Parameters.Add("COMM", OracleDbType.Varchar2);

        //-----------------------------------------------------------------------------------------------------------------------------------------

        streamMemberCmd = new(@"INSERT INTO T_RULE_STREAM_MEMBERS_GOLDEN (F_RECORD_ID,F_STREAM_ID, F_GROUP_ID,F_DATE_STAMP,
						        F_USER_UPDATED,F_GUID,F_ORDER) VALUES (:RID,:SID,:GID,sysdate,:USR,:GUID,:ORD)", oraConn);
        streamMemberCmd.Parameters.Add("RID", OracleDbType.Int32);
        streamMemberCmd.Parameters.Add("SID", OracleDbType.Int32);
        streamMemberCmd.Parameters.Add("GID", OracleDbType.Int32);
        streamMemberCmd.Parameters.Add("USR", OracleDbType.Varchar2);
        streamMemberCmd.Parameters.Add("GUID", OracleDbType.Varchar2);
        streamMemberCmd.Parameters.Add("ORD", OracleDbType.Int16);

        //-----------------------------------------------------------------------------------------------------------------------------------------

        ruleCmd = new(@"INSERT INTO T_RULES_GOLDEN (F_RULE_NUMBER,F_RULE_NAME,F_RULE_TYPE,F_DATE_APPLIED,F_USER_UPDATED,F_DATE_STAMP,F_GLOBAL_ID,F_SCOPE,F_COMMENTS) 
                            VALUES (:RNUM,:NAME,:TYPE,:APPL,:USR,sysdate,:GUID,:SCO,:COMM)", oraConn);
        ruleCmd.Parameters.Add("RNUM", OracleDbType.Int16);
        ruleCmd.Parameters.Add("NAME", OracleDbType.Varchar2);
        ruleCmd.Parameters.Add("TYPE", OracleDbType.Varchar2);
        ruleCmd.Parameters.Add("APPL", OracleDbType.Date);
        ruleCmd.Parameters.Add("USR", OracleDbType.Varchar2);
        ruleCmd.Parameters.Add("GUID", OracleDbType.Varchar2);
        ruleCmd.Parameters.Add("SCO", OracleDbType.Int16);
        ruleCmd.Parameters.Add("COMM", OracleDbType.Varchar2);

        //-----------------------------------------------------------------------------------------------------------------------------------------

        ruleDCmd = new(@"INSERT INTO T_D_RULES_GOLDEN (F_RULE_NUMBER,F_FORMAT_FROM,F_SUBSECTION_A,F_SUBSECTION_B,F_SUBSECTION_C,F_SUBSECTION_D,
							F_FORMAT_TO,F_SUBSECTION_ID_TO, F_RULE_NAME,F_DATE_CREATED,F_DATE_APPLIED,F_USER_UPDATED,F_PREFIXTYPE,F_CALC,F_DESC) 
							VALUES (:RNUM,:FF,:SSA,:SSB,:SSC,:SSD,:FT,:SSTO,:NAME,:DC,:DA,:USR,:PT,EMPTY_CLOB(),EMPTY_CLOB())", oraConn);
        ruleDCmd.Parameters.Add("RNUM", OracleDbType.Int16);
        ruleDCmd.Parameters.Add("FF", OracleDbType.Char, 3);
        ruleDCmd.Parameters.Add("SSA", OracleDbType.Varchar2);
        ruleDCmd.Parameters.Add("SSB", OracleDbType.Varchar2);
        ruleDCmd.Parameters.Add("SSC", OracleDbType.Varchar2);
        ruleDCmd.Parameters.Add("SSD", OracleDbType.Varchar2);
        ruleDCmd.Parameters.Add("FT", OracleDbType.Char, 3);
        ruleDCmd.Parameters.Add("SSTO", OracleDbType.Varchar2);
        ruleDCmd.Parameters.Add("NAME", OracleDbType.Varchar2);
        ruleDCmd.Parameters.Add("DC", OracleDbType.Date);
        ruleDCmd.Parameters.Add("DA", OracleDbType.Date);
        ruleDCmd.Parameters.Add("USR", OracleDbType.Varchar2);
        ruleDCmd.Parameters.Add("PT", OracleDbType.Decimal);
        ruleDCmd.BindByName = true;

        //-----------------------------------------------------------------------------------------------------------------------------------------

        groupCmd = new(@"INSERT INTO T_RULE_GROUPS_GOLDEN (F_GROUP_ID,F_GROUP_NAME,F_GROUP_DESC,F_EXIT_ON_FIRST_FAIL, F_NEXT_GROUP_ON_FAIL,
                            F_NEXT_GROUP_ON_SUCCESS,F_USER_UPDATED,F_DATE_STAMP,F_GLOBAL_ID,F_SCOPE) 
                        VALUES (:GID,:DN,:GDESC,:EFF,:NGF,:NGS,:USR,sysdate,:GUID,:SCO)", oraConn);
        groupCmd.Parameters.Add("GID", OracleDbType.Int32);
        groupCmd.Parameters.Add("GN", OracleDbType.Varchar2);
        groupCmd.Parameters.Add("GDESC", OracleDbType.Varchar2);
        groupCmd.Parameters.Add("EFF", OracleDbType.Int32);
        groupCmd.Parameters.Add("NGF", OracleDbType.Int32);
        groupCmd.Parameters.Add("NGS", OracleDbType.Int32);
        groupCmd.Parameters.Add("USR", OracleDbType.Varchar2);
        groupCmd.Parameters.Add("GUID", OracleDbType.Varchar2);
        groupCmd.Parameters.Add("SCO", OracleDbType.Int32);

        //-----------------------------------------------------------------------------------------------------------------------------------------


        /*
         * 
            SELECT count(*) FROM T_RULES_GOLDEN

            SELECT count(*) FROM T_D_RULES_GOLDEN

            SELECT count(*) FROM T_RULE_GROUPS_GOLDEN

            SELECT count(*) FROM T_GROUP_MEMBERS_GOLDEN

            SELECT count(*) FROM T_RULE_STREAMS_GOLDEN

            SELECT count(*) FROM T_RULE_STREAM_MEMBERS_GOLDEN
         * 
         */

    }

    static bool DisableTriggersOracle()
    {
        bool ok = true;

        OracleCommand oraCmd = new()
        {
            Connection = oraConn,
            CommandText = "ALTER TRIGGER T_RULE_GROUPS_TRIG DISABLE"
        };
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "ALTER TRIGGER T_RULE_STREAM_MEMBERS_TRIG DISABLE";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "ALTER TRIGGER T_RULE_STREAMS_TRIG DISABLE";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "ALTER TRIGGER T_RULES_TRIG DISABLE";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }


    public static bool PopulateOraFromGolden()
    {

        bool ok = DeleteGoldenContentFromOracle();

        if (ok)
        {
            ok = DisableTriggersOracle();
        }

        if (ok)
        {
            ok = PopulateRules();
        }

        if (ok)
        {
            ok = PopulateDRules();
        }

        if (ok)
        {
            ok = PopulateRuleGroups();
        }

        if (ok)
        {
            ok = PopulateRuleGroupMembers();
        }

        if (ok)
        {
            ok = PopulateRuleStreams();
        }

        if (ok)
        {
            ok = PopulateRuleStreamMembers();
        }

        if (ok)
        {
            ok = InsertNewDRules();
        }

        if (ok)
        {
            ok = RebuildStreamsAndGroups();
        }

        return ok;
    }


    internal static bool RebuildStreamsAndGroups()
    {
        int x;

        OracleCommand oraDelCmd = new()
        {
            Connection = oraConn,
            CommandText = "DELETE FROM T_GROUP_MEMBERS"
        };
        oraConn.Open();
        x = oraDelCmd.ExecuteNonQuery();
        oraConn.Close();

        bool ok = x > 0;
        if (ok)
        {
            oraDelCmd.CommandText = "DELETE FROM T_RULE_GROUPS";
            oraConn.Open();
            x = oraDelCmd.ExecuteNonQuery();
            oraConn.Close();

            ok = (x > 0);
        }

        if (ok)
        {
            oraDelCmd.CommandText = "DELETE FROM T_RULE_STREAM_MEMBERS";
            oraConn.Open();
            x = oraDelCmd.ExecuteNonQuery();
            oraConn.Close();

            ok = (x > 0);
        }

        if (ok)
        {
            oraDelCmd.CommandText = "DELETE FROM T_RULE_STREAMS";
            oraConn.Open();
            oraDelCmd.ExecuteNonQuery();
            oraConn.Close();

            ok = (x > 0);
        }

        //if (ok)
        //{
        //    ok = DisableTriggersOracle();
        //}


        //------------------------------------------------------------------------------------------------------

        OracleCommand oraInsCmd = new()
        {
            Connection = oraConn
        };

        if (ok)
        {
            oraInsCmd.CommandText = @"INSERT INTO T_GROUP_MEMBERS (F_GROUP_ID, F_RULE_NUMBER,F_RULE_DESC, F_RULE_TYPE, F_DATE_STAMP, F_USER_UPDATED, F_SCOPE)
                                    SELECT F_GROUP_ID, F_RULE_NUMBER,F_RULE_DESC, F_RULE_TYPE, F_DATE_STAMP, F_USER_UPDATED, F_SCOPE FROM T_GROUP_MEMBERS_GOLDEN
                                    WHERE F_RULE_TYPE = 'D' ";
            oraConn.Open();
            x = oraInsCmd.ExecuteNonQuery();
            oraConn.Close();
            ok = (x > 0);
        }

        if (ok)
        {
            oraInsCmd.CommandText = @"INSERT INTO T_RULE_GROUPS (F_GROUP_ID, F_GROUP_NAME, F_GROUP_DESC, F_EXIT_ON_FIRST_FAIL, 
                                        F_NEXT_GROUP_ON_SUCCESS, F_USER_UPDATED, F_DATE_STAMP, F_GLOBAL_ID, F_SCOPE)
                                    SELECT F_GROUP_ID, F_GROUP_NAME, F_GROUP_DESC, F_EXIT_ON_FIRST_FAIL, 
                                        F_NEXT_GROUP_ON_SUCCESS, F_USER_UPDATED, F_DATE_STAMP, F_GLOBAL_ID, F_SCOPE FROM T_RULE_GROUPS_GOLDEN";
            oraConn.Open();
            x = oraInsCmd.ExecuteNonQuery();
            oraConn.Close();

            ok = (x > 0);

        }


        if (ok)
        {
            oraInsCmd.CommandText = @"INSERT INTO T_RULE_STREAM_MEMBERS (F_RECORD_ID,F_STREAM_ID, F_GROUP_ID, F_DATE_STAMP, F_USER_UPDATED, F_GUID, F_ORDER) 
                                    SELECT F_RECORD_ID,F_STREAM_ID,F_GROUP_ID,F_DATE_STAMP,F_USER_UPDATED,F_GUID,F_ORDER FROM T_RULE_STREAM_MEMBERS_GOLDEN";
            oraConn.Open();
            x = oraInsCmd.ExecuteNonQuery();
            oraConn.Close();

            ok = (x > 0);

        }

        if (ok)
        {
            oraInsCmd.CommandText = @"INSERT INTO T_RULE_STREAMS (F_STREAM_ID, F_NAME,F_ACTIVE,F_DATE_STAMP,F_USER_UPDATED,F_GUID,F_COMMENTS) 
                                   SELECT F_STREAM_ID, F_NAME,F_ACTIVE,F_DATE_STAMP,F_USER_UPDATED,F_GUID,F_COMMENTS FROM T_RULE_STREAMS_GOLDEN ";
            oraConn.Open();
            x = oraInsCmd.ExecuteNonQuery();
            oraConn.Close();

            ok = (x > 0);
        }

        return ok;
    }

    internal static bool InsertNewDRules()
    {
        OracleCommand oraRuleInsCmd = new()
        {
            Connection = oraConn,
            CommandText = @"  INSERT INTO T_RULES (F_RULE_NUMBER, F_RULE_NAME,F_RULE_TYPE,F_DATE_APPLIED,F_USER_UPDATED,
                                        F_DATE_STAMP,F_GLOBAL_ID,F_SCOPE,F_COMMENTS)
                                        SELECT F_RULE_NUMBER, F_RULE_NAME,F_RULE_TYPE,F_DATE_APPLIED,F_USER_UPDATED,
                                        F_DATE_STAMP,F_GLOBAL_ID,F_SCOPE,F_COMMENTS
                                        FROM T_RULES_GOLDEN g
                                        WHERE g.F_RULE_NUMBER NOT IN (SELECT F_RULE_NUMBER FROM T_RULES)
                                        AND g.F_RULE_TYPE = 'D' "
        };
        oraConn.Open();
        oraRuleInsCmd.ExecuteNonQuery();
        oraConn.Close();

        OracleCommand oraRuleDInsCmd = new()
        {
            Connection = oraConn,
            CommandText = @" INSERT INTO T_D_RULES (F_RULE_NUMBER,F_FORMAT_FROM,F_SUBSECTION_A,F_SUBSECTION_B,F_SUBSECTION_C,F_SUBSECTION_D,
                                        F_FORMAT_TO,F_SUBSECTION_ID_TO,F_RULE_NAME,F_DATE_CREATED,F_DATE_APPLIED,F_USER_UPDATED,F_PREFIXTYPE,F_CALC,F_DESC, F_CONVERTED)
                                        SELECT F_RULE_NUMBER,F_FORMAT_FROM,F_SUBSECTION_A,F_SUBSECTION_B,F_SUBSECTION_C,F_SUBSECTION_D,
                                        F_FORMAT_TO,F_SUBSECTION_ID_TO,F_RULE_NAME,F_DATE_CREATED,F_DATE_APPLIED,F_USER_UPDATED,F_PREFIXTYPE,F_CALC,F_DESC,0
                                        FROM  T_D_RULES_GOLDEN g 
                                        WHERE g.F_RULE_NUMBER NOT IN (SELECT F_RULE_NUMBER FROM T_D_RULES)"
        };
        oraConn.Open();
        oraRuleDInsCmd.ExecuteNonQuery();
        oraConn.Close();

        return true;
    }

    internal static bool DeleteGoldenContentFromOracle()
    {
        bool ok = true;

        OracleCommand oraCmd = new()
        {
            Connection = oraConn,
            CommandText = "DELETE FROM T_RULES_GOLDEN"
        };
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "DELETE FROM T_D_RULES_GOLDEN";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "DELETE FROM T_RULE_GROUPS_GOLDEN";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "DELETE FROM T_GROUP_MEMBERS_GOLDEN";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "DELETE FROM T_RULE_STREAMS_GOLDEN";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        oraCmd.CommandText = "DELETE FROM T_RULE_STREAM_MEMBERS_GOLDEN";
        oraConn.Open();
        oraCmd.ExecuteNonQuery();
        oraConn.Close();

        return ok;
    }

    internal static bool PopulateRules()
    {
        int x = 0;
        int y = 0;

        SqlCommand sqlCmd = new()
        {
            Connection = sqlConn,
            CommandText = @"SELECT * FROM T_RULES WHERE F_RULE_TYPE = 'D'"
        };

        sqlConn.Open();

        SqlDataReader sqlRdr = sqlCmd.ExecuteReader();
        while (sqlRdr.Read())
        {
            y++;

            ruleCmd.Parameters["RNUM"].Value = sqlRdr["F_RULE_NUMBER"] as int? ?? 0;
            ruleCmd.Parameters["NAME"].Value = sqlRdr["F_RULE_NAME"] as string;
            ruleCmd.Parameters["TYPE"].Value = sqlRdr["F_RULE_TYPE"] as string;
            ruleCmd.Parameters["APPL"].Value = sqlRdr["F_DATE_APPLIED"] as DateTime? ?? DateTime.Now;
            ruleCmd.Parameters["USR"].Value = sqlRdr["F_USER_UPDATED"] as string;
            ruleCmd.Parameters["GUID"].Value = sqlRdr["F_GLOBAL_ID"] as string;
            ruleCmd.Parameters["SCO"].Value = sqlRdr["F_SCOPE"] as int? ?? 0;
            ruleCmd.Parameters["COMM"].Value = sqlRdr["F_COMMENTS"] as string;
            oraConn.Open();
            x += ruleCmd.ExecuteNonQuery();
            oraConn.Close();
        }

        sqlRdr.Close();
        sqlConn.Close();

        return (x == y);
    }

    internal static bool PopulateDRules()
    {
        int x = 0;
        int y = 0;

        SqlCommand sqlCmd = new()
        {
            Connection = sqlConn,
            CommandText = @"SELECT * FROM T_D_RULES"
        };

        sqlConn.Open();

        SqlDataReader sqlRdr = sqlCmd.ExecuteReader();
        while (sqlRdr.Read())
        {
            y++;

            oraConn.Open();

            string calc = sqlRdr["F_CALC"] as string;
            string desc = sqlRdr["F_DESC"] as string;

            byte[] calcArray = null;
            byte[] descArray = null;

            if (!string.IsNullOrEmpty(calc))
            {
                calcArray = Encoding.Unicode.GetBytes(calc);
            }

            if (!string.IsNullOrEmpty(desc))
            {
                descArray = Encoding.Unicode.GetBytes(desc);
            }

            ruleDCmd.Parameters["RNUM"].Value = sqlRdr["F_RULE_NUMBER"] as int? ?? 0;
            ruleDCmd.Parameters["FF"].Value = sqlRdr["F_FORMAT_FROM"] as char? ?? null;
            ruleDCmd.Parameters["SSA"].Value = sqlRdr["F_SUBSECTION_A"] as string;
            ruleDCmd.Parameters["SSB"].Value = sqlRdr["F_SUBSECTION_B"] as string;
            ruleDCmd.Parameters["SSC"].Value = sqlRdr["F_SUBSECTION_C"] as string;
            ruleDCmd.Parameters["SSD"].Value = sqlRdr["F_SUBSECTION_D"] as string;
            ruleDCmd.Parameters["FT"].Value = sqlRdr["F_FORMAT_TO"] as char? ?? null;
            ruleDCmd.Parameters["SSTO"].Value = sqlRdr["F_SUBSECTION_ID_TO"] as string;
            ruleDCmd.Parameters["NAME"].Value = sqlRdr["F_RULE_NAME"] as string;
            ruleDCmd.Parameters["DC"].Value = sqlRdr["F_DATE_CREATED"] as DateTime? ?? DateTime.Now;
            ruleDCmd.Parameters["DA"].Value = sqlRdr["F_DATE_APPLIED"] as DateTime? ?? DateTime.Now;
            ruleDCmd.Parameters["USR"].Value = sqlRdr["F_USER_UPDATED"] as string;
            ruleDCmd.Parameters["PT"].Value = sqlRdr["F_PREFIXTYPE"] as decimal? ?? 0;

            x += ruleDCmd.ExecuteNonQuery();
            oraConn.Close();

            string sql = "SELECT F_RULE_NUMBER,F_CALC,F_DESC FROM T_D_RULES_GOLDEN WHERE F_RULE_NUMBER = :RNUM FOR UPDATE";

            OracleCommand oraClobCommand = new(sql, oraConn);
            oraClobCommand.Parameters.Add("RNUM", OracleDbType.Int16);

            oraConn.Open();

            oraClobCommand.Parameters["RNUM"].Value = sqlRdr["F_RULE_NUMBER"] as int? ?? 0;
            OracleDataReader oraRdr = oraClobCommand.ExecuteReader();

            while (oraRdr.Read())
            {
                var txn = oraConn.BeginTransaction();
                if (calcArray is not null)
                {
                    OracleClob calcClob = oraRdr.GetOracleClob(1);
                    calcClob.Write(calcArray, 0, calcArray.Length);
                }

                if (descArray is not null)
                {
                    OracleClob descClob = oraRdr.GetOracleClob(2);
                    descClob.Write(descArray, 0, descArray.Length);
                }

                txn.Commit();
                txn.Dispose();
            }
            oraConn.Close();
        }
        sqlRdr.Close();
        sqlConn.Close();

        return (x == y);
    }

    internal static bool PopulateRuleGroups()
    {
        int x = 0;
        int y = 0;

        SqlCommand sqlCmd = new()
        {
            Connection = sqlConn,
            CommandText = @"SELECT * FROM T_RULE_GROUPS"
        };

        sqlConn.Open();

        SqlDataReader sqlRdr = sqlCmd.ExecuteReader();
        while (sqlRdr.Read())
        {
            y++;

            groupCmd.Parameters["GID"].Value = sqlRdr["F_GROUP_ID"] as int? ?? 0;
            groupCmd.Parameters["GN"].Value = sqlRdr["F_GROUP_NAME"] as string;
            groupCmd.Parameters["GDESC"].Value = sqlRdr["F_GROUP_DESC"] as string;
            groupCmd.Parameters["EFF"].Value = sqlRdr["F_EXIT_ON_FIRST_FAIL"] as int? ?? 0;
            groupCmd.Parameters["NGF"].Value = sqlRdr["F_NEXT_GROUP_ON_FAIL"] as int? ?? 0;
            groupCmd.Parameters["NGS"].Value = sqlRdr["F_NEXT_GROUP_ON_SUCCESS"] as int? ?? 0;
            groupCmd.Parameters["USR"].Value = sqlRdr["F_USER_UPDATED"] as string;
            groupCmd.Parameters["GUID"].Value = sqlRdr["F_GLOBAL_ID"] as string;
            groupCmd.Parameters["SCO"].Value = sqlRdr["F_SCOPE"] as int? ?? 0;

            oraConn.Open();
            x += groupCmd.ExecuteNonQuery();
            oraConn.Close();
        }

        sqlRdr.Close();
        sqlConn.Close();

        return (x == y);
    }

    internal static bool PopulateRuleGroupMembers()
    {
        int x = 0;
        int y = 0;

        groupMemberCmd = new(@"INSERT INTO T_GROUP_MEMBERS_GOLDEN (F_GROUP_ID,F_RULE_NUMBER,F_RULE_DESC,F_RULE_TYPE,F_USER_UPDATED,F_DATE_STAMP,F_SCOPE) 
                               VALUES (:GID,:RN,:RDESC,'D',:USR,sysdate,:SCO)", oraConn);

        groupMemberCmd.Parameters.Add("GID", OracleDbType.Int32);
        groupMemberCmd.Parameters.Add("RN", OracleDbType.Int32);
        groupMemberCmd.Parameters.Add("RDESC", OracleDbType.NVarchar2, 255);
        groupMemberCmd.Parameters.Add("USR", OracleDbType.Varchar2, 15);
        groupMemberCmd.Parameters.Add("SCO", OracleDbType.Int32);

        groupMemberCmd.BindByName = true;

        SqlCommand sqlCmd = new()
        {
            Connection = sqlConn,
            CommandText = @"SELECT * FROM T_GROUP_MEMBERS WHERE F_RULE_TYPE = 'D'"
        };

        sqlConn.Open();

        SqlDataReader sqlRdr = sqlCmd.ExecuteReader();
        while (sqlRdr.Read())
        {
            y++;

            groupMemberCmd.Parameters["GID"].Value = sqlRdr["F_GROUP_ID"] as int? ?? 0;
            groupMemberCmd.Parameters["RN"].Value = sqlRdr["F_RULE_NUMBER"] as int? ?? 0;
            groupMemberCmd.Parameters["RDESC"].Value = sqlRdr["F_RULE_DESC"] as string;
            groupMemberCmd.Parameters["USR"].Value = sqlRdr["F_USER_UPDATED"] as string;
            groupMemberCmd.Parameters["SCO"].Value = sqlRdr["F_SCOPE"] as int? ?? 0;

            oraConn.Open();
            x += groupMemberCmd.ExecuteNonQuery();
            oraConn.Close();
        }

        sqlRdr.Close();
        sqlConn.Close();

        return (x == y);
    }

    internal static bool PopulateRuleStreams()
    {
        int x = 0;
        int y = 0;

        SqlCommand sqlCmd = new()
        {
            Connection = sqlConn,
            CommandText = @"SELECT * FROM T_RULE_STREAMS"
        };

        sqlConn.Open();

        SqlDataReader sqlRdr = sqlCmd.ExecuteReader();
        while (sqlRdr.Read())
        {
            y++;

            string tmp = (sqlRdr["F_GUID"] as Guid? ?? Guid.Empty).ToString();

            streamCmd.Parameters["SID"].Value = sqlRdr["F_STREAM_ID"] as long? ?? 0;
            streamCmd.Parameters["NAME"].Value = sqlRdr["F_NAME"] as string;
            streamCmd.Parameters["ACTIVE"].Value = sqlRdr["F_ACTIVE"] as int? ?? 0;
            streamCmd.Parameters["USR"].Value = sqlRdr["F_USER_UPDATED"] as string;
            streamCmd.Parameters["GUID"].Value = tmp;
            streamCmd.Parameters["COMM"].Value = sqlRdr["F_COMMENTS"] as string;

            oraConn.Open();
            x += streamCmd.ExecuteNonQuery();
            oraConn.Close();
        }

        sqlRdr.Close();
        sqlConn.Close();

        return (x == y);
    }

    internal static bool PopulateRuleStreamMembers()
    {
        int x = 0;
        int y = 0;

        SqlCommand sqlCmd = new()
        {
            Connection = sqlConn,
            CommandText = @"SELECT * FROM T_RULE_STREAM_MEMBERS"
        };

        sqlConn.Open();

        SqlDataReader sqlRdr = sqlCmd.ExecuteReader();
        while (sqlRdr.Read())
        {
            y++;

            string tmp = (sqlRdr["F_GUID"] as Guid? ?? Guid.Empty).ToString();

            streamMemberCmd.Parameters["RID"].Value = sqlRdr["F_RECORD_ID"] as long? ?? 0;
            streamMemberCmd.Parameters["SID"].Value = sqlRdr["F_STREAM_ID"] as long? ?? 0;
            streamMemberCmd.Parameters["GID"].Value = sqlRdr["F_GROUP_ID"] as long? ?? 0;
            streamMemberCmd.Parameters["USR"].Value = sqlRdr["F_USER_UPDATED"] as string;
            streamMemberCmd.Parameters["GUID"].Value = tmp;
            streamMemberCmd.Parameters["ORD"].Value = sqlRdr["F_ORDER"] as int? ?? 0;

            oraConn.Open();
            x += streamMemberCmd.ExecuteNonQuery();
            oraConn.Close();
        }

        sqlRdr.Close();
        sqlConn.Close();

        return (x == y);
    }
}
