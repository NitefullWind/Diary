using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
        List<UserDiary> AllDiary;
        StorageFolder localFolder;
        string ThisDate;

        public DetailPage()
        {
            this.InitializeComponent();
            //显示日期
            //Time_Block.Text = DateTime.Now.ToString("yyyy年MM月dd日 dddd");
            ThisDate = DateTime.Now.ToString("yyyy-MM-dd");
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
            ReadUserDiary();
        }

        //读取日记
        private async void ReadUserDiary()
        {
            //获得App的安装目录的根目录
            localFolder = ApplicationData.Current.LocalFolder;
            //获得文件的操作流
            Stream stream = await localFolder.OpenStreamForReadAsync("UserDiary.json");
            //插管子读取
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));
            //把二进制流转换成文本
            string json = await sr.ReadToEndAsync();
            //获取所有日记对象链表
            AllDiary = JsonConvert.DeserializeObject<List<UserDiary>>(json);
            
            //查找是否有今日的日记
            UserDiary NowDiary = AllDiary.Find(
                delegate(UserDiary d)
                { return d.Time == ThisDate; });
            if (NowDiary == null || NowDiary.Time == null)
            {
                DiaryTitle_Box.Text = "";
                UserDiary_Box.Text = "";
            }
            else
            {
                DiaryTitle_Box.Text = NowDiary.Title;
                UserDiary_Box.Text = NowDiary.Text;
            }
            sr.Dispose();
            stream.Dispose();
        }

        //跳到修改用户信息界面
        private void SettingBar_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChangeUserInfoPage));
        }
        //跳到登陆界面
        private void BackBar_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        //文本框内Tab键不设置跳转
        private void UserDiary_Box_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Tab)
            {
                e.Handled = true;
            }
        }

        //日期变化时
        private void ChoseDate_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            ThisDate = ChoseDate.Date.ToString("yyyy-MM-dd");
            ReadUserDiary();
        }

        //日期重设为今日
        private void BackToday_Btn_Click(object sender, RoutedEventArgs e)
        {
            ChoseDate.Date = DateTime.Now;
        }


        //保存日记
        private async void SaveDiary()
        {
            //查找是否有今日的日记
            UserDiary NowDiary = AllDiary.Find(
                delegate(UserDiary d)
                { return d.Time == ThisDate; });
            if (NowDiary == null || NowDiary.Time == null)
            {
                //新日记添加到链表中
                AllDiary.Add(new UserDiary { Time = ThisDate, Title = DiaryTitle_Box.Text, Text = UserDiary_Box.Text });
            }
            //或者保存修改的日记
            else
            {
                NowDiary.Title = DiaryTitle_Box.Text;
                NowDiary.Text = UserDiary_Box.Text;
            }
            var strJson = JsonConvert.SerializeObject(AllDiary);
            //将json字符串写入json文件中
            Stream stream = await localFolder.OpenStreamForWriteAsync("UserDiary.json", CreationCollisionOption.ReplaceExisting);
            StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
            sw.WriteLine(strJson);
            sw.Flush();
            sw.Dispose();
            stream.Dispose();
        }

        //保存
        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            SaveDiary();
        }
        //清空
        private void Clean_Btn_Click(object sender, RoutedEventArgs e)
        {
            UserDiary_Box.Text = "";
        }
        //删除
        private async void Delite_Btn_Click(object sender, RoutedEventArgs e)
        {

            MessageDialog msg = new MessageDialog("日记删除后不可恢复，确定要继续？");
            msg.Commands.Add(new UICommand("Yes!", new UICommandInvokedHandler(this.SureHandler)));
            msg.Commands.Add(new UICommand("Oh,no!"));
            await msg.ShowAsync();
        }
        //点“确定”时
        private void SureHandler(IUICommand command)
        {
            DiaryTitle_Box.Text = "";
            UserDiary_Box.Text = "";
            SaveDiary();
        }

    }
}
