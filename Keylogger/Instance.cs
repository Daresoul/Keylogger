using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keylogger
{
    class Instance
    {
        public string Name;
        public string Text;

        public Instance(string _name)
        {
            Name = _name;
        }

        public void AddText(string text)
        {
            Text += text + "\n";
        }

        public string GetText()
        {
            return Text;
        }
    }
}
