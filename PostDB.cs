using System;
using System.Data.SQLite;

namespace pa3_nwkostolni
{
    public class PostDB
    {
        static void Main2(){
            
            string cs = @"URI=file:c:\source\repos\321\pa3-nwkostolni\pa3-nwkostolni\posts.db";

            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "select SQLITE_Version()";

            using var cmd = new SQLiteCommand(stm, con);

            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version : {version}");

        }
    }
}