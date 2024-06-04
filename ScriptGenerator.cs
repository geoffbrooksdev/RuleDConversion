using static RuleManager.Utils;

namespace RuleManager;

internal static class ScriptGenerator
{

    internal static bool GenerateRegionals(string OracleConnString, string outputFolder)
    {
        bool ok = true;

        string filedropcreateName = "aaa_drop_create_tmpruled.sql";

        string fileFunctionName = "aab_related_functions.sql";

        string fileinsertDName = "zzz_update_d_rules.sql";

        try
        {
            string filecontents = @$"BEGIN
								EXECUTE IMMEDIATE 'DROP TABLE TMPRULED';
    
								EXCEPTION
									WHEN OTHERS THEN
										IF sqlcode != -942 THEN
											RAISE;
										END IF;  
							END;
                            {Environment.NewLine}{Environment.NewLine} / {Environment.NewLine}{Environment.NewLine}
							CREATE TABLE TMPRULED (F_RULE_NUMBER NUMBER(*,0) , F_RULE_NAME VARCHAR2(4000), F_CALC_ORA VARCHAR2(4000));{Environment.NewLine}{Environment.NewLine}";
            File.WriteAllText($"{outputFolder}/{filedropcreateName}", filecontents);

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            List<RegionalStream> regionalStreams = GetRegionalRuleStreamList();

            DropAndCreateTempTable(OracleConnString);

            OracleConnection conn = new()
            {
                ConnectionString = OracleConnString
            };

            OracleConnection tmpDconn = new()
            {
                ConnectionString = OracleConnString
            };

            OracleCommand cmd = new()
            {
                Connection = conn,
                CommandText = @"SELECT d.F_RULE_NUMBER, r.F_RULE_NAME, d.F_CALC_ORA
                                FROM T_D_RULES d
                                JOIN T_GROUP_MEMBERS m ON m.F_RULE_NUMBER = d.F_RULE_NUMBER
                                JOIN T_RULES r ON r.F_RULE_NUMBER = d.F_RULE_NUMBER
                                WHERE m.F_GROUP_ID IN 
                                (SELECT F_GROUP_ID FROM T_RULE_STREAM_MEMBERS
                                WHERE F_STREAM_ID = :SID)
                                AND d.F_RULE_NUMBER IN
                                (SELECT F_RULE_NUMBER 
                                FROM T_D_RULES 
                                WHERE LENGTH(F_CALC_ORA) > 0
                                AND TO_CHAR(F_CALC_ORA) != 'UD_RUNSQLD('' '')')"
            };

            cmd.Parameters.Add("SID", OracleDbType.Int32);

            foreach (RegionalStream regionalStream in regionalStreams)
            {
                filecontents = $"SET DEFINE OFF; {Environment.NewLine}";  //to escape & character in content

                cmd.Parameters[0].Value = regionalStream.Id;

                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int ruleNumber = dr["F_RULE_NUMBER"] as int? ?? 0;
                    string ruleOraSQL = dr["F_CALC_ORA"] as string;
                    ruleOraSQL = ruleOraSQL.Replace(Environment.NewLine, " ");
                    ruleOraSQL = ruleOraSQL.Replace("\n", " ");
                    filecontents += $"INSERT INTO TMPRULED VALUES({ruleNumber}, null ,TO_NCLOB(q''^'{ruleOraSQL}'^''));{Environment.NewLine}";
                    filecontents += $"/ {Environment.NewLine}";
                }

                dr.Close();
                conn.Close();

                filecontents += $"COMMIT;";

                if (!string.IsNullOrEmpty(filecontents))
                {
                    File.WriteAllText($"{outputFolder}/{regionalStream.Name}.sql", filecontents);
                }
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------

            filecontents = @"DECLARE CURSOR tmpWSMcursor IS   
								SELECT m.F_SOURCE, m.F_MAPPED, d.F_RULE_NUMBER, d.F_CALC_ORA
								FROM T_WS_MAP m, TMPRULED d
								WHERE d.F_RULE_NUMBER = TO_NUMBER(m.F_SOURCE)
								AND m.F_TYPE = 12
								AND m.F_SCOPE = 4 
								AND m.F_ORIGIN = 4; 

								recordWSM tmpWSMcursor%rowtype;       
						BEGIN   
							OPEN tmpWSMcursor;
							LOOP
								FETCH tmpWSMcursor INTO recordWSM;
        
								EXIT WHEN tmpWSMcursor%notfound;
        
								UPDATE T_D_RULES SET F_CALC = recordWSM.F_CALC_ORA , F_USER_UPDATED = 'ORAUPDATE'
								WHERE F_RULE_NUMBER = recordWSM.F_MAPPED;    

								UPDATE T_RULES SET F_USER_UPDATED = 'ORAUPDATE', F_DATE_STAMP = sysdate  
								WHERE F_RULE_NUMBER = recordWSM.F_MAPPED;     
      
							END LOOP;

							CLOSE tmpWSMcursor; 
    
							COMMIT;

                            UPDATE T_D_RULES SET F_CALC = REPLACE(F_CALC,'^''','') WHERE F_USER_UPDATED = 'ORAUPDATE';
                            COMMIT;
                            UPDATE T_D_RULES SET F_CALC = REPLACE(F_CALC,'''^','') WHERE F_USER_UPDATED = 'ORAUPDATE';
                            COMMIT;

						END;";

            File.WriteAllText($"{outputFolder}/{fileinsertDName}", filecontents);

            //-------------------------------------------------------------------------------------------------------------------------------------------

            string functionContents = $"create or replace TYPE STUDIOSPLITRECORD AS OBJECT(ID Number,DATA Varchar2(4000));";
            
            functionContents += $" {Environment.NewLine} {Environment.NewLine}/ {Environment.NewLine}{Environment.NewLine}";

            functionContents += "create or replace TYPE STUDIOSPLITTABLE AS TABLE OF STUDIOSPLITRECORD;";

            functionContents += $" {Environment.NewLine} {Environment.NewLine}/ {Environment.NewLine}{Environment.NewLine}";

            functionContents += @"create or replace FUNCTION       fnSplit(
                                          i_str    IN  VARCHAR2,
                                          i_delim  IN  VARCHAR2 DEFAULT ','
                                        ) RETURN SYS.ODCIVARCHAR2LIST DETERMINISTIC
                                        AS
                                          p_result       SYS.ODCIVARCHAR2LIST := SYS.ODCIVARCHAR2LIST();
                                          p_start        NUMBER(5) := 1;
                                          p_end          NUMBER(5);
                                          c_len CONSTANT NUMBER(5) := LENGTH( i_str );
                                          c_ld  CONSTANT NUMBER(5) := LENGTH( i_delim );
                                        BEGIN
                                          IF c_len > 0 THEN
                                            p_end := INSTR( i_str, i_delim, p_start );
                                            WHILE p_end > 0 LOOP
                                              p_result.EXTEND;
                                              p_result( p_result.COUNT ) := TRIM(SUBSTR( i_str, p_start, p_end - p_start ));
                                              p_start := p_end + c_ld;
                                              p_end := INSTR( i_str, i_delim, p_start );
                                            END LOOP;
                                            IF p_start <= c_len + 1 THEN
                                              p_result.EXTEND;
                                              p_result( p_result.COUNT ) := TRIM(SUBSTR( i_str, p_start, c_len - p_start + 1 ));
                                            END IF;
                                          END IF;
                                          RETURN p_result;
                                        END;";

            functionContents += $" {Environment.NewLine} {Environment.NewLine}/ {Environment.NewLine}{Environment.NewLine}";

            functionContents += @"create or replace FUNCTION       fnStudioSplit(
                                      i_str    IN  VARCHAR2,
                                      i_delim  IN  VARCHAR2 DEFAULT ','
                                    ) RETURN STUDIOSPLITTABLE
                                    AS
                                      p_result       STUDIOSPLITTABLE;
                                      p_start        NUMBER(5) := 1;
                                      p_end          NUMBER(5);
                                      c_len CONSTANT NUMBER(5) := LENGTH( i_str );
                                      c_ld  CONSTANT NUMBER(5) := LENGTH( i_delim );
                                      p_recid       NUMBER(5) :=1;
                                    BEGIN

                                        p_result := STUDIOSPLITTABLE();

                                      IF c_len > 0 THEN
                                        p_end := INSTR( i_str, i_delim, p_start );
                                        WHILE p_end > 0 LOOP
                                          p_result.EXTEND;
                                          p_result( p_result.COUNT ) := STUDIOSPLITRECORD(p_recid, TRIM(SUBSTR( i_str, p_start, p_end - p_start )));
                                          p_start := p_end + c_ld;
                                          p_end := INSTR( i_str, i_delim, p_start );
                                           p_recid := p_recid + 1;
                                        END LOOP;

                                        IF p_start <= c_len + 1 THEN
                                          p_result.EXTEND;
                                          p_result( p_result.COUNT ) := STUDIOSPLITRECORD (p_recid,TRIM(SUBSTR( i_str, p_start, c_len - p_start + 1 )));
                                          p_recid := p_recid + 1;
                                        END IF;

                                      END IF;
                                      RETURN p_result;
                                    END;";

            functionContents += $" {Environment.NewLine} {Environment.NewLine}/ {Environment.NewLine}{Environment.NewLine}";

            functionContents += @"create or replace FUNCTION ISNUMERIC (num_string IN VARCHAR2)
                                      RETURN INT
                                    IS
                                      num_check NUMBER;
                                    BEGIN
                                      num_check := TO_NUMBER(num_string);
                                      RETURN 1;
                                    EXCEPTION
                                    WHEN VALUE_ERROR THEN
                                      RETURN 0;
                                    END ISNUMERIC;";

            functionContents += $" {Environment.NewLine} {Environment.NewLine}/ {Environment.NewLine}{Environment.NewLine}";

            functionContents += @"create or replace function Stuff(pExpr varchar2,pStart integer,pLength integer, pReplace varchar2) return varchar2 is
                                    begin
                                      return case
                                               when pStart is not null and pLength is not null and
                                                    pStart > 0 and pStart < length(pExpr) then
                                                 substr(pExpr, 1, pStart - 1)||pReplace||substr(pExpr, pStart + pLength)
                                               else null
                                             end; 
                                    end;";

            functionContents += $" {Environment.NewLine} {Environment.NewLine}/ {Environment.NewLine}{Environment.NewLine}";

            File.WriteAllText($"{outputFolder}/{fileFunctionName}", functionContents);
        }
        catch (Exception ex)
        {
            ok = false;
            Console.WriteLine(ex.Message);
            throw;
        }

        return ok;
    }

    internal static bool GenerateGeneral(string OracleConnString, string outputFolder)
    {
        bool ok;
        StringBuilder sb = new();

        try
        {
            DropAndCreateTempTable(OracleConnString);

            OracleConnection conn = new()
            {
                ConnectionString = OracleConnString
            };

            OracleCommand cmd = new();
            cmd.Connection = conn;

            cmd.CommandText = @"INSERT INTO TMPRULED (F_RULE_NUMBER,F_CALC,F_CALC_ORA)
								SELECT F_RULE_NUMBER, F_CALC, F_CALC_ORA 
								FROM T_D_RULES 
								WHERE LENGTH(F_CALC_ORA) > 0";

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            //-------------------------------------------------------------------------------------------------------------

            cmd.CommandText = @"SELECT F_RULE_NUMBER, F_CALC_ORA FROM TMPRULED";

            conn.Open();
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sb.AppendLine($"INSERT INTO TMPRULED VALUES({reader["F_RULE_NUMBER"]}, null , TO_NCLOB(q'^{reader["F_CALC_ORA"]}^'));");
            };
            reader.Close();
            conn.Close();

            File.WriteAllText(outputFolder + "scripts.sql", sb.ToString());

            ok = true;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return ok;
    }


    static bool DropAndCreateTempTable(string OracleConnString)
    {
        bool ok;

        try
        {
            OracleConnection conn = new()
            {
                ConnectionString = OracleConnString
            };

            OracleCommand cmd = new()
            {
                Connection = conn
            };

            cmd.CommandText = @"BEGIN
								    EXECUTE IMMEDIATE 'DROP TABLE TMPRULED';    
								    EXCEPTION
									    WHEN OTHERS THEN
										    IF sqlcode != -942 THEN
											    RAISE;
										    END IF;  
							    END; ";

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            //---------------------------------------------------------------------------------------------------------------------------------------

            cmd.CommandText = @"CREATE TABLE TMPRULED (F_RULE_NUMBER NUMBER(10,0) NULL, F_CALC NVARCHAR2(4000) NULL, F_CALC_ORA NVARCHAR2(4000) NULL)";

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            ok = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        return ok;
    }
}
