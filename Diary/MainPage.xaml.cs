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
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            //获得App的可以访问的私有空间
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //System.IO.Directory.Exisit(localFolder);
            //获得文件的操作流
            Stream stream = await localFolder.OpenStreamForReadAsync(user_TextBox.Text +".json");
            //插管子读取
            StreamReader json = new StreamReader(stream, Encoding.UTF8);
            //把二进制流转换成文本
            string users = await json.ReadToEndAsync();
            //将数组解析成对象
            dynamic usersDiary = JsonConvert.DeserializeObject(users);
            string userPassword = usersDiary.Password;
            if(user_TextBox.Text==""||psw_TextBox.Password=="")
            {
                MessageDialog msg = new MessageDialog(" 用户名或密码不能为空！");
                msg.Title = "警告！";
                psw_TextBox.Password = "";
                var msginfo = await msg.ShowAsync();
            }
            else if (psw_TextBox.Password.Equals(userPassword))
            {
                Frame.Navigate(typeof(DetailPage), usersDiary);
            }
            else
            {
                MessageDialog msg = new MessageDialog(" 用户名或密码输入错误!");
                msg.Title = "警告！";
                psw_TextBox.Password = "";
                var msginfo = await msg.ShowAsync();
            }
        }
    }

    //public class user
    //{
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //} 
}
