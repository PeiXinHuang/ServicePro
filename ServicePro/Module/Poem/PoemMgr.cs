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
        public static void SetPoemToDb()
        {
            string[] filedir = Directory.GetFiles(poemFolderPath, "*.json", SearchOption.AllDirectories);
            for (int i = 0; i < filedir.Length; i++)
            {
                string jsonTxt =  File.ReadAllText(filedir[i]);
                PoemsData poemsData = JsonConvert.DeserializeObject<PoemsData>(jsonTxt);
                foreach (Poem poem in poemsData.poems)
                {
                    PoemDbMgr.InsertPoem(poem);
                }
            }
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
    }

    class PoemsData
    {
        public Poem[] poems;
        public int count = 0;
    }
}
