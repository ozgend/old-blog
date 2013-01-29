using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronom.Demo.WebApiService.Helper
{
    public class IOCContainer
    {
        public enum InitializationTypes { Transient, Singleton }
        struct CreationContext
        {
            public Creator CreateOperation { get; set; }
            public InitializationTypes Initialization { get; set; }
        }

        private static readonly Lazy<IOCContainer> s_Instance = new Lazy<IOCContainer>(() => new IOCContainer(), true);
        public static IOCContainer Instance
        {
            get { return s_Instance.Value; }
        }

        public delegate object Creator(IOCContainer container);

        private readonly Dictionary<string, object> m_configuration = new Dictionary<string, object>();
        private readonly Dictionary<Type, CreationContext> m_typeToCreator = new Dictionary<Type, CreationContext>();
        private readonly Dictionary<Type, object> m_Instances = new Dictionary<Type, object>();

        public Dictionary<string, object> Configuration
        {
            get { return m_configuration; }
        }

        public void Register<T>(Creator creator, InitializationTypes initializationType = InitializationTypes.Singleton)
        {
            m_typeToCreator.Add(typeof(T), new CreationContext
            {
                CreateOperation = creator,
                Initialization = initializationType
            });
        }

        public T Create<T>()
        {
            CreationContext context = m_typeToCreator[typeof(T)];

            switch (context.Initialization)
            {
                case InitializationTypes.Transient:
                    return (T)context.CreateOperation(this);
                case InitializationTypes.Singleton:
                    bool hasType = m_Instances.ContainsKey(typeof(T));
                    T type = default(T);
                    if (!hasType)
                    {
                        type = (T)context.CreateOperation(this);
                        m_Instances.Add(typeof(T), type);
                    }
                    else
                    {
                        type = (T)m_Instances[typeof(T)];
                    }
                    return type;
                default:
                    throw new ArgumentException();
            }


        }

        public T GetConfiguration<T>(string name)
        {
            return (T)m_configuration[name];
        }
    }
}