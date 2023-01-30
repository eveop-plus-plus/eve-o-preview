using System;
using System.Drawing;

namespace EveOPreview.View
{
	public interface IThumbnailViewFactory
	{
		IThumbnailView Create(IntPtr id, int epoch, string title, Size size);
	}
}