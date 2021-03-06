﻿using System;
using System.IO;
using antdlib.common;
using anthilla.core;
using Parameter = antdlib.common.Parameter;

namespace antdlib.config {
    public class ManageMaster {

        public string FilePath { get; }
        public string FilePathBackup { get; }
        public string Name { get; private set; }
        public string Password { get; private set; }

        public ManageMaster() {
            FilePath = $"{Parameter.AntdCfg}/services/login.conf";
            FilePathBackup = $"{Parameter.AntdCfg}/services/login.conf.bck";
            Name = LoadHostModel().Item1;
            Password = LoadHostModel().Item2;
        }

        private Tuple<string, string> LoadHostModel() {
            if(!File.Exists(FilePath)) {
                return new Tuple<string, string>("master", "250841977126621-227309917-30068297103565105222953-2509920183-30734-3192717661-14017");
            }
            try {
                var text = File.ReadAllText(FilePath);
                var arr = text.Split(new[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);
                if(arr.Length == 0) {
                    return new Tuple<string, string>("master", "250841977126621-227309917-30068297103565105222953-2509920183-30734-3192717661-14017");
                }
                if(arr.Length == 1) {
                    return new Tuple<string, string>("master", arr[0]);
                }
                if(arr.Length == 2) {
                    return new Tuple<string, string>(arr[0], arr[1]);
                }
                return new Tuple<string, string>("master", "250841977126621-227309917-30068297103565105222953-2509920183-30734-3192717661-14017");
            }
            catch(Exception) {
                return new Tuple<string, string>("master", "250841977126621-227309917-30068297103565105222953-2509920183-30734-3192717661-14017");
            }
        }

        public void Setup() {
            if(!File.Exists(FilePath)) {
                FileWithAcl.WriteAllText(FilePath, $"{Name} {Password}", "644", "root", "wheel");
            }
        }

        public void Export(string name, string password) {
            FileWithAcl.WriteAllText(FilePath, $"{name} {Encryption.XHash(password)}", "644", "root", "wheel");
        }

        public void ChangeName(string name) {
            Name = LoadHostModel().Item1;
            Password = LoadHostModel().Item2;
            Name = name;
            Export(Name, Password);
        }

        public void ChangePassword(string password) {
            Name = LoadHostModel().Item1;
            Password = LoadHostModel().Item2;
            Password = password;
            Export(Name, Password);
        }
    }
}
