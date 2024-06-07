using RuleDConversion.Models;
using Rule = RuleDConversion.Models.Rule;

namespace RuleDConversion;

internal static class DataUtils
{
    internal static string ExecuteTest(string productid, string queryToRun, int ruleNumber, string OracleConnString, bool doCommit = false)
    {
        string ret;       

        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };

        conn.Open();

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = queryToRun,
            CommandType = CommandType.Text
        };

        try
        {
            queryToRun = queryToRun.Replace("'@'", $"'{productid}'");

            if (queryToRun.StartsWith("UD_", StringComparison.CurrentCultureIgnoreCase) && !queryToRun.StartsWith("UD_RUNSQLD", StringComparison.CurrentCultureIgnoreCase))
            {               
                ret = $" {ruleNumber} is a UD function and can't be tested";
                return ret;
            }

            if (queryToRun.StartsWith("SELECT", StringComparison.CurrentCultureIgnoreCase))
            {
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Read();
                dr.Close();
                cmd.Dispose();
                conn.Dispose();

                ret = $" Ok";
            }
            else if (queryToRun.StartsWith("INSERT", StringComparison.CurrentCultureIgnoreCase))
            {
                OracleTransaction trans;

                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = trans;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                if (doCommit)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }                
                cmd.Dispose();
                conn.Dispose();

                ret = $" Ok";
            }
            else if (queryToRun.StartsWith("UPDATE", StringComparison.CurrentCultureIgnoreCase))
            {
                OracleTransaction trans;

                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = trans;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                if (doCommit)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
                cmd.Dispose();
                conn.Dispose();

                ret = $" Ok";
            }
            else if (queryToRun.StartsWith("DELETE", StringComparison.CurrentCultureIgnoreCase))
            {
                OracleTransaction trans;

                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = trans;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                if (doCommit)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
                cmd.Dispose();
                conn.Dispose();
                        
                ret = $" Ok";
            }
            else
            {
                OracleTransaction trans;

                trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = trans;
                cmd.Prepare();
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                if (doCommit)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
                cmd.Dispose();
                conn.Dispose();

                ret = $" Ok";
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Trim().StartsWith("ORA-02291"))
            {
                ret = $" {ruleNumber} tests Ok (with referential integrity problem)";
            }
            else
            {
                ret = $"Exception in rule test. Message: {ex.Message} \r\n StackTrace: {ex.StackTrace}";
            }
        }

        return ret;
    }

    internal static string SaveChanges(bool updateCalc, string currentSql, string calc, string ruleName, int ruleNumber, string OracleConnString)
    {
        string ret;

        string queryToRun = calc;

        if (!queryToRun.StartsWith("UD_RUNSQLD('"))
        {
            queryToRun = $"UD_RUNSQLD('{calc} ')";
        }

        if (!currentSql.StartsWith("UD_RUNSQLD('"))
        {
            currentSql = $"UD_RUNSQLD('{currentSql} ')";
        }

        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };

        conn.Open();

        OracleCommand cmd = new()
        {
            Connection = conn
        };

        if (updateCalc)
        {
            cmd.CommandText = @"UPDATE T_D_RULES 
										SET F_CALC_ORA = :CALC, 
										F_CALC = :SQL1, 
										F_CALC_SQL = :SQL2,
										F_RULE_NAME = :RNAME,
										F_CONVERTED = 1,
                                        F_REVIEWED = 1,
										F_USER_UPDATED = 'GEOFFB',
										F_DATE_CREATED = sysdate
									WHERE F_RULE_NUMBER = :RN ";
        }
        else
        {
            cmd.CommandText = @"UPDATE T_D_RULES  
								SET F_CONVERTED = 1,
                                    F_REVIEWED = 1,
									F_USER_UPDATED = 'GEOFFB',
									F_DATE_CREATED = sysdate
    							WHERE F_RULE_NUMBER = :RN ";
        }

        cmd.CommandType = CommandType.Text;

        OracleTransaction trans;
        trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        try
        {
            cmd.Parameters.Clear();

            if (updateCalc)
            {
                cmd.Parameters.Add("CALC", queryToRun);
                cmd.Parameters.Add("SQL1", currentSql);
                cmd.Parameters.Add("SQL2", currentSql);
                cmd.Parameters.Add("RNAME", ruleName);
            }

            cmd.Parameters.Add("RN", ruleNumber);

            cmd.Transaction = trans;
            cmd.Prepare();
            int x = cmd.ExecuteNonQuery();
            if (x == 1)
            {
                trans.Commit();
            }
            cmd.Dispose();
            conn.Dispose();

            ret = $"{ruleNumber} updated Ok!";

            trans.Dispose();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ret = $"UPDATE FAILED: Message: {ex.Message} \r\n StackTrace: {ex.StackTrace}";
            trans.Dispose();
        }

        return ret;
    }

    internal static string UpdateConvertedFlag(int ruleNumber, string OracleConnString)
    {
        string ret;


        OracleConnection conn = new()
        {
            ConnectionString = OracleConnString
        };

        conn.Open();

        OracleCommand cmd = new()
        {
            Connection = conn,
            CommandText = @"UPDATE T_D_RULES  
							    SET F_CONVERTED = 1,
                                SET F_REVIEWED = 1,
							    F_USER_UPDATED = 'GEOFFB',
							    F_DATE_CREATED = sysdate
						    WHERE F_RULE_NUMBER = :RN ",
            CommandType = CommandType.Text
        };

        OracleTransaction trans;
        trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        try
        {
            cmd.Parameters.Add("RN", ruleNumber);
            cmd.Transaction = trans;
            cmd.Prepare();
            int x = cmd.ExecuteNonQuery();
            if (x == 1)
            {
                trans.Commit();
            }
            cmd.Dispose();
            conn.Dispose();

            ret = $"{ruleNumber} updated Ok!";

            trans.Dispose();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            ret = $"UPDATE FAILED: Message: {ex.Message} \r\n StackTrace: {ex.StackTrace}";
            trans.Dispose();
        }

        return ret;
    }

    #region Entity inserts

    internal static RuleStream GetStreamFromReader(OracleDataReader reader)
    {
        return new()
        {
            StreamId = reader["F_STREAM_ID"] as long? ?? 0,
            Name = reader["F_NAME"] as string,
            Active = reader["F_ACTIVE"] as bool? ?? false,
            DateStamp = reader["F_DATE_STAMP"] as DateTime? ?? DateTime.MinValue,
            UserUpdated = reader["F_USER_UPDATED"] as string,
            Guid = reader["F_GUID"] as string,
            Comments = reader["F_COMMENTS"] as string
        };
    }

    internal static RuleStreamMember GetStreamMemberFromReader(OracleDataReader reader)
    {
        return new()
        {
            RecordId = reader["F_RECORD_ID"] as long? ?? 0,
            StreamId = reader["F_STREAM_ID"] as long? ?? 0,
            GroupId = reader["F_GROUP_ID"] as long? ?? 0,
            DateStamp = reader["F_DATE_STAMP"] as DateTime? ?? DateTime.MinValue,
            UserUpdated = reader["F_USER_UPDATED"] as string,
            Guid = reader["F_GUID"] as string,
            Order = reader["F_ORDER"] as int? ?? 0
        };
    }

    internal static GroupMember GetGroupMemberFromReader(OracleDataReader reader)
    {
        return new()
        {
            GroupId = reader["F_GROUP_ID"] as long? ?? 0,
            RuleNumber = reader["F_GROUP_ID"] as long? ?? 0,
            RuleType = reader["F_RULE_TYPE"] as string,
            RuleDescription = reader["F_RULE_DESC"] as string,
            DateStamp = reader["F_DATE_STAMP"] as DateTime? ?? DateTime.MinValue,
            UserUpdated = reader["F_USER_UPDATED"] as string,
            Scope = reader["F_SCOPE"] as int? ?? 0
        };
    }

    internal static RuleGroup GetGroupFromReader(OracleDataReader reader)
    {
        return new()
        {
            GroupId = reader["F_GROUP_ID"] as long? ?? 0,
            GroupName = reader["F_GROUP_NAME"] as string,
            ExitOnFirstFail = reader["F_EXIT_ON_FIRST_FAIL"] as int? ?? 0,
            NextGroupOnFail = reader["F_NEXT_GROUP_ON_FAIL"] as int? ?? 0,
            NextGroupOnSuccess = reader["F_NEXT_GROUP_ON_SUCCESS"] as int? ?? 0,
            DateStamp = reader["F_DATE_STAMP"] as DateTime? ?? DateTime.MinValue,
            UserUpdated = reader["F_USER_UPDATED"] as string,
            Scope = reader["F_SCOPE"] as int? ?? 0,
            GlobalId = reader["F_GLOBAL_ID"] as string
        };
    }

    internal static Rule GetRuleFromReader(OracleDataReader reader)
    {
        return new()
        {
            RuleNumber = reader["F_RULE_NUMBER"] as long? ?? 0,
            RuleName = reader["F_RULE_NAME"] as string,
            RuleType = reader["F_RULE_TYPE"] as string,
            DateApplied = reader["F_DATE_APPLIED"] as DateTime? ?? DateTime.MinValue,
            DateStamp = reader["F_DATE_STAMP"] as DateTime? ?? DateTime.MinValue,
            UserUpdated = reader["F_USER_UPDATED"] as string,
            Scope = reader["F_SCOPE"] as int? ?? 0,
            GlobalId = reader["F_GLOBAL_ID"] as string,
            Comments = reader["F_COMMENTS"] as string
        };
    }

    #endregion
}
