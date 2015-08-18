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

using antdlib.CommandManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace antdlib.CCTable {
    public class CCTableRepository {

        public static List<CCTableModel> GetAll() {
            var list = DeNSo.Session.New.Get<CCTableModel>(c => c != null).ToList();
            foreach (var item in list) {
                item.Content = GetRows(item.Guid);
            }
            return list;
        }

        public static List<CCTableModel> GetAllByContext(string context) {
            var list = DeNSo.Session.New.Get<CCTableModel>(c => c != null && c.Context == context).ToList();
            foreach (var item in list) {
                item.Content = GetRows(item.Guid);
            }
            return list;
        }

        public static CCTableModel GetByGuid(string guid) {
            var cc = DeNSo.Session.New.Get<CCTableModel>(c => c != null && c.Guid == guid).FirstOrDefault();
            cc.Content = GetRows(cc.Guid);
            return cc;
        }

        public static List<CCTableRowModel> GetRows(string guid) {
            var list = DeNSo.Session.New.Get<CCTableRowModel>(c => c != null && c.TableGuid == guid).ToList();
            foreach (var i in list) {
                var f = i.InputCommand.GetFirstString();
                var a = i.InputCommand.GetAllStringsButFirst();
                var b = "";
                var c = a != null || a != "" ? a : b;
                i.ValueResult = Terminal.Execute(f + " " + c);
            }
            return list;
        }

        public static void CreateTable(string alias, string type, string context) {
            var model = new CCTableModel {
                _Id = Guid.NewGuid().ToString(),
                Guid = Guid.NewGuid().ToString(),
                Alias = alias.UppercaseAllFirstLetters(),
                Context = context
            };
            model.Type = GetTableType(type);
            DeNSo.Session.New.Set(model);
        }

        public static void CreateRow(string tableGuid, string tableName, string label, string inputType,
            string inputLabel, string inputCommand, string notes,
            CCTableFlags.OsiLevel flagOsi, CCTableFlags.CommandFunction flagFunction) {
            var model = new CCTableRowModel {
                _Id = Guid.NewGuid().ToString(),
                Guid = Guid.NewGuid().ToString(),
                NUid = UID.ShortGuid,
                TableGuid = tableGuid,
                Label = label,
                InputType = inputType,
                InputLabel = inputLabel,
                InputCommand = inputCommand,
                Notes = notes,
                FlagOsi = flagOsi,
                FlagCommandFunction = flagFunction
            };
            model.HtmlInputID = "New" + tableName.UppercaseAllFirstLetters().RemoveWhiteSpace() + model.Label.UppercaseAllFirstLetters().RemoveWhiteSpace();
            model.HtmlSumbitID = "Update" + tableName.UppercaseAllFirstLetters().RemoveWhiteSpace() + model.Label.UppercaseAllFirstLetters().RemoveWhiteSpace();
            DeNSo.Session.New.Set(model);
        }

        public static void CreateRowDataView(string tableGuid, string tableName, string label, string inputCommand, string result) {
            var model = new CCTableRowModel {
                _Id = Guid.NewGuid().ToString(),
                Guid = Guid.NewGuid().ToString(),
                NUid = UID.ShortGuid,
                TableGuid = tableGuid,
                Label = label,
                InputCommand = inputCommand,
                ValueResult = result,
                MapRules = new List<CCTableRowMap>() { },
                HasMap = false
            };
            model.ValueResultArray = result.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            model.HtmlInputID = "New" + tableName.UppercaseAllFirstLetters().RemoveWhiteSpace() + model.Label.UppercaseAllFirstLetters().RemoveWhiteSpace();
            model.HtmlSumbitID = "Update" + tableName.UppercaseAllFirstLetters().RemoveWhiteSpace() + model.Label.UppercaseAllFirstLetters().RemoveWhiteSpace();
            DeNSo.Session.New.Set(model);
        }

        public static void CreateRowConf(string tableGuid, string tableName, string file) {
            var model = new CCTableRowModel {
                _Id = Guid.NewGuid().ToString(),
                Guid = Guid.NewGuid().ToString(),
                NUid = UID.ShortGuid,
                TableGuid = tableGuid,
                File = file
            };
            model.HtmlInputID = "New" + tableName.UppercaseAllFirstLetters().RemoveWhiteSpace() + model.Label.UppercaseAllFirstLetters().RemoveWhiteSpace();
            model.HtmlSumbitID = "Update" + tableName.UppercaseAllFirstLetters().RemoveWhiteSpace() + model.Label.UppercaseAllFirstLetters().RemoveWhiteSpace();
            DeNSo.Session.New.Set(model);
        }

        public static void DeleteTable(string guid) {
            var cc = DeNSo.Session.New.Get<CCTableModel>(c => c != null && c.Guid == guid).FirstOrDefault();
            DeNSo.Session.New.Delete(cc);
        }

        public static void DeleteTableRow(string guid) {
            var cc = DeNSo.Session.New.Get<CCTableRowModel>(c => c != null && c.Guid == guid).FirstOrDefault();
            DeNSo.Session.New.Delete(cc);
        }

        public static void EditTableRow(string guid, string command) {
            var row = DeNSo.Session.New.Get<CCTableRowModel>(c => c != null && c.Guid == guid).FirstOrDefault();
            var i = row.HtmlInputID;
            CommandRepository.Edit(i, command);
        }

        public static void Refresh(string guid) {
            var row = DeNSo.Session.New.Get<CCTableRowModel>(c => c != null && c.Guid == guid).FirstOrDefault();
            var command = row.InputCommand;
            var result = Terminal.Execute(command); 
            row.ValueResult = result;
            row.ValueResultArray = result.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            DeNSo.Session.New.Set(row);
        }

        public static void SaveMapData(string rowGuid, string labelArray, string indexArray) {
            var r = DeNSo.Session.New.Get<CCTableRowModel>(c => c != null && c.Guid == rowGuid).FirstOrDefault();
            var labelArraySplit = labelArray.Split(new String[] { "," }, StringSplitOptions.None).ToArray();
            var indexArraySplit = indexArray.Split(new String[] { "," }, StringSplitOptions.None).ToArray();
            for (int i = 0; i < labelArraySplit.Length; i++) {
                var map = new CCTableRowMap();
                map.MapLabel = labelArraySplit[i];
                map.MapIndex = indexArraySplit[i].Split(new String[] { ";" }, StringSplitOptions.None).ToArray().ToIntArray();
                r.MapRules.Add(map);
            }
            r.HasMap = true;
            DeNSo.Session.New.Set(r);
        }

        public static List<CCTableRowMapped> MapData(string[] result, List<CCTableRowMap> mapList) {
            //var resultArray = result.Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var resultArray = result;
            var x = new List<CCTableRowMapped>() { };
            foreach (var map in mapList) {
                string z = "";
                foreach (var i in map.MapIndex) {
                    z += resultArray[i] + " ";
                }
                var y = new CCTableRowMapped();
                y.Key = map.MapLabel;
                y.Value = z;
                x.Add(y);
            }
            return x;
        }

        public static CCTableFlags.CommandFunction GetCommandFunction(string src) {
            int n = Convert.ToInt32(src);
            switch (n) {
                case 0:
                    return CCTableFlags.CommandFunction.Stable;
                case 1:
                    return CCTableFlags.CommandFunction.Testing;
                default:
                    return CCTableFlags.CommandFunction.None;
            }
        }

        public static CCTableFlags.OsiLevel GetOsiLevel(string src) {
            int n = Convert.ToInt32(src);
            switch (n) {
                case 1:
                    return CCTableFlags.OsiLevel.Physical;
                case 2:
                    return CCTableFlags.OsiLevel.DataLink;
                case 3:
                    return CCTableFlags.OsiLevel.Network;
                case 4:
                    return CCTableFlags.OsiLevel.Transport;
                case 5:
                    return CCTableFlags.OsiLevel.Session;
                case 6:
                    return CCTableFlags.OsiLevel.Presentation;
                case 7:
                    return CCTableFlags.OsiLevel.Application;
                default:
                    return CCTableFlags.OsiLevel.None;
            }
        }

        public static CCTableFlags.TableType GetTableType(string src) {
            int n = Convert.ToInt32(src);
            switch (n) {
                case 1:
                    return CCTableFlags.TableType.Settings;
                case 2:
                    return CCTableFlags.TableType.DataView;
                case 3:
                    return CCTableFlags.TableType.Conf;
                default:
                    return CCTableFlags.TableType.None;
            }
        }
    }
}
