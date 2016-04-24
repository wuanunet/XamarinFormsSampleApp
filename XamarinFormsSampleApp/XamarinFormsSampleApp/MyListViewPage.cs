using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsSampleApp.MyServices;

namespace XamarinFormsSampleApp
{
    public class MyListViewPage : ContentPage
    {
        private Button searchButton;
        private Entry cityEntry;
        private Entry areaEntry;
        private List<FamilyStore> familyStoreDataList;
        private readonly WebApiServices myWebApiService;

        public MyListViewPage(string title)
        {
            familyStoreDataList = new List<FamilyStore>();
            myWebApiService = new WebApiServices();

            searchButton = new Button { Text = "Search" };
            cityEntry = new Entry { Placeholder = "請輸入城市名稱" };
            areaEntry = new Entry { Placeholder = "請輸入行政區域" };

            if (string.IsNullOrWhiteSpace(cityEntry.Text))
            {
                cityEntry.Text = "台北市";
            }
            if (string.IsNullOrWhiteSpace(areaEntry.Text))
            {
                areaEntry.Text = "大安區";
            }

            searchButton.Clicked += async (sender, e) =>
            {
                familyStoreDataList = await GetFamilyStoreData(cityEntry.Text, areaEntry.Text);

                if (familyStoreDataList.Count > 0)
                {
                    var sourceData = familyStoreDataList.Select(s => new StoreData
                    {
                        Name = s.NAME,
                        Address = s.addr,
                        Tel = s.TEL
                    }).ToList();

                    BindListView(sourceData);
                }

                await DisplayAlert("資料結果為", familyStoreDataList.Count + " 筆", "知道了");
                Debug.WriteLine(familyStoreDataList.Count);
            };

            Title = title;

            var defaultData = new List<StoreData>
            {
                new StoreData {Name = "全家大安店", Address = "台北市大安區大安路一段20號", Tel = "02-27117896"},
                new StoreData {Name = "全家仁慈店", Address = "台北市大安區仁愛路四段48巷6號", Tel = "02-27089002"},
                new StoreData {Name = "全家明曜店", Address = "台北市大安區仁愛路四段151巷34號", Tel = "02-27780326"},
                new StoreData {Name = "全家國泰店", Address = "台北市大安區仁愛路四段266巷15弄10號", Tel = "02-27542056"},
                new StoreData {Name = "全家忠愛店", Address = "台北市大安區仁愛路四段27巷43號", Tel = "02-27314580"}
            };

            BindListView(defaultData);
        }

        private async Task<List<FamilyStore>> GetFamilyStoreData(string city, string area)
        {
            var result = new List<FamilyStore>();

            var resultData = await myWebApiService.GetDataAsync(cityEntry.Text, areaEntry.Text);

            if (string.IsNullOrWhiteSpace(resultData) == false)
            {
                result = JsonConvert.DeserializeObject<List<FamilyStore>>(resultData);
            }

            return result;
        }

        private void BindListView(List<StoreData> sourceData)
        {
            var listView = new ListView
            {
                IsPullToRefreshEnabled = true,
                RowHeight = 80,
                ItemsSource = sourceData,
                ItemTemplate = new DataTemplate(typeof(MyListViewCell))
            };

            listView.ItemTapped += (sender, e) =>
            {
                var baseUrl = "https://www.google.com.tw/maps/place/";
                var storeData = e.Item as StoreData;

                if (storeData != null)
                {
                    Device.OpenUri(new Uri($"{baseUrl}{storeData.Address}"));
                }

                ((ListView)sender).SelectedItem = null;
            };

            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    cityEntry,
                    areaEntry,
                    searchButton,
                    new Label
                    {
                        HorizontalTextAlignment= TextAlignment.Center,
                        Text = Title,
                        FontSize = 30
                    },
                    listView
                }
            };
        }
    }

    public class StoreData
    {
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
    }

    public class FamilyStore
    {
        public string NAME { get; set; }
        public string TEL { get; set; }
        public string POSTel { get; set; }
        public double px { get; set; }
        public double py { get; set; }
        public string addr { get; set; }
        public double SERID { get; set; }
        public string pkey { get; set; }
        public string oldpkey { get; set; }
        public string post { get; set; }
        public string all { get; set; }
        public string road { get; set; }
        public object twoice { get; set; }
    }
}