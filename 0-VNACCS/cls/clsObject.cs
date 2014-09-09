using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections.Generic;

/// <summary>
/// Summary description for clsObject
/// </summary>
public class clsObject
{
    public static string SERVER_PRIVATE_KEY =
        "<RSAKeyValue><Modulus>ja+rf2MnRyn5PIokGebrEWK1m84xdFCXW9dzBTcLnmIL53gzsG3/4WSvNVQ0bQSbj4ktLJz8hJcjjsPUEVWp0b6tTkShli8HeD0dn4rSTXjrjeHWO+7PQOuv/yjmkZInyJWdVJH9yoIEN4Pr7LR4e6GMVaqgJsXt1lyN1ZQjbSM=</Modulus><Exponent>AQAB</Exponent><P>v6JdBDNY2Yjsck/KGjjleq2HmQ5kBW5d/qrykVxpQpqGKCWE/ohtJyHEXdK3v7uT/win4Kc1+wX99p+6MStxuw==</P><Q>vUaI1TONKiEYDoZkAUm0tR41BxZTUGswxo11/K7Pv1e7GqCbVzPoKcftzmyQh/mNDAr1Yh9r5PQuv2/2dTRnuQ==</Q><DP>vyDczNe5gh1CVjCmTcj5d4WjfFASCiitrtYo4Dne8gLUUy44mvTOiPzwsPL9OUmIrhCf/zxGZnrvdQ6R0YCXCQ==</DP><DQ>RF3N3vSX8LezcMuqI6zz0NfwX3b48PtGyvEdxP9/mqWdt6h7c6wUF8NXalBchEDnaYoryB2BY1mv6QYQB5W+QQ==</DQ><InverseQ>t64CCx53F6CgV7kX8XplenlEQgcLibglo7rlMDDP+M2h+8LwLKuL+faN3t2PvMtGGEmnapdWYevka0fGwLKzng==</InverseQ><D>RRl5kETxMPm9fdL5TFCcL/xuCbCD2fA8ASSQCekQl4vFtnue6dqbwnbyJBYPA2QN4fDKwUlGtftn8gZ7dIDMsq6AdVRmaOidSQZ36VCaFBwDknBxfPOEUAAGrsKTWxvpnze9kNIaMmdiQcnCfuY0WAeulHDBWtky/ngeaTA0A+E=</D></RSAKeyValue>";

    public static string SERVER_PUBLIC_KEY =
        "<RSAKeyValue><Modulus>ja+rf2MnRyn5PIokGebrEWK1m84xdFCXW9dzBTcLnmIL53gzsG3/4WSvNVQ0bQSbj4ktLJz8hJcjjsPUEVWp0b6tTkShli8HeD0dn4rSTXjrjeHWO+7PQOuv/yjmkZInyJWdVJH9yoIEN4Pr7LR4e6GMVaqgJsXt1lyN1ZQjbSM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

    public static string CLIENT_PRIVATE_KEY =
       "<RSAKeyValue><Modulus>sgywJfN34jDI7L5YGzHeJ2L38svScUFAaOVt/rVH1r+Pq8dwt4mOoM50H6ZMGR7fhfkwvozAHbxd19Bp4in5IuueiZLIzMBnRtAZpRfcqg7N+Ul+fgi3NFbDdwDk5YMHgyLUzduWkTwR3Si1RRyQtwE9HJx5RUW0dOdpA7x6JlU=</Modulus><Exponent>AQAB</Exponent><P>1aj6A1sASyNBvk7xQEPIv2v6MjhMEjhCUn/gf2Y4m+p0Uj8Zns8k+hKqfCCSNNCTzWijqJH3wDft85j+9KdPWw==</P><Q>1VUuwdgOF17YDgFH0sEVvL3aTAq5xcbFGB4J0mAIqixeXEe1TrYxbLloQiKBAwLzAw4k9aTMZORRe5LqJC2ADw==</Q><DP>j3TEYSphuRF0G/ZfL3oTuMskE0IPeZBCn6fysTeOPMmfznVFm4aT2kBcLbmk0UdWZe2jLRTM10f2Qb/xN416zw==</DP><DQ>o9TpNaDRIY2K6yZ8nBQAt5Qtw1wxUvvVfTIEZPWvcdDlHoVLbglpZ4/zy2ZoIYHwUpZYN/W5qnnVQcuP2PzAbQ==</DQ><InverseQ>DgZJtgj5CrE1aVAHoJhbp0GFZEItxsDi+Ia4bizDnzqawb7XsV5Qx4vGfE7LpefyZmjMuy1D0H3vugaOSn9RTw==</InverseQ><D>OKXGyVxL8SAaXRA2UfisuJVJU+s7OiNLjdQpCde9UnrCCCI9H3fZdBobYhJzta5X6XUd6BI58XwD020Qsw8tn0HDhOkD0nxnNMo2pHJqAe/W34n1ksjVYB2F2NvCgPiOj2WBhzFRNAm02bDCQlOHSbTK9OA5YoHvS1vE/RvBuAU=</D></RSAKeyValue>";

    public static string CLIENT_PUBLIC_KEY =
        "<RSAKeyValue><Modulus>sgywJfN34jDI7L5YGzHeJ2L38svScUFAaOVt/rVH1r+Pq8dwt4mOoM50H6ZMGR7fhfkwvozAHbxd19Bp4in5IuueiZLIzMBnRtAZpRfcqg7N+Ul+fgi3NFbDdwDk5YMHgyLUzduWkTwR3Si1RRyQtwE9HJx5RUW0dOdpA7x6JlU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
}

public class SQL_ENTITY
{
    public SQL_ENTITY(string sql)
    {
        m_SQL = sql;
    }

    protected string m_SQL;
    public string SQL { get { return m_SQL; } set { m_SQL = value; } }

    protected IDbDataParameter[] m_Params;
    public IDbDataParameter[] Params { get { return m_Params; } set { m_Params = value; } }
}

public class clsConstants
{
    public const int ENC_KEYSIZE = 1024;
    public const string GETGOLDKEY = "001";
    public const string GETDATASET = "101";
    public const string GETDATASET_C = "102";
    public const string GETDATASET_C_T = "103";
    public const string CHECKEXIST = "201";
    public const string CHECKEXIST_C = "202";
    public const string CHECKEXIST_C_T = "203";
    public const string GETFVALUE = "301";
    public const string GETFVALUE_C = "302";
    public const string GETFVALUE_C_T = "303";
    public const string AFFECTDATA = "401";
    public const string AFFECTDATA_C = "402";
    public const string AFFECTDATA_C_T = "403";
    public const string AFFECTDATA_C_T_P = "404";
}