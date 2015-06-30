﻿///-------------------------------------------------------------------------------------
///     Copyright (c) 2014, Anthilla S.r.l. (http://www..com)
///     All rights reserved.
///
///     Redistribution and use in source and binary forms, with or without
///     modification, are permitted provided that the following conditions are met:
///         * Redistributions of source code must retain the above copyright
///           notice, this list of conditions and the following disclaimer.
///         * Redistributions in binary form must reproduce the above copyright
///           notice, this list of conditions and the following disclaimer in the
///           documentation and/or other materials provided with the distribution.
///         * Neither the name of the Anthilla S.r.l. nor the
///           names of its contributors may be used to endorse or promote products
///           derived from this software without specific prior written permission.
///
///     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
///     ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
///     WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///     DISCLAIMED. IN NO EVENT SHALL ANTHILLA S.R.L. BE LIABLE FOR ANY
///     DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
///     (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
///     LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
///     ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
///     (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
///     SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
///
///     20141110
///-------------------------------------------------------------------------------------

using Antd.Apps;
using Antd.UnitFiles;
using Nancy;
using Nancy.Security;
using System.Dynamic;

namespace Antd {

    public class AppsModule : NancyModule {

        public AppsModule()
            : base("/apps") {
            this.RequiresAuthentication();

            Get["/"] = x => {
                dynamic vmod = new ExpandoObject();
                if (AppsManagement.Detect(".squashfs") == false) {
                    ViewBag.Message = "No app detected in " + Folder.Apps;
                    vmod.AppExists = false;
                }
                else if (AnthillaSP.Setting.CheckSquash() == false) {
                    ViewBag.Message = "No squashfs detected in " + Folder.Apps;
                    vmod.AppExists = false;
                }
                else {
                    vmod.AppExists = true;
                }
                return View["_page-apps", vmod];
            };


            Get["/set/anthillasp"] = x => {
                //1 check lo squash, se esiste lo monta, se no lo crea
                if (AnthillaSP.Setting.CheckSquash() == false) {
                    //1-b check la cartella, se esiste crea lo squash e lo monta, altrimenti ritorna errore
                    if (AppsManagement.Detect(".squashfs") != false) {
                        //crea lo squash
                        //mksquashfs /framework/Anthilla/Antd /mnt/cdrom/DIR_antd_2015.squash.xz  -comp xz -Xbcj x86 -Xdict-size 75%  
                        AnthillaSP.Setting.CreateSquash();
                        //monta lo squash
                        AnthillaSP.Setting.MountSquash();
                        //crea le units
                        AnthillaSP.SetAndRun();
                        return Response.AsJson(true);
                    }
                    else {
                        return Response.AsJson("Error!");
                    }
                }
                else {
                    //monta lo squash
                    AnthillaSP.Setting.MountSquash();
                    //crea le units
                    AnthillaSP.SetAndRun();
                    return Response.AsJson(true);
                }
            };

            Get["/launch"] = x => {
                ConsoleLogger.Log(">> App >> AnthillaSP");
                ConsoleLogger.Log(">> Check squashfs");
                var b = AnthillaSP.Setting.CheckSquash();
                if (b == false) {
                    ConsoleLogger.Warn(">> Squashfs does not exist!");
                    return Response.AsJson(b);
                }
                else {
                    ConsoleLogger.Log(">> Mount squashfs in /framework/anthillasp");
                    AnthillaSP.Setting.MountSquash();
                    ConsoleLogger.Log(">> Create AnthillaSP units in /mnt/cdrom/Overlay/anthillasp/");
                    AnthillaSP.SetAndRun();
                    return Response.AsJson(true);
                }
            };

            Get["/start/sp"] = x => {
                var start = Command.Launch("mono", "/framework/anthillasp/anthillasp/AnthillaSP.exe &").output;
                return Response.AsJson(start);
            };

            Get["/start/server"] = x => {
                var start = Command.Launch("mono", "/framework/anthillasp/anthillaserver/AnthillaServer.exe &").output;
                return Response.AsJson(start);
            };

            Get["/stop/sp"] = x => {
                var stop = Systemctl.Stop("anthillasp-launcher.service");
                return Response.AsJson(stop);
            };

            Get["/stop/server"] = x => {
                var stop = Systemctl.Stop("anthillaserver-launcher.service");
                return Response.AsJson(stop);
            };

            Get["/status/sp"] = x => {
                var status = Systemctl.Status("anthillasp-launcher.service");
                return Response.AsJson(status);
            };

            Get["/status/server"] = x => {
                var status = Systemctl.Status("anthillaserver-launcher.service");
                return Response.AsJson(status);
            };
        }
    }
}