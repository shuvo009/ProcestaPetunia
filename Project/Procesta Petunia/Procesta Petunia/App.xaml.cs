//Title :           Petunia Start App.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Startup works are hare.  
using System;
using System.Windows;
namespace Procesta_Petunia
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Startup Parameter
        private void PetuniaStart(object sender, StartupEventArgs e)
        {
            SplashScreen petuniaSplash = new SplashScreen("SplashScreen.png");
            petuniaSplash.Show(false);
            if (e.Args.Length>0)
            {
                switch (e.Args[0])
                {
                    case "-h":
                        //PetuniaVariables.SetAllVariables();
                        //LoadLoaclData();
                        // Minimize System tray code here. 
                        petuniaSplash.Close(TimeSpan.FromSeconds(0.3));
                        break;
                    case "-b":
                        //PetuniaVariables.SetAllVariables();
                        //LoadLoaclData();
                        petuniaSplash.Close(TimeSpan.FromSeconds(0.3));
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }
            petuniaSplash.Close(TimeSpan.FromSeconds(0.9));
        }
    }
}
