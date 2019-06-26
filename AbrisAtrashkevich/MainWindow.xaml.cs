using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using AbrisAtrashkevich.Model;
using Newtonsoft.Json;


namespace AbrisAtrashkevich
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Arts.ItemsSource = artList;
            this.ArtsChar.ItemsSource = artscharList;
            this.Price.ItemsSource = priceList;
            this.Shop.ItemsSource = shopList;
            this.Barcode.ItemsSource = barcodeList;
        }

        public Visibility TableVisible { get; set; } = Visibility.Collapsed;

        HttpClient client = new HttpClient();


        ObservableCollection<DatumArts> artList = new ObservableCollection<DatumArts>();
        ObservableCollection<DatumArtsChar> artscharList = new ObservableCollection<DatumArtsChar>();
        ObservableCollection<DatumPrice> priceList = new ObservableCollection<DatumPrice>();
        ObservableCollection<DatumBarcode> barcodeList = new ObservableCollection<DatumBarcode>();
        ObservableCollection<DatumShop> shopList = new ObservableCollection<DatumShop>();


        private string _login;
        public string login { get { return _login; } set { if (_login != value) { _login = value; NotifyPropertyChanged(); } } }

        private string _password;
        public string password { get { return _password; } set { if (_password != value) { _password = value; NotifyPropertyChanged(); } } }


        private string _DbID;
        public string DbID { get { return _DbID; } set { if (_DbID != value) { _DbID = value; NotifyPropertyChanged(); } } }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }

        async void GetCookies()
        {
            //689 O
            string url = $"http://demo.abris.site/?demo{DbID}";
            //string url = $"http://demo.abris.site/?demo{account}";
            HttpResponseMessage response = await client.GetAsync(url);
        }

        private void getLoginpass()
        {
            login = login1.Text;
            password = password1.Password;
            DbID = DbId1.Text;
        }

        async Task<string> SendLoginDev()
        {

            var dict = new Dictionary<string, string>();
            string url = "http://demo.abris.site/Server/request.php";
            dict.Add("jsonrpc", "2.0"); ;
            dict.Add("method", "authenticate");
            dict.Add("params", $"[{{\"usename\":\"{login}\",\"passwd\":\"{password}\"}}]");
            dict.Add("client_version", "Dev");
            HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(dict));
            return response.Content.ReadAsStringAsync().Result;
        }

        async Task<string> GetEntityAsync(string entityname)
        {

            var dict = new Dictionary<string, string>();
            string url = "http://demo.abris.site/Server/request.php";
            dict.Add("jsonrpc", "2.0"); ;
            dict.Add("method", "getTableDataPredicate");
            dict.Add("params", $"[{{\"entityName\":\"{entityname}\", \"schemaName\":\"public\"}}]");
            HttpResponseMessage response = await client.PostAsync(url, new FormUrlEncodedContent(dict));
            return response.Content.ReadAsStringAsync().Result;
        }

        private void GetTableData_OnClick(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(1000);
            TableViewer.Visibility = Visibility.Visible;
        }

        private void ClearTableData_OnClick(object sender, RoutedEventArgs e)
        {
            TableViewer.Visibility = Visibility.Collapsed;
        }

        private async void RofloStart()
        {
            GetCookies();
            var dw = await SendLoginDev();
        }

        private async void GetEntity_OnClick(object sender, RoutedEventArgs e)
        {
            getLoginpass();
            RofloStart();
            GetCookies();
            var dw = await SendLoginDev();

            var artsJson = await GetEntityAsync("arts");
            var artscharJson = await GetEntityAsync("artschar");
            var priceJson = await GetEntityAsync("prices");
            var shopJson = await GetEntityAsync("shop");
            var barcodesJson = await GetEntityAsync("barcodes");

            var arts = JsonConvert.DeserializeObject<EntityJson<DatumArts>>(artsJson);
            var artschar = JsonConvert.DeserializeObject<EntityJson<DatumArtsChar>>(artscharJson);
            var price = JsonConvert.DeserializeObject<EntityJson<DatumPrice>>(priceJson);
            var shop = JsonConvert.DeserializeObject<EntityJson<DatumShop>>(shopJson);
            var barcodes = JsonConvert.DeserializeObject<EntityJson<DatumBarcode>>(barcodesJson);

            if (arts != null && arts.Result != null)
            {
                artList.Clear();
                foreach (var item in arts.Result.Data)
                {
                    artList.Add(item);
                }
            }

            if (artschar != null && artschar.Result != null)
            {
                artscharList.Clear();
                foreach (var item in artschar.Result.Data)
                {
                    artscharList.Add(item);
                }
            }

            if (price != null && price.Result != null)
            {
                priceList.Clear();
                foreach (var item in price.Result.Data)
                {
                    priceList.Add(item);
                }
            }

            if (shop != null && shop.Result != null)
            {
                shopList.Clear();
                foreach (var item in shop.Result.Data)
                {
                    shopList.Add(item);
                }
            }

            if (barcodes != null && barcodes.Result != null)
            {
                barcodeList.Clear();
                foreach (var item in barcodes.Result.Data)
                {
                    barcodeList.Add(item);
                }
            }

            var fds = 0;

        }

    }
}