using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace EZE_BASKET
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        int framecount = 0; MySqlDataReader reader; Grid grid = new Grid(); MySqlDataReader reader2;
        Label[] qty = new Label[50]; int qtycount = 0;
        Frame[] f = new Frame[50];
        Grid[] dr = new Grid[50]; Label commentlabel = new Label();
        Editor[] ed = new Editor[50];
        MySqlConnection con2 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");

        public Page1()
        {
            InitializeComponent();

            grid.ColumnSpacing = 0; grid.RowSpacing = 0; grid.Margin = 0; grid.Padding = 0;
            MySqlConnection con1 = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            string strr = "SELECT * FROM product_table WHERE bar_code= '" + Category.code + "'";
            MySqlCommand cmd = new MySqlCommand(strr, con1);
            con1.Open();
            reader = cmd.ExecuteReader();
            reader.Read();


            one(); two(); three();

        }
        protected override void OnAppearing()
        {

        }
        private void one()
        {
            var lab = new Label() { FontSize = 20, TextColor = Color.Black, FontAttributes = FontAttributes.Bold };
            var img = new Image() { HorizontalOptions = LayoutOptions.Fill, Aspect = Aspect.AspectFit };
            var fr = new Frame() { HorizontalOptions = LayoutOptions.Fill, CornerRadius = 9, Padding = 0, Margin = 0, BorderColor = Color.LightBlue };
            lab.Text = "#" + reader["name"].ToString();
            img.Source = ImageSource.FromStream(() =>
            {
                byte[] imm = (byte[])reader["image"];

                MemoryStream j = new MemoryStream(imm);
                return j;
            });
            fisrtstack.Children.Add(lab);
            fr.Content = img; fisrtstack.Children.Add(fr);
        }
        private void two()
        {

            grid.RowDefinitions.Add(new RowDefinition { Height = 80 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 120 });
            StackLayout st = new StackLayout() { Spacing = 2, Margin = 0, Padding = 2, HeightRequest = 80 };
            Label lb = new Label() { FontSize = 15, FontAttributes = FontAttributes.Bold };
            lb.Text = reader["price"].ToString();
            Label lb2 = new Label() { FontSize = 12 }; lb2.Text = reader["description"].ToString();
            st.Children.Add(lb);
            st.Children.Add(lb2);
            StackLayout g = new StackLayout() { Padding = 0, Margin = 0, Spacing = 5, HorizontalOptions = LayoutOptions.Fill };

            Button b = new Button() { BackgroundColor = Color.DarkSlateBlue, TextColor = Color.White, CornerRadius = 10, Text = "Add to cart", HeightRequest = 40, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Start }; b.Clicked += new System.EventHandler(this.buttonclickb);
            StackLayout stk = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 7, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };
            Button b1 = new Button() { BackgroundColor = Color.DarkOrange, TextColor = Color.White, CornerRadius = 10, Text = "-", Padding = 0, HeightRequest = 40, FontSize = 9, HorizontalOptions = LayoutOptions.Start, WidthRequest = 50, VerticalOptions = LayoutOptions.Fill, ClassId = qtycount.ToString() };
            b1.Clicked += new System.EventHandler(this.buttonclickb1);
            qty[qtycount] = new Label() { FontSize = 12, Text = "0", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, };
            Button b2 = new Button() { BackgroundColor = Color.LimeGreen, TextColor = Color.White, CornerRadius = 10, Padding = 0, Text = "+", FontSize = 9, HeightRequest = 40, HorizontalOptions = LayoutOptions.End, WidthRequest = 50, VerticalOptions = LayoutOptions.Fill, ClassId = qtycount.ToString() };
            b2.Clicked += new System.EventHandler(this.buttonclickb2);
            stk.Children.Add(b1); stk.Children.Add(qty[qtycount]); stk.Children.Add(b2);
            g.Children.Add(b); g.Children.Add(stk);
            grid.Children.Add(st, 0, 0); grid.Children.Add(g, 1, 0);
            fisrtstack.Children.Add(grid); qtycount++;
        }
        private void three()
        {
            StackLayout st1 = new StackLayout() { Padding = 0, Spacing = 0, Margin = 0, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Fill, BackgroundColor = Color.LightBlue };

            Button b1 = new Button(); Button b2 = new Button(); Button b3 = new Button();
            b1.Text = "product details"; b1.ClassId = reader["name"].ToString(); b2.Text = "specifications"; b2.ClassId = reader["name"].ToString(); b3.Text = "refund policy"; b3.ClassId = qtycount.ToString();
            b3.HorizontalOptions = LayoutOptions.Fill;

            b1.StyleId = framecount.ToString();
            b2.StyleId = framecount.ToString();
            b3.StyleId = framecount.ToString();
            b3.HorizontalOptions = LayoutOptions.End;
            b1.Clicked += new System.EventHandler(this.buttonclickj1);
            b2.Clicked += new System.EventHandler(this.buttonclickj2);
            b3.Clicked += new System.EventHandler(this.buttonclickj3);
            st1.Children.Add(b1); st1.Children.Add(b2); st1.Children.Add(b3);
            Label l = new Label() { FontSize = 14, TextColor = Color.Black };

            l.Text = reader["details"].ToString();

            f[framecount] = new Frame() { BorderColor = Color.LightBlue, Padding = 0, Margin = 0, HorizontalOptions = LayoutOptions.Fill };
            f[framecount].Content = l;
            fisrtstack.Children.Add(st1);
            fisrtstack.Children.Add(f[framecount]);

        }
       
        private void buttonclickbr(object sender, EventArgs e)
        {
            Button bm = (Button)sender;
            int r = Int32.Parse(bm.StyleId.ToString());

            StackLayout kl = new StackLayout() { Padding = 0, Spacing = 0, Margin = 0 };
            Label ll = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 15, Text = logincustomer.namestring };
            Label lll = new Label() { FontSize = 11, Text = ed[r].Text };
            kl.Children.Add(ll); kl.Children.Add(lll);
            dr[r].Children.Add(logincustomer.profile, 0, 0); dr[framecount].Children.Add(kl, 1, 0);
        }
        private void buttonclickj1(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.StyleId.ToString());
            string ss = "SELECT details FROM product_table WHERE name= '" + k.ClassId.ToString() + "'";
            MySqlConnection co = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            MySqlCommand cc = new MySqlCommand(ss, co);

            co.Open();
            MySqlDataReader rer;

            rer = cc.ExecuteReader(); rer.Read();
            Label m = new Label(); m.Text = rer["details"].ToString();
            f[asdf].Content = null; f[asdf].Content = m;
            co.Close();
        }
        private void buttonclickj2(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.StyleId.ToString());
            string ss = "SELECT specification FROM product_table WHERE name= '" + k.ClassId.ToString() + "'";
            MySqlConnection co = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            MySqlCommand cc = new MySqlCommand(ss, co);

            co.Open();
            MySqlDataReader rer;

            rer = cc.ExecuteReader(); rer.Read();
            Label m = new Label(); m.Text = rer["specification"].ToString();//specification
            f[asdf].Content = null; f[asdf].Content = m;
            co.Close();
        }
        private void buttonclickj3(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.StyleId.ToString());
            string ss = "SELECT refund FROM pruduct_table WHERE name= '" + k.ClassId.ToString() + "'";
            MySqlConnection co = new MySqlConnection("Server=db4free.net;port=3306;database=jafferpp;User Id=jafferpp;Password=58b31c63;charset=utf8;Connection Timeout=40");
            MySqlCommand cc = new MySqlCommand(ss, co);

            co.Open();
            MySqlDataReader rer;

            rer = cc.ExecuteReader(); rer.Read();
            Label m = new Label(); m.Text = rer["refund"].ToString();//refund
            f[asdf].Content = null; f[asdf].Content = m;
            co.Close();
        }
        private void buttonclickb(object sender, EventArgs e)//adto cart buton
        {
            Button k = (Button)sender;

        }
        private void buttonclickb1(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.ClassId.ToString());
            int a = Int32.Parse(qty[asdf].Text);
            if (a != 0)
            {
                a--;
            }
            qty[asdf].Text = a.ToString();

        }
        private void buttonclickb2(object sender, EventArgs e)
        {
            Button k = (Button)sender;
            int asdf = Int32.Parse(k.ClassId.ToString());
            int a = Int32.Parse(qty[asdf].Text);

            a++;

            qty[asdf].Text = a.ToString();
        }
    }
}