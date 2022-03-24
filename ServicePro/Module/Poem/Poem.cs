using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePro.Module.Poem
{
    class Poem
    {
        /// <summary>
        /// 诗词名
        /// </summary>
        public string title = "";

        /// <summary>
        /// 作者
        /// </summary>
        public string author = "";

        /// <summary>
        /// 朝代
        /// </summary>
        public string dynasty = "";

        /// <summary>
        /// 诗词内容
        /// </summary>
        public string content = "";

        /// <summary>
        /// 诗词类型
        /// </summary>
        public string types = "";

        /// <summary>
        /// 诗词注释
        /// </summary>
        public string annotation = "";

        /// <summary>
        /// 诗词翻译
        /// </summary>
        public string translation = "";

        public Poem(string _title, string _author, string _dynasty, string _content, string _types)
        {
            this.title = _title;
            this.author = _author;
            this.dynasty = _dynasty;
            this.content = _content;
            this.types = _types;
        }

        public override string ToString()
        {
            return string.Format("title: {0}, author: {1}, dynasty: {2}, content: {3}, types:{4} ",
                title, author, dynasty, content, types);
        }
    }
}
