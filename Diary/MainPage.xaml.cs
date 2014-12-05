using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel;
using Windows.Data.Json;
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
    public sealed partial class MainPage : Page
    {
        private dynamic usersDiary;
        public MainPage()
        {
            this.InitializeComponent();
            getStream();
        }

        private async void getStream()
        {
            //获得App的可以访问的私有空间
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //获得文件的操作流
            Stream stream = await localFolder.OpenStreamForReadAsync("UserData.json");
            //插管子读取
            StreamReader sr = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));
            //把二进制流转换成文本
            string users = await sr.ReadToEndAsync();
            //将数组解析成对象
            usersDiary = JsonConvert.DeserializeObject(users);

            //设置欢迎字符
            user_TextBlock.Text = "您好，" + usersDiary.UserName;
            sr.Dispose();
            stream.Dispose();
        }
        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            if(psw_TextBox.Password=="")
            {
                wrong_TextBlock.Text = "**密码不能为空**";
            }
            else if (psw_TextBox.Password.Equals(usersDiary.Password.ToString()))
            {
                wrong_TextBlock.Text = "";
                Frame.Navigate(typeof(DetailPage), usersDiary);
            }
            else
            {
                psw_TextBox.Password = "";
                wrong_TextBlock.Text = "**密码错误**";
            }
        }
    }
}
