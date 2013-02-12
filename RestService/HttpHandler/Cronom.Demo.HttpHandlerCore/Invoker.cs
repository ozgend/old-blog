using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cronom.Demo.HttpHandlerCore
{

    internal class Invoker
    {
// ReSharper disable InconsistentNaming
        private static readonly Lazy<Invoker> _invoker = new Lazy<Invoker>(() => new Invoker(), true);
// ReSharper restore InconsistentNaming
        private MobileActions _mobileActions;

        private Invoker() { }

        public static Invoker Instance
        {
            get { return _invoker.Value; }
        }

        private MobileActions MobileActionInstance
        {
            get { return _mobileActions ?? (_mobileActions = new MobileActions()); }
        }

        private IEnumerable<MethodInfo> MobileActionMethods
        {
            get { return MobileActionInstance.GetType().GetMethods(); }
        }

        public object InvokeMethod(string command, string payload)
        {
            var methodInfo = GetAction(command);

            return methodInfo.Invoke(MobileActionInstance, new object[] { payload });
        }

        private MethodInfo GetAction(string command)
        {
            foreach (var method in MobileActionMethods.Where(method => method.Name.Equals(command, StringComparison.InvariantCultureIgnoreCase)))
            {
                return method;
            }

            throw new Exception(string.Format("Invalid action: '{0}'", command));
        }
    }
}