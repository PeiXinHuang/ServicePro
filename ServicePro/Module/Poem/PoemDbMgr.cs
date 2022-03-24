using MySqlConnector;
using ServicePro.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.Poem
{
    class PoemDbMgr : DbManager
    {
        public static void InsertPoem(Poem poem)
        {
            if (string.IsNullOrEmpty(poem.title) || string.IsNullOrEmpty(poem.content))
            {
                Console.WriteLine("[PoemDbMgr] poem can not be inserted into db because title or content is empty " + poem.ToString());
                return;
            };
          
            bool hasContainThisPoem = false;
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where title = '{0}'", poem.title);
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    hasContainThisPoem = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Charge poem is exit or not faile cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            if (hasContainThisPoem)
            {
                Console.WriteLine("[PoemDbMgr]" + poem.title + " has exit in poem db, pass");
                return;
            }

            try
            {
                conn.Open();

                string sql = string.Format("insert into poem(title, author, dynasty, content, type) " + 
                    "values('{0}','{1}','{2}','{3}','{4}')",
                    poem.title, poem.author, poem.dynasty, poem.content, poem.types);
                Console.WriteLine(sql);
                //执行插入语句
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Insert poem db fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Poem> GetPoemsByIdRange(int beginId, int EndId)
        {
            List<Poem> poems = new List<Poem>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where id > {0} and id <= {1} ", beginId, EndId);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    string title = reader[1].ToString();
                    string author = reader[2].ToString();
                    string dynasty = reader[3].ToString();
                    string content = reader[4].ToString();
                    string types = reader[5].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, types);
                    poems.Add(poem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get poem fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return poems;
        }

        public static Poem GetPoemsById(int id)
        {
            List<Poem> poems = new List<Poem>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where id  = {0} ", id);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string title = reader[1].ToString();
                    string author = reader[2].ToString();
                    string dynasty = reader[3].ToString();
                    string content = reader[4].ToString();
                    string types = reader[5].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, types);
                    poems.Add(poem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get poem fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            if (poems.Count == 0)
                return poems[0];
            return null;
        }

        public static int GetPoemMaxId()
        {
            int maxId = 0;
            try
            {
                conn.Open();
                string sql = "select max(id) from poem";
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    maxId = Convert.ToInt32(reader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get poem maxId fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return maxId;

        }
        public static List<Poem> GetPoemsByAuthor(string _author)
        {
            List<Poem> poems = new List<Poem>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where author  = '{0}' ", _author);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string title = reader[1].ToString();
                    string author = reader[2].ToString();
                    string dynasty = reader[3].ToString();
                    string content = reader[4].ToString();
                    string types = reader[5].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, types);
                    poems.Add(poem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get poem by author fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return poems;
        }
    }
}
