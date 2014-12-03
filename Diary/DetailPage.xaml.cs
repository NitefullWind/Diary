using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Diary
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        private double BoxOpactity { get; set; }
        public DetailPage()
        {
            this.InitializeComponent();
            BoxOpactity = 5;
        }

        //要从当前页面离开跳转到其他页面发生的事情
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        //从别的页面跳转到当前页面发生的事情
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var lang = e.Parameter.ToString();
            dynamic usersDiary = JsonConvert.DeserializeObject(lang);
            UserDiary_Box.Text = usersDiary.UserName + "\n" + usersDiary.text + "\n";
        }
    }
}
