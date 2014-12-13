using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    class UserDiary
    {
        private string time;
        private string title;
        private string text;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}
