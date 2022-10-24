using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace CaseCreator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setup();
        }

        public async void setup()
        {
            string items_json;
            using (var Client = new WebClient { Proxy = null })
            {
                Client.Headers["user-agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                items_json = await Client.DownloadStringTaskAsync($"https://www.rolimons.com/itemapi/itemdetails");
            }
            dynamic data = JsonConvert.DeserializeObject(items_json);

            var items = data.items;
            foreach (var current_item in items) 
            {
                ItemsPanel.Children.Add(new ItemBox(false ,current_item.Name, current_item.Value[2].ToString(), current_item.Value[0].ToString()));

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double fullprice = 0;
            double total_chances = 0;
            List<Item> ya = new List<Item>();
            
            foreach (var currentelement in AddedItemsPanel.Children)
            {
                var current_element = currentelement as ItemBox;

                var percentage = double.Parse(current_element.yestextbox.Text);
                var prize_value = (percentage / 100) * double.Parse(current_element.priceyea);

                fullprice = fullprice + prize_value;

                var new_item = new Item
                {
                    name = current_element.nameyea,
                    value = int.Parse(current_element.priceyea),
                    image = $"https://www.roblox.com/asset-thumbnail/image?assetId={current_element.yeaid}&width=420&height=420&format=png",
                    chance = int.Parse(current_element.yestextbox.Text),
                };

                total_chances = total_chances + percentage;
                ya.Add(new_item);
                
            }


            fullprice = fullprice + 0.07 * fullprice;
            var new_case = new Case_Root
            {
                name = namebox.Text,
                price = (int)fullprice,
                items = ya
            };

            if(total_chances != 100)
            {
                MessageBox.Show("Your Chances dont add up to 100!");
            }

            string output = JsonConvert.SerializeObject(new_case);
            Clipboard.SetText(output);
            MessageBox.Show("Copied your cases data in your clipboard");

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ItemsPanel.Children.Clear();
            string items_json;
            using (var Client = new WebClient { Proxy = null })
            {
                Client.Headers["user-agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                items_json = await Client.DownloadStringTaskAsync($"https://www.rolimons.com/itemapi/itemdetails");
            }
            dynamic data = JsonConvert.DeserializeObject(items_json);

            var items = data.items;
            foreach (var current_item in items)
            {
                string text = current_item.Value[0].ToString();
                if (text.Contains(searchbox.Text))
                {
                    ItemsPanel.Children.Add(new ItemBox(false, current_item.Name, current_item.Value[2].ToString(), current_item.Value[0].ToString()));

                }
                

            }

        }
    }
}
