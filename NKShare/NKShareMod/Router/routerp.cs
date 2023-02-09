﻿using System;
using System.Reflection;
using NKShare.NKShareMod.Router.routerTypeDAO;
using NKShare.NKShareMod.Router.routerTypeIMP;

namespace NKShare.NKShareMod.Router {
    class routerP {
        private const string TypeFullName = "NKShare.NKShareMod.Router.routerTypeIMP";
        private readonly Interf_routerType _router = null;
        public int RouterType = 0;

        public routerP(string gateway) {
            //string longName = "cn.softname2.routerControl.routerType,Version=1.0.0.0,Culture=neutral,PublicKeyToken=null";
            //Assembly assem = Assembly.Load(longName);
            //
            //  问题：
            //      为什么Assembly.GetAssembly获取到的程序集市softname_Graph5（总体？）而不是cn.softname2.routerControl.routerType
            //      目前的处理方法是排除干扰选项，只留下cn.softname2.routerControl.routerType程序集下的类
            //      我希望直接获取到cn.softname2.routerControl.routerType的程序集名称
            //
            Assembly assembyle = Assembly.GetAssembly(new router1().GetType());
            Type[] _TypeList = assembyle.GetTypes();
            foreach (Type type in _TypeList) {
                if (type.FullName.IndexOf(TypeFullName) != -1) {
                    Interf_routerType routerTest = (Interf_routerType)System.Activator.CreateInstance(type, gateway);
                    if (routerTest.IsRouterClassType()) {
                        _router = routerTest;
                        this.RouterType = routerTest.routerType();
                        //log.writeLog($"路由模块构造方法执行完毕，适配路由器种类为 routerType:{routerType}", log.msgType.info);
                        return;
                    }
                }
            }
            //log.writeLog($"路由模块构造方法执行完毕，没有适配到已知路由", log.msgType.info);
        }

        public int setInternetMode_AutoIP(
            String routerAcc,   //不需要账号的路由器routerAcc填"admin"或""
            String routerPwd
        ) {
            int ret = _router.setInternetMode_AutoIP(routerAcc, routerPwd);
            //log.writeLog($"路由Type:{this.routerType} 已经执行【自动获取IP函数】\nrouterAcc:{routerAcc}\nrouterPwd:{routerPwd}\n返回值：{ret}", log.msgType.info);
            return ret;
        }

        public int setInternetMode_PPPOE(
            string routerAcc,
            string routerPwd,
            string netAcc,
            string netPwd,
            int callMode,        //拨号模式，0为正常拨号，1234567...对应特殊拨号1234567...    
            int linkMode         //连接模式：1.自动连接，开机和断线后自动连接
                                 //          2.按需连接，有网络时自动连接
                                 //          3.手动连接，由用户手动连接
         ) {
            int ret = _router.setInternetMode_PPPOE(routerAcc, routerPwd, netAcc, netPwd, callMode, linkMode);
            //log.writeLog($"路由Type:{this.routerType} 已经执行【PPPOE拨号函数】\nrouterAcc:{routerAcc}\nrouterPwd:{routerPwd}\nnetAcc:{netAcc}\nnetPwd:{netPwd}\ncallMode:{callMode}\nlinkMode:{linkMode}\n返回值:{ret}", log.msgType.info);
            return ret;
        }

        public int setCloneMAC(
            String routerAcc,
            String routerPwd,
            String Mac         //传入"router"为使用路由器地址
                               //    "pc"    为使用本机MAC地址
                               //其余为指定MAC地址，格式用XX-XX-XX-XX-XX-XX
                               //最后有个校验，如果不是这种格式，此函数将返回失败代码0
        ) {
            int ret = _router.setCloneMAC(routerAcc, routerPwd, Mac);
            //log.writeLog($"路由Type:{this.routerType} 已经执行【克隆MAC函数】\nrouterAcc:{routerAcc}\nrouterPwd:{routerPwd}\nMac:{Mac}\n返回值：{ret}", log.msgType.info);
            return ret;
        }

        public int setWIFI(
            String routerAcc,
            String routerPwd,
            String wifiName,
            String wifiPwd,
            Boolean Start       //true 启动wifi;  false 关闭wifi;
        ) {
            int ret = _router.setWIFI(routerAcc, routerPwd, wifiName, wifiPwd, Start);
            //log.writeLog($"路由Type:{this.routerType} 已经执行【设置wifi函数】\nrouterAcc:{routerAcc}\nrouterPwd:{routerPwd}\nwifiName:{wifiName}\nwifiPwd:{wifiPwd}\nStart:{Start}\n返回值：{ret}", log.msgType.info);
            return ret;
        }

        public int setReboot(
            string routerAcc,
            string routerPwd
        ) {
            int ret = _router.setReboot(routerAcc, routerPwd);
            //log.writeLog($"路由Type:{this.routerType} 已经执行【重启路由函数】\nrouterAcc:{routerAcc}\nrouterPwd:{routerPwd}\n返回值:{ret}", log.msgType.info);
            return ret;
        }

        public bool IsSupportRouterType() {
            if (this.RouterType > 0)
                return true;
            return false;
        }


    }
}
