﻿using antdlib.models;
using Nancy;
using Newtonsoft.Json;

namespace Antd.AppConfig {

    public class AppConfigurationModule : NancyModule {

        private readonly AppConfiguration _appConfiguration = new AppConfiguration();

        public AppConfigurationModule() {

            Get["/config"] = _ => {
                var list = _appConfiguration.Get();
                return JsonConvert.SerializeObject(list);
            };

            Post["/config"] = _ => {
                var antdPort = Request.Form.AntdPort;
                var antdUiPort = Request.Form.AntdUiPort;
                var model = new AppConfigurationModel {
                    AntdPort = antdPort,
                    AntdUiPort = antdUiPort
                };
                _appConfiguration.Save(model);
                return HttpStatusCode.OK;
            };
        }
    }
}