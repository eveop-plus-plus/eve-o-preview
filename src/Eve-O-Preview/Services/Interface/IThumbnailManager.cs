using EveOPreview.View;
using System;
using System.Collections.Generic;

namespace EveOPreview.Services
{
	public interface IThumbnailManager
	{
		void Start();
		void Stop();

		void UpdateThumbnailsSize();
		void UpdateThumbnailFrames();

		Dictionary<IntPtr, IThumbnailView> GetViews();

		bool IsActiveClient(IThumbnailView view);
	}
}