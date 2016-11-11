﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using antdlib.common.Tool;

namespace Antd.Storage {
    public class ZfsSnap {
        public class Model {
            public string Guid { get; set; }
            public string Name { get; set; }
            public string Used { get; set; }
            public string Available { get; set; }
            public string Refer { get; set; }
            public string Mountpoint { get; set; }
            public bool IsEmpty { get; set; }

            public int Index { get; set; }
            public DateTime Created { get; set; }
            public long Dimension { get; set; }
        }

        public  List<Model> List() {
            var bash = new Bash();
            var result = bash.Execute("zfs list -t snap");
            var list = new List<Model>();
            if(string.IsNullOrEmpty(result)) {
                return list;
            }
            var lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList().Skip(1);
            foreach(var line in lines) {
                var cells = Regex.Split(line, @"\s+");
                var model = new Model {
                    Guid = Guid.NewGuid().ToString(),
                    Name = cells[0],
                    Used = cells[1],
                    Available = cells[2],
                    Refer = cells[3],
                    Mountpoint = cells[4]
                };
                list.Add(model);
            }
            return list;
        }
    }
}
