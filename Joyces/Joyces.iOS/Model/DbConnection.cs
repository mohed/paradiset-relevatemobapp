using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SQLite.Net;
using UIKit;
using System.IO;
using SQLite.Net.Attributes;

namespace Joyces.iOS.Model
{
    [Table("Token")]
    public class Stock
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
    }



    class DbConnection
    {
        private SQLiteConnection GetConnection()
        {

            var fileName = "Abalon.db3";
           
            var libraryPath = Path.Combine(NSBundle.MainBundle.BundlePath, "Assets");
            var path = Path.Combine(libraryPath, fileName);

            var platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var connection = new SQLiteConnection(platform, path);

            return connection;
        }

        public void SaveRefreshToken(string sRefreshToken)
        {
            SQLiteConnection db = GetConnection();

            var hej2 = db.Query<Stock>("INSERT INTO Token (Id, RefreshToken) VALUES ()");
            var hej = db.Query<Stock>("SELECT Top 1 FROM Token");

            //db.CreateTable<Stock>();

            Stock s = new Stock();
            s.Id = 1;
            s.RefreshToken = sRefreshToken;


            db.Insert(s); // after creating the newStock object
        }
    }
}