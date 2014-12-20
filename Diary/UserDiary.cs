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
        private int mood;

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
        public int Mood
        {
            get { return mood; }
            set { mood = value; }
        }
    }
}
