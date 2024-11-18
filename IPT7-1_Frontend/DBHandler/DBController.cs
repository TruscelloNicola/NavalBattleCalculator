using MongoDB.Driver;
using IPT71Frontend.DBHandler.Classes;
using IPT7_1_Frontend.DBHandler.Classes;
using IPT7_1_Frontend.Components.Pages;
using System.Collections.Generic;
using System.Collections;
using MongoDB.Bson;

namespace IPT71Frontend.DBHandler
{
    public class DBController
    {
        MongoClient _dbClient;
        private static DBController singleInstance = null;
        IMongoDatabase _db;
        IMongoCollection<Ship> _shipCollection;
        IMongoCollection<SaveContainsShip> _saveToShipCollection;
        IMongoCollection<SaveSlot> _saveCollection;
        IMongoCollection<UserLogin> _loginCollection;
        public DBController(MongoClient mongoclient)
        {
            _dbClient = mongoclient;
            _db = _dbClient.GetDatabase("IPT71DB");
        }
        public static DBController SingleInstance
        {
            get
            {
                if (singleInstance == null)
                {
                    singleInstance = new DBController(new MongoClient("Insert connection string here"));
                    singleInstance._shipCollection = singleInstance._db.GetCollection<Ship>("Ships", null);
                    singleInstance._saveToShipCollection = singleInstance._db.GetCollection<SaveContainsShip>("SaveToShip", null);
                    singleInstance._saveCollection = singleInstance._db.GetCollection<SaveSlot>("SaveSlots", null);
                    singleInstance._loginCollection = singleInstance._db.GetCollection<UserLogin>("Logins", null);
                }
                return singleInstance;
            }
        }
        public Dictionary<string, List<Ship>> ReadAllSortedShips()
        {
            var ships = _shipCollection.Find(Builders<Ship>.Filter.Empty).ToList();

            Dictionary<string, List<Ship>> dictionary = new Dictionary<string, List<Ship>>();
            List<Ship> ScreeningGroup = new List<Ship>();
            List<Ship> BattleLine = new List<Ship>();
            List<Ship> CarrierGroup = new List<Ship>();
            List<Ship> SubmarineWolfpack = new List<Ship>();

            foreach (Ship sh in ships)
            {
                switch (sh.Type)
                {
                    case ("Submarine Wolfpack"):
                        SubmarineWolfpack.Add(sh);
                        break;
                    case ("Carrier Group"):
                        CarrierGroup.Add(sh);
                        break;
                    case ("Battle Line"):
                        BattleLine.Add(sh);
                        break;
                    default:
                        ScreeningGroup.Add(sh);
                        break;
                }
            }

            dictionary.Add("Screening Group", ScreeningGroup);
            dictionary.Add("Battle Line", BattleLine);
            dictionary.Add("Carrier Group", CarrierGroup);
            dictionary.Add("Submarine Wolfpack", SubmarineWolfpack);

            return dictionary;
        }
        public List<Ship> ReadAllShips()
        {
            return _shipCollection.Find(Builders<Ship>.Filter.Empty).ToList();
        }
        public List<UserLogin> ReadAllUserLogins()
        {
            var userLogins = _loginCollection.Find(FilterDefinition<UserLogin>.Empty).ToList();
            var saveSlots = _saveCollection.Find(FilterDefinition<SaveSlot>.Empty).ToList();
            var saveContainsShips = _saveToShipCollection.Find(FilterDefinition<SaveContainsShip>.Empty).ToList();
            var ships = _shipCollection.Find(FilterDefinition<Ship>.Empty).ToList();

            List<UserLogin> logins = new List<UserLogin>();

            foreach (UserLogin login in userLogins)
            {
                var save = saveSlots.Find(e => login.SaveSlotId == e._id);

                if (save != null)
                {
                    save.SaveContainsShips = new List<SaveContainsShip>();

                    foreach (SaveContainsShip instance in saveContainsShips)
                    {
                        if (save._id == instance.SaveSlotId)
                        {
                            var result = ships.Find(e => e._id == instance.ShipId);

                            instance._ship = result;

                            save.SaveContainsShips.Add(instance);
                        }
                    }
                    
                }

                login.SaveSlot = save;

                logins.Add(login);
            }


            return logins;
        }
        public UserLogin ReadUserLogin(string username, string password)
        {
            return _loginCollection.Find(e => e.UserName == username && e.UserPassword == password).First();
        }
        public List<SaveSlot> ReadAllSavesForUser(UserLogin userlogin)
        {
            return _saveCollection.Find(Builders<SaveSlot>.Filter.Eq(e => e._id, userlogin.SaveSlotId)).ToList();
        }
        public List<SaveSlot> ReadAllSaves()
        {
            return _saveCollection.Find(FilterDefinition<SaveSlot>.Empty).ToList();
        }
        public List<SaveContainsShip> ReadAllSavesToShip(SaveSlot saveslot)
        {
            return _saveToShipCollection.Find(Builders<SaveContainsShip>.Filter.Eq(e => e.SaveSlotId, saveslot._id)).ToList();
        }
        public void InsertUserLogin(string username, string password)
        {
            _loginCollection.InsertOne(new UserLogin { UserName = username, UserPassword = password });
        }
        public SaveSlot InsertSave(UserLogin userlogin)
        {
            _saveCollection.InsertOne(new SaveSlot { SaveDate = DateTime.Now.ToString(), SaveName = "SaveForUser" });
            var result = _saveCollection.Find(e => e.SaveName == "SaveForUser").First();

            var filter = Builders<UserLogin>.Filter.Eq(e => e._id, userlogin._id);
            var query = Builders<UserLogin>.Update.Set(e => e.SaveSlotId, result._id);
            _loginCollection.UpdateOne(filter, query);
            query = Builders<UserLogin>.Update.Set(e => e.SaveSlot, result);
            _loginCollection.UpdateOne(filter, query);

            return result;
        }
        public void InsertSaveToShip(List<Ship> shipList, string side, SaveSlot insertedSave)
        {
            var results = shipList.GroupBy(n => n._id).Select(g => new ShipAmount { ShipId = g.Key, Count = g.Count() }).ToList();
            foreach (ShipAmount instance in results)
            {
                SaveContainsShip toInsert = new SaveContainsShip { 
                    ShipId = instance.ShipId, 
                    Amount = instance.Count, 
                    Side = side, SaveSlotId = insertedSave._id, 
                    _saveSlot = insertedSave, 
                    _ship = shipList.Find(e => e._id == instance.ShipId)
                };

                _saveToShipCollection.InsertOne(toInsert);
            }
            
        }
        public void DeleteAllSaves(UserLogin userlogin)
        {
            var selUser = SingleInstance.ReadAllUserLogins().Find(e => e.UserName == userlogin.UserName && e.UserPassword == userlogin.UserPassword);
            _saveToShipCollection.DeleteMany(e => e.SaveSlotId == selUser.SaveSlotId);
            _saveCollection.DeleteOne(e => e._id == selUser.SaveSlotId);
        }
        class ShipAmount
        {
            public ObjectId ShipId { get; set; }
            public int Count { get; set; }
        }
    }
}