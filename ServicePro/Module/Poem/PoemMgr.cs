using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.Poem
{
    class PoemMgr
    {
        static string poemFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Resources/Poems";
        static string poemToSendFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Resources/PoemsToSend";
        public static bool SetPoemJosnStrToDb(string jsonStr)
        {
            PoemsData poemsData = JsonConvert.DeserializeObject<PoemsData>(jsonStr);
            foreach (Poem poem in poemsData.poems)
            {
                PoemDbMgr.InsertPoem(poem);
            }
            return poemsData.count != 0;
        }

        public static string GetPoemJsonRangeFromDb(int poemCount = 1000, int beginId = 0)
        {
            int poemMaxId = PoemDbMgr.GetPoemMaxId();
            List<Poem> poems = new List<Poem>();
            while (beginId < poemMaxId && poems.Count < poemCount)
            {
                List<Poem> poemTemps = PoemDbMgr.GetPoemsByIdRange(beginId, beginId + 99);
                beginId += 99;
                poems.AddRange(poemTemps);
            }
           
            if(poems.Count == 0)
            {
                return "";
            }
            
            PoemsData poemsData = new PoemsData();
            poemsData.poems = poems.ToArray();
            poemsData.count = poems.Count;
            string poemJsonStr = JsonConvert.SerializeObject(poemsData, Formatting.Indented);

            return poemJsonStr;
        }

        public static string GetPoemJsonByAuthor(string author)
        {
            List<Poem> poems = PoemDbMgr.GetPoemsByAuthor(author);
            if (poems.Count == 0)
            {
                return "";
            }
            PoemsData poemsData = new PoemsData();
            poemsData.poems = poems.ToArray();
            poemsData.count = poems.Count;
            string poemJsonStr = JsonConvert.SerializeObject(poemsData, Formatting.Indented);

            return poemJsonStr;
        }

        public static string GetPoemJsonByDynasty(string dynasty)
        {
            List<Poem> poems = PoemDbMgr.GetPoemsByDynasty(dynasty);
            if (poems.Count == 0)
            {
                return "";
            }
            PoemsData poemsData = new PoemsData();
            poemsData.poems = poems.ToArray();
            poemsData.count = poems.Count;
            string poemJsonStr = JsonConvert.SerializeObject(poemsData, Formatting.Indented);

            return poemJsonStr;
        }

        public static string GetPoemJsonByBook(string book)
        {
            List<Poem> poems = PoemDbMgr.GetPoemsByBook(book);
            if (poems.Count == 0)
            {
                return "";
            }
            PoemsData poemsData = new PoemsData();
            poemsData.poems = poems.ToArray();
            poemsData.count = poems.Count;
            string poemJsonStr = JsonConvert.SerializeObject(poemsData, Formatting.Indented);

            return poemJsonStr;
        }

        public static string GetPoemJsonByType(string type)
        {
            List<Poem> poems = PoemDbMgr.GetPoemsByType(type);
            if (poems.Count == 0)
            {
                return "";
            }
            PoemsData poemsData = new PoemsData();
            poemsData.poems = poems.ToArray();
            poemsData.count = poems.Count;
            string poemJsonStr = JsonConvert.SerializeObject(poemsData, Formatting.Indented);

            return poemJsonStr;
        }

        //public static string GetPoemAuthorListJson()
        //{
        //    List<string> authors = PoemDbMgr.GetPoemAuthorList();
        //    if (authors.Count == 0)
        //    {
        //        return "";
        //    }
        //    PoemAuthorList poemAuthorList = new PoemAuthorList();
        //    poemAuthorList.authors = authors.ToArray();
        //    poemAuthorList.count = authors.Count;
        //    string jsonStr = JsonConvert.SerializeObject(poemAuthorList, Formatting.Indented);
        //    return jsonStr;
        //}
        //public static string GetPoemDynastyListJson()
        //{
        //    List<string> dynastys = PoemDbMgr.GetPoemDynastyList();
        //    if (dynastys.Count == 0)
        //    {
        //        return "";
        //    }
        //    PoemDynastyList poemDynastyList = new PoemDynastyList();
        //    poemDynastyList.dynastys = dynastys.ToArray();
        //    poemDynastyList.count = dynastys.Count;
        //    string jsonStr = JsonConvert.SerializeObject(poemDynastyList, Formatting.Indented);
        //    return jsonStr;
        //}
        //public static string GetPoemBookListJson()
        //{
        //    List<string> books = PoemDbMgr.GetPoemBookList();
        //    if (books.Count == 0)
        //    {
        //        return "";
        //    }
        //    PoemBookList poemBookList = new PoemBookList();
        //    poemBookList.books = books.ToArray();
        //    poemBookList.count = books.Count;
        //    string jsonStr = JsonConvert.SerializeObject(poemBookList, Formatting.Indented);
        //    return jsonStr;
        //}
        //public static string GetPoemTypeListJson()
        //{
        //    List<string> types = PoemDbMgr.GetPoemTypeList();
        //    if (types.Count == 0)
        //    {
        //        return "";
        //    }
        //    PoemTypeList poemTypeList = new PoemTypeList();
        //    poemTypeList.types = types.ToArray();
        //    poemTypeList.count = types.Count;
        //    string jsonStr = JsonConvert.SerializeObject(poemTypeList, Formatting.Indented);
        //    return jsonStr;
        //}
    }

    class PoemsData
    {
        public Poem[] poems;
        public int count = 0;
    }

    //class PoemAuthorList
    //{
    //    public string[] authors;
    //    public int count;
    //}

    //class PoemDynastyList
    //{
    //    public string[] dynastys;
    //    public int count;
    //}

    //class PoemBookList
    //{
    //    public string[] books;
    //    public int count;
    //}

    //class PoemTypeList
    //{
    //    public string[] types;
    //    public int count;
    //}
}
