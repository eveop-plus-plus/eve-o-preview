using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveOPreview.Configuration
{
    public class SwitchingHotkey
    {
        public string Hotkey { get; set; }
        public string[] Clients { get; set; }

		public Keys GetKey()
		{
			// Protect from incorrect values
			object rawValue = (new KeysConverter()).ConvertFromInvariantString(Hotkey);
			return rawValue != null ? (Keys)rawValue : Keys.None;
		}
	}
}
