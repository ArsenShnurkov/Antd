﻿//-------------------------------------------------------------------------------------
//     Copyright (c) 2014, Anthilla S.r.l. (http://www.anthilla.com)
//     All rights reserved.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted provided that the following conditions are met:
//         * Redistributions of source code must retain the above copyright
//           notice, this list of conditions and the following disclaimer.
//         * Redistributions in binary form must reproduce the above copyright
//           notice, this list of conditions and the following disclaimer in the
//           documentation and/or other materials provided with the distribution.
//         * Neither the name of the Anthilla S.r.l. nor the
//           names of its contributors may be used to endorse or promote products
//           derived from this software without specific prior written permission.
//
//     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//     ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//     WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//     DISCLAIMED. IN NO EVENT SHALL ANTHILLA S.R.L. BE LIABLE FOR ANY
//     DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//     (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//     LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//     ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//     (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//     SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//     20141110
//-------------------------------------------------------------------------------------

using System.Linq;
using antdlib.common;
using antdlib.models;
using antdlib.views;
using Antd.Apps;
using Antd.Database;
using Nancy;
using Newtonsoft.Json;

namespace Antd.ServerModules {
    public class AppsManagementModule : NancyModule {

        public AppsManagementModule() {
            Get["/apps/management"] = x => {
                var applicationRepository = new ApplicationRepository();
                var model = new PageAppsManagementModel {
                    AppList = applicationRepository.GetAll().Select(_ => new ApplicationModel(_))
                };
                return JsonConvert.SerializeObject(model);
            };

            Post["/apps/setup"] = x => {
                string app = Request.Form.AppName;
                if(string.IsNullOrEmpty(app)) {
                    return HttpStatusCode.InternalServerError;
                }
                var appsManagement = new AppsManagement();
                appsManagement.Setup(app);
                return HttpStatusCode.OK;
            };

            Get["/apps/status/{unit}"] = x => {
                string unitName = x.unit;
                var status = Systemctl.Status(unitName);
                return Response.AsJson(status);
            };

            Get["/apps/active/{unit}"] = x => {
                string unitName = x.unit;
                var status = Systemctl.IsActive(unitName);
                return Response.AsJson(status ? "active" : "inactive");
            };

            Post["/apps/restart"] = x => {
                string unitName = Request.Form.Name;
                Systemctl.Restart(unitName);
                return HttpStatusCode.OK;
            };

            Post["/apps/stop"] = x => {
                string unitName = Request.Form.Name;
                Systemctl.Stop(unitName);
                return HttpStatusCode.OK;
            };
        }
    }
}