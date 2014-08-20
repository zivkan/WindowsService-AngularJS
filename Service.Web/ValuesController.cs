using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web.Http;

namespace Service.Web
{
    public class ValuesController : ApiController
    {
        private static IDictionary<int, string> list = new ConcurrentDictionary<int, string>();
        private static int nextId = 1;
        private static object lockObject = new Object();

        public IEnumerable<KeyValuePair<int, string>> Get()
        {
            return list;
        }

        public KeyValuePair<int, string> Get(int id)
        {
            return new KeyValuePair<int, string>(id, list[id]);
        }

        public KeyValuePair<int, string> Add(string str)
        {
            int id;
            lock (lockObject)
            {
                id = nextId++;
            }
            list.Add(id, str);

            return new KeyValuePair<int, string>(id, str);
        } 

    }
}
