using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;

namespace MyToolkit.Paging
{
	public class MyPage : MyPlainPage
	{
		public MyFrame Frame { get; internal set; }

		private String pageKey;
		public override void OnNavigatedTo(MyNavigationEventArgs e)
		{
			if (pageKey != null) // new instance
				return;

			var frameState = MySuspensionManager.SessionStateForFrame(Frame);
			pageKey = "Page" + Frame.BackStackDepth;

			if (e.NavigationMode == NavigationMode.New)
			{
				var nextPageKey = pageKey;
				var nextPageIndex = Frame.BackStackDepth;
				while (frameState.Remove(nextPageKey))
				{
					nextPageIndex++;
					nextPageKey = "Page-" + nextPageIndex;
				}
				LoadState(e.Parameter, null);
			}
			else
				LoadState(e.Parameter, (Dictionary<String, Object>)frameState[pageKey]);
		}

		public override void OnNavigatedFrom(MyNavigationEventArgs e)
		{
			if (e.NavigationMode != NavigationMode.Back)
			{
				var frameState = MySuspensionManager.SessionStateForFrame(Frame);
				var pageState = new Dictionary<String, Object>();
				SaveState(pageState);
				frameState[pageKey] = pageState;
			}
		}

		protected virtual void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
		{

		}

		protected virtual void SaveState(Dictionary<String, Object> pageState)
		{

		}
	}
}