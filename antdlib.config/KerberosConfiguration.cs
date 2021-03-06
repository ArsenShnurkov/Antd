﻿using antdlib.common;
using antdlib.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using anthilla.core;
using IoDir = System.IO.Directory;
using Parameter = antdlib.common.Parameter;

namespace antdlib.config {
    public class KerberosConfiguration {

        private readonly KerberosConfigurationModel _serviceModel;

        private readonly string _cfgFile = $"{Parameter.AntdCfgServices}/kerberos.conf";
        private const string ServiceName = "named.service";
        private const string MainFilePath = "/etc/kerberos/kkk.conf";
        private const string MainFilePathBackup = "/etc/kerberos/.kkk.conf";

        public KerberosConfiguration() {
            IoDir.CreateDirectory(Parameter.AntdCfgServices);
            if(!File.Exists(_cfgFile)) {
                _serviceModel = new KerberosConfigurationModel();
            }
            else {
                try {
                    var text = File.ReadAllText(_cfgFile);
                    var obj = JsonConvert.DeserializeObject<KerberosConfigurationModel>(text);
                    _serviceModel = obj;
                }
                catch(Exception) {
                    _serviceModel = new KerberosConfigurationModel();
                }

            }
        }

        public void Save(KerberosConfigurationModel model) {
            var text = JsonConvert.SerializeObject(model, Formatting.Indented);
            FileWithAcl.WriteAllText(_cfgFile, text);
            ConsoleLogger.Log("[kerberos] configuration saved");
        }

        public void Set() {
            Enable();
            Stop();

            #region [    named.conf generation    ]

            if(File.Exists(MainFilePath)) {
                if(File.Exists(MainFilePathBackup)) {
                    File.Delete(MainFilePathBackup);
                }
                File.Copy(MainFilePath, MainFilePathBackup);
            }
            var lines = new List<string>
            {
                "options {"
            };

            FileWithAcl.WriteAllLines(MainFilePath, lines, "644", "root", "wheel");

            #endregion

            Start();
        }

        public bool IsActive() {
            if(!File.Exists(_cfgFile)) {
                return false;
            }
            return _serviceModel != null && _serviceModel.IsActive;
        }

        public KerberosConfigurationModel Get() {
            return _serviceModel;
        }

        public void Enable() {
            _serviceModel.IsActive = true;
            Save(_serviceModel);
            ConsoleLogger.Log("[kerberos] enabled");
        }

        public void Disable() {
            _serviceModel.IsActive = false;
            Save(_serviceModel);
            ConsoleLogger.Log("[kerberos] disabled");
        }

        public void Stop() {
            Systemctl.Stop(ServiceName);
            ConsoleLogger.Log("[kerberos] stop");
        }

        public void Start() {
            if(Systemctl.IsEnabled(ServiceName) == false) {
                Systemctl.Enable(ServiceName);
            }
            if(Systemctl.IsActive(ServiceName) == false) {
                Systemctl.Restart(ServiceName);
            }
            ConsoleLogger.Log("[kerberos] start");
        }
    }
}
