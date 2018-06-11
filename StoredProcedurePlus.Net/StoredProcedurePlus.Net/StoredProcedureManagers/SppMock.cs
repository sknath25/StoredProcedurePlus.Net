using StoredProcedurePlus.Net.EntityManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredProcedurePlus.Net.StoredProcedureManagers
{
    public class MockEventArgs : EventArgs
    {
        public MockEventArgs() : base()
        {
            Input = null;
        }

        public MockEventArgs(IDataEntityAdapter input) : base()
        {
            Input = input;
        }

        public IDataEntityAdapter Input { get; private set; }
        public int Result { get; set; }
    }

    public delegate void EventHandler<MockEventArgs>(object sender, MockEventArgs e);
}
