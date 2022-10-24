using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CaseCreator
{
    /// <summary>
    /// Interaktionslogik für ItemBox.xaml
    /// </summary>
    public partial class ItemBox : UserControl
    {
        public string yeaid { get; set; }
        public string priceyea { get; set; }
        public string nameyea { get; set; }

        

        public ItemBox(bool showbox ,string id, string price, string name)
        {
            InitializeComponent();
            limitedyes.Source = new BitmapImage(new Uri($"https://www.roblox.com/asset-thumbnail/image?assetId={id}&width=50&height=50&format=png"));
            raplabel.Content = $"RAP: {price}";
            namelabel.Content = $"Name: {name}";
            if (showbox)
            {
                coolbox.Visibility = Visibility.Hidden;
            }
            yeaid = id;
            priceyea = price;
            nameyea = name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AddedItemsPanel.Children.Add(new ItemBox(true ,yeaid, priceyea, nameyea)) ;
        }
    }
}
