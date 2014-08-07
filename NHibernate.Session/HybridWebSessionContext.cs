using System.Collections;
using NHibernate.Context;
using NHibernate.Engine;
using System;

namespace NHibernate.Session
{
    /// <summary>
    /// This custom ICurrentSessionContext class will allow a web project to have background 
    /// threads that access session through the same session factory as web request threads.  If the HttpContext
    /// is available (web request threads) the HttpContext items dictionary is used to store the session otherwise 
    /// a thread static private variable is used. 
    /// </summary>
    public class HybridWebSessionContext :  MapBasedSessionContext
    {
        private const string ItemsKey = "NHibernate.Session.HybridWebSessionContext";

        [ThreadStatic]
        private static IDictionary _threadSessionDictionary;
		
        // This constructor should be kept, otherwise NHibernate will fail to create an instance of this class.
        public HybridWebSessionContext(ISessionFactoryImplementor factory) : base(factory) { }

        protected override IDictionary GetMap()
		{
			var currentContext = ReflectiveHttpContext.HttpContextCurrentGetter();
            if (currentContext == null) return _threadSessionDictionary;
			
			var items = ReflectiveHttpContext.HttpContextItemsGetter(currentContext);
            var session = items[ItemsKey] as IDictionary;
            return session ?? _threadSessionDictionary;
		}

		protected override void SetMap(IDictionary value)
		{
			var currentContext = ReflectiveHttpContext.HttpContextCurrentGetter();
            if (currentContext != null)
            {
                var items = ReflectiveHttpContext.HttpContextItemsGetter(currentContext);
                items[ItemsKey] = value;
                return;
            }

            _threadSessionDictionary = value;
		}
    }
}