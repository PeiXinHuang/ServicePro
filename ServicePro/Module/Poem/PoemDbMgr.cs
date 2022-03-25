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
            if (string.IsNullOrEmpty(poem.title) || string.IsNullOrEmpty(poem.author) || string.IsNullOrEmpty(poem.content))
            {
                Console.WriteLine("[PoemDbMgr] poem can not be inserted into db because title author or content is empty " + poem.ToString());
                return;
            };
          
            bool hasContainThisPoem = false;
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where title = '{0}' and author = '{1}'", poem.title, poem.author);
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

                string sql = string.Format("insert into poem(title, author, dynasty, content, type, book,translation, annotation,appreciation) " + 
                    "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                    poem.title, poem.author, poem.dynasty, poem.content, poem.type,poem.book, poem.translation, poem.annotation,poem.appreciation);
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
                    string type = reader[5].ToString();
                    string book = reader[6].ToString();
                    string translation = reader[7].ToString();
                    string annotation = reader[8].ToString();
                    string appreciation = reader[9].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, type, book, annotation, translation, appreciation);
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
                    string type = reader[5].ToString();
                    string book = reader[6].ToString();
                    string translation = reader[7].ToString();
                    string annotation = reader[8].ToString();
                    string appreciation = reader[9].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, type, book, annotation, translation, appreciation);
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
                    string type = reader[5].ToString();
                    string book = reader[6].ToString();
                    string translation = reader[7].ToString();
                    string annotation = reader[8].ToString();
                    string appreciation = reader[9].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, type, book, annotation, translation, appreciation);
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

        public static List<Poem> GetPoemsByDynasty(string _dynasty)
        {
            List<Poem> poems = new List<Poem>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where dynasty  = '{0}' ", _dynasty);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string title = reader[1].ToString();
                    string author = reader[2].ToString();
                    string dynasty = reader[3].ToString();
                    string content = reader[4].ToString();
                    string type = reader[5].ToString();
                    string book = reader[6].ToString();
                    string translation = reader[7].ToString();
                    string annotation = reader[8].ToString();
                    string appreciation = reader[9].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, type, book, annotation, translation, appreciation);
                    poems.Add(poem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get poem by dynasty fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return poems;
        }

        public static List<Poem> GetPoemsByType(string _type)
        {
            List<Poem> poems = new List<Poem>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where type  = '{0}' ", _type);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string title = reader[1].ToString();
                    string author = reader[2].ToString();
                    string dynasty = reader[3].ToString();
                    string content = reader[4].ToString();
                    string type = reader[5].ToString();
                    string book = reader[6].ToString();
                    string translation = reader[7].ToString();
                    string annotation = reader[8].ToString();
                    string appreciation = reader[9].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, type, book, annotation, translation, appreciation);
                    poems.Add(poem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get poem by type fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return poems;
        }

        public static List<Poem> GetPoemsByBook(string _book)
        {
            List<Poem> poems = new List<Poem>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem where book  = '{0}' ", _book);

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string title = reader[1].ToString();
                    string author = reader[2].ToString();
                    string dynasty = reader[3].ToString();
                    string content = reader[4].ToString();
                    string type = reader[5].ToString();
                    string book = reader[6].ToString();
                    string translation = reader[7].ToString();
                    string annotation = reader[8].ToString();
                    string appreciation = reader[9].ToString();
                    Poem poem = new Poem(title, author, dynasty, content, type, book, annotation, translation, appreciation);
                    poems.Add(poem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get poem by book fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return poems;
        }

        public static List<string> GetPoemAuthorList()
        {
            List<string> authors = new List<string>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem");

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string author = reader[2].ToString();
                    if(!string.IsNullOrEmpty(author) && !authors.Contains(author))
                    {
                        authors.Add(author);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get authors list fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return authors;
        }


        public static List<string> GetPoemDynastyList()
        {
            List<string> dynastys = new List<string>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem");

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string dynasty = reader[3].ToString();
                    if (!string.IsNullOrEmpty(dynasty) && !dynastys.Contains(dynasty))
                    {
                        dynastys.Add(dynasty);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get dynastys list fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dynastys;
        }


        public static List<string> GetPoemTypeList()
        {
            List<string> types = new List<string>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem");

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string type = reader[5].ToString();
                    if (!string.IsNullOrEmpty(type) && !types.Contains(type))
                    {
                        types.Add(type);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get types list fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return types;
        }

        
        public static List<string> GetPoemBookList()
        {
            List<string> books = new List<string>();
            try
            {
                conn.Open();
                string sql = string.Format("select * from poem");

                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string book = reader[6].ToString();
                    if (!string.IsNullOrEmpty(book) && !books.Contains(book))
                    {
                        books.Add(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[PoemDbMgr] Get books list fail cause by " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return books;
        }
    }
}
