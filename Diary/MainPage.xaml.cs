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
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using Windows.System;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace Diary
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private dynamic UserInfo;
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
            UserInfo = JsonConvert.DeserializeObject(users);

            //获取当前小时数
            int NowTime = DateTime.Now.Hour;
            string WelcomeStr = "您好";
            if (NowTime >= 6 && NowTime < 9) { WelcomeStr = "早安，";} ;
            if (NowTime >= 9 && NowTime < 11) { WelcomeStr = "上午好，"; };
            if (NowTime >= 11 && NowTime < 13) { WelcomeStr = "中午好，";} ;
            if (NowTime >= 13 && NowTime < 19) { WelcomeStr = "下午好，";} ;
            if (NowTime >= 19 && NowTime < 23) { WelcomeStr = "晚上好，";} ;
            if (NowTime >= 23 || NowTime < 4) { WelcomeStr = "夜深了，早点休息，"; };
            if (NowTime >= 4 && NowTime < 6) { WelcomeStr = "一日之计在于晨，"; };
            //设置欢迎字符
            user_TextBlock.Text = WelcomeStr + UserInfo.UserName;
            sr.Dispose();
            stream.Dispose();
        }
        //按钮登录
        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        //回车登录
        private void psw_TextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                Login();
            }
        }

        //登录函数
        private void Login()
        {
            if(psw_TextBox.Password=="")
            {
                wrong_TextBlock.Text = "**密码不能为空**";
            }
            else if (psw_TextBox.Password.Equals(UserInfo.UserPassword.ToString()))
            {
                wrong_TextBlock.Text = "";
                Frame.Navigate(typeof(DetailPage));
            }
            else
            {
                wrong_TextBlock.Text = "**密码错误**";
                psw_TextBox.Password = "";
            }
        }

        //从别的页面跳转到当前页面发生的事情
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}
