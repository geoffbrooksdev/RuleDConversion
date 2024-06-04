namespace RuleDConversion;

internal class RuleStreamReader
{
    static readonly string GoldenStagingConnString;

    static RuleStreamReader()
    {
        GoldenStagingConnString = "Server=USENKMSQL013D.global.ul.com;Database=GoldenStaging;Trusted_Connection=True;";
    }

    internal static List<int> GetRuleStreamMembers(int streamId)
    {
        List<int> streammembers = [];

        SqlConnection conn = new(GoldenStagingConnString);

        SqlCommand cmd = new()
        {
            Connection = conn,
            CommandText = @"SELECT g.* FROM  T_GROUP_MEMBERS g
							JOIN T_RULE_STREAM_MEMBERS m ON m.F_GROUP_ID = g.F_GROUP_ID
							AND m.F_STREAM_ID = @SID
							AND F_RULE_TYPE = 'D'",
            CommandType = CommandType.Text
        };

        cmd.Parameters.AddWithValue("@SID", streamId);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            streammembers.Add(reader.GetInt32(0));
        }

        reader.Close();
        cmd.Dispose();
        conn.Close();

        return streammembers;
    }

}
