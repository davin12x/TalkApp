using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Windows;
using System.Diagnostics;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ParseStarterProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private object color;

        public MainPage()
        {
            this.InitializeComponent();
            receiveData();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached. The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)//Receiving messageBox
        {
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            //((App)Application.Current).runMe()
            sendData();
            receiveData();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
        public async void sendData()
        {
            string getMessage = "Lalit: "+this.textBox.Text + "\n";
            this.textBox.Text = "";
            textBox1.Foreground = new SolidColorBrush(Colors.Blue);
            //textBox1.Text = getMessage;//setting message
            var BAGGA = new ParseObject("ChatMessage");
            BAGGA["Bagga"] = "Bagga";
            BAGGA["Color"] = "blue";
            BAGGA["Message"] =getMessage;
            await BAGGA.SaveAsync();
        
        }
        public async void receiveData()
        {
            /*   ParseQuery<ParseObject> query = ParseObject.GetQuery("TestObject");
               ParseObject testObject = await query.GetAsync("aK6dx8B1ry");
               string Score = testObject.Get<string>("bagga");
               string objectId = testObject.ObjectId;
               DateTime? updatedAt = testObject.UpdatedAt;
               DateTime? createdAt = testObject.CreatedAt;
               await testObject.FetchAsync();
               //Debug.WriteLine(createdAt);
               textBox1.Text = Score;*/
            //var query = ParseObject.GetQuery("ChatMessage").OrderBy("createdAt").Limit(100);
            this.textBox1.Text = "";
            var query = from tabledata in ParseObject.GetQuery("ChatMessage")
                        orderby tabledata.Get<DateTime>("createdAt") ascending
                        select tabledata;
            string[] getData = new string[20];
            int count = 0;
            await query.FindAsync().ContinueWith(t =>
             {
                 IEnumerable<ParseObject> results = t.Result;
                 foreach (var obj in results)
                 {
                     getData[count] = obj.Get<string>("Message");
                     Debug.WriteLine(getData[count]);     
                     count = count + 1;
                  }   
             });
            for(int i=0;i<count;i++)//Printing
            {
                textBox1.Foreground = new SolidColorBrush(Colors.Blue);
                textBox1.Text += getData[i].ToString()+"\n";
            }
        }
    }
}