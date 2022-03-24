using MySqlConnector;
using ServicePro.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.Poem
{
    class FavoriteDbMgr : DbManager
    {

        public static void SetFavorite(int poemId, string userMail, int isFavorite)
        {
            if(IsContaionFavorite(poemId, userMail))
            {
                UpdateFavorite(poemId, userMail, isFavorite);
            }
            else
            {
                InsertFavorite(poemId, userMail, isFavorite);
            }
        }

        public static void UpdateFavorite(int poemId, string userMail, int isFavorite)
        {
            try
            {
                conn.Open();

                //数据库更新语句
                string sql = string.Format(
                    "Update favorite Set isFavorite = {0} where  poemId = {1} and userMail = '{2}'",
                    isFavorite.ToString(), poemId.ToString(), userMail);

                //执行更新语句
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("[FavoriteDbMgr]Update isFavorite Failed: " + e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public static void InsertFavorite(int poemId, string userMail, int isFavorite)
        {
            try
            {
                conn.Open();

                string sql = string.Format("insert into favorite(poemId, userMail, isFavorite) " +
                    "values('{0}','{1}','{2}')",
                    poemId, userMail, isFavorite.ToString());
                Console.WriteLine(sql);
                //执行插入语句
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("[FavoriteDbMgr] Insert favorite db fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool GetIsFavorite(int poemId, string userMail)
        {
            bool isFavorite = false;
            if(IsContaionFavorite(poemId, userMail))
            {
                try
                {
                    conn.Open();
                    string sql = string.Format("select * from favorite where userMail = '{0}' and poemId = {1}",
                        userMail, poemId.ToString());

                    MySqlCommand command = new MySqlCommand(sql, conn);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader[2].ToString().Equals("1"))
                        {
                            isFavorite = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[FavoriteDbMgr] Get Favorite faile cause by " + ex.ToString());
                }
                finally
                {
                    conn.Close();
                }
            }
            return isFavorite;
        }

        public static bool IsContaionFavorite(int poemId, string userMail)
        {
            bool isContain = false;
            try
            {
                conn.Open();
                string sql = string.Format("select * from favorite where userMail = '{0}' and poemId = {1}",
                    userMail, poemId.ToString());

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isContain = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FavoriteDbMgr] Charge is exit or not faile cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return isContain;
        }
    }
}
