﻿namespace WebAPI_test_1.Settings
{
    public class MongoDBSettings
    {
        public string Host {  get; set; }
        public int Port { get; set; }
        public string User {  get; set; }
        public string Password { get; set; }

        public string ConnectionString
        {
            get
            {
                return $"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
    }
}
