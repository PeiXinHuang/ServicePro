using MySqlConnector;
using ServicePro.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.Favorite
{
    class FavoriteDbMgr : DbManager
    {

        public static List<Favorite> GetAllFavoriteByUserMail(string userMail)
        {
            List<Favorite> favorites = new List<Favorite>();
            
            try
            {
                conn.Open();
                string sql = string.Format("select * from favorite where userMail = '{0}'",userMail);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Favorite favorite = new Favorite();

                    if (reader[3].ToString().Equals("1"))
                    {
                        favorite.poemId = int.Parse(reader[0].ToString());
                        favorite.userMail = reader[1].ToString();
                        favorite.poemTitle = reader[2].ToString();
                        favorite.isFavorite = true;
                        favorites.Add(favorite);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FavoriteDbMgr] Get Favorite by mail faild cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
           
            return favorites;
        }

        public static void SetFavorite(int poemId, string userMail, string poemTitle, int isFavorite)
        {
            if(IsContaionFavorite(poemId, userMail))
            {
                UpdateFavorite(poemId, userMail, isFavorite);
            }
            else
            {
                InsertFavorite(poemId, userMail, poemTitle, isFavorite);
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

        public static void InsertFavorite(int poemId, string userMail, string poemTitle, int isFavorite)
        {
            try
            {
                conn.Open();

                string sql = string.Format("insert into favorite(poemId, userMail, poemTitle, isFavorite) " +
                    "values('{0}','{1}','{2}','{3}')",
                    poemId, userMail, poemTitle, isFavorite.ToString());
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
                Console.WriteLine("[FavoriteDbMgr] Charge is exit or not fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return isContain;
        }
    }
}
