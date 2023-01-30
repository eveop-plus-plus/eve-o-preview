using EveOPreview.Configuration;
using EveOPreview.UI.Hotkeys;
using EveOPreview.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveOPreview.Services
{
    class SwitchGroupManager
    {
        private SwitchingHotkey configuration;
        private HotkeyHandler hotkeyHandler;
        private IThumbnailManager thumbnailManager;
        private Dictionary<string, int> clientOrder;

        public SwitchGroupManager(SwitchingHotkey configuration, IThumbnailManager thumbnailManager)
        {
            this.configuration = configuration;
            this.thumbnailManager = thumbnailManager;
            if (configuration.Clients != null)
            {
                clientOrder = new Dictionary<string, int>();
                int index = 0;
                foreach (var clientName in configuration.Clients)
                {
                    clientOrder[clientName] = index;
                    index += 1;
                }
            }
        }

        internal void Register()
        {
            if (hotkeyHandler != null)
            {
                hotkeyHandler.Unregister();
            }

            var key = configuration.GetKey();
            if (key == Keys.None)
            {
                throw new Exception(String.Format("Failed to parse SwitchingHotkey key '{0}'", configuration.Hotkey));
            }

            hotkeyHandler = new HotkeyHandler(IntPtr.Zero, key);
            hotkeyHandler.Pressed += SwitchHotkeyPressed_Handler;
            hotkeyHandler.Register();
        }
        internal void Unregister()
        {
            hotkeyHandler.Unregister();
            hotkeyHandler = null;
        }

        private void SwitchHotkeyPressed_Handler(object sender, HandledEventArgs e)
        {
            e.Handled = true;

            IThumbnailView[] views;
            if (clientOrder == null)
            {
                views = thumbnailManager.GetViews().Values
                    .OrderBy(view => view.Epoch).ToArray();
            }
            else
            {
                views = thumbnailManager.GetViews().Values
                    .Where(view => clientOrder.ContainsKey(view.Title))
                    .OrderBy(view => clientOrder[view.Title])
                    .ToArray();
            }
            
            if (views.Length == 0)
            {
                return;
            }

            int currentIndex = Array.FindIndex(views, view => thumbnailManager.IsActiveClient(view)) + 1;
            var activateView = views[currentIndex % views.Length];
            activateView.ThumbnailActivated?.Invoke(activateView.Id);
        }

    }
}
