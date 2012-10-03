using System;

namespace NMock3.Tutorial
{
	public class ContactManagementPresenter : PresenterBase
	{
		private IContactManagementView _view { get { return (IContactManagementView) base.View; } }

		public IContactManagementDataSource _dataSource;
		public IStandardizationService _standardizationService;

		public ContactManagementPresenter(IContactManagementView view) : base(view)
		{
		}

		protected override void BindEvents()
		{
			_view.Load += _view_Load;
		}

		private void _view_Load(object sender, ContactEventArgs e)
		{
			ContactManagementBusinessLogic logic = new ContactManagementBusinessLogic(_dataSource);

			Contact contact = logic.GetContact(e.ContactId);

			_view.Name = contact.Name;
			_view.ContactType = contact.ContactType;

			_view.Addresses = contact.Addresses;
			_view.InternetHandles = contact.InternetHandles;
			_view.PhoneNumbers = contact.PhoneNumbers;

			Status = "Load";
		}
	}
}
