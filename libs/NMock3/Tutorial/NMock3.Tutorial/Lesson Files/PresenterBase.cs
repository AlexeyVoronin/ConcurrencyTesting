using System;

namespace NMock3.Tutorial
{
	public class PresenterBase
	{
		//This property is only used for testing event execution of events that don't do anything.
		public string Status { get; protected set; }

		public PresenterBase(IViewBase view)
		{
			View = view;

			Status = "Constructed";
		}

		protected IViewBase View{ get; private set;}

		/// <summary>
		/// Normally a call to this would exist inside of the constructor and this would be private
		/// </summary>
		public void BindEventsInternal()
		{
			View.Init += view_Init;
			BindEvents();
		}

		protected virtual void BindEvents()
		{}

		private void view_Init(object sender, EventArgs e)
		{
			Status = "Init";
		}
	}
}
