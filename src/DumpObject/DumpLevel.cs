using System;
using System.Collections;
using System.Collections.Generic;

namespace DumpObject
{
    public class DumpLevel : IEnumerable<DumpLevel>
    {
        private readonly List<DumpLevel> _children;

        public DumpLevel()
        {
            _children = new List<DumpLevel>();
        }

        public int Level { get; set; }
        public string Header { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
        
        public void AddChildren(DumpLevel dump)
        {
            if (dump == null)
                throw new ArgumentNullException("dump");

            _children.Add(dump);
        }

        public IEnumerator<DumpLevel> GetEnumerator()
        {
            return _children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class StopDumpLevel : DumpLevel { }
}
