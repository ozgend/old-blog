﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cronom.Demo.HttpHandlerCore
{

    internal class Invoker
    {
        private static readonly object _lock = new object();
        private static Invoker _invoker;
        private MobileActions _mobileActions;

        private Invoker() { }

        public static Invoker Instance
        {
            get
            {
                if (_invoker == null)
                {
                    lock (_lock)
                    {
                        if (_invoker == null)
                        {
                            _invoker = new Invoker();
                        }
                    }
                }
                return _invoker;
            }
        }

        private MobileActions MobileActionInstance
        {
            get
            {
                if (_mobileActions == null)
                {
                    _mobileActions = new MobileActions();
                }
                return _mobileActions;
            }
        }

        private MethodInfo[] MobileActionMethods
        {
            get { return MobileActionInstance.GetType().GetMethods(); }
        }

        public object InvokeMethod(string command, string payload)
        {
            MethodInfo methodInfo = GetAction(command);

            return methodInfo.Invoke(MobileActionInstance, new object[] { payload });
        }

        private MethodInfo GetAction(string command)
        {
            foreach (
                MethodInfo method in MobileActionMethods.Where(
                method => method.Name.Equals(command, StringComparison.InvariantCultureIgnoreCase)))
            {
                return method;
            }

            throw new Exception(string.Format("Invalid action: '{0}'", command));
        }
    }
}