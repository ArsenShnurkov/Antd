﻿using System.Collections.Generic;
using antdlib.common.Tool;

namespace antd.commands {
    public class CommandsFunction {

        public static string RootFrameworkAntdShellScripts => "/framework/antd/ShellScript";
        private static readonly Bash BashTool = new Bash();
        private static readonly Read ReadTool = new Read();
        private static readonly Write WriteTool = new Write();

        public static Dictionary<string, Command<dynamic, dynamic>> List => GetDict();

        private static Dictionary<string, Command<dynamic, dynamic>> GetDict() {
            var dict = new Dictionary<string, Command<dynamic, dynamic>>();

            #region [    Command - Misc    ]
            dict["anthilla"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "$custom",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["modprobe"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "modprobe $package",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["mono-antdsh-update"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "mono /framework/antdsh/antdsh.exe update $context",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["mono-antdsh-update-check"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "mono /framework/antdsh/antdsh.exe update check",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["rmmod"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "rmmod $modules",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["set-vlan"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "ip link set $vlan_interface_name down",
                    "ip link del $vlan_interface_name",
                    "ip link add name $vlan_interface_name link $interface_name type vlan id $vlan_id",
                    "ip link set $vlan_interface_name txqueuelen 10000",
                    "ip link set $vlan_interface_name up"
                },
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["sysctl-p"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "sysctl -p",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["systemd-machine-id-setup"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "systemd-machine-id-setup",
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Bond    ]
            dict["bond-add-if"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ifenslave $bond $net_if",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["bond-del-if"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ifenslave -d $bond $net_if",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["bond-set"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "ip link set $bond down",
                    "ip link del $bond",
                    "ip link add name $bond type bond",
                    "ip link set $bondme txqueuelen 10000",
                    "ip link set $bond down",
                    "echo 4 > /sys/class/net/$bond/bonding/mode",
                    "echo 1 > /sys/class/net/$bond/bonding/lacp_rate",
                    "echo 1 > /sys/class/net/$bond/lacp_rate",
                    "echo 100 > /sys/class/net/$bond/bonding/miimon",
                    "ip link set $bond up"
                },
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["set-bond"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "ip link set $bond_name down",
                    "ip link del $bond_name",
                    "ip link add name $bond_name type bond",
                    "ip link set $bond_name txqueuelen 10000",
                    "ip link set $bond_name down",
                    "echo 4 > /sys/class/net/$bond_name/bonding/mode",
                    "echo 1 > /sys/class/net/$bond_name/bonding/lacp_rate",
                    "echo 1 > /sys/class/net/$bond_name/lacp_rate",
                    "echo 100 > /sys/class/net/$bond_name/bonding/miimon",
                    "ip link set $bond_name up"
                },
                Function = (x, y) => BashTool.Execute(x, false)
            };
            #endregion

            #region [    Command - Brctl    ]
            dict["brctl-add"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "brctl addbr $bridge",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["brctl-add-if"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "brctl addif $bridge $net_if",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["brctl-del"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "brctl delbr $bridge",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["brctl-del-if"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "brctl delif $bridge $net_if",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["brctl-set-pathcost"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "brctl setpathcost $bridge $path $cost set path cost",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["brctl-set-portprio"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "brctl setportprio $bridge $port $priority set port priority",
                Function = (x, y) => BashTool.Execute(x, false)
            };
            dict["brctl-show-br"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl show $bridge",
                Grep = "$bridge",
                Function = (x, y) => BashTool.Execute(x).Grep(y)
            };
            dict["brctl-show-brid"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl show $bridge",
                Grep = "$bridge",
                Function = (x, y) => BashTool.Execute(x).Grep(y).Print(2)
            };
            dict["brctl-show-brif"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl show $bridge",
                Grep = "$bridge",
                Function = (x, y) => BashTool.Execute(x).Grep(y).Print(4)
            };
            dict["brctl-show-brstpstatus"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl show $bridge",
                Grep = "$bridge",
                Function = (x, y) => BashTool.Execute(x).Grep(y).Print(3)
            };
            dict["brctl-show-macs"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl showmacs $bridge",
                Function = (x, y) => BashTool.Execute(x).Grep(y)
            };
            dict["brctl-show-stp"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl showstp $bridge",
                Function = (x, y) => BashTool.Execute(x).Grep(y)
            };
            dict["brctl-stp-off"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl stp $bridge off",
                Function = (x, y) => BashTool.Execute(x).Grep(y)
            };
            dict["brctl-stp-on"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "brctl stp $bridge on",
                Function = (x, y) => BashTool.Execute(x).Grep(y)
            };
            #endregion

            #region [    Command - Cat    ]
            dict["cat"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "$file",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["cat-etc-gentoorel"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/etc/gentoo-release",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["cat-etc-hostname"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/etc/hostname",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["cat-etc-hosts"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/etc/hosts",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["cat-etc-lsbrel"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/etc/lsb-release",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["cat-etc-osrel"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/etc/os-release",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["cat-etc-resolv"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/etc/resolv.conf",
                Function = (x, y) => ReadTool.File(x)
            };
            #endregion

            #region [    Command - Dhclient    ]
            dict["dhclient-killall"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "killall dhclient",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["dhclient4"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "dhclient $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["dhclient6"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "dhclient -6 $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Echo (write/append)    ]
            dict["echo-write"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "$file",
                Grep = "$value",
                Function = (x, y) => WriteTool.WriteFile(x, y)
            };
            dict["echo-append"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "$file",
                Grep = "$value",
                Function = (x, y) => WriteTool.AppendFile(x, y)
            };
            #endregion

            #region [    Command - Fdisk    ]
            dict["fdisk-print"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "echo -e \"p\" | fdisk $disk_device",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["fdisk-set-partition"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "echo -e \"n\\n $part_number\\n $part_first_sector\\n $part_size\\n w\\n\" | fdisk $disk_device",
                    "parted $disk_device $part_number 1 $name",
                    "parted $disk_device align-check opt $part_number"
                },
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Hostnamectl    ]
            dict["hostnamectl"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "hostnamectl",
                Function = (x, y) => BashTool.Execute(x).SplitBash()
            };
            dict["hostnamectl-get-arch"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Architecture: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-bootid"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Boot ID: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-chassis"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Chassis: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-deployment"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Deployment: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-hostname"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Static hostname: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-iconname"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Icon name: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-kernel"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Kernel: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-location"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Location: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-machineid"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Machine ID: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-os"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Operating System: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["hostnamectl-get-virtualization"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl",
                Grep = "Virtualization: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["set-chassis"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl set-chassis $host_chassis",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["set-deployment"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl set-deployment $host_deployment",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["set-hostname"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl set-hostname $host_name",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["set-location"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "hostnamectl set-location \"$host_location\"",
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Ipv4    ]
            dict["set-network-interface"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "ip link set dev $interface_name down",
                    "ip link set dev $interface_name up",
                    "ip link set $interface_name txqueuelen 10000",
                    "ip link set dev $interface_name up"
                },
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-add-addr"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr add $address/$range dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-add-addr-broadcast"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr add $address/$range broadcast $broadcast dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-add-multipath-route"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip route add default scope global nexthop dev $net1 nexthop dev $net2",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-add-nat"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip route add nat $ip_address via $ip_via_address",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-add-route"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip route add $ip_address via $gateway dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-add-tunnel-point-to-point"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip tunnel add $net_if mode sit ttl $ttl remote $tunnel local $local_address",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-del-addr"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr del $address/$range dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-del-addr-broadcast"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr del $address/$range broadcast $broadcast dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-del-route"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip route del $ip_address via $gateway dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-del-tunnel-point-to-point"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip tunnel del $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-disable-if"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip link set $net_if down",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-enable-if"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip link set $net_if up",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-flush-configuration"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr flush dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-get-if-addr"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr show $net_if",
                Function = (x, y) => BashTool.Execute(x).Grep("inet .").First().Print(2)
            };
            dict["ip4-get-if-brd"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr show $net_if",
                Function = (x, y) => BashTool.Execute(x).Grep("inet .").First().Print(4)
            };
            dict["ip4-get-if-macaddress"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/sys/class/net/$net_if/address",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["ip4-get-if-mtu"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "/sys/class/net/$net_if/mtu",
                Function = (x, y) => ReadTool.File(x)
            };
            dict["ip4-if-isdown"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr show $net_if",
                Function = (x, y) => BashTool.Execute(x).Grep("state DOWN").First()
            };
            dict["ip4-if-isup"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr show $net_if",
                Function = (x, y) => BashTool.Execute(x).Grep("state UP").First()
            };
            dict["ip4-set-macaddress"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "ip link set $net_if down",
                    "ip link set dev $net_if address $address",
                    "ip link set $net_if up"
                },
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-set-mtu"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip link set dev $net_if mtu $mtu",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-show-if-addr"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip addr show $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-show-if-link"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -s link ls $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-show-routes"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip route show $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-show-tunnels"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip tunnel show $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip4-show-updown"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip link show $net_if",
                //todo fare multigrep  | grep -ho \' UP \\| DOWN \'
                Function = (x, y) => BashTool.Execute(x).Grep("UP").First()
            };
            #endregion

            #region [    Command - Ipv6    ]
            dict["ip6-add-addr"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 addr add $address/$range dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-add-neigh"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 neigh add $ip_address lladdr $ip_lay_address dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-add-new-address"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 addr add $ip_address dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-add-route"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 route add $ip_address via $gateway",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-add-route-dev"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 addr del $address/$range dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-del-addr"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 addr del $address/$range dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-del-address"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 addr del $ip_address dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-del-neigh"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 neigh del $ip_address lladdr $ip_lay_address dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-del-route"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 route del $ip_address via $gateway",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-del-route-dev"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 route del $gateway dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-flush-configuration"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 addr flush dynamic",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-show-if-link"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 link show $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-show-if-stats"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 -s link ls $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-show-neigh"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 neigh show dev $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-show-routes"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 route show $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["ip6-show-tunnels"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ip -6 tunnel show $net_if",
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Time & Date    ]
            dict["ntpdate"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "ntpdate $server",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["set-ntpdate"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "ntpdate $date_server",
                    "timedatectl --no-pager --no-ask-password --adjust-system-clock set-ntp yes"
                },
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["set-timezone"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl --no-pager --no-ask-password --adjust-system-clock set-timezone $host_timezone",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["sync-clock"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "hwclock --systohc",
                    "hwclock --hctosys"
                },
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["timedatectl"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(IEnumerable<string>),
                Arguments = "timedatectl",
                Function = (x, y) => BashTool.Execute(x).SplitBash()
            };
            dict["timedatectl-get-localtime"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl",
                Grep = "Local time: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["timedatectl-get-nettimeon"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl",
                Grep = "Network time on: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["timedatectl-get-ntpsync"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl",
                Grep = "NTP synchronized: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["timedatectl-get-rtcintz"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl",
                Grep = "RTC in local TZ: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["timedatectl-get-rtctime"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl",
                Grep = "RTC time: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["timedatectl-get-timezone"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl",
                Grep = "Time zone: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            dict["timedatectl-get-univtime"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "timedatectl",
                Grep = "Universal time: ",
                Function = (x, y) => BashTool.Execute(x).Grep(y).First().Print(2, ':')
            };
            #endregion

            #region [    Command - Rsync    ]
            dict["rsync"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "rsync -aHA $source/ $destination/",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["rsync-delete-after"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "rsync -aHA --delete-after $source/ $destination/",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["rsync-delete-during"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "rsync -aHA --delete-during $source/ $destination/",
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Systemctl    ]
            dict["systemctl-start"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "systemctl start $service",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["systemctl-stop"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "systemctl stop $service",
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Virsh    ]
            dict["virsh-destroy"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh destroy $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-reboot"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh reboot $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-reset"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh reset $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-restore"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh restore $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-resume"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh resume $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-shutdown"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh shutdown $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-start"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh start $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-suspend"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh suspend $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-dompmsuspend"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh dompmsuspend $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["virsh-dompmwakeup"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "virsh dompmwakeup $domain",
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            #region [    Command - Storage    ]
            dict["zpool-mklabel"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "parted /dev/$disk_device mklabel $zpool_label Yes",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["zpool-create"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "zpool create -f -o altroot=$pool_altroot -o ashift=12 -O casesensitivity=insensitive -O normalization=formD $pool_name $pool_type $disk_byid",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["zpool-create-simple"] = new Command<dynamic, dynamic> {
                InputType = typeof(string),
                OutputType = typeof(string),
                Arguments = "zpool create -f -o altroot=$pool_altroot $pool_name Storage01 $disk",
                Function = (x, y) => BashTool.Execute(x)
            };
            dict["zfs-create"] = new Command<dynamic, dynamic> {
                InputType = typeof(IEnumerable<string>),
                OutputType = typeof(string),
                Arguments = new[] {
                    "zfs create -o compression=lz4 -o atime=off $pool_name/$dataset_name",
                    "zfs set xattr=on $pool_name/$dataset_name",
                    "zfs set acltype=posixacl $pool_name/$dataset_name",
                    "zfs set aclinherit=passthrough-x $pool_name/$dataset_name",
                    "zfs set mountpoint=/$pool_altroot/$pool_name/$dataset_name $pool_name/$dataset_name",
                    "zpool add $pool_name log $disk_log",
                    "zpool add $pool_name cache $disk_cache",
                    "zpool set cachefile=/etc/zfs/zpool.cache $pool_name",
                    "zpool set cachefile=/etc/zfs/zpool.cache $pool_name"
                },
                Function = (x, y) => BashTool.Execute(x)
            };
            #endregion

            //dict["bond-add-if"] = new Command<dynamic, dynamic> {
            //    InputType = typeof(IEnumerable<string>),
            //    OutputType = typeof(string),
            //    Arguments = "ifenslave $bond $net_if",
            //    Function = (x, y) => BashTool.Execute(x)
            //};

            return dict;
        }
    }
}