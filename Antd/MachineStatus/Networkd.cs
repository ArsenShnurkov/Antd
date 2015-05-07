﻿///-------------------------------------------------------------------------------------
///     Copyright (c) 2014, Anthilla S.r.l. (http://www.anthilla.com)
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

using Antd.UnitFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Antd.Status {
    public class Networkd {

        private static void EnableRequiredServices() {
            Systemctl.Start("systemd-networkd.service");
            Systemctl.Start("systemd-resolved.service");
            Systemctl.Enable("systemd-networkd.service");
            Systemctl.Enable("systemd-resolved.service");
        }

        //public static void MountDir() { }

        //public string matchName { get; set; }
        //public string matchHost { get; set; }
        //public string matchVirtualization { get; set; }
        //public string networkDHCP { get; set; }
        //public string networkDNS { get; set; }
        //public string networkBridge { get; set; }
        //public string networkIPForward { get; set; }
        //public string addressAddress { get; set; }
        //public string routeGateway { get; set; }

        public static void CreateUnit(string filename, string matchName, string matchHost, string matchVirtualization,
                                      string networkDHCP, string networkDNS, string networkBridge, string networkIPForward,
                                      string addressAddress, string routeGateway) {
            Directory.CreateDirectory("/cfg/network");
            string path = Path.Combine("/cfg/network", filename + ".network");
            if (File.Exists(path)) {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path)) {
                sw.WriteLine("[Match]");
                sw.WriteLine("Name=" + matchName);
                if (matchHost != "") { sw.WriteLine("Host=" + matchHost); }
                if (matchVirtualization != "") { sw.WriteLine("Virtualization=" + matchVirtualization); }
                sw.WriteLine("");
                sw.WriteLine("[Network]");
                if (networkDHCP != "") sw.WriteLine("DHCP=" + networkDHCP);
                if (networkDNS != "") sw.WriteLine("DNS=" + networkDNS);
                if (networkBridge != "") sw.WriteLine("Bridge=" + networkBridge);
                if (networkIPForward != "") sw.WriteLine("IPForward=" + networkIPForward);
                if (addressAddress != "") sw.WriteLine("");
                if (addressAddress != "") sw.WriteLine("[Address]");
                if (addressAddress != "") sw.WriteLine("Address=" + addressAddress);
                if (routeGateway != "") sw.WriteLine("");
                if (routeGateway != "") sw.WriteLine("[Route]");
                if (routeGateway != "") sw.WriteLine("Gateway=" + routeGateway);

                sw.WriteLine("");
            }
        }

        //[Match]
        //Name=enp1s0

        //[Network]
        //DNS=10.1.10.1

        //[Address]
        //Address=10.1.10.9/24

        //[Route]
        //Gateway=10.1.10.1

        public static void CreateFirstUnit() {
            CreateUnit("antd", "eth0", "", "", "", "10.1.10.1", "", "", "10.1.10.9/24", "10.1.10.1");
        }

        public static string ReadAntdUnit() {
            string path = Path.Combine("/cfg/network", "antd.network");
            string text = File.ReadAllText(path);
            return text;
        }
    }
}
