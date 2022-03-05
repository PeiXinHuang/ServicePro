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
                    PoemDbMgr poemDbMgr = new PoemDbMgr();
                    poemDbMgr.InsertPoem(poem);
                }
            }
        }

        public static void GetPoemFromDb(int poemCount = 1000, int beginId = 0)
        {
            PoemDbMgr poemDbMgr = new PoemDbMgr();
            int poemMaxId = poemDbMgr.GetPoemMaxId();
            List<Poem> poems = new List<Poem>();
            while (beginId < poemMaxId && poems.Count < poemCount)
            {
                List<Poem> poemTemps = poemDbMgr.GetPoemsById(beginId, beginId + 99);
                beginId += 99;
                poems.AddRange(poemTemps);
            }
           
            if(poems.Count == 0)
            {
                return;
            }
            
            PoemsData poemsData = new PoemsData();
            poemsData.poems = poems.ToArray();
            poemsData.count = poems.Count;
            string poemJsonStr = JsonConvert.SerializeObject(poemsData, Formatting.Indented);

            Console.WriteLine(poemJsonStr);

            string path = poemToSendFolderPath + "/poem0.json";
            File.WriteAllText(path, poemJsonStr);
        }
    }

    class PoemsData
    {
        public Poem[] poems;
        public int count = 0;
    }
}
