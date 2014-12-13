using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public sealed partial class ChangeUserInfoPage : Page
    {
        public ChangeUserInfoPage()
        {
            this.InitializeComponent();
        }

        private User UserInfo = new User();
        //保存用户信息
        bool InfoIsOK;
        private async void Sure_Btn_Click(object sender, RoutedEventArgs e)
        {
            InfoIsOK = false;
            if(UserName_Box.Text==""||UserPassword.Password=="")
            {
                MessageDialog msg = new MessageDialog("用户名或密码不能为空");
                await msg.ShowAsync();
            }
            else
            {
                MessageDialog msg = new MessageDialog("确认要使用\n用户名：" + UserName_Box.Text + "\n密码：" + UserPassword.Password + "\n作为您的信息吗？");
                msg.Commands.Add(new UICommand("我确定！", new UICommandInvokedHandler(this.SureHandler)));
                msg.Commands.Add(new UICommand("改一下", new UICommandInvokedHandler(this.ChangeHandler)));
                await msg.ShowAsync();
            }
            if(InfoIsOK)
            {
                //跳到登陆界面
                Frame.Navigate(typeof(MainPage));
            }
        }

        //点“改一下”时
        private void ChangeHandler(IUICommand command)
        {
            UserPassword.Password = "";
        }
        //点“我确定”时
        private void SureHandler(IUICommand command)
        {
            //保存信息
            WriteInfo();
            InfoIsOK = true;
        }

        //创建用户信息
        private async void WriteInfo()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            //将用户信息写入json字符串
            UserInfo.UserName = UserName_Box.Text;
            UserInfo.UserPassword = UserPassword.Password;
            //解析
            var strJson = JsonConvert.SerializeObject(UserInfo);
            ////将json字符串写入json文件中
            Stream stream = await localFolder.OpenStreamForWriteAsync("UserData.json", CreationCollisionOption.ReplaceExisting);
            StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
            sw.WriteLine(strJson);
            sw.Flush();
            sw.Dispose();
            stream.Dispose();
        }
    }
}
